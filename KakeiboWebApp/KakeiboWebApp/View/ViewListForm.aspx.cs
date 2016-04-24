using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FrameworkClassicTea;                  // 独自フレームワーククラス
using KakeiboWebApp.Model;

namespace KakeiboWebApp.View
{
    /// <summary>
    /// 【ビュー系】家計簿の一覧表示画面
    /// </summary>
    public partial class ViewListForm : AbstractView
    {
        #region １．プライベート変数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ViewListForm";
        /// <summary>
        /// モデルクラス・ViewListForm受け渡し用
        /// </summary>
        private ModelListForm _model = new ModelListForm();
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
        public ViewListForm() : base(THIS_CLASS_NAME) 
        {

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
            // 何か処理があれば記入
            /*todo:15/10/19 仮設定で、ページロード時に必ずセットさせる
              note: class="form-control datepicker-is"をtextboxに指定したらカレンダーピッカーが表示されたのでコメント化
            Session["txtSearchDayOne"] = startdate.Value;
            txtSearchDayOne.Text = startdate.Value;
            Session["txtSearchDayTwo"] = enddate.Value;
            txtSearchDayTwo.Text = enddate.Value;
            */

            //--- ポストバック処理 ---
            // ポストバック発生時に毎回Page_Loadが呼び出されるので、
            // IsPostBackで初回のみ処理を行う
            if (!IsPostBack)
            {
                //--- 初回のLoad_Pageのみ処理（IsPostBackがFalse）

                //--- セッションを格納
                if (Session["txtSearchDayOne"] != null) 
                {
                    txtSearchDayOne.Text = Session["txtSearchDayOne"].ToString();
                }

                if (Session["txtSearchDayTwo"] != null)
                {
                    txtSearchDayTwo.Text = Session["txtSearchDayTwo"].ToString();
                }

                if (Session["txtSearchGoods"] != null)
                {
                    txtSearchGoods.Text = Session["txtSearchGoods"].ToString();
                }

                if (Session["txtSearchItem"] != null)
                {
                    txtSearchItem.Text = Session["txtSearchItem"].ToString();
                }

                //init(); 下の処理へ移した。
                SetGridView1FromDataSet();
            }
            else
            {
                //--- ポストバック発生時に行いたい処理を記述
                //init();

            }

        }
        #endregion

        #region ３．イベント

        #region btnSearchDay_OnClick
        /// <summary>
        /// ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchDay_OnClick(object sender, EventArgs e)
        {
            //--- GridView1にDataSetを反映
            SetGridView1FromDataSet();
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
            Response.Redirect("ViewMenu.aspx");
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

        #region GridView1_PageIndexChanging
        /// <summary>
        /// ページ変更
        /// </summary>
        /// <remarks>
        /// ページ番号が変わったら、押された番号をGridViewに表示させる
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // DataSetを再取得
            SetGridView1FromDataSet();

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = _model.dsKakeibo.tblReceipt;
            GridView1.DataBind();
        }
        #endregion

        #region GridView1_SelectedIndexChanging
        /// <summary>
        /// GridView1の行選択をクリックした時
        /// </summary>
        /// <remarks>
        /// 詳細画面を表示する
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_SelectedIndexChanged(Object sender, EventArgs e)
        {

            //note 以下の方法で選択した行を取得することも出来る。
            //GridViewRow row = GridView1.SelectedRow;
            //Session["Data"] = row;

            GridViewRow row = GridView1.SelectedRow;
            Session["ID"] = row.Cells[1].Text;
            Session["SWITCHSCREEN"] = false;    // 新規登録以外で詳細画面を表示するため、falseを設定
            Response.Redirect("ViewDetailForm.aspx");
        }
        #endregion

        #region btnAdd_Click
        /// <summary>
        /// 新規登録ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["ID"] = string.Empty;
            Session["SWITCHSCREEN"] = true;
            Response.Redirect("ViewDetailForm.aspx");
        }
        #endregion

        #region txtSearchDayOne_TextChanged
        /// <summary>
        /// 日付に値がセットされた時の処理
        /// </summary>
        /// <remarks>
        /// 一覧画面から詳細画面へ遷移して、戻った時のために日付を格納
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtSearchDayOne_TextChanged(object sender, EventArgs e)
        {
            Session["txtSearchDayOne"] = txtSearchDayOne.Text;
        }
        #endregion

        #region txtSearchDayTwo_TextChanged
        /// <summary>
        /// 日付に値がセットされた時の処理
        /// </summary>
        /// <remarks>
        /// 一覧画面から詳細画面へ遷移して、戻った時のために日付を格納
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtSearchDayTwo_TextChanged(object sender, EventArgs e)
        {
            Session["txtSearchDayTwo"] = txtSearchDayTwo.Text;
        }
        #endregion

        #region txtSearchGoods_TextChanged
        /// <summary>
        /// 商品名に値がセットされた時の処理
        /// </summary>
        /// <remarks>
        /// 一覧画面から詳細画面へ遷移して、戻った時のために商品名を格納
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtSearchGoods_TextChanged(object sender, EventArgs e)
        {
            Session["txtSearchGoods"] = txtSearchGoods.Text;
        }
        #endregion

