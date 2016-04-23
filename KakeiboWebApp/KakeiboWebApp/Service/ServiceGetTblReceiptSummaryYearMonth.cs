using System;
using FrameworkClassicTea;
using KakeiboWebApp.Data;
using KakeiboWebApp.Data.DataSetKakeiboTableAdapters;
using KakeiboWebApp.Model;

namespace KakeiboWebApp.Service
{
    /// <summary>
    /// 【サービス系】tblReceiptの取得処理
    /// </summary>
    public class ServiceGetTblReceiptSummaryYearMonth : AbstractService
    {
        #region １．プライベート定数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ServiceGetTblReceiptSummaryYearMonth";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// データセットを取得
        /// </remarks>
        public ServiceGetTblReceiptSummaryYearMonth(string searchYM)
            : base(THIS_CLASS_NAME) 
        {
            //note 下記の記述だと基底クラスの_modelを参照し続けるため、無限ループする
            //　_model = new ModelSummaryForm(searchYM);
            
            // 基底クラスの_modelをModelSummaryFormでインスタンス化
            _model = new ModelSummaryForm();
            // ModelSummaryFormクラスのsearchYMプロパティにsearchYMを設定
            ((ModelSummaryForm)_model).searchYM = searchYM;
        }
        #endregion

        #region ３．パブリックメソッド

        #region processStarts
        /// <summary>
        /// 処理実行
        /// </summary>
        protected override void processStarts()
        {
            //--- 呼び出し ---
            GetDataSet();

        }
        #endregion

        #region processEnd
        /// <summary>
        /// 終了処理
        /// </summary>
        protected override void processEnd()
        {
        }
        #endregion

        #endregion

        #region ４．プライベートメソッド

        #region GetDataSet
        /// <summary>
        /// 家計簿DB（Access）よりtblReceiptを取得
        /// </summary>
        /// <remarks>
        /// DataSetKakeiboのTableAdapterクラスを生成して、データベースから値を取得し、
        /// 基底クラス：AbstractServiceのプライベート変数：_modelに取得データをセットする
        /// </remarks>
        private void GetDataSet()
        {

            //--- 基底クラスのモデルクラスから取得 ---
            string ym = ((ModelSummaryForm)_model).searchYM;

            //--- テーブルアダプタの生成 ---
            tblReceiptSumYearMonthTableAdapter ta = new tblReceiptSumYearMonthTableAdapter();
            //--- データセットの生成 ---
            DataSetKakeibo ds = new DataSetKakeibo();

            //--- DBのテーブルを取得 ---
            int result = ta.FillBySearchYearMonth(ds.tblReceiptSumYearMonth, ym);

            //--- モデルクラスに受渡し --
            //note: 不要みたい　base._model = new ModelSummaryForm();
            ((ModelSummaryForm)_model).dsKakeibo = ds;

            //--- 取得結果をログに出力 ---
            System.Diagnostics.Trace.Write(ds.tblReceipt.Count.ToString());
        }
        #endregion

        #endregion
    }
}