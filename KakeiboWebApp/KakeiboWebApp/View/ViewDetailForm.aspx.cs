using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameworkClassicTea;
using KakeiboWebApp.Model;

namespace KakeiboWebApp.View
{
    /// <summary>
    /// 【ビュー系】家計簿の明細表示画面
    /// </summary>
    public partial class ViewDetailForm : AbstractView
    {
        #region １．プライベート変数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ViewDetailForm";
        /// <summary>
        /// ModelDetailForm
        /// </summary>
        /// <remarks>
        /// note 14/2/15 考察；PostBackされたときも初期化されているため、Page_Loadで生成する
        /// </remarks>
        private ModelDetailForm _model;

        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <example>基底クラスでログ出力するためにコンストラクタを呼出し</example>
        /// <remarks></remarks>
        public ViewDetailForm() : base(THIS_CLASS_NAME) 
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

            //--- セッション情報の取得 ---
            string id = Session["ID"].ToString();
            bool blSwitchScreen = bool.Parse(Session["SWITCHSCREEN"].ToString());  // 新規登録ボタンが押されて遷移した時はtrue


            //--- ポストバック処理 ---
            // ポストバック発生時に毎回Page_Loadが呼び出されるので、IsPostBackで初回のみ処理を行う
            if (!IsPostBack)
            {
                //--- 初期処理 ---
                init(blSwitchScreen, id);

                //--- 各テキストボックスに値を設定 ---
                if (blSwitchScreen == false)
                {
                    SetDetailsView();
                }

                //--- DropDownListのプロパティ設定 ---
                //todo 14/2/9 毎回、サーバーにアクセスするから遅くなるかも。javascriptの方が良いのかな？
                //値が変更された時に、ポストバックを自動で行う
                ddlItem.AutoPostBack = true;

                SetDropDownList(blSwitchScreen);
            }
            else
            {
                //--- ポストバック発生時に行いたい処理を記述

                //--- 初期処理 ---
                init(blSwitchScreen, id);
            }
        }
        #endregion

        #region ３．イベント

