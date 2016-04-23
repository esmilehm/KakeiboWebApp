using FrameworkClassicTea;
using KakeiboWebApp.Service;
using KakeiboWebApp.Data;
using System;

namespace KakeiboWebApp.Model
{
    /// <summary>
    /// 【モデル系】家計簿明細一覧画面用
    /// </summary>
    /// <remarks>
    /// todo 14/5/7 Serviceクラスを呼ぶ方法がまちまち。統一したい。
    /// 引数無しは、dskakeiboプロパティ内で行っていて、引数・dateはReplacedByValueFromTableで行っている。
    /// たしか、Serviceでもmodelを使うため、引数無しのコンストラクタ内にはTable取得処理を書かなかった気もする。
    /// 無限ループした為だった。
    /// </remarks>
    public class ModelListForm : AbstractModel
    {

        #region １．プライベート変数
        /// <summary>
        /// テキストボックス
        /// </summary>
        private string _textbox = string.Empty;
        /// <summary>
        /// 家計簿AccessのReceiptテーブル
        /// </summary>
        private DataSetKakeibo _dsKakeibo = null;
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
        /// 検索する商品名を格納
        /// </remarks>
        private string _goods = string.Empty;
        /// <summary>
        /// item
        /// </summary>
        /// <remarks>
        /// 検索する品目名を格納
        /// </remarks>
        private string _item = string.Empty;
        #endregion

        #region ２．コンストラクタ

        #region 引数無し
        /// <summary>
        /// コンストラクタ・引数無し
        /// </summary>
        /// <remarks>
        /// note 14/05/07 ここでServiceを呼ぶ処理を記述していると、Serviceで使用する際に無限ループするため、何も記述しない。
        /// </remarks>
        public ModelListForm()
        {
        }
        #endregion

        #region 引数・date
        /// <summary>
        /// コンストラクタ・引数有り
        /// </summary>
        /// <param name="date">検索日</param>
        public ModelListForm(DateTime date)
        {
            DataSetOfTheSearchInDate(date);
        }
        #endregion

        #region 引数・Days
        /// <summary>
        /// コンストラクタ・引数有り
        /// </summary>
        /// <param name="dateOne">検索開始日</param>
        /// <param name="dateTwo">検索終了日</param>
        public ModelListForm(DateTime dateOne,DateTime dateTwo)
        {
            DataSetOfTheSearchInDateRange(dateOne,dateTwo);
        }
        #endregion

        #region 引数・Goods
        /// <summary>
        /// コンストラクタ・引数有り
        /// </summary>
        /// <param name="goods">検索商品名</param>
        public ModelListForm(string goods)
        {
            DataSetOfTheSearchInGoods(goods);
        }
        #endregion

        #region 引数・Days＆Goods
        /// <summary>
        /// コンストラクタ・引数有り
        /// </summary>
        /// <param name="dateOne">検索開始日</param>
        /// <param name="dateTwo">検索終了日</param>
        /// <param name="goods">検索商品名</param> 
        public ModelListForm(DateTime dateOne, DateTime dateTwo,string goods)
        {
            DataSetOfTheSearchInTheGoodsAndDateRange(dateOne,dateTwo,goods);
        }
        #endregion

        #region 引数・Days&Item
        /// <summary>
        /// コンストラクタ・引数有り
        /// </summary>
        /// <param name="dateOne">検索開始日</param>
        /// <param name="dateTwo">検索終了日</param>
        /// <param name="item">検索品目名</param>
        public ModelListForm(DateTime dateOne, DateTime dateTwo, string item,bool dummy =false)
        {
            DataSetOfTheSearchInTheItemAndDateRange(dateOne, dateTwo, item);
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
                // 初回取得時のみ
                if (this._dsKakeibo == null)
                {
                    GetAllTblReceipt();
                }
                
                return this._dsKakeibo;

            }
            set
            {
                this._dsKakeibo = value;
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

        #region goods
        /// <summary>
        /// 検索商品
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
            }
        }
        #endregion

        #region item
        /// <summary>
        /// 検索商品
        /// </summary>
        /// <remarks>
        /// </remarks>
        public string item
        {
            get
            {
                return this._item;
            }
            set
            {
                this._item  = value;
            }
        }
        #endregion
        #endregion

        #region ４．プライベートメソッド

        #region GetAllTblReceipt
        /// <summary>
        /// GetAllTblReceipt
        /// </summary>
        /// <remarks>
        /// TblReceiptの全レコード取得
        /// </remarks>
        private void GetAllTblReceipt()
        {
            ServiceGetTblReceipt svGet = new ServiceGetTblReceipt();
            svGet.doStart();
            this._dsKakeibo = ((ModelListForm)svGet._model)._dsKakeibo;

        }
        #endregion

