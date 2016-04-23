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
    public class ServiceGetTblReceiptSearchGoods : AbstractService
    {
        #region １．プライベート定数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ServiceGetTblReceiptSearchGoods";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 親クラスの引数を指定するやり方があっているか不明。
        /// </remarks>
        public ServiceGetTblReceiptSearchGoods(string goods)
            : base(THIS_CLASS_NAME) 
        {
            _model = new ModelListForm();
            ((ModelListForm)_model).goods = goods.ToString();
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
            string goods = ((ModelListForm)_model).goods;

            //--- テーブルアダプタの生成 ---
            tblReceiptTableAdapter ta = new tblReceiptTableAdapter();
            //--- データセットの生成 ---
            DataSetKakeibo ds = new DataSetKakeibo();
            
            //--- DBのテーブルを取得 ---
            int result = ta.FillBySearchGoods(ds.tblReceipt, goods);
            
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