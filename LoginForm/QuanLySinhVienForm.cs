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
    public partial class QuanLySinhVienForm : Form
    {
        string constr = ConnectString.GetConnectionString(); // Đảm bảo kết nối đúng tới QLSVNhom

        public QuanLySinhVienForm()
        {
            InitializeComponent();
            Load_DB_toGridView();  // Tự động tải dữ liệu khi form được khởi động
        }

        // Phương thức tải dữ liệu từ DB lên DataGridView
        void Load_DB_toGridView()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT MASV, HOTEN, NGAYSINH, DIACHI, MALOP, TENDN, MATKHAU FROM SINHVIEN", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);

                        // Tạo cột mới chỉ để hiển thị chuỗi placeholder
                        tb.Columns.Add("MATKHAU_HIDDEN", typeof(string));

                        foreach (DataRow row in tb.Rows)
                        {
                            // Đặt giá trị "****" hoặc "HIDDEN" cho cột MATKHAU_HIDDEN
                            row["MATKHAU_HIDDEN"] = "****";
                        }

                        // Đặt nguồn dữ liệu cho DataGridView
                        dgvSV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvSV.DataSource = tb;

                        // Hiển thị cột mới MATKHAU_HIDDEN thay cho cột MATKHAU thật
                        dgvSV.Columns["MATKHAU"].Visible = false; // Ẩn cột gốc
                        dgvSV.Columns["MATKHAU_HIDDEN"].HeaderText = "MATKHAU"; // Hiển thị cột placeholder

                        dgvSV.ReadOnly = true; // Đặt chế độ chỉ đọc cho DataGridView
                    }
                }
            }
        }

    }
}