        #region DataSetOfTheSearchInDate
        /// <summary>
        /// DataSetOfTheSearchInDate
        /// </summary>
        /// <remarks>
        /// 日付を元に、tblReceiptからレコードを抽出
        /// </remarks>
        /// <param name="date">検索する日付</param>
        private void DataSetOfTheSearchInDate(DateTime dateOne)
        {
            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptSearchDay svGet = new ServiceGetTblReceiptSearchDay(dateOne);
            svGet.doStart();
            //--- 検索したtblReceiptを持ったDataSetを反映
            this._dsKakeibo = ((ModelListForm)svGet._model)._dsKakeibo;

        }
        #endregion

        #region DataSetOfTheSearchInDateRange
        /// <summary>
        /// DataSetOfTheSearchInDateRange
        /// </summary>
        /// <remarks>
        /// 日付を元に、tblReceiptからレコードを抽出
        /// </remarks>
        /// <param name="dateOne">検索開始日</param>
        /// <param name="dateTwo">検索終了日</param>
        private void DataSetOfTheSearchInDateRange(DateTime dateOne,DateTime dateTwo)
        {
            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptSearchDays svGet = new ServiceGetTblReceiptSearchDays(dateOne,dateTwo);
            svGet.doStart();
            //--- 検索したtblReceiptを持ったDataSetを反映
            this._dsKakeibo = ((ModelListForm)svGet._model)._dsKakeibo;

        }
        #endregion

        #region DataSetOfTheSearchInGoods
        /// <summary>
        /// DataSetOfTheSearchInGoods
        /// </summary>
        /// <remarks>
        /// 商品名を元に、tblReceiptからレコードを抽出
        /// </remarks>
        /// <param name="goods">検索する商品名</param>
        private void DataSetOfTheSearchInGoods(string goods)
        {
            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptSearchGoods svGet = new ServiceGetTblReceiptSearchGoods(goods);
            svGet.doStart();
            //--- 検索したtblReceiptを持ったDataSetを反映
            this._dsKakeibo = ((ModelListForm)svGet._model)._dsKakeibo;

        }
        #endregion

        #region DataSetOfTheSearchInTheGoodsAndDateRange
        /// <summary>
        /// DataSetOfTheSearchInTheGoodsAndDateRange
        /// </summary>
        /// <remarks>
        /// 日付を元に、tblReceiptからレコードを抽出
        /// </remarks>
        /// <param name="dateOne">検索開始日</param>
        /// <param name="dateTwo">検索終了日</param>
        /// <param name="goods">検索商品</param>
        private void DataSetOfTheSearchInTheGoodsAndDateRange(DateTime dateOne, DateTime dateTwo,string goods)
        {
            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptSearchDaysAndGoods svGet =
                new ServiceGetTblReceiptSearchDaysAndGoods(dateOne,dateTwo,goods);
            svGet.doStart();
            //--- 検索したtblReceiptを持ったDataSetを反映
            this._dsKakeibo = ((ModelListForm)svGet._model)._dsKakeibo;

        }
        #endregion

        #region DataSetOfTheSearchInYearMonth
        /// <summary>
        /// DataSetOfTheSearchInYearMonth
        /// </summary>
        /// <remarks>
        /// todo 2014/07/27 引数をstring型でyyyymmに変更する。その為、コンストラクタも新たに作成する
        /// 年月(yyyymm)を元に、tblReceiptからレコードを抽出
        /// </remarks>
        /// <param name="dateOne">検索年月日</param>
        private void DataSetOfTheSearchInYearMonth(DateTime dateOne)
        { 
            //--- tblReciptの値を取得するServiceを実行 ---
            ServiceGetTblReceiptSearchYearMonth svGet =
                new ServiceGetTblReceiptSearchYearMonth(dateOne);
            svGet.doStart();
            //--- 検索したtblReciptを持ったDataSetを反映
            this._dsKakeibo = ((ModelListForm)svGet._model)._dsKakeibo;
        }
        #endregion

        #region DataSetOfTheSearchInTheItemAndDateRange
        /// <summary>
        /// 日付を元に、tblReceiptAddItemNameからレコードを抽出
        /// </summary>
        /// <param name="dateOne"></param>
        /// <param name="dateTwo"></param>
        /// <param name="item"></param>
        private void DataSetOfTheSearchInTheItemAndDateRange(DateTime dateOne, DateTime dateTwo, string item)
        {
            //--- tblReceiptAddItemNameの値を取得するControlを実行 ---
            ServiceGetTblReceiptAddItemName svGet =
                new ServiceGetTblReceiptAddItemName(dateOne, dateTwo, item);
            svGet.doStart();
            //--- 検索したtblReceiptを持ったDataSetを反映
            this._dsKakeibo = ((ModelListForm)svGet._model)._dsKakeibo;
        }
        #endregion

        #endregion

    }
}