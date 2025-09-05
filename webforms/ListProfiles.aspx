<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListProfiles.aspx.cs" Inherits="ListProfiles" %>
<!DOCTYPE html>
<html>
<head runat="server">
  <title>Profiles List</title>
  <style>
    body{font-family:Segoe UI,Arial}
    .wrap{max-width:1000px;margin:60px auto}
    h2{margin:10px 0 16px}
    .msg{margin-top:10px;color:#16a34a}
    .empty{padding:20px;background:#fafafa;border:1px dashed #ddd;border-radius:10px;text-align:center}
    .btn{padding:6px 12px;border:none;border-radius:6px;cursor:pointer;color:#fff}
    .btn-edit{background:#16a34a}
    .btn-del{background:#dc2626}
  </style>
</head>
<body>
<form id="form1" runat="server">
  <div class="wrap">
    <h2>All Profiles</h2>

    <asp:GridView ID="gvProfiles" runat="server"
                  AutoGenerateColumns="False"
                  DataKeyNames="Id"
                  AllowPaging="true" PageSize="10"
                  OnPageIndexChanging="gvProfiles_PageIndexChanging"
                  OnRowCommand="gvProfiles_RowCommand">
      <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Bio" HeaderText="Bio" />
        <asp:BoundField DataField="CreatedAt" HeaderText="Created" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
        <asp:TemplateField HeaderText="Actions">
          <ItemTemplate>
            <asp:LinkButton ID="btnEdit" runat="server" CommandName="editRow"
              CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-edit">Edit</asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="btnDel" runat="server" CommandName="delRow"
              CommandArgument='<%# Eval("Id") %>'
              OnClientClick="return confirm('Delete this profile?');"
              CssClass="btn btn-del">Delete</asp:LinkButton>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
      <PagerSettings Mode="Numeric" Position="Bottom" />
    </asp:GridView>

    <asp:Panel ID="pnlEmpty" runat="server" Visible="false" CssClass="empty">
      No profiles found.
    </asp:Panel>

    <asp:Label ID="lblMsg" runat="server" CssClass="msg"></asp:Label>
  </div>
</form>
</body>
</html>
