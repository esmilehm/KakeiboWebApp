using System;
using FrameworkClassicTea;
using KakeiboWebApp.Data;
using KakeiboWebApp.Service;
using System.Data;

namespace KakeiboWebApp.Model
{
    /// <summary>
    /// 【モデル系】詳細画面用
    /// </summary>
    /// <remarks>
    /// todo 14/2/16 課題；使用するサービス系が出来ていないと完成しないクラス。切離せるようにインターフェースに変える？
    /// 
    /// </remarks>
    public class ModelDetailForm : AbstractModel
    {
        
        #region １．プライベート変数宣言
        //--- 解説
        // 対となるViewで使用する画面オブジェクト用の変数を宣言
        //
        //-------------------------------------------------------------------------------- 

        /// <summary>
        /// id
        /// </summary>
        private int _id = 0;
        /// <summary>
        /// date
        /// </summary>
        private string _date = string.Empty;
        /// <summary>
        /// goods
        /// </summary>
        private string _goods = string.Empty;
        /// <summary>
        /// price
        /// </summary>
        private int _price = 0;
        /// <summary>
        /// itemid
        /// </summary>
        private int _itemid = 0;
        /// <summary>
        /// syushi
        /// </summary>
        private Boolean _syushi = false;
        /// <summary>
        /// itemdetaildid
        /// </summary>
        private int _itemdetailsid = 0;
        /// <summary>
        /// 家計簿AccessのReceiptテーブル
        /// </summary>
        /// <remarks>
        //// note 14/1/19 IDをKeyに取得したDataSetを各項目にセットして値を渡す。
        /// </remarks>
        private DataSetKakeibo _dsKakeibo = null;
        /// <summary>
        /// tblReceipt
        /// </summary>
        private  DataSetKakeibo.tblReceiptDataTable _tblReceipt = null;
        /// <summary>
        /// tblItemMaster
        /// </summary>
        private DataSetKakeibo.tblItemMasterDataTable _tblItemMaster = null;
        /// <summary>
        /// tblReceipt_Update
        /// </summary>
        private DataSetKakeibo.tblReceipt_UpdateDataTable _tblReceipt_Update = null;
        /// <summary>
        /// プロパティからの値によってtblReceptの値が変わった事をあらわす
        /// </summary>
        /// <remarks>
        /// True=値が変わった/False=値が変わっていない
        /// </remarks>
        private Boolean _isChange = false;

        #endregion

        #region ２．コンストラクタ定義

        #region 引数なし
        /// <summary>
        /// コンストラクタ・引数なし
        /// </summary>
        /// <remarks>
        /// note 14/02/01 GetDsReceiptでnewした時に、引数有りだと無限ループになるため
        /// note 14/02/10 引数無しは、新規レコード作成用のインスタンスを作成する仕組み用に使用する
        /// note 14/05/07 ここでServiceを呼ぶ処理を記述していると、Serviceで使用する際に無限ループするため、何も記述しない。
        /// </remarks>
        public ModelDetailForm()
        {
            this._dsKakeibo = new DataSetKakeibo();
            this._tblReceipt = this._dsKakeibo.tblReceipt;
            // TODO 更新用のテーブルをどうやってやるか検討中
            this._tblReceipt_Update = this._dsKakeibo.tblReceipt_Update;

        }
        #endregion

        #region 引数・id
        /// <summary>
        /// コンストラクタ・選択した行のidを引数に取得
        /// </summary>
        /// <param name="id">選択した行のidの値</param>
        public ModelDetailForm(int id)
        {
            ReplacedByValueFromTable(id);
        }
        #endregion

        #endregion

        #region ３．プロパティ定義

        #region データセット・テーブル

        #region dsKakeibo
        /// <summary>
        /// データセット・dsKakeibo
        /// </summary>
        /// <remarks>
        /// </remarks>
        public DataSetKakeibo dsKakeibo
        {
            get
            {
                return this._dsKakeibo;
            }
            set
            {
                this._dsKakeibo = value;
            }
        }
        #endregion

