<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="web_portfolio.Profile" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Profile</title>
    <meta charset="utf-8" />
    <style>
        body { font-family: Arial; margin: 40px; }
        .card { max-width: 600px; margin: 0 auto; padding: 24px; border:1px solid #ddd; border-radius:10px; }
        .row { margin: 10px 0; }
        .label { color:#666; }
        .val { font-weight:600; }
        a.btn { display:inline-block; padding:8px 14px; border:1px solid #1976d2; border-radius:6px; text-decoration:none; }
    </style>
</head>
<body>
<form id="form1" runat="server">
  <div class="card">
    <h2>Profile</h2>
    <div class="row"><span class="label">Name: </span>  <asp:Label ID="lblName"  runat="server" CssClass="val" /></div>
    <div class="row"><span class="label">Email: </span> <asp:Label ID="lblEmail" runat="server" CssClass="val" /></div>
    <div class="row"><span class="label">Bio: </span>   <asp:Label ID="lblBio"   runat="server" CssClass="val" /></div>
    <div class="row" style="margin-top:18px">
      <a class="btn" href="EditProfile.aspx">Edit Profile</a>
    </div>
  </div>
</form>
</body>
</html>
