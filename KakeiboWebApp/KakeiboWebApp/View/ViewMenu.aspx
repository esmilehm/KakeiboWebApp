<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ViewMenu.aspx.cs" Inherits="KakeiboWebApp.View.ViewMenu" %>
<%-- イベントハンドら自動接続機能：AutoEventWireupはOnにする --%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="ja">
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
    <%-- 削除中    <link rel="stylesheet" href="../Style/styleTwoColumn.css"/> --%>
    <%-- Debug用    <link rel="stylesheet" href="../Style/styleDebugColumn.css"/> --%>
    <link rel="stylesheet" href="../css/bootstrap.min.css"/>
    <title>家計簿アプリ</title>
</head>
<body>
<div id="wrap"  class="container">
<form id="form1" runat="server">

<%-- ヘッダー --%>
<header>
<div class="jumbotron">
<h1><span class="glyphicon glyphicon-book">家計簿アプリ</span></h1>
</div>
<div class="message"><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></div>
</header>

<%-- 【サイドバーとコンテンツを囲むdiv要素】 --%>
<div class="row">
<%-- サイドバー --%>
<div class="col-xs-4">
<ul class="nav nav-pills nav-stacked">
<li class="active"><a href="#">TOP</a></li>
<li><a href="ViewListForm.aspx">明細表</a></li>
<li><a href="ViewSummaryForm.aspx">集計表</a></li>
<li><a href="#">etc.</a></li>
</ul>
</div>
<%-- コンテンツ --%>
<div class="col-xs-8">
サイドメニューで選んだ項目の詳細メニューを表示させる予定Topを選んだ場合は、更新情報を表示させたい。
  <div class="panel panel-default">
    <div class="panel panel-heading"><h1 class="panel-title">今月の食費（円）</h1></div>
    <div class="panel panel-body"><asp:Label ID="lblSumOfFood" runat="server" Text=""></asp:Label></div>
    <div class="panel panel-heading"><h1 class="panel-title">今月の交際費（円）</h1></div>
    <div class="panel panel-body"><asp:Label ID="lblSumOfEntertainment" runat="server" Text=""></asp:Label></div>
    <div class="panel panel-heading"><h1 class="panel-title">今月の娯楽費（円）</h1></div>
    <div class="panel panel-body"><asp:Label ID="lblSumOfRecreation" runat="server" Text=""></asp:Label></div>
    <div class="panel panel-heading"><h1 class="panel-title">今月の交通費（円）</h1></div>
    <div class="panel panel-body"><asp:Label ID="lblSumOfTrafficCost" runat="server" Text=""></asp:Label></div>
  </div>

</div>

</div><%-- row用の終了div --%>

<%-- フッター --%>
<footer>
<h2>フッター</h2>
</footer>

</form>
</div><%-- wrap用の終了div --%>>
<%-- jQuery & Bootstrap --%>
<script src="../js/jquery-1.11.3.min.js"  type="text/javascript"></script>
<script src="../js/bootstrap.min.js"  type="text/javascript"></script>
</body>
</html>