        #region tblReceipt
        /// <summary>
        /// レシートのテーブル
        /// </summary>
        /// <remarks>
        /// </remarks>
        public DataSetKakeibo.tblReceiptDataTable tblReceipt
        {
            get
            {
                return this._tblReceipt;
            }
            set
            {
                this._tblReceipt = value;
            }
        }
        #endregion

        #region tblItemMaster
        /// <summary>
        /// 商品マスタのテーブル
        /// </summary>
        /// <remarks>
        /// note 14/2/15 備考；このModelクラスをNewして値を受け渡すとき、どうやら初回取得時のみの条件が発生するみたい
        /// </remarks>
        public DataSetKakeibo.tblItemMasterDataTable tblItemMaster
        {
            get
            {
                //--- 初回取得時のみ ---
                if (this._tblItemMaster == null)
                {
                    // tblItemMasterの値を取得するControlを実行
                    ServiceGetTblItemMaster gtItemMaster = new ServiceGetTblItemMaster();
                    gtItemMaster.doStart();

                    this._tblItemMaster = ((ModelDetailForm)gtItemMaster._model)._tblItemMaster;
                }

                return this._tblItemMaster;
            }
            set
            {
                this._tblItemMaster = value;
            }
        }
        #endregion

        #region tblReceipt_Update
        /// <summary>
        /// レシートのテーブル
        /// </summary>
        /// <remarks>
        /// </remarks>
        public DataSetKakeibo.tblReceipt_UpdateDataTable tblReceipt_Update
        {
            get
            {
                return this._tblReceipt_Update;
            }
            set
            {
                this._tblReceipt_Update = value;
            }
        }
        #endregion
      
        #endregion


        #region 画面オブジェクト

        #region id
        /// <summary>
        /// 選択された行のID
        /// </summary>
        /// <remarks>
        /// </remarks>
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        #endregion

        #region date
        /// <summary>
        /// 購入日
        /// </summary>
        /// <remarks>
        /// </remarks>
        public string date
        {
            get
            {
                return this._date;
            }
            set
            {
                this._date = value;

                // テーブルにも反映
                //this.tblReceipt[0].DATE =  DateTime.Parse(this._date);
                // フラグも変更
                this._isChange = true;
            }
        }
        #endregion

        #region goods
        /// <summary>
        /// 商品名
        /// </summary>
        /// <remarks>
        /// </remarks>
        public string goods
        {
            get
            {
                return this._goods;
            }
            set
            {
                this._goods = value;

                // テーブルにも反映
                //this._tblReceipt[0].GOODS = this._goods;
                // フラグも変更
                this._isChange = true;
            }
        }
        #endregion

