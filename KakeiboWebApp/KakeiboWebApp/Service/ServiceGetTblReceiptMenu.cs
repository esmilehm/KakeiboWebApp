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
    public class ServiceGetTblReceiptMenu : AbstractService
    {
        #region １．プライベート定数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ServiceGetTblReceiptMenu";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 日付範囲と商品名で検索してデータセットを取得
        /// </remarks>
        public ServiceGetTblReceiptMenu(DateTime dateOne, DateTime dateTwo, byte itemID)
            : base(THIS_CLASS_NAME) 
        {
            //todo ModelMenuFormクラスと同じ処理なのに個別にModelの型を用意しているため、このServiceGetTblReceiptクラスが汎用的に利用出来てない。見直す。
            _model = new ModelMenuForm();
            ((ModelMenuForm)_model).dateOne = dateOne.ToString();
            ((ModelMenuForm)_model).dateTwo = dateTwo.ToString();
            ((ModelMenuForm)_model).itemID = itemID;
        }
        #endregion

        #region ３．パブリックメソッド

        #region processStarts
        /// <summary>
        /// 処理実行
        /// </summary>
        protected override void processStarts()
        {
            //TODO ここでエラーを起こせば、基底クラス側のTry-Catch で　つかまる。　throw new Exception("独自例外");

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
            string dateOne = ((ModelMenuForm)_model).dateOne;
            string dateTwo = ((ModelMenuForm)_model).dateTwo;
            byte itemID = ((ModelMenuForm)_model).itemID;

            //--- テーブルアダプタの生成 ---
            tblReceiptTableAdapter ta = new tblReceiptTableAdapter();
            //--- データセットの生成 ---
            DataSetKakeibo ds = new DataSetKakeibo();
            
            //--- DBのテーブルを取得 ---
            int result = ta.FillBySearchDaysAndItemID(
                ds.tblReceipt,
                DateTime.Parse(dateOne),
                DateTime.Parse(dateTwo),itemID);
            
            //--- モデルクラスに受渡し --
            //note: 不要みたい　base._model = new ModelMenuForm();
            ((ModelMenuForm)_model).dsKakeibo = ds;

            //--- 取得結果をログに出力 ---
            System.Diagnostics.Trace.Write(ds.tblReceipt.Count.ToString());
        }
        #endregion

        #endregion
    }
}