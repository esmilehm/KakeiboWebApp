using FrameworkClassicTea;
using KakeiboWebApp.Service;
using KakeiboWebApp.Data;
using System;

namespace KakeiboWebApp.Model
{
    /// <summary>
    /// 【モデル系】家計簿メニュー画面用
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class ModelMenuForm : AbstractModel
    {

        #region １．プライベート変数
        /// <summary>
        /// テキストボックス
        /// </summary>
        /// <remarks>
        /// いまのところ利用目的は無し
        /// </remarks>
        private string _textbox = string.Empty;
        /// <summary>
        /// 家計簿AccessのReceiptテーブル
        /// </summary>
        private DataSetKakeibo _dsKakeibo = null;
        /// <summary>
        /// 家計簿AccessのtblItemMaster
        /// </summary>
        private DataSetKakeibo.tblItemMasterDataTable _tblItemMaster = null;
        /// <summary>
        /// dateOne
        /// </summary>
        /// <remarks>
        /// 検索する日付の値を格納・開始日
        /// </remarks>
        private string _dateOne = string.Empty;
        /// <summary>
        /// dateTwo
        /// </summary>
        /// <remarks>
        /// 検索する日付の値を格納・終了日
        /// </remarks>
        private string _dateTwo = string.Empty;
        /// <summary>
        /// goods
        /// </summary>
        /// <remarks>
        /// 検索するアイテムIDを格納
        /// </remarks>
        private byte _itemID = 0;
        #endregion

        #region ２．コンストラクタ

        #region 引数無し
        /// <summary>
        /// コンストラクタ・引数無し
        /// </summary>
        /// <remarks>
        /// note 14/05/07 ここでServiceを呼ぶ処理を記述していると、Serviceで使用する際に無限ループするため、何も記述しない。
        /// </remarks>
        public ModelMenuForm()
        {
        }
        #endregion

        #region 引数・Days＆Item_ID
        /// <summary>
        /// コンストラクタ・引数有り
        /// </summary>
        /// <param name="dateOne">検索開始日</param>
        /// <param name="dateTwo">検索終了日</param>
        /// <param name="goods">検索アイテムID</param> 
        public ModelMenuForm(DateTime dateOne, DateTime dateTwo,byte itemID)
        {
            DataSetOfTheSearchInTheItemIDAndDateRange(dateOne, dateTwo, itemID);
        }
        #endregion

        #endregion

        #region ３．プロパティ

        #region dsKakeibo
        /// <summary>
        /// 家計簿DB（Access）のtblReceiptを持ったデータセット
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
                    ServiceGetTblItemMasterMenu gtItemMaster = new ServiceGetTblItemMasterMenu();
                    gtItemMaster.doStart();

                    this._tblItemMaster = ((ModelMenuForm)gtItemMaster._model)._tblItemMaster;
                }
                return this._tblItemMaster;
            }
            set
            {
                this._tblItemMaster = value;
            }
        }
        #endregion

        #region dateOne
        /// <summary>
        /// 検索開始日
        /// </summary>
        /// <remarks>
        /// </remarks>
        public string dateOne
        {
            get
            {
                return this._dateOne;
            }
            set
            {
                this._dateOne = value;
            }
        }
        #endregion

        #region dateTwo
        /// <summary>
        /// 検索終了日
        /// </summary>
        /// <remarks>
        /// </remarks>
        public string dateTwo
        {
            get
            {
                return this._dateTwo;
            }
            set
            {
                this._dateTwo = value;
            }

        }
        #endregion

        #region itemID
        /// <summary>
        /// 検索商品
        /// </summary>
        /// <remarks>
        /// </remarks>
        public byte itemID
        {
            get 
            {
                return this._itemID;
            }
            set 
            {
                this._itemID = value;
            }
        }
        #endregion

        #endregion

        #region ４．プライベートメソッド

        #region DataSetOfTheSearchInTheItemIDAndDateRange
        /// <summary>
        /// DataSetOfTheSearchInTheItemIDAndDateRange
        /// </summary>
        /// <remarks>
        /// 日付を元に、tblReceiptからレコードを抽出
        /// </remarks>
        /// <param name="dateOne">検索開始日</param>
        /// <param name="dateTwo">検索終了日</param>
        /// <param name="goods">検索アイテムID</param>
        private void DataSetOfTheSearchInTheItemIDAndDateRange(DateTime dateOne, DateTime dateTwo,byte itemID)
        {
            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptMenu svGet =
                new ServiceGetTblReceiptMenu(dateOne, dateTwo, itemID);
            svGet.doStart();
            //--- 検索したtblReceiptを持ったDataSetを反映
            this._dsKakeibo = ((ModelMenuForm)svGet._model)._dsKakeibo;

        }
        #endregion


        #endregion

    }
}