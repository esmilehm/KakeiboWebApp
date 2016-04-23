using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameworkClassicTea;
using FrameworkClassicTea.Tool;
using KakeiboWebApp.Model;

namespace KakeiboWebApp.View
{
    /// <summary>
    /// 【ビュー系】家計簿のログイン画面
    /// </summary>
    public partial class ViewLogin : AbstractView
    {
        #region １．プライベート変数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ViewLogin";

        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <example>基底クラスでログ出力するためにコンストラクタを呼出し</example>
        /// <remarks></remarks>
        public ViewLogin() : base(THIS_CLASS_NAME) 
        {

        }
        #endregion

        #region ２．Page_Load
        /// <summary>
        /// ページロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region ３．イベント

        #region btnLogin_Click
        /// <summary>
        /// ログインボタン
        /// todo:2016/02/21 ボタンクリックイベントだと、コンストラクタが呼ばれる。HTML側のaタグでページ移動だと呼ばれない。統一したい。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //todo: 15/10/18 ログイン名とパスワードは未実装。DBから取得してマッチング予定
            Response.Redirect("ViewMenu.aspx");
        }
        #endregion

        #endregion

        #region ４．プライベートメソッド
        #endregion

    }
}