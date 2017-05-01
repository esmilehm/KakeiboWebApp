using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FrameworkClassicTea;                  // 独自フレームワーククラス
using KakeiboWebApp.Model;
using KakeiboWebApp.Model.Receipt;          // 新しいmodel

namespace KakeiboWebApp.View
{
    /// <summary>
    /// 【ビュー系】家計簿の一覧表示画面
    /// </summary>
    /// <remarks>
    /// 【画面概要】
    /// 
    /// </remarks>
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
        /// <remarks>
        /// [概要]
        /// *_初期表示時
        /// *__コントロールの初期化
        /// *__セッションの初期化
        /// *__GridViewに値をセット
        /// *_ポストバック時
        /// *__無し
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            /*
             
                初期処理
             
             */


            //何か処理があれば記入
            /*todo:15/10/19 仮設定で、ページロード時に必ずセットさせる
              note: class="form-control datepicker-is"をtextboxに指定したらカレンダーピッカーが表示されたのでコメント化
            Session["txtSearchDayOne"] = startdate.Value;
            txtSearchDayOne.Text = startdate.Value;
            Session["txtSearchDayTwo"] = enddate.Value;
            txtSearchDayTwo.Text = enddate.Value;
            */



            /*
               ポストバック処理
             
               NOTE:
               ポストバック発生時に毎回Page_Loadが呼び出されるので、
               IsPostBackで初回のみ処理を行う
             
             */


