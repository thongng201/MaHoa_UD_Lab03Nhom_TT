using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm
{
    public static class ConnectString
    {
        // Phương thức trả về chuỗi kết nối tới cơ sở dữ liệu QLSVNhom
        public static string GetConnectionString()
        {
            // Chuỗi kết nối tới SQL Server và database QLSVNhom
            return "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QLSVNhom;Integrated Security=True;Encrypt=False;";
        }
    }
}

