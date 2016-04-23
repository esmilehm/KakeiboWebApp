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
    /// <remarks>
    /// todo: 処理の実装。モデルクラスの作成
    /// todo: DBからデータを取得して、jsonpデータのファイルを作成するを作成する
    /// todo: javascriptを利用してテーブルを作成する
    /// </remarks>
    public partial class ViewSummaryForm : AbstractView
    {
        #region １．プライベート変数宣言
        /// <summary>
        /// クラス名
        /// </summary>
        private const string THIS_CLASS_NAME = "ViewSummaryForm";
        /// <summary>
        /// モデルクラス・ViewSummaryForm受け渡し用
        /// </summary>
        private ModelSummaryForm _model = new ModelSummaryForm();
        #endregion

        #region ２．コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <example>基底クラスでログ出力するためにコンストラクタを呼出し</example>
        /// <remarks></remarks>
        public ViewSummaryForm() : base(THIS_CLASS_NAME) 
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

            //--- ポストバック処理 ---
            // ポストバック発生時に毎回Page_Loadが呼び出されるので、
            // IsPostBackで初回のみ処理を行う
            if (!IsPostBack)
            {
                //--- 初回のLoad_Pageのみ処理（IsPostBackがFalse）
                string yearNow = DateTime.Now.ToString("yyyy");
                string monthNow = DateTime.Now.ToString("MM");
                _model = new ModelSummaryForm(yearNow + monthNow);
                init();

            }
            else
            {
                //--- ポストバック発生時に行いたい処理を記述
                if (txtSearchYM.Text == string.Empty) 
                {
                    txtSearchYM.Text = DateTime.Now.ToString("yyyyMM");
                }
                _model = new ModelSummaryForm(txtSearchYM.Text);
                init();
            }
        }
        #endregion

        #region ３．イベント

        #region btnSearch_Click
        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //--- ポストバック発生時に行いたい処理を記述
            //_model = new ModelSummaryForm(txtSearchYM.Text);
            //init();

            // NOTE:詳細ボタンのイベントを登録するため、Page_Loadへ移動。
            //      動的にイベントを登録するときは、OnInitかPage_Loadで行わないと予期しない動きになるらしい。MSDNより。
        }
        #endregion

        #region btnDetail_Click
        /// <summary>
        /// 詳細ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            // 日付の組み立て
            //DateTime dt = DateTime.Parse(this.txtSearchYM.Text + 1);
            int year = int.Parse( this.txtSearchYM.Text.Substring(0,4));
            int month = int.Parse(this.txtSearchYM.Text.Substring(4));

            // 今月の最初の日
            DateTime dtFDM = new DateTime(year,month , 1);

            // 今月の最後の日
            DateTime dtLDM = new DateTime(dtFDM.Year, dtFDM.Month,
                DateTime.DaysInMonth(dtFDM.Year, dtFDM.Month));

            // クリックされた行を取得
            string index = ((Button)sender).ID.Substring(11);    // "tCellBtn_B_xx"からインデックスのみ抽出
            string item = ((TableCell)tbl1.FindControl("tCellITEM_" + index)).Text;            

            Session["txtSearchDayOne"] = dtFDM;
            Session["txtSearchDayTwo"] = dtLDM;
            Session["txtSearchItem"] = item;
            Session["txtSearchGoods"] = "";
            Response.Redirect("ViewListForm.aspx");
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

        #endregion

        #region ４．プライベートメソッド

        #region init
        /// <summary>
        /// 初期処理
        /// </summary>
        private void init()
        {
            // 集計表の作成
            makeTable(_model);
        }
        #endregion

        #region makeTable
        /// <summary>
        /// makeTable
        /// </summary>
        /// <remarks>
        /// UIWebControlsのTable集計表を作成
        /// </remarks>
        private void makeTable(ModelSummaryForm model)
        {
            // Total number of rows.
            int rowCnt;
            // Current row count.
            int rowCtr;

            rowCnt = _model.dsKakeibo.tblReceiptSumYearMonth.Count;
            //cellCnt = 4;

            /* ヘッダー部の設定*/
            TableRow tHeaderRow = new TableRow();
            tHeaderRow.TableSection = TableRowSection.TableHeader;
            tbl1.Rows.Add(tHeaderRow);

            TableHeaderCell tHeaderCellYM = new TableHeaderCell();
            tHeaderRow.Cells.Add(tHeaderCellYM);
            tHeaderCellYM.Text = "年月";

            TableHeaderCell tHeaderCellITEM = new TableHeaderCell();
            tHeaderRow.Cells.Add(tHeaderCellITEM);
            tHeaderCellITEM.Text = "科目";

            TableHeaderCell tHeaderCellPRICE = new TableHeaderCell();
            tHeaderRow.Cells.Add(tHeaderCellPRICE);
            tHeaderCellPRICE.Text = "金額";

            TableHeaderCell tHeaderCellMEMO = new TableHeaderCell();
            tHeaderRow.Cells.Add(tHeaderCellMEMO);
            tHeaderCellMEMO.Text = "メモ";


            /* データ部の設定*/
            int sumPrice = 0;
            for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
            {
                // Create a new row and add it to the table.
                TableRow tRow = new TableRow();
                tbl1.Rows.Add(tRow);

                // Create a new cell and add it to the row.
                TableCell tCellYM = new TableCell();
                tCellYM.ID = "tCellYM_" + rowCtr;
                tRow.Cells.Add(tCellYM);
                tCellYM.Text = model.dsKakeibo.tblReceiptSumYearMonth[rowCtr-1]["YM"].ToString();

                TableCell tCellITEM = new TableCell();
                tCellITEM.ID = "tCellITEM_" + rowCtr;
                tRow.Cells.Add(tCellITEM);
                tCellITEM.Text = model.dsKakeibo.tblReceiptSumYearMonth[rowCtr - 1]["ITEM"].ToString();

                TableCell tCellPRICE = new TableCell();
                tCellPRICE.ID = "tCellPRICE_" + rowCtr;
                tRow.Cells.Add(tCellPRICE);
                tCellPRICE.HorizontalAlign = HorizontalAlign.Right;
                int price = int.Parse(model.dsKakeibo.tblReceiptSumYearMonth[rowCtr - 1]["PRICE"].ToString());
                tCellPRICE.Text = string.Format("{0:#,0}", price) + "円";
                sumPrice = sumPrice + price;

                //TableCell tCellMEMO = new TableCell();
                //tCellMEMO.ID = "tCellMEMO_" + rowCtr;
                //tRow.Cells.Add(tCellMEMO);
                //System.Web.UI.WebControls.HyperLink h = new HyperLink();
                //h.Text = "詳細";
                //h.Target = "_blank";
                //h.NavigateUrl = "ViewDetailForm.aspx";
                //tCellMEMO.Controls.Add(h);

                TableCell tCellBtn = new TableCell();
                tCellBtn.ID = "tCellBtn_" + rowCtr;
                tRow.Cells.Add(tCellBtn);
                System.Web.UI.WebControls.Button b = new Button();
                b.Text = "詳細";
                b.ID = "tCellBtn_B_" + rowCtr;
                //b.Click += new EventHandler(this.btnDetail_Click);      //TODO 初回表示ではイベントが紐付かないのかクリックしてもイベント発生しない。
                
                tCellBtn.Controls.Add(b);
                b.Click += new EventHandler(this.btnDetail_Click);

            }

            /* フッター部 */
            TableRow tFooterRow = new TableRow();
            tFooterRow.TableSection = TableRowSection.TableFooter;
            tbl1.Rows.Add(tFooterRow);

            TableCell tFooterCellYM = new TableHeaderCell();
            tFooterRow.Cells.Add(tFooterCellYM);
            tFooterCellYM.Text = "-";

            TableCell tFooterCellITEM = new TableHeaderCell();
            tFooterRow.Cells.Add(tFooterCellITEM);
            tFooterCellITEM.Text = "合計";

            TableCell tFooterCellPRICE = new TableHeaderCell();
            tFooterRow.Cells.Add(tFooterCellPRICE);
            tFooterCellPRICE.HorizontalAlign = HorizontalAlign.Left;
            tFooterCellPRICE.Text = string.Format("{0:#,0}", sumPrice) + "円";

            TableCell tFooterCellMEMO = new TableHeaderCell();
            tFooterRow.Cells.Add(tFooterCellMEMO);
            tFooterCellMEMO.Text = "-";

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
            _model = new ModelSummaryForm();

        }
        #endregion

        #endregion

    }
}