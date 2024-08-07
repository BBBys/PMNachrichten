using MySql.Data.MySqlClient;

namespace DBZugriffMySQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CmdAlleSehen_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new("Data Source=localhost;" +
                "Initial Catalog=firma;UID=root");
            MySqlCommand cmd = new("SELECT * FROM personen", con);

            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                LstAnzeige.Items.Clear();
                while (reader.Read())
                {
                    DateTime geburtstag = (DateTime)reader["geburtstag"];
                    LstAnzeige.Items.Add($"{reader["name"]} # " +
                        $"{reader["vorname"]} # " +
                        $"{reader["personalnummer"]} # " +
                        $"{reader["gehalt"]} # " +
                        $"{geburtstag.ToShortDateString()}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void CmdAendern_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new("Data Source=localhost;" +
                "Initial Catalog=firma;UID=root");
            string op = sender == CmdErhoehen ? "*" : "/";
            MySqlCommand cmd = new($"UPDATE personen " +
                $"SET gehalt = gehalt {op} 1.05", con);

            try
            {
                con.Open();
                int anzahl = cmd.ExecuteNonQuery();
                MessageBox.Show("Datensätze geändert: " + anzahl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
    }
}