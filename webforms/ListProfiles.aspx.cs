using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class ListProfiles : System.Web.UI.Page
{
    private string CS => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindGrid();
    }

    private void BindGrid()
    {
        using (var con = new SqlConnection(CS))
        using (var cmd = new SqlCommand("dbo.sp_Profiles_List", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            var dt = new DataTable();
            using (var rd = cmd.ExecuteReader()) dt.Load(rd);

            gvProfiles.DataSource = dt;
            gvProfiles.DataBind();

            pnlEmpty.Visible = (dt.Rows.Count == 0);
        }
    }

    protected void gvProfiles_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvProfiles.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void gvProfiles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (string.IsNullOrEmpty(e.CommandArgument?.ToString())) return;
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "delRow")
        {
            using (var con = new SqlConnection(CS))
            using (var cmd = new SqlCommand("dbo.sp_Profiles_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            lblMsg.Text = "Profile deleted.";
            BindGrid();
        }
        else if (e.CommandName == "editRow")
        {
            Response.Redirect("EditProfile.aspx?id=" + id);
        }
    }
}
