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
    public class ServiceGetTblReceiptSummary : AbstractService
    {
        #region １．プライベート定数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ServiceGetTblReceiptSummary";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// データセットを取得
        /// </remarks>
        public ServiceGetTblReceiptSummary()
            : base(THIS_CLASS_NAME) 
        {
            _model = new ModelSummaryForm();
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
        /// tblReceiptSumYearMonthから全件取得する関数
        /// </remarks>
        private void GetDataSet()
        {

            //--- テーブルアダプタの生成 ---
            tblReceiptSumYearMonthTableAdapter ta = new tblReceiptSumYearMonthTableAdapter();
            //--- データセットの生成 ---
            DataSetKakeibo ds = new DataSetKakeibo();

            //--- DBのテーブルを取得 ---

            int result = ta.Fill(ds.tblReceiptSumYearMonth);

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