using FrameworkClassicTea;
using KakeiboWebApp.Data;
using KakeiboWebApp.Data.DataSetKakeiboTableAdapters;
using KakeiboWebApp.Model;


namespace KakeiboWebApp.Service
{
    /// <summary>
    /// 【サービス系】tblReceipt明細情報の取得処理
    /// </summary>
    public class ServiceGetTblReceiptDetail : AbstractService
    {
        #region １．プライベート定数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ServiceGetDetailtblReceipt";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 親クラスの引数を指定するやり方があっているか不明。
        /// </remarks>
        public ServiceGetTblReceiptDetail(int id) : base(THIS_CLASS_NAME) 
        {
            _model = new ModelDetailForm();
            ((ModelDetailForm)_model).id = id;
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
        /// 家計簿DB（Access）よりtblReceiptを取得して明細情報を設定する処理
        /// </summary>
        private void GetDataSet()
        {
            //--- モデルクラスから取得 ---
            int id = ((ModelDetailForm)_model).id;

            //--- テーブルアダプタの生成 ---
            tblReceiptTableAdapter ta = new tblReceiptTableAdapter();
            //--- データセットの生成 ---
            DataSetKakeibo ds = new DataSetKakeibo();
            
            //--- DBのテーブルを取得 ---
            int result = ta.FillByOneRecord(ds.tblReceipt,id);

            //--- モデルクラスに受渡し --
            ((ModelDetailForm)_model).dsKakeibo = ds;
            ((ModelDetailForm)_model).tblReceipt = ds.tblReceipt;

            ((ModelDetailForm)_model).date = ds.tblReceipt[0].DATE.ToString();
            ((ModelDetailForm)_model).goods = ds.tblReceipt[0].GOODS;
            ((ModelDetailForm)_model).price = ds.tblReceipt[0].PRICE;
            ((ModelDetailForm)_model).itemid = ds.tblReceipt[0].ITEM_ID;
            ((ModelDetailForm)_model).itemdetailsid = ds.tblReceipt[0].ITEM_DETAILS_ID;

            //### 検討中ロジック追加 ###
            // note: 更新・削除用にtblReceipt_Updateも取得する
            tblReceipt_UpdateTableAdapter ta_Update = new tblReceipt_UpdateTableAdapter();
            int result_Update = ta_Update.FillByOneRecord(ds.tblReceipt_Update, id);
            ((ModelDetailForm)_model).tblReceipt_Update = ds.tblReceipt_Update;
            //### 検討中ロジック追加 ###


            //--- 取得結果をログに出力 ---
            System.Diagnostics.Trace.Write(ds.tblReceipt.Count.ToString());
        }
        #endregion

        #endregion
    }
}