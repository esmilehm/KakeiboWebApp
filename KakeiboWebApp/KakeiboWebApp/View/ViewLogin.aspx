<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="ViewLogin.aspx.cs" EnableSessionState="true" Inherits="KakeiboWebApp.View.ViewLogin" %>

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
    <%-- 削除中 <link rel="stylesheet" href="../Style/styleLoginForm.css"/> --%>
    <%-- Debug用    <link rel="stylesheet" href="../Style/styleDebugColumn.css"/> --%>
    <link rel="stylesheet" href="../css/bootstrap.min.css"/>
    <title>ログイン画面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <div class="panel panel-primary verticalcenter">
      <div class="panel-heading">
        <h1 class="panel-title">ログイン</h1>
      </div>
      <div class="panel-body">
        <form>
          <div class="form-group">
            <label for="username">ログイン名</label>
            <input type="text" class="form-control" id="username" />
          </div>
          <div class"form-group">
            <label for="password">パスワード</label>
            <input type="password" class="form-control" id="password" />
          </div>
          <button  type="submit" class="btn btn-default">ログイン</button>
          <asp:Button ID="btnLogin" runat="server" class="btn btn-default" onclick="btnLogin_Click" Text="ログイン"/>
        </form>
      </div>
    </div>
    </div>
    </form>
<%-- jQuery & Bootstrap --%>
<script src="../js/jquery-1.11.3.min.js"  type="text/javascript"></script>
<script src="../js/bootstrap.min.js"  type="text/javascript"></script>
</body>
</html>
