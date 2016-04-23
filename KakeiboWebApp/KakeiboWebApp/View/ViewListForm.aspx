<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewListForm.aspx.cs" Inherits="KakeiboWebApp.View.ViewListForm" %>

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
  <link rel="stylesheet" href="../Style/styleCommon.css" />
  <%-- 削除中    <link rel="stylesheet" href="../Style/styleOneColumn.css"/> --%>
  <%-- Debug用    <link rel="stylesheet" href="../Style/styleDebugColumn.css"/> --%>
  <link rel="stylesheet" href="../css/bootstrap.min.css" />
  <link rel="stylesheet" href="../css/bootstrap-datepicker.min.css">
  <title>家計簿アプリ</title>

<%-- GridView Pagerスタイル --%>
<style type="text/css">
<!--
.PagerStyle span
{
text-decoration: none;
color:Blue;
padding-left: 4px;
padding-right: 4px;
}
.PagerStyle a
{
text-decoration: none;
padding-left: 4px;
padding-right: 4px;
}
.PagerStyle a:hover
{
color:White;
background-color:Silver;
padding-left: 4px;
padding-right: 4px; 
}
-->
</style>
</head>

<body>

<div id="wrap" class="container">
<form id="form1" runat="server">
<%-- ヘッダー --%>
<header>
<h1>明細一覧</h1>

<div class="btn-group">
  <asp:Button ID="btnReturn" runat="server" class="btn btn-primary btn-lg" onclick="btnReturn_Click" Text="戻る" />
  <asp:Button ID="btnClose" runat="server" class="btn btn-primary btn-lg" onclick="btnClose_Click" Text="閉じる" />
  <asp:Button ID="btnAdd"   runat="server" class="btn btn-primary btn-lg" onclick="btnAdd_Click"   Text="新規登録" />
</div>

<%-- 検索日＆商品--%>
<div class="btn btn-primary  btn-lg"><a data-toggle="collapse" href="#closepanel2" style="color:White; text-decoration:none;">検索画面</a></div>
<%-- 検索画面のアコーディオン --%>
  <div id="closepanel2" class="panel-collapse collapse">
    <div class="panel-body">
      <div class="well well-lg">
        <div class="form-group">
        <%-- 検索日--%>
        <asp:Label ID="lblSearchDayOne" runat="server" Text="開始日" Visible="true"></asp:Label>
        <asp:TextBox ID="txtSearchDayOne" runat="server" 
        ontextchanged="txtSearchDayOne_TextChanged" Visible="true" class="form-control datepicker-is"></asp:TextBox>
        <asp:Label ID="lblSearchDayTwo" runat="server" Text="終了日" Visible="true"></asp:Label>
        <asp:TextBox ID="txtSearchDayTwo" runat="server" 
        ontextchanged="txtSearchDayTwo_TextChanged" Visible="true" class="form-control datepicker-is"></asp:TextBox>
        <%-- パターン１    
        <label for="date" class="control-label">開始日</label>
        <input type="text" class="form-control datepicker-is" id="startdate" runat="server">
        <label for="date" class="control-label">終了日</label>
        <input type="text" class="form-control datepicker-is" id="enddate" runat="server">
        --%>
        <%-- 品目名 --%>
        <asp:Label ID="lblSearchItem" runat="server" Text="品目名"></asp:Label>
        <asp:TextBox ID="txtSearchItem" runat="server" ontextchanged="txtSearchItem_TextChanged"  class="form-control"></asp:TextBox>
        <%-- 商品 --%>
        <asp:Label ID="lblSearchGoods" runat="server" Text="商品名"></asp:Label>
        <asp:TextBox ID="txtSearchGoods" runat="server" ontextchanged="txtSearchGoods_TextChanged"  class="form-control"></asp:TextBox>
        <%-- ボタン --%>
        <asp:Button  runat="server" class="btn btn-default" Text="検索" OnClick="btnSearchDay_OnClick"/><%-- id="btnSearchDay" --%>
        </div>
        <%-- 合計額 --%>
        <div class="form-group">
          <asp:Label ID="lblSumPriceName" runat="server" Text="合計額"></asp:Label>
          <asp:TextBox ID="txtSumPrice" runat="server"></asp:TextBox>
        </div>
      </div>
    </div>
  </div>
</header>

<%-- コンテンツ --%>
<div id="contents">
<%-- グリッドビュー --%>
  <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
  OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
  AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover">
  <AlternatingRowStyle BackColor="White" />
  <Columns>
  <asp:BoundField DataField="ID" HeaderText="No"></asp:BoundField>
  <asp:BoundField DataField="DATE" HeaderText="日付"></asp:BoundField>
  <asp:BoundField DataField="Item" HeaderText="品目名"></asp:BoundField>
  <asp:BoundField DataField="GOODS" HeaderText="商品名"></asp:BoundField>
  <asp:BoundField DataField="PRICE" HeaderText="金額"></asp:BoundField>
  </Columns>
  <PagerStyle CssClass="PagerStyle" horizontalalign="Center"/>
  </asp:GridView>
</div>
<%-- フッター --%>
<footer>
</footer>
</form>
</div>

<%-- jQuery & Bootstrap --%>
  <script src="../js/jquery-1.11.3.min.js" type="text/javascript"></script>
  <script src="../js/bootstrap.min.js" type="text/javascript"></script>
  <script src="../js/bootstrap-datepicker.min.js" type="text/javascript"></script>
  <script src="../js/bootstrap-datepicker.ja.min.js" type="text/javascript"></script>
  <script type="text/javascript">
    $('.datepicker-is').datepicker({
      format: "yyyy/mm/dd",
      language: "ja",
      autoclose: true,
      orientation: "bottom auto"
    });
  </script>
</body>
</html>