            if (!IsPostBack)
            {
                //初回Load_Pageの時（IsPostBackがFalse）

                this.txtSearchDayOne.Text = string.Empty;
                this.txtSearchDayTwo.Text = string.Empty;
                this.txtSearchGoods.Text = string.Empty;
                this.txtSearchItem.Text = string.Empty;


                //セッションを格納
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

                //NOTE: init(); 下の処理へ移した。
                SetGridView1FromDataSet();
            }
            else
            {
                //ポストバック発生時に行いたい処理を記述

                //init();

                //** todo テスト **
                ReceiptInterface record = new ReceiptAllRecord();
                record.getTblRecipt();
                //** todo **
                
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
        /// <remarks>
        /// [概要]
        /// *テーブルより検索した結果をGridViewにバインドする
        /// </remarks>
        protected void btnSearchDay_OnClick(object sender, EventArgs e)
        {
            /*

               GridView1にDataSetを反映
            
             */
            
            SetGridView1FromDataSet();
        }
        #endregion

        #region btnReturn_Click
        /// <summary>
        /// 戻るボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// [概要]
        /// *画面遷移する
        /// </remarks>
        protected void btnReturn_Click(object sender, EventArgs e)
        {

            /*

               メニュー画面へ遷移
            
             */

            Response.Redirect("ViewMenu.aspx");

        }
        #endregion

        #region btnClose_Click
        /// <summary>
        /// 閉じるボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// [概要]
        /// *Webフォームを閉じるための関数は.NET Frameworkには無い。
        ///  なので、javascriptで行う方法を使う。
        /// </remarks>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            /*

               JavaScriptの登録。Windowを閉じる
            
             */
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

            //NOTE: 以下の方法で選択した行を取得することも出来る。
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
        /// GridView1に値をセットする
        /// </summary>
        /// <remarks>
        /// TODO 検索方法の組み合わせをもっと分かりやすいロジックに修正する
        ///       見やすく。分かりやすく。修正しやすく。
        ///       ⇒2017/04/16 追記：Modelパッケージにinterfaceを使った試作のReceiptクラスを作成
        ///         これをもとに、if文を使った場合わけから、interfaceを使った場合わけに変えてみる
        ///  *-検索ロジック
        ///  __１．開始日と終了日の範囲のみで検索
        ///  __２．開始日と終了日の範囲と品目名で検索
        ///  __３．開始日と終了日の範囲と商品で検索
        ///  __４．開始日と終了日の範囲と品目名＆商品で検索
        ///  __５．品目名のみで検索
        ///  __６．商品のみで検索
        ///  __７．開始日のみで検索（開始日の年月のみで検索）
        /// </remarks>
        private void SetGridView1FromDataSet() 
        {

            /*

               開始日と終了日の日付範囲を確認
            
             */
            if (txtSearchDayOne.Text != string.Empty & txtSearchDayTwo.Text != string.Empty)
            {
                if (DateTime.Parse(txtSearchDayOne.Text) > DateTime.Parse(txtSearchDayTwo.Text))
                {
                    //終了日を開始日と同じに変える
                    txtSearchDayTwo.Text = txtSearchDayOne.Text;
                }
            }


            /* 
                検索項目を参照して値の有無により検索方法を選択する
             
                TODO modelクラス側に検索項目を全て渡しておいて、値の有無によっての検索はmodel側に任せる。
             */
            if( 
                ( txtSearchDayOne.Text.Length != 0 ) &
                ( txtSearchDayTwo.Text.Length != 0 ) &
                ( txtSearchItem.Text.Length   != 0 ) &
                ( txtSearchGoods.Text.Length  != 0 )
                )
            {

                // 全項目で検索
                _model = new ModelListForm();

            }else if(
                (txtSearchDayOne.Text.Length != 0) &
                (txtSearchDayTwo.Text.Length != 0) &
                (txtSearchItem.Text.Length   != 0) &
                (txtSearchGoods.Text.Length  == 0)
                )
            {

                // 開始日＆終了日＆品目名の３つで検索
                _model = new ModelListForm(
                    DateTime.Parse(txtSearchDayOne.Text),
                    DateTime.Parse(txtSearchDayTwo.Text),
                    txtSearchItem.Text,
                    true);

            }else if(
                (txtSearchDayOne.Text.Length != 0) &
                (txtSearchDayTwo.Text.Length != 0) &
                (txtSearchItem.Text.Length   != 0) &
                (txtSearchGoods.Text.Length  == 0)
                )
            {

                // 開始日＆終了日＆商品の３つで検索
                _model = new ModelListForm(
                    DateTime.Parse(txtSearchDayOne.Text),
                    DateTime.Parse(txtSearchDayTwo.Text),
                    txtSearchGoods.Text);

            }
            else if (
               (txtSearchDayOne.Text.Length != 0) &
               (txtSearchDayTwo.Text.Length != 0) &
               (txtSearchItem.Text.Length   == 0) &
               (txtSearchGoods.Text.Length  == 0)
               )
            {

                // 開始日＆終了日の２つで検索
                _model = new ModelListForm(
                    DateTime.Parse(txtSearchDayOne.Text),
                    DateTime.Parse(txtSearchDayTwo.Text));

            }
            else if (
               (txtSearchDayOne.Text.Length != 0) &
               (txtSearchDayTwo.Text.Length == 0) &
               (txtSearchItem.Text.Length   == 0) &
               (txtSearchGoods.Text.Length  == 0)
               )
            {
                // 開始日のみで検索
                _model = new ModelListForm(DateTime.Parse(txtSearchDayOne.Text));
            }
            else if (
               (txtSearchDayOne.Text.Length == 0) &
               (txtSearchDayTwo.Text.Length == 0) &
               (txtSearchItem.Text.Length   != 0) &
               (txtSearchGoods.Text.Length  != 0)
               )
            {
                // 品目名と商品のみで検索
                //todo searchResult = "TargetIsItemAndGoods";
            }
            else if (
               (txtSearchDayOne.Text.Length == 0) &
               (txtSearchDayTwo.Text.Length == 0) &
               (txtSearchItem.Text.Length   != 0) &
               (txtSearchGoods.Text.Length  == 0)
               )
            {
                // 品目名のみで検索
                //todo searchResult = "TargetIsItem";
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

        }


        /// <summary>
        /// GridView1に値をセットする際に
        /// </summary>
        private void SetGridView1FromDataSet_Graffiti01()
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

            // TODO テスト用 +++++++++++++++
            //for (int i = 0; i < 100000; i++) 
            //{
            //    System.Data.DataRow dr = _model.dsKakeibo.tblReceipt.NewRow();
            //    //dr = _model.dsKakeibo.tblReceipt.Rows[600];
            //    dr[0] = System.DateTime.Now;
            //    dr[1] = 1;
            //    dr[2] = "交際費";
            //    dr[3] = "松や";
            //    dr[4] = 500;
            //    dr[5] = _model.dsKakeibo.tblReceipt.Rows.Count + i;
            //    dr[6] = false;
            //    dr[7] = 0;

            //    _model.dsKakeibo.tblReceipt.Rows.Add(dr);
            //}
            // +++++++++++++++

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

        #region SetDataTable
        /// <summary>
        /// 型無しデータセットの作成見本
        /// </summary>
        /// <returns>
        /// ネットで調べたが、型有りデータセットを手動で作るのは大変らしい。
        /// なので、形無しデータセットのみを残す。
        /// </returns>
        private System.Data.DataTable SetDataTable() 
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("A", Type.GetType("System.String"));
            dt.Columns.Add("B", Type.GetType("System.Int32"));
            dt.Columns.Add("C", Type.GetType("System.DateTime"));

            
            // 4行追加します。
            for (int i = 0; i < 4; i++)
            {
                System.Data.DataRow dr = dt.NewRow();
                dr["A"] = "文字列を格納します。";
                dr["B"] = i;
                dr["C"] = DateTime.Now;
                dt.Rows.Add(dr);
            }

            // DataSetにdtを追加します。
            ds.Tables.Add(dt);
           
            dt.TableName = "Table1";
            
            return dt;
        }
        #endregion

        #endregion

    }
}