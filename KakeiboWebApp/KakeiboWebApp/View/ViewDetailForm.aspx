<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDetailForm.aspx.cs" EnableSessionState="true" Inherits="KakeiboWebApp.View.ViewDetailForm" %>

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
    <%-- 削除中    <link rel="stylesheet" href="../Style/styleDetailForm.css"/> --%>
    <%-- Debug用    <link rel="stylesheet" href="../Style/styleDebugColumn.css"/> --%>
    <link rel="stylesheet" href="../css/bootstrap.min.css"/>
    <link rel="stylesheet" href="../css/bootstrap.min.css"/>
    <link rel="stylesheet" href="../css/bootstrap-datepicker.min.css">
    <title>家計簿アプリ</title>
</head>

<body>
<div id="wrap" class="container">

<form id="form1" runat="server">

<%-- ヘッダー --%>
<header>
<h1>入力画面</h1>
</header>
<%-- 【サイドバーとコンテンツを囲むdiv要素】 --%>
<div class="row">
<%-- コンテンツ --%>
<div id="contents" class="col-lg-10 col-sm-12">
<p>下記の項目を入力してください</p>
  <div class="form-group">
    <asp:Label ID="lblDate" runat="server" Text="購入日" for="title" 
      class="control-label " Visible="true"></asp:label>
    <asp:TextBox ID="txtDate" runat="server" class="form-control datepicker-is" Visible="true"></asp:TextBox>


  <div class="form-group">
    <asp:Label ID="lblGoods" runat="server" Text="商品名" for="title" class="control-label "></asp:label>
    <a data-toggle="collapse" href="#closepanel2">品目一覧</a>
    <div id="closepanel2" class="panel-collapse collapse">
    <div class="panel-body">
    <%-- 商品項目 --%>
      <div class="btn-group-vertical">
        <asp:Button ID="btnEntry1"  runat="server" class="btn btn-default" onclick="btnEntry1_Click" Text="ジュース"  />          
        <asp:Button ID="btnEntry2"  runat="server" class="btn btn-default" onclick="btnEntry2_Click" Text="お菓子パン" />
        <asp:Button ID="btnEntry3"  runat="server" class="btn btn-default" onclick="btnEntry3_Click" Text="おかずパン" />         

        <asp:Button ID="btnEntry4"  runat="server" class="btn btn-default" onclick="btnEntry4_Click" Text="缶ビール"　/>
        <asp:Button ID="btnEntry5"  runat="server" class="btn btn-default" onclick="btnEntry5_Click" Text="お惣菜"  />
        <asp:Button ID="Button12"   runat="server" class="btn btn-default" onclick="btnEntry12_Click" Text="おにぎり" />

        <asp:Button ID="Button11"   runat="server" class="btn btn-default" onclick="btnEntry11_Click" Text="漫画喫茶"  />
        <asp:Button ID="btnEntry6"  runat="server" class="btn btn-default" onclick="btnEntry6_Click" Text="ガソリン代" />
        <asp:Button ID="Button10"   runat="server" class="btn btn-default" onclick="btnEntry10_Click" Text="洗車代" /> 

        <asp:Button ID="btnEntry7"  runat="server" class="btn btn-default" onclick="btnEntry7_Click" Text="週刊少年ジャンプ" />
        <asp:Button ID="btnEntry8"  runat="server" class="btn btn-default" onclick="btnEntry8_Click" Text="週刊少年マガジン" />
        <asp:Button ID="btnEntry9"  runat="server" class="btn btn-default" onclick="btnEntry9_Click" Text="週刊少年サンデー" />

        <asp:Button ID="btnEntry13" runat="server" class="btn btn-default" onclick="btnEntry13_Click" Text="床屋" />
        <asp:Button ID="btnEntry14" runat="server" class="btn btn-default" onclick="btnEntry14_Click" Text="家賃" />
      </div>
    </div>
  </div>
  <asp:TextBox ID="txtGoods" runat="server" class="form-control"></asp:TextBox>
  </div>
  <div class="form-group">
    <asp:Label ID="lblPrice" runat="server" Text="値段" for="title" class="control-label "></asp:label>
    <asp:TextBox ID="txtPrice" runat="server" class="form-control"></asp:TextBox>
  </div>
  <div class="form-group">
    <asp:Label ID="lblItemID" runat="server" Text="品目ID" for="title" class="control-label "></asp:label>
    <asp:DropDownList ID="ddlItem" runat="server"  onselectedindexchanged="ddlItem_SelectedIndexChanged"></asp:DropDownList>
  </div>
  <div class="form-group">
    <asp:TextBox ID="txtItemID" runat="server" class="form-control"></asp:TextBox>
  </div>
  <div class="form-group">
    <asp:Label ID="lblItem" runat="server" Text="品目名" for="title" class="control-label "></asp:label>
    <asp:TextBox ID="txtItem" runat="server" class="form-control"></asp:TextBox>
  </div>
  <div class="form-group">
    <asp:Label ID="lblItemDetailsID" runat="server" Text="品目詳細ID" for="title" class="control-label "></asp:label>
    <asp:TextBox ID="txtItemDetailsID" runat="server" class="form-control"></asp:TextBox>
  </div>
  <div class="form-group">
    <asp:Label ID="lblItemDetails" runat="server" Text="品目詳細名" for="title" class="control-label "></asp:label>
    <asp:TextBox ID="txtItemDetails" runat="server" class="form-control"></asp:TextBox>
  </div>

<p>
<asp:Button ID="btnDelete" runat="server" class="btn btn-primary btn-lg" onclick="btnDelete_Click" Text="削除"/>
<asp:Button ID="btnUpdate" runat="server" class="btn btn-primary btn-lg" onclick="btnUpdate_Click" Text="更新"/> 
<asp:Button ID="btnAdd" runat="server" class="btn btn-primary btn-lg" onclick="btnAdd_Click" Text="登録"/>
</p>
<asp:Button ID="btnReturn" runat="server" class="btn btn-primary btn-block" onclick="btnReturn_Click" Text="戻る"/>
<asp:Button ID="btnClose"  runat="server" class="btn btn-default btn-block" onclick="btnClose_Click"  Text="閉じる"/>
</div>
<%-- フッター --%>
<footer>    
</footer>
</div>
</div>
</form>
</div>
<%-- jQuery & Bootstrap --%>
<script src="../js/jquery-1.11.3.min.js"  type="text/javascript"></script>
<script src="../js/bootstrap.min.js"  type="text/javascript"></script>
<script src="../js/bootstrap-datepicker.min.js"  type="text/javascript"></script>
<script src="../js/bootstrap-datepicker.ja.min.js"  type="text/javascript"></script>
<script  type="text/javascript">
  $('.datepicker-is').datepicker({
    format: "yyyy/mm/dd",
    language: "ja",
    autoclose: true,
    orientation: "bottom auto"
  });
</script>
</body>
</html>
