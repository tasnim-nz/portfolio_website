<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="web_portfolio.EditProfile" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Edit Profile</title>
  <meta charset="utf-8" />
  <style>
    body { font-family: Arial; margin: 40px; }
    .card { max-width: 600px; margin: 0 auto; padding: 24px; border:1px solid #ddd; border-radius:10px; }
    .row { margin:12px 0; display:flex; gap:12px; align-items:center; }
    label { width: 90px; color:#444; }
    input[type=text], textarea { flex:1; padding:8px; border:1px solid #ccc; border-radius:6px; }
    textarea { height: 100px; }
    .actions { margin-top:18px; display:flex; gap:10px; }
    .btn { padding:8px 14px; border:1px solid #1976d2; border-radius:6px; background:#1976d2; color:#fff; }
    .btn.secondary { background:#fff; color:#1976d2; }
    .error { color:#c62828; font-size: .9rem; }
    .msg { display:block; margin-top:15px; font-weight:bold; }
  </style>
</head>
<body>
<form id="form1" runat="server">
  <div class="card">
    <h2>Edit Profile</h2>

    <div class="row">
      <label for="txtName">Name</label>
      <asp:TextBox ID="txtName" runat="server" />
    </div>
    <asp:RequiredFieldValidator ID="rfvName" runat="server"
      ControlToValidate="txtName" ErrorMessage="Name is required"
      CssClass="error" Display="Dynamic" />

    <div class="row">
      <label for="txtEmail">Email</label>
      <asp:TextBox ID="txtEmail" runat="server" />
    </div>
    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
      ControlToValidate="txtEmail" ErrorMessage="Email is required"
      CssClass="error" Display="Dynamic" />
    <asp:RegularExpressionValidator ID="revEmail" runat="server"
      ControlToValidate="txtEmail"
      ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
      ErrorMessage="Invalid email format" CssClass="error" Display="Dynamic" />

    <div class="row">
      <label for="txtBio">Bio</label>
      <asp:TextBox ID="txtBio" runat="server" TextMode="MultiLine" />
    </div>

    <div class="actions">
      <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" />
      <a class="btn secondary" href="Profile.aspx">Cancel</a>
    </div>

    <!-- ✅ Save/DB operation এর মেসেজ দেখানোর জন্য Label -->
    <asp:Label ID="lblMsg" runat="server" CssClass="msg" ForeColor="Green"></asp:Label>
  </div>
</form>
</body>
</html>
