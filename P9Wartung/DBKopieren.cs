using MySqlConnector;
using System;
namespace Borys.Nachrichten
{
  internal partial class WartMain
  {
    private static void DBKopieren(MySqlConnection con, string dBTab1, string dbTab2)
    {
      using (MySqlCommand cmd1 = new MySqlCommand("", con))
      using (MySqlCommand cmd2 = new MySqlCommand("", con))
      {
        cmd1.CommandText = $"select * from {dBTab1} ";
        con.Open();
        MySqlDataReader reader = cmd1.ExecuteReader();
        while (reader.Read())
        {
          throw new NotImplementedException("??");
          _ = cmd1.ExecuteNonQuery();
          cmd1.CommandText = $"TRUNCATE TABLE {dbTab2}";
          _ = cmd1.ExecuteNonQuery();
        }
      }
    }
  }
}
