using FrameworkClassicTea;
using KakeiboWebApp.Data;
using KakeiboWebApp.Data.DataSetKakeiboTableAdapters;
using KakeiboWebApp.Model;
using System.Data;

namespace KakeiboWebApp.Service
{
    /// <summary>
    /// 【サービス系】tblReceiptの更新処理
    /// </summary>
    public class ServiceUpdateTblReceiptDetail : AbstractService
    {
        #region １．プライベート定数
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ServiceUpdateTblReceiptDetail";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 親クラスの引数を指定するやり方があっているか不明。
        /// </remarks>
        public ServiceUpdateTblReceiptDetail(): base(THIS_CLASS_NAME) 
        {
            _model = new ModelDetailForm();
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
            UpdateDataSet();

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

        #region UpdateDataSet

        /// <summary>
        /// 家計簿DB（Access）のtblReceiptへ更新処理を行う
        /// </summary>
        /// <remarks>
        /// todo 14/02/10 テーブル反映結果がViewに返せて無い。resultをViewに渡して、メッセージを表示する等。
        /// todo 17/05/01 ModelDetailForm.csでtblReceipt_Updateが空だった。原因は、ServiceGetTblReceiptDetailでtblReceipt_Updateに何もセットしてないためだった。
        /// </remarks>
        private void UpdateDataSet()
        {
            //--- テーブルアダプタの生成 ---
            tblReceipt_UpdateTableAdapter ta = new tblReceipt_UpdateTableAdapter();
            
            //--- データセットの生成 ---
            DataSetKakeibo ds = new DataSetKakeibo();
            
            //--- DBのテーブルを取得 ---
            //int result = ta.Update(((ModelDetailForm)_model).tblReceipt);
            int result = ta.Update(((ModelDetailForm)_model).tblReceipt_Update);

            //--- 取得結果をログに出力 ---
            System.Diagnostics.Trace.Write(result.ToString());
        }
        #endregion

        #endregion
    }
}