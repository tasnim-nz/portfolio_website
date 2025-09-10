using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_portfolio1
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string email = Session["profile_email"] as string;
                if (string.IsNullOrWhiteSpace(email))
                    email = Request.QueryString["email"];

                if (!string.IsNullOrWhiteSpace(email))
                    LoadProfile(email);
                else
                {
                    lblName.Text = "Nuzhat Tasnim";
                    lblEmail.Text = "nuzhat@gmail.com";
                    lblBio.Text = "I am a passionate web developer.I love to learn and  build web design";
                }
            }
        }

        private void LoadProfile(string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = "SELECT TOP 1 Name, Email, Bio FROM Profiles WHERE Email=@E";

            using (var con = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@E", email);
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        lblName.Text = rdr["Name"].ToString();
                        lblEmail.Text = rdr["Email"].ToString();
                        lblBio.Text = rdr["Bio"].ToString();
                    }
                    else
                    {
                        lblName.Text = "Not found";
                        lblEmail.Text = email;
                        lblBio.Text = "";
                    }
                }
            }
        }
    }
}