        #region ddlItem_TextChanged
        /// <summary>
        /// 品目名が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 【品目IDと品目名のテキストボックスに反映】
            txtItemID.Text = ddlItem.SelectedItem.Value;
            txtItem.Text = ddlItem.SelectedItem.Text;
        }

        #endregion

        #region btnReturn_Click
        /// <summary>
        /// 戻るボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewListForm.aspx");
        }
        #endregion

        #region btnClose_Click
        /// <summary>
        /// 閉じるボタン押下時
        /// </summary>
        /// <remarks>
        /// Webフォームを閉じるための関数は.NET Frameworkには無い。
        /// なので、javascriptで行う方法を使う。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            script.Append("<script language='javascript'>\n");
            script.Append("window.close();\n");
            script.Append("</script>\n");

            // JavaScriptを登録する。
            ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", script.ToString());
        }
        #endregion

        #region btnUpdate_Click
        /// <summary>
        /// 更新ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //--- 画面の値をテーブルに取得 ---
            GetDetailsView();

            //--- テーブル反映処理 ---
            Boolean flg = _model.tblReceptUpdateSubmit;

            //--- 画面に表示されている値をクリア ---
            ClearDetailsView();
        }
        #endregion

        #region btnDelete_Click
        /// <summary>
        /// 削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //--- テーブル反映処理 ---
            Boolean flg = _model.tblReceptDeleteSubmit;

            //--- 画面に表示されている値をクリア ---
            ClearDetailsView();
        }
        #endregion

        #region btnAdd_Click
        /// <summary>
        /// 登録ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //--- 画面の値をテーブルに取得 ---
            GetDetailsView();

            //--- テーブルに反映 ---
            Boolean flg = _model.tblReceiptAddSubmit;

            //--- 画面に表示されている値をクリア ---
            ClearDetailsView();

        }
        #endregion

        #region btnView1_Click
        /// <summary>
        /// カレンダーボタンがクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /* note:15/10/18 jQueryのCollapseコントロールに置き換えたので不要になった
        protected void btnView1_Click(object sender, EventArgs e)
        {
          MultiView1.ActiveViewIndex = 0;
        }
        */
        #endregion

        #region btnView2_Click
        /// <summary>
        /// 登録商品ボタンがクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /* note:15/10/18 jQueryのCollapseコントロールに置き換えたので不要になった
        protected void btnView2_Click(object sender, EventArgs e)
        {
          MultiView1.ActiveViewIndex = 1;
        }
         */ 
        #endregion

        #region btnEntry1_Click
        /// <summary>
        /// 登録ボタン１がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry1_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "ジュース";
            ddlItem.SelectedIndex = 1;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry2_Click
        /// <summary>
        /// 登録ボタン２がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry2_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "お菓子パン";
            ddlItem.SelectedIndex = 1;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry3_Click
        /// <summary>
        /// 登録ボタン３がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry3_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "おかずパン";
            ddlItem.SelectedIndex = 1;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry4_Click
        /// <summary>
        /// 登録ボタン４がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry4_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "缶ビール";
            ddlItem.SelectedIndex = 1;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry5_Click
        /// <summary>
        /// 登録ボタン５がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry5_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "お惣菜";
            ddlItem.SelectedIndex = 1;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry6_Click
        /// <summary>
        /// 登録ボタン６がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry6_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "ガソリン代";
            ddlItem.SelectedIndex = 4;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry7_Click
        /// <summary>
        /// 登録ボタン７がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry7_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "週刊少年ジャンプ";
            txtPrice.Text = "255";
            ddlItem.SelectedIndex = 3;
            SetddlItem();
            btnAdd.Focus();
        }
        #endregion

        #region btnEntry8_Click
        /// <summary>
        /// 登録ボタン８がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry8_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "週刊少年マガジン";
            txtPrice.Text = "260";
            ddlItem.SelectedIndex = 3;
            SetddlItem();
            btnAdd.Focus();
        }
        #endregion

        #region btnEntry9_Click
        /// <summary>
        /// 登録ボタン９がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry9_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "週刊少年サンデー";
            txtPrice.Text = "270";
            ddlItem.SelectedIndex = 3;
            SetddlItem();
            btnAdd.Focus();
        }
        #endregion

        #region btnEntry10_Click
        /// <summary>
        /// 登録ボタン10がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry10_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "洗車代";
            txtPrice.Text = "2100";
            ddlItem.SelectedIndex = 7;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry11_Click
        /// <summary>
        /// 登録ボタン11がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry11_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "漫画喫茶";
            ddlItem.SelectedIndex = 3;
            SetddlItem();
            txtPrice.Focus();
        }

        #endregion

        #region btnEntry12_Click
        /// <summary>
        /// 登録ボタン12がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry12_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "おにぎり";
            txtPrice.Text = "100";
            ddlItem.SelectedIndex = 1;
            SetddlItem();
            txtPrice.Focus();
        }
        #endregion

        #region btnEntry13_Click
        /// <summary>
        /// 登録ボタン13がクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEntry13_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "床屋";
            txtPrice.Text = "2500";
            ddlItem.SelectedIndex = 7;
            SetddlItem();
            btnAdd.Focus();
        }
        #endregion

        #region btnEntry14_Click
        protected void btnEntry14_Click(object sender, EventArgs e)
        {
            txtGoods.Text = "家賃";
            txtPrice.Text = "30000";
            ddlItem.SelectedIndex = 10;
            SetddlItem();
            btnAdd.Focus();
        }
        #endregion


        #endregion

        #region ４．プライベートメソッド

        #region init
        /// <summary>
        /// 初期処理
        /// </summary>
        /// <remarks>
        /// 新規登録時はinSwitchScreenがTrue／更新・削除時はinSwitchScreenがFalse
        /// </remarks>
        private void init(bool inSwitchScreen, string id)
        {
            //--- 遷移画面の切替・新規登録用か更新・削除用にModelを生成 ---
            if (inSwitchScreen == true)
            {
                // tblItemMasterのみ取得する
                _model = new ModelDetailForm();
            }
            else
            {
                // idをkeyに該当するtblReceiptレコードを取得する
                _model = new ModelDetailForm(int.Parse(id));
            }

            //--- 画面表示設定 ---
            //todo 14/5/6 品目詳細IDと品目詳細名は、まだ未使用のため、非表示
            lblItemDetailsID.Visible = false;
            lblItemDetails.Visible = false;
            txtItemDetailsID.Visible = false;
            txtItemDetails.Visible = false;
        }
        #endregion

        #region SetDetailsView
        /// <summary>
        /// DetailsViewのプロパティを設定する関数
        /// </summary>
        /// <remarks>
        /// 画面のテキストに値を設定
        /// </remarks>
        private void SetDetailsView()
        {
            txtDate.Text = _model.date;
            txtGoods.Text = _model.goods;
            txtPrice.Text = _model.price.ToString();
            txtItemID.Text = _model.itemid.ToString();
            txtItemDetailsID.Text = _model.itemdetailsid.ToString();
            txtItem.Text = "仮";  //gm.DsReceipt.tblReceipt.Rows[0][2].ToString();
            txtItemDetails.Text = "仮";  //gm.DsReceipt.tblReceipt.Rows[0][1].ToString();
        }
        #endregion

        #region GetDetailsView
        /// <summary>
        /// 画面からテーブルに値を設定
        /// </summary>
        /// <remarks>
        /// 更新、削除、登録ボタンが押された際に、値をテーブルに渡す為の処理
        /// </remarks>
        private void GetDetailsView()
        {
            _model.date = txtDate.Text;
            _model.goods = txtGoods.Text;
            _model.price = int.Parse(txtPrice.Text);
            _model.itemid = int.Parse(txtItemID.Text);
            //todo 14/5/6 品目詳細IDと品目詳細名は、まだ未使用のため仮値を設定 
            //_model.itemdetailsid = int.Parse(txtItemDetailsID.Text);
            _model.itemdetailsid = 0;
        }
        #endregion

        #region SetDropDownList
        /// <summary>
        /// 品目のDropDownListを設定
        /// </summary>
        private void SetDropDownList(bool inSwitchScreen)
        {

            //--- DropDownListの設定 ---
            // データソースプロパティにテーブルを設定
            ddlItem.DataSource = _model.tblItemMaster;
            // DropDownListの値を設定（画面には表示されない値を設定）
            ddlItem.DataValueField = _model.tblItemMaster.Columns[0].ColumnName.ToString();
            // DropDownListに表示する値を設定
            ddlItem.DataTextField = _model.tblItemMaster.Columns[1].ColumnName.ToString();
            // DropDownListにデータをバインドさせます
            ddlItem.DataBind();

            //--- DropDownListの先頭に "---------" を追加 ---
            ddlItem.Items[0].Text = "---------";

            //--- DropDownListに表示させる品目IDを指定 ---

            // 登録用画面以外の場合は、データベースに登録されている品目IDを選択させる
            if (inSwitchScreen == true)
            {
                ddlItem.SelectedIndex = 0;
            }
            else
            {
                // DropDownListに表示させる品目IDを指定
                ListItem li = ddlItem.Items.FindByValue(_model.itemid.ToString());     // DropDownListのValueFild内にマッチする行(ListItem)を調べる
                int selectID = ddlItem.Items.IndexOf(li);                           // DropDownListにマッチする行番号を取得する
                ddlItem.SelectedIndex = selectID;                                   // DropDownListでマッチした行番号を選択する
                txtItem.Text = ddlItem.SelectedItem.Text;
            }

        }
        #endregion

        #region ClearDetailsView
        /// <summary>
        /// 画面に表示している内容をクリアーする
        /// </summary>
        private void ClearDetailsView()
        {
            //txtDate.Text = "";
            txtGoods.Text = "";
            txtPrice.Text = "";
            ddlItem.SelectedIndex = 0;
            txtItemID.Text = "";
            txtItem.Text = "";
            // txtItemDetailsID.Text = "";
            // txtItemDetails.Text = "";

        }
        #endregion

        #region SetddlItem
        /// <summary>
        /// 品目IDを登録ボタンから変更した時に呼び出す
        /// </summary>
        /// <remarks>
        /// ddlItemのイベントに置き換えられれば不要なメソッド
        /// </remarks>
        private void SetddlItem()
        {
            //TODO: 2015/04/29 ddlItemが変更されたら発生するイベントに紐付けしたい
            // 【品目IDと品目名のテキストボックスに反映】
            txtItemID.Text = ddlItem.SelectedItem.Value;
            txtItem.Text = ddlItem.SelectedItem.Text;
        }
        #endregion

        #endregion

    }
}