using FrameworkClassicTea;
using KakeiboWebApp.Service;
using KakeiboWebApp.Data;
using System;

namespace KakeiboWebApp.Model
{
    /// <summary>
    /// 【モデル系】家計簿集計画面用
    /// </summary>
    public class ModelSummaryForm : AbstractModel
    {
        #region １．プライベート変数
        /// <summary>
        /// 家計簿AccessのReceiptテーブル
        /// </summary>
        private DataSetKakeibo _dsKakeibo = null;
        /// <summary>
        /// SearchYM
        /// </summary>
        /// <remarks>
        /// 検索する年月
        /// </remarks>
        private string _searchYM = string.Empty;
        #endregion

        #region ２．コンストラクタ

        #region 引数無し
        /// <summary>
        /// コンストラクタ・引数無し
        /// </summary>
        /// <remarks>
        /// note 14/05/07 ここでServiceを呼ぶ処理を記述していると、Serviceで使用する際に無限ループするため、何も記述しない。
        /// </remarks>
        public ModelSummaryForm()
        {
            
        }
        #endregion

        #region 引数・searchYM
        public ModelSummaryForm(string searchYM)
        {
            DataSetOfTheSummaryInTheYearMonth(searchYM);
        }
        #endregion

        #endregion

        #region ３．プロパティ

        #region dsKakeibo
        /// <summary>
        /// 家計簿DB（Access）のtblReceiptを持ったデータセット
        /// </summary>
        /// <remarks>
        /// todo なぜdsKakeiboをプロパティで持たせていたのか？
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

        #region searchYM
        /// <summary>
        /// 検索年月
        /// </summary>
        /// <remarks>
        /// </remarks>
        public string searchYM
        {
            get
            {
                return this._searchYM;
            }
            set
            {
                this._searchYM = value;
            }
        }
        #endregion

        #endregion

        #region ４．プライベートメソッド

        #region DataSetOfTheSummaryInTheYearMonthAndItemID
        /* todo 全件取得できるように後で見直す。
        /// <summary>
        /// 年月とアイテムIDを元に、tblReceiptを集計したレコードを抽出
        /// </summary>
        private void DataSetOfTheSummaryInTheYearMonthAndItemID()
        {
            //--- tblReceiptの値を取得するControlを実行 ---
            ServiceGetTblReceiptSummaryYearMonth svGet =
                new ServiceGetTblReceiptSummary();
            svGet.doStart();
            //--- 集計したtblReceiptを持ったDataSetを反映
            this._dsKakeibo = ((ModelSummaryForm)svGet._model)._dsKakeibo;

        }
        */
        #endregion

        #region DataSetOfTheSummaryInTheYearMonth
        /// <summary>
        /// DataSetOfTheSummaryInTheYearMonth
        /// </summary>
        /// <remarks>
        /// 年月を、tblReceiptSumYearMonthから該当年月のレコードを抽出
        /// Serviceクラスを基に、指定データをTableAdapterクラスを指定してデータを取得する
        /// </remarks>
        /// <param name="ym">検索する年月</param>
        private void DataSetOfTheSummaryInTheYearMonth(string searchym)
        {
            //--- tblReceiptSumYearMonthの値を取得するControlを実行 ---
            ServiceGetTblReceiptSummaryYearMonth svGet = new ServiceGetTblReceiptSummaryYearMonth(searchym);
            svGet.doStart();
            //--- 検索したtblReceiptSumYearMonthを持ったDataSetを反映
            this._dsKakeibo = ((ModelSummaryForm)svGet._model)._dsKakeibo;

        }
        #endregion

        #endregion
    }
}