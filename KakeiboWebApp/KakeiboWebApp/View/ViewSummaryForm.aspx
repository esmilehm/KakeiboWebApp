<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewSummaryForm.aspx.cs" Inherits="KakeiboWebApp.View.ViewSummaryForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <!-- IE互換モードを無効 -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- レスポンシブWebデザイン適用 -->
    <meta name="viewport" content="width=device-width, intial-scale=1.0">
    <!--[if lt IE 9]>
    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <link rel="stylesheet" href="../Style/styleCommon.css"/>
    <%-- Debug用    <link rel="stylesheet" href="../Style/styleDebugColumn.css"/> --%>
    <link rel="stylesheet" href="../css/bootstrap.min.css"/>
    <link rel="stylesheet" href="../css/bootstrap-datepicker.min.css">
    <title>家計簿アプリ</title>
</head>

<body>

  <form id="form1" runat="server">

    <%-- ヘッダー --%>
    <div class="well well-lg">
      <asp:Label ID="lblSearchYM" runat="server" Text="検索年月"></asp:Label>
      <asp:TextBox ID="txtSearchYM" runat="server"></asp:TextBox>
      <asp:Button ID="Button1"  class="btn btn-default" runat="server" Text="検索" onclick="btnSearch_Click" />
    </div>

    <%-- コンテナ・コンテンツ --%>
    <div id="contents"  class="col-lg-10 col-sm-12">
      <h1>集計一覧</h1>
        <%-- 【集計テーブル】 --%>
      <asp:Table ID="tbl1" class=" table table-bordered table-striped table-hover" runat="server">
      </asp:Table>
    </div>

    <div class="col-lg-10 col-sm-12">
    <asp:Button ID="btnReturn" runat="server" class="btn btn-primary btn-block" onclick="btnReturn_Click" Text="戻る"/>
    </div>

<%-- jQuery & Bootstrap --%>
<script src="../js/jquery-1.11.3.min.js" type="text/javascript"></script>
<script src="../js/bootstrap.min.js" type="text/javascript"></script>
<script src="../js/bootstrap-datepicker.min.js" type="text/javascript"></script>
<script src="../js/bootstrap-datepicker.ja.min.js" type="text/javascript"></script>

  </form>

</body>
</html>
