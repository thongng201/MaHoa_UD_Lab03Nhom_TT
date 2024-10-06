using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class LoginForm : Form
    {

       SqlConnection sqlcon = null;

        public LoginForm()
        {
            InitializeComponent();
        }


        private void btnDN_Click(object sender, EventArgs e)
        {
            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=QLSVNhom;Integrated Security=True;Encrypt=False;");
            }

            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Hash the password using SHA1
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                passwordBytes = sha1.ComputeHash(passwordBytes); // Hash the password
            }

            // Use a parameterized query to prevent SQL injection attacks
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "SELECT * FROM NHANVIEN WHERE TENDN = @username AND MATKHAU = @password";
            sqlcmd.Parameters.AddWithValue("@username", username);
            sqlcmd.Parameters.AddWithValue("@password", passwordBytes); // Use the hashed binary value

            sqlcmd.Connection = sqlcon;

            SqlDataReader data = sqlcmd.ExecuteReader();

            if (data.Read())
            {
                MessageBox.Show("Đăng nhập thành công");
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }

            // Đóng Reader
            data.Close();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Thoát khỏi ứng dụng
            this.Close();
        }
    }
}