        #region txtSearchItem_TextChanged
        /// <summary>
        /// 品目名に値がセットされた時の処理
        /// </summary>
        /// <remarks>
        /// 一覧画面から詳細画面へ遷移して、戻った時のために品目名を格納
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            Session["txtSearchItem"] = txtSearchItem.Text;
        }
        #endregion

        #endregion

        #region ４．プライベートメソッド

        #region init
        /// <summary>
        /// 初期処理
        /// </summary>
        private void init()
        {
            // GridView1の設定
            GridView1.AllowPaging = true;                              // ページング機能を有効にする
            GridView1.PageSize = 20;                                   // 1ページに表示する件数
            GridView1.DataSource = _model.dsKakeibo.tblReceipt;        // GridView1に表示するDataSet
            GridView1.AutoGenerateSelectButton = true;                 // 選択ボタンを追加

            GridView1.DataBind();                                      // GridView1にDataSetをバインド

            GridView1.PageIndex = GridView1.PageCount;                 // 最新ページを表示させる
            GridView1.DataBind();                                      // GridView1にDataSetをバインド

        }
        #endregion

        #region SetGridView1FromDataSet
        /// <summary>
        /// GridView1に値をセットする際に
        /// </summary>
        private void SetGridView1FromDataSet()
        {
            //--- 日付範囲のチェック ---
            if (txtSearchDayOne.Text != string.Empty & txtSearchDayTwo.Text != string.Empty)
            {
                if (DateTime.Parse(txtSearchDayOne.Text) > DateTime.Parse(txtSearchDayTwo.Text)) 
                {
                    //todo 14/05/10 とりあえず、終了日を開始日と同じに変える
                    txtSearchDayTwo.Text = txtSearchDayOne.Text;
                }
            }

            //--- 検索する日付でDataSetを取得する
            //todo 14/05/06 日付の判定はViewが良いのか？それとも、Serviceか？検討する。Viewはどこまでif文を使わず処理できるようにするかの方が良いか？
            if (txtSearchDayOne.Text == string.Empty &
                txtSearchDayTwo.Text == string.Empty &
                txtSearchItem.Text   == string.Empty &
                txtSearchGoods.Text  == string.Empty)
            {
                // 全件検索
                _model = new ModelListForm();
            }
            else if (txtSearchDayOne.Text != string.Empty &
                     txtSearchDayTwo.Text == string.Empty &
                     txtSearchItem.Text == string.Empty &
                     txtSearchGoods.Text == string.Empty)
            {
                // 開始日のみ指定検索
                _model = new ModelListForm(DateTime.Parse(txtSearchDayOne.Text));
            }
            else if (txtSearchDayOne.Text != string.Empty &
                     txtSearchDayTwo.Text != string.Empty &
                     txtSearchItem.Text   == string.Empty &
                     txtSearchGoods.Text  == string.Empty)
            {
                // 日付範囲指定検索
                _model = new ModelListForm(
                    DateTime.Parse(txtSearchDayOne.Text),
                    DateTime.Parse(txtSearchDayTwo.Text));
            }
            else if (txtSearchDayOne.Text != string.Empty &
                     txtSearchDayTwo.Text != string.Empty &
                     txtSearchItem.Text != string.Empty &
                     txtSearchGoods.Text == string.Empty)
            {
                // 日付範囲指定と品目名で検索
                _model = new ModelListForm(
                    DateTime.Parse(txtSearchDayOne.Text),
                    DateTime.Parse(txtSearchDayTwo.Text),
                    txtSearchItem.Text,
                    true);
            }
            else if (txtSearchDayOne.Text != string.Empty &
                     txtSearchDayTwo.Text != string.Empty &
                     txtSearchItem.Text == string.Empty &
                     txtSearchGoods.Text != string.Empty)
            {
                // 日付範囲指定と商品名で検索
                _model = new ModelListForm(
                    DateTime.Parse(txtSearchDayOne.Text),
                    DateTime.Parse(txtSearchDayTwo.Text),
                    txtSearchGoods.Text);
            }
            else if (txtSearchDayOne.Text == string.Empty &
                     txtSearchDayTwo.Text == string.Empty &
                     txtSearchItem.Text != string.Empty &
                     txtSearchGoods.Text != string.Empty)
            {
                // 品目名と商品名で検索
                // TODO 品目名と商品名だけで検索するサービスを追加する
                _model = new ModelListForm();
            }
            else if (txtSearchDayOne.Text == string.Empty &
                     txtSearchDayTwo.Text == string.Empty &
                     txtSearchItem.Text   != string.Empty &
                     txtSearchGoods.Text  == string.Empty)
            {
                // 品目名で検索
                // TODO 品目名だけで検索するサービスを追加する
                _model = new ModelListForm();
            }
            else if (txtSearchDayOne.Text == string.Empty &
                     txtSearchDayTwo.Text == string.Empty &
                     txtSearchItem.Text == string.Empty &
                     txtSearchGoods.Text != string.Empty)
            {
                // 商品名で検索
                _model = new ModelListForm(txtSearchGoods.Text);
            }



            //--- 初期処理を呼ぶ ---
            init();
        
            //--- 合計金額を集計する ---
            string price = string.Empty;
            price = _model.dsKakeibo.tblReceipt.Compute("Sum(PRICE)", "").ToString();   // TODO tblReceiptのままだと合計されない
            if (price != string.Empty)
            {
                txtSumPrice.Text = string.Format("{0:C}", int.Parse(price));
            }
            else
            {
                txtSumPrice.Text = string.Format("{0:C}", "0"); ;
            }
            //txtSumPrice.Text = string.Format("{0,9:C}", int.Parse(price)); todo 14/5/6 これだと左側に隙間が出来るなぜか？ 
        }
        #endregion

        #region GraffitiVoid
        /// <summary>
        /// テスト用メソッド
        /// </summary>
        /// <remarks>
        /// 新たに作ったクラスを呼出すテスト用のメソッド
        /// </remarks>
        private void GraffitiVoid()
        {
            _model = new ModelListForm();

        }
        #endregion

        #endregion

    }
}