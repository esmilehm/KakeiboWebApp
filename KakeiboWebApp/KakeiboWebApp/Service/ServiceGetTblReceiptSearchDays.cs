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
    public class ServiceGetTblReceiptSearchDays : AbstractService
    {
        #region １．プライベート定数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ServiceGetTblReceiptSearchDays";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 日付範囲指定で検索してデータセットを取得
        /// </remarks>
        public ServiceGetTblReceiptSearchDays(DateTime dateOne,DateTime dateTwo)
            : base(THIS_CLASS_NAME) 
        {
            _model = new ModelListForm();
            ((ModelListForm)_model).dateOne = dateOne.ToString();
            ((ModelListForm)_model).dateTwo = dateTwo.ToString();
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
        private void GetDataSet()
        {
            //--- モデルクラスから取得 ---
            string dateOne = ((ModelListForm)_model).dateOne;
            string dateTwo = ((ModelListForm)_model).dateTwo;

            //--- テーブルアダプタの生成 ---
            tblReceiptTableAdapter ta = new tblReceiptTableAdapter();
            //--- データセットの生成 ---
            DataSetKakeibo ds = new DataSetKakeibo();
            
            //--- DBのテーブルを取得 ---
            int result = ta.FillBySearchDays(ds.tblReceipt, DateTime.Parse(dateOne),DateTime.Parse(dateTwo));
            
            //--- モデルクラスに受渡し --
            //note: 不要みたい　base._model = new ModelListForm();
            ((ModelListForm)_model).dsKakeibo = ds;

            //--- 取得結果をログに出力 ---
            System.Diagnostics.Trace.Write(ds.tblReceipt.Count.ToString());
        }
        #endregion

        #endregion
    }
}