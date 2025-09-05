using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace web_portfolio
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtName.Text = Session["profile_name"] as string ?? "Your Name";
                txtEmail.Text = Session["profile_email"] as string ?? "you@example.com";
                txtBio.Text = Session["profile_bio"] as string ?? "Write a short bio about yourself.";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string bio = txtBio.Text.Trim();

            // web.config থেকে কানেকশন স্ট্রিং পড়া
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    // প্রথমে চেক করব Email আছে কি না
                    string checkSql = "SELECT COUNT(*) FROM Profiles WHERE Email=@E";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.AddWithValue("@E", email);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            // INSERT
                            string insertSql = "INSERT INTO Profiles (Name, Email, Bio) VALUES (@N,@E,@B)";
                            using (SqlCommand insertCmd = new SqlCommand(insertSql, con))
                            {
                                insertCmd.Parameters.AddWithValue("@N", name);
                                insertCmd.Parameters.AddWithValue("@E", email);
                                insertCmd.Parameters.AddWithValue("@B", string.IsNullOrWhiteSpace(bio) ? (object)DBNull.Value : bio);
                                insertCmd.ExecuteNonQuery();
                            }
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            lblMsg.Text = "✅ Profile saved successfully!";
                        }
                        else
                        {
                            // UPDATE
                            string updateSql = "UPDATE Profiles SET Name=@N, Bio=@B WHERE Email=@E";
                            using (SqlCommand updateCmd = new SqlCommand(updateSql, con))
                            {
                                updateCmd.Parameters.AddWithValue("@N", name);
                                updateCmd.Parameters.AddWithValue("@B", string.IsNullOrWhiteSpace(bio) ? (object)DBNull.Value : bio);
                                updateCmd.Parameters.AddWithValue("@E", email);
                                updateCmd.ExecuteNonQuery();
                            }
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            lblMsg.Text = "🔄 Profile updated successfully!";
                        }
                    }
                }

                // সেশনেও রাখছি যাতে Profile.aspx এ সাথে সাথে দেখা যায়
                Session["profile_name"] = name;
                Session["profile_email"] = email;
                Session["profile_bio"] = bio;

                // চাইলে সরাসরি রিডাইরেক্ট করতে পারো
                // Response.Redirect("Profile.aspx?email=" + Server.UrlEncode(email));
            }
            catch (SqlException ex)
            {
                lblMsg.ForeColor = System.Drawing.Color.Maroon;
                if (ex.Number == 2627 || ex.Number == 2601) // unique constraint
                    lblMsg.Text = "⚠️ This email already exists!";
                else
                    lblMsg.Text = "❌ Database error: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblMsg.ForeColor = System.Drawing.Color.Maroon;
                lblMsg.Text = "❌ Error: " + ex.Message;
            }
        }
    }
}
