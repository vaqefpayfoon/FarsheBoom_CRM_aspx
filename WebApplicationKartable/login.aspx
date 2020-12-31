<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplicationKartable.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="UTF-8" />

  <title>فرش بوم</title>

    <link rel="stylesheet" href="css/style.css" media="screen" type="text/css" />
    <style>
        .textstyle
        {opacity: 1; background-color: rgb(255, 255, 255); background-position: initial initial; background-repeat: initial initial;}
    </style>

</head>
<body>
    <form id="form1" runat="server">
<script src="js/JavaScript1.js"></script>
<div id="logmsk" style="display: block;">
    <div id="userbox">
        <h1 id="signup" style="background-color: rgb(118, 171, 219); background-position: initial initial; background-repeat: initial initial;font-family:FarsiWebFont">صفحه ورود</h1>
        <asp:Label ID="lblError" runat="server" BackColor="Navy" ForeColor="Yellow"></asp:Label>
        <div id="sumsk" style="display: none;">Sending</div>     
        <asp:TextBox ID="name" runat="server" CssClass="textstyle"></asp:TextBox>
        <asp:TextBox ID="pass" runat="server" CssClass="textstyle" TextMode="Password"></asp:TextBox>
        <asp:Button ID="signupb" runat="server" OnClick="btn_enter_Click" Text="ورود" />
    </div>
</div>

  <script src="js/index.js"></script>
    </form>
</body>
</html>

