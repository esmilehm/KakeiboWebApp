using System;
using System.Web.UI.WebControls;
using FrameworkClassicTea;                  // 独自フレームワーククラス
using KakeiboWebApp.Model;
using System.Text;

namespace KakeiboWebApp.View
{
    /// <summary>
    /// 【ビュー系】家計簿のMenu画面
    /// </summary>
    public partial class ViewMenu : AbstractView
    {
        #region １．プライベート変数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ViewMenu";
        /// <summary>
        /// モデルクラス・ViewMenuForm受け渡し用
        /// </summary>
        private ModelMenuForm _model = new ModelMenuForm();
        /// <summary>
        /// セッション情報；検索開始日
        /// </summary>
        private const string SESSION_TXTSEARCHDAYONE = "txtSearchDayOne";
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <example>基底クラスでログ出力するためにコンストラクタを呼出し</example>
        /// <remarks></remarks>
        public ViewMenu() : base(THIS_CLASS_NAME) 
        {
            //TODO 例外を発生させても、基底クラス側でキャッチされなかった。なぜ？throw new Exception("独自例外");
            
        }
        #endregion

        #region ２．Page_Load
        /// <summary>
        /// ページロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
             
            //--- 初期処理 ---
            init();

            //--- ポストバック処理 ---
            // ポストバック発生時に毎回Page_Loadが呼び出されるので、
            // IsPostBackで初回のみ処理を行う
            if (!IsPostBack)
            {
                //--- 初回のLoad_Pageのみ処理（IsPostBackがFalse）

                //--- セッションを格納

                //init(); 下の処理へ移した。

            }
            else
            {
                //--- ポストバック発生時に行いたい処理を記述
                //init();

            }

        }
        #endregion

        #region ３．イベント

        /// <summary>
        /// Page_Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// デバックモードではエラーをキャッチ出来ないのか、Page_Errorが呼出されない。
        /// </remarks>
        public override void Page_Error(Object sender, EventArgs e)
        {
            Exception ex = this.Server.GetLastError();
            this.Server.ClearError();
            if (ex is ApplicationException)
            {
                // 業務上の例外に対する処理
                lblMessage.Text = "Page_Errorでキャッチ：エラーが発生しました";
                
            }
            else
            {
                // 技術的な例外に対する処理
            }
        }

        /// <summary>
        /// Application_Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            //' 例外の内容を取得し、StringBuilder に格納する
            StringBuilder message = new StringBuilder();
            if (Server != null)
            {
                Exception ex;
 
                // 例外情報を取得する
                for (ex = Server.GetLastError(); ex != null; ex = ex.InnerException)
                {
                    message.AppendFormat("{0}: {1}{2}", ex.GetType().FullName, ex.Message, ex.StackTrace);
                }
 
                //
                // 例外情報と内部例外情報をログとして出力する処理など
                //
                lblMessage.Text = "Application_Errorでキャッチ：" + message;

                // 例外をクリア（カスタムエラーページを設定している場合はコメントアウトすること）
                Server.ClearError();
            }
        }
        #endregion

        #region ４．プライベートメソッド

        #region init
        /// <summary>
        /// 初期処理
        /// </summary>
        private void init()
        {

            /*** 今月の月頭と月末の日付を求める ***/
            DateTime dt = DateTime.Now;
            DateTime firstDay = new DateTime(dt.Year, dt.Month, 1);
            DateTime endDay = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

            /*** 今月の食費を設定 ***/
            // 日付範囲指定とアイテムIDで検索
            byte itemID = (byte)_model.tblItemMaster.Rows[1][0];

            _model = new ModelMenuForm(firstDay, endDay,itemID);

            //--- 合計金額を集計する ---
            string price = string.Empty;
            price = _model.dsKakeibo.tblReceipt.Compute("Sum(PRICE)", "").ToString();
            if (price != string.Empty)
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine("【Debug】" + string.Format("{0:C}", int.Parse(price)));
                lblSumOfFood.Text = string.Format("{0:C}", int.Parse(price));
            }
            else
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine(string.Format("【Debug】" + "{0:C}", 0));
                lblSumOfFood.Text = string.Format("{0:C}",0);
            }


            /*** 今月の交際費を設定 ***/
            // 日付範囲指定とアイテムIDで検索
            itemID = (byte)_model.tblItemMaster.Rows[2][0];

            _model = new ModelMenuForm(firstDay, endDay, itemID);

            //--- 合計金額を集計する ---
            price = string.Empty;
            price = _model.dsKakeibo.tblReceipt.Compute("Sum(PRICE)", "").ToString();
            if (price != string.Empty)
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine("【Debug】" + string.Format("{0:C}", int.Parse(price)));
                lblSumOfEntertainment.Text = string.Format("{0:C}", int.Parse(price));
            }
            else
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine(string.Format("【Debug】" + "{0:C}", 0));
                lblSumOfEntertainment.Text = string.Format("{0:C}", 0);
            }


            /*** 今月の娯楽費を設定 ***/
            // 日付範囲指定とアイテムIDで検索
            itemID = (byte)_model.tblItemMaster.Rows[3][0];

            _model = new ModelMenuForm(firstDay, endDay, itemID);

            //--- 合計金額を集計する ---
            price = string.Empty;
            price = _model.dsKakeibo.tblReceipt.Compute("Sum(PRICE)", "").ToString();
            if (price != string.Empty)
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine("【Debug】" + string.Format("{0:C}", int.Parse(price)));
                lblSumOfRecreation.Text = string.Format("{0:C}", int.Parse(price));
            }
            else
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine(string.Format("【Debug】" + "{0:C}", 0));
                lblSumOfRecreation.Text = string.Format("{0:C}", 0);
            }

            /*** 今月の交通費を設定 ***/
            // 日付範囲指定とアイテムIDで検索
            itemID = (byte)_model.tblItemMaster.Rows[4][0];

            _model = new ModelMenuForm(firstDay, endDay, itemID);

            //--- 合計金額を集計する ---
            price = string.Empty;
            price = _model.dsKakeibo.tblReceipt.Compute("Sum(PRICE)", "").ToString();
            if (price != string.Empty)
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine("【Debug】" + string.Format("{0:C}", int.Parse(price)));
                lblSumOfTrafficCost.Text = string.Format("{0:C}", int.Parse(price));
            }
            else
            {
                //出力ウインドウにメッセージを一行表示する
                //System.Diagnostics.Debug.WriteLine(string.Format("【Debug】" + "{0:C}", 0));
                lblSumOfTrafficCost.Text = string.Format("{0:C}", 0);
            }


        }
        #endregion

        #endregion


    }
}