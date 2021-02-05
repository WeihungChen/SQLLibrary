using System.Data;
using SQLLibrary;

namespace SQLLibrarySample
{
    class Program
    {
        static void Main(string[] args)
        {
            MySQL sql = new MySQL(new MySQLConnectData
            {
                Server = "Server",
                Port = 1111,
                UserID = "User",
                Password = "PW",
                DataBase = "DB"
            });
            sql.Select("Table").SelectColumn("ColumnName").Where().Column("ColumnName").EqualsValue("Target").Complete().Execute(out DataTable table);
        }
    }
}
