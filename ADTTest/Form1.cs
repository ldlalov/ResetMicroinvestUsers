using ADTTest.Micro;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADTTest
{
    public partial class Reset : Form
    {
        public Reset()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            Success.Visible = false;

        }
        private void button3_Click(object sender, EventArgs e)//Connect
        {
            string server = textBox1.Text;
            string user = textBox2.Text;
            string password = textBox3.Text;
            Success.Visible = false;
            if (server == "")
            {
                conString = $"server=.;Integrated Security=True;encrypt=false;";
            }
            else if (user == "" || password == "")
            {
                MessageBox.Show("Ако сте указали сървър, попълнете потребител и парола.");
                return;
            }
            else
            {
                conString = $"server={server};User Id={user};Password={password};encrypt=false;";
            }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            try
            {
                comboBox2.Items.AddRange(GetDatabaseList().ToArray());
            }
            catch (Exception)
            {
                MessageBox.Show("Неуспешно свързване");
                Success.Visible = false;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                return;
            }
            Success.Visible = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //conString = $"server={textBox1.Text};Database={comboBox2.Text};Integrated Security=True;encrypt=false;";
            string database = comboBox2.Text;
            conString += $"Database = {database};";
            db = new MultiContext(conString);
            try
            {
                comboBox1.Items.AddRange(db.Users.Select(u => u.Name).ToArray());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Базата не е на Микроинвест");
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            User currentUser = db.Users.FirstOrDefault(x => x.Name == comboBox1.SelectedItem);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            db.Users.FirstOrDefault(x => x.Name == comboBox1.SelectedItem).Password = "YsAB16V90Bs=";
            db.SaveChanges();
            //foreach (var item in db.Users)
            //{
            //    richTextBox1.Text += ($"{item.Id} | {item.Name} -> {item.Password/*String.Join("       ",asciiBytes)*/}");
            //    richTextBox1.Text += Environment.NewLine;
            //}
            //textBox1.Text = db.ConnectionString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.Users.FirstOrDefault(x => x.Name == comboBox1.SelectedItem).UserLevel = 3;
            db.SaveChanges();

        }
        public string conString;

        MultiContext db = new MultiContext("");
        public List<string> GetDatabaseList()
        {
            List<string> list = new List<string>();

            // Open connection to the database
            
            using (SqlConnection con = new SqlConnection(conString))
            {

                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return list;

        }

    }
}