        #region price
        /// <summary>
        /// 値段
        /// </summary>
        /// <remarks>
        /// </remarks>
        public int price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
                // テーブルにも反映
                //this._tblReceipt[0].PRICE = this._price;
                // フラグも変更
                this._isChange = true;
            }
        }
        #endregion

        #region itemid
        /// <summary>
        /// 品目ID
        /// </summary>
        /// <remarks>
        /// </remarks>
        public int itemid
        {
            get
            {
                return this._itemid;
            }
            set
            {
                this._itemid = value;
                // テーブルにも反映
                //this.tblReceipt[0].ITEM_ID = byte.Parse(this._itemid.ToString());
                // フラグも変更
                this._isChange = true;
            }
        }
        #endregion

        #region syushi
        /// <summary>
        /// 収支
        /// </summary>
        /// <remarks>
        /// </remarks>
        public Boolean syushi
        {
            get
            {
                // 初回取得時のみ

                //GraffitiControlDetail gt = new GraffitiControlDetail();
                //gt.doStart();
                //this._syushi = ((GraffitiFormDBDetilModel)gt._model)._syushi;

                // todo 処理を追記する
                this._syushi = false;

                return this._syushi;

            }
            set
            {
                this._syushi = value;
            }
        }
        #endregion

        #region itemdetailsid
        /// <summary>
        /// 品目詳細ID
        /// </summary>
        /// <remarks>
        /// </remarks>
        public int itemdetailsid
        {
            get
            {
                return this._itemdetailsid;
            }
            set
            {
                this._itemdetailsid = value;
                // テーブルにも反映
                //this.tblReceipt[0].ITEM_DETAILS_ID = byte.Parse(this._itemdetailsid.ToString());
                // フラグも変更
                this._isChange = true;
            }
        }
        #endregion
        
        #endregion


        #region 登録・更新・削除処理

        #region tblReceiptAddSubmit
        /// <summary>
        /// 新規レコード追加用
        /// </summary>
        public Boolean tblReceiptAddSubmit
        {
            get
            {
                // 登録
                AddRecordToTable();
                return true;
            }
        }
        #endregion

        #region tblReceptUpdateSubmit
        /// <summary>
        /// tblReceptの更新処理を行うプロパティ
        /// </summary>
        public Boolean tblReceptUpdateSubmit
        {
            get
            {
                if (this._isChange == true)
                {
                    // 更新
                    ReplacedByValueFromProperty();
                    // テーブル更新したので、画面と値が同じ状態を表す
                    this._isChange = false;
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        #endregion

        #region tblReceptDeleteSubmit
        /// <summary>
        /// tblReceptの更新処理を行うプロパティ
        /// </summary>
        public Boolean tblReceptDeleteSubmit
        {
            get
            {
                // 削除
                DeleteRecordForTable();
                return true;
            }
        }
        #endregion


        #endregion

        #endregion

        #region ４．プライベートメソッド

        #region ReplacedByValueFromTable
        /// <summary>
        /// tblReceiptから取得した値をプロパティに反映
        /// </summary>
        /// <remarks>
        /// テーブルtblReceiptを取得して、データセットDsReceiptを作成する
        /// </remarks>
        private void ReplacedByValueFromTable_PlanA(int id)
        {

            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptDetail svGet = new ServiceGetTblReceiptDetail(id);
            svGet.doStart();

            //--- 値をプロパティ用の項目にセット ---
            // todo 14/2/15 課題；プロパティが増えると、受渡しの項目を下記に増やす手間が生まれる。まとめて渡せないか？
            // note 14/2/15 考察；この処理を各プロパティに入れると、値無しでエラーになるので、ここにまとめておく
            this._dsKakeibo = ((ModelDetailForm)svGet._model).dsKakeibo;
            this._tblReceipt = ((ModelDetailForm)svGet._model).dsKakeibo.tblReceipt;
            // TODO 更新方法を検討中
            this._tblReceipt_Update = ((ModelDetailForm)svGet._model).dsKakeibo.tblReceipt_Update;

            this._date = ((ModelDetailForm)svGet._model).date;
            this._goods = ((ModelDetailForm)svGet._model).goods;
            this._price = ((ModelDetailForm)svGet._model).price;
            this._itemid = ((ModelDetailForm)svGet._model).itemid;
            this._itemdetailsid = ((ModelDetailForm)svGet._model).itemdetailsid;

        }

        private void ReplacedByValueFromTable(int id)
        {
            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptDetail svGet = new ServiceGetTblReceiptDetail(id);
            svGet.doStart();

            //--- 値をプロパティ用の項目にセット ---
            // todo 14/2/15 課題；プロパティが増えると、受渡しの項目を下記に増やす手間が生まれる。まとめて渡せないか？
            // note 14/2/15 考察；この処理を各プロパティに入れると、値無しでエラーになるので、ここにまとめておく
            this._dsKakeibo = ((ModelDetailForm)svGet._model).dsKakeibo;
            this._tblReceipt = ((ModelDetailForm)svGet._model).dsKakeibo.tblReceipt;
            // TODO 更新方法を検討中
            this._tblReceipt_Update = ((ModelDetailForm)svGet._model).dsKakeibo.tblReceipt_Update;
            
            this._date = ((ModelDetailForm)svGet._model).date;
            this._goods = ((ModelDetailForm)svGet._model).goods;
            this._price = ((ModelDetailForm)svGet._model).price;
            this._itemid = ((ModelDetailForm)svGet._model).itemid;
            this._itemdetailsid = ((ModelDetailForm)svGet._model).itemdetailsid;

        }
        #endregion

        #region ReplacedByValueFromProperty
        /// <summary>
        /// Propertyから取得した値をtblReceiptに反映
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void ReplacedByValueFromProperty()
        {
            //--- 更新用のコントロールを生成 ---
            ServiceUpdateTblReceiptDetail gtUpdate = new ServiceUpdateTblReceiptDetail();

            //--- 画面からのtblReceiptを反映 ---
            // todo 16/04/24 ViewかModelで検索した際に、インスタンス生成していれば不要と思う
            this._tblReceipt_Update = new DataSetKakeibo.tblReceipt_UpdateDataTable();
            DataSetKakeibo.tblReceipt_UpdateRow dr = this._tblReceipt_Update.NewtblReceipt_UpdateRow();
            dr.DATE = DateTime.Parse(this._date);
            dr.GOODS = this._goods;
            dr.PRICE = this._price;
            dr.ITEM_ID = byte.Parse(this._itemid.ToString());
            dr.ITEM_DETAILS_ID = 0;
            this._tblReceipt_Update.AddtblReceipt_UpdateRow(dr);

            //---
            //this._tblReceipt_Update[0].DATE = DateTime.Parse(this._date);
            //this._tblReceipt_Update[0].GOODS = this._goods;
            //this._tblReceipt_Update[0].PRICE = this._price;
            //this._tblReceipt_Update[0].ITEM_ID = byte.Parse(this._itemid.ToString());
            ////todo 14/5/6 品目詳細IDと品目詳細名は、まだ未使用のため仮値を設定
            ////this._tblReceipt_Update[0].ITEM_DETAILS_ID = byte.Parse(this._itemdetailsid.ToString());
            //this._tblReceipt_Update[0].ITEM_DETAILS_ID = 0;
            //-----------

            // 更新用コントロールのmodelプロパテｨに画面用コントロールのmodelプロパテｨを反映させる
            ((ModelDetailForm)gtUpdate._model)._tblReceipt_Update = this._tblReceipt_Update;

            //--- 更新処理を実行 ---
            gtUpdate.doStart();

        }

        private void ReplacedByValueFromProperty_old()
        {
            //--- 更新用のコントロールを生成 ---
            ServiceUpdateTblReceiptDetail gtUpdate = new ServiceUpdateTblReceiptDetail();

            //--- 画面からのtblReceiptを反映 ---
            this._tblReceipt[0].DATE = DateTime.Parse(this._date);
            this._tblReceipt[0].GOODS = this._goods;
            this._tblReceipt[0].PRICE = this._price;
            this._tblReceipt[0].ITEM_ID = byte.Parse(this._itemid.ToString());
            //todo 14/5/6 品目詳細IDと品目詳細名は、まだ未使用のため仮値を設定
            //this._tblReceipt[0].ITEM_DETAILS_ID = byte.Parse(this._itemdetailsid.ToString());
            this._tblReceipt[0].ITEM_DETAILS_ID = 0;

            // 更新用コントロールのmodelプロパテｨに画面用コントロールのmodelプロパテｨを反映させる
            ((ModelDetailForm)gtUpdate._model)._tblReceipt = this._tblReceipt;

            //--- 更新処理を実行 ---
            gtUpdate.doStart();

        }
        #endregion

        #region AddRecordToTable
        /// <summary>
        /// 新規レコードをtblReceiptに反映
        /// </summary>
        private void AddRecordToTable()
        {

            //--- 登録用のコントロールを生成 ---
            ServiceUpdateTblReceiptDetail gtAddRow = new ServiceUpdateTblReceiptDetail();
            gtAddRow._model = new ModelDetailForm();

            //--- 新規レコード作成 ---
            DataSetKakeibo.tblReceipt_UpdateRow addRow = this._tblReceipt_Update.NewtblReceipt_UpdateRow();


            //--- 各プロパティの値を渡してレコード追加 ---
            addRow.DATE = DateTime.Parse(this._date);
            addRow.GOODS = this._goods;
            addRow.PRICE = this._price;
            addRow.ITEM_ID = byte.Parse(this._itemid.ToString());
            //todo 14/5/6 品目詳細IDと品目詳細名は、まだ未使用のため仮値を設定
            // addRow.ITEM_DETAILS_ID = byte.Parse(this._itemdetailsid.ToString());
            addRow.ITEM_DETAILS_ID = 0;

            this._tblReceipt_Update.AddtblReceipt_UpdateRow(addRow);

            ((ModelDetailForm)gtAddRow._model)._tblReceipt_Update = this._tblReceipt_Update;


            //--- 追加処理を実行 ---
            gtAddRow.doStart();

        }

        private void AddRecordToTable_old()
        {
            
            //--- 登録用のコントロールを生成 ---
            ServiceUpdateTblReceiptDetail gtAddRow = new ServiceUpdateTblReceiptDetail();
            gtAddRow._model = new ModelDetailForm();
            
            //--- 新規レコード作成 ---
            DataSetKakeibo.tblReceiptRow addRow = this._tblReceipt.NewtblReceiptRow();

            //--- 各プロパティの値を渡してレコード追加 ---
            addRow.DATE = DateTime.Parse(this._date);
            addRow.GOODS = this._goods;
            addRow.PRICE = this._price;
            addRow.ITEM_ID = byte.Parse(this._itemid.ToString());
            //todo 14/5/6 品目詳細IDと品目詳細名は、まだ未使用のため仮値を設定
            // addRow.ITEM_DETAILS_ID = byte.Parse(this._itemdetailsid.ToString());
            addRow.ITEM_DETAILS_ID = 0;

            this._tblReceipt.AddtblReceiptRow(addRow);

            ((ModelDetailForm)gtAddRow._model)._tblReceipt = this._tblReceipt;


            //--- 追加処理を実行 ---
            gtAddRow.doStart();

        }
        #endregion

        #region DeleteRecordForTable
        /// <summary>
        /// レコードを削除
        /// </summary>
        private void DeleteRecordForTable()
        {

            //--- 削除用のコントロールを生成 ---
            ServiceUpdateTblReceiptDetail gtDelete = new ServiceUpdateTblReceiptDetail();

            //--- tblReceiptのレコードを削除にする ---
            // todo 16/04/24 ViewかModelで検索した際に、インスタンス生成していれば不要と思う
            this._tblReceipt_Update = new DataSetKakeibo.tblReceipt_UpdateDataTable();
            DataSetKakeibo.tblReceipt_UpdateRow dr = this._tblReceipt_Update.NewtblReceipt_UpdateRow();
            dr.ID = this._tblReceipt[0].ID;
            this._tblReceipt_Update.AddtblReceipt_UpdateRow(dr);
            this._tblReceipt_Update[0].Delete();

            //this._tblReceipt_Update[0].Delete();

            // 更新用コントロールのmodelプロパテｨに画面用コントロールのmodelプロパテｨを反映させる
            ((ModelDetailForm)gtDelete._model)._tblReceipt_Update = this._tblReceipt_Update;


            //--- 削除処理を実行 ---
            gtDelete.doStart();
        }

        private void DeleteRecordForTable_old()
        {

            //--- 削除用のコントロールを生成 ---
            ServiceUpdateTblReceiptDetail gtDelete = new ServiceUpdateTblReceiptDetail();

            //--- tblReceiptのレコードを削除にする ---
            this._tblReceipt[0].Delete();

            // 更新用コントロールのmodelプロパテｨに画面用コントロールのmodelプロパテｨを反映させる
            ((ModelDetailForm)gtDelete._model)._tblReceipt = this._tblReceipt;


            //--- 削除処理を実行 ---
            gtDelete.doStart();
        }
        #endregion


        #endregion
    }
}