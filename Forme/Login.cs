using MySql.Data.MySqlClient;
using Pedagoška_sveska.Resursi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pedagoška_sveska.Forme
{
    public partial class Login : Form
    {
        private Panel panel = new Panel();
        private Label Label = new Label();
        private Label LabelUsername = new Label();
        private TextBox Username = new TextBox();
        private Label LabelPassword = new Label();
        private TextBox Password = new TextBox();
        private Button btnEnter = new Button();

        public Login()
        {
            Dizajn();
        }
        private void Dizajn()
        {
            //
            //  Forma
            //
            ClientSize = new Size(500,500);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            Click += Evrithing_Click;

            //
            //  panel
            //
            panel.Location = new Point(125, 75);
            panel.Size = new Size(250, 340);
            panel.Click += Evrithing_Click;
            Controls.Add(panel);

            //
            //  Label
            //
            Label.Text = "Login";
            Label.Location = new Point(0, 0);
            Label.TextAlign = ContentAlignment.MiddleCenter;
            Label.Size = new Size((int)panel.Width, 60);
            Label.ForeColor = Color.Black;
            Label.Font = new Font("Times New Roman", 40F, FontStyle.Regular);
            Label.Click += Evrithing_Click;
            panel.Controls.Add(Label);

            //
            //  LabelUsername
            //
            LabelUsername.Text = "Username";
            LabelUsername.Location = new Point(0, 60);
            LabelUsername.TextAlign = ContentAlignment.MiddleCenter;
            LabelUsername.Size = new Size((int)panel.Width, 60);
            LabelUsername.ForeColor = Color.Black;
            LabelUsername.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            LabelUsername.Click += Evrithing_Click;
            panel.Controls.Add(LabelUsername);

            //
            //  Username
            //
            Username.Location = new Point(39, 120);
            Username.Size = new Size(172, 60);
            Username.MaxLength = 12;
            Username.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            Username.ForeColor = Color.Black;
            Username.TabIndex = 1;
            if (Username.Text == "")
            {
                Username.Text = "Unesite ime";
                Username.ForeColor = Color.LightGray;
            }
            Username.Click += Username_Click;
            panel.Controls.Add(Username);

            //
            //  LabelPassword
            //
            LabelPassword.Text = "Password";
            LabelPassword.Location = new Point(0, 170);
            LabelPassword.TextAlign = ContentAlignment.MiddleCenter;
            LabelPassword.Size = new Size((int)panel.Width, 60);
            LabelPassword.ForeColor = Color.Black;
            LabelPassword.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            LabelPassword.Click += Evrithing_Click;
            panel.Controls.Add(LabelPassword);

            //
            //  Password
            //
            Password.Location = new Point(39, 230);
            Password.Size = new Size(172, 60);
            Password.ForeColor = Color.Black;
            Password.MaxLength = 12;
            Password.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            if (Password.Text == "")
            {
                Password.Text = "Unesite šifru";
                Password.ForeColor = Color.LightGray;
            }
            Password.Click += Password_Click;
            panel.Controls.Add(Password);

            //
            //  Enter
            //
            btnEnter.TabIndex = 0;
            btnEnter.Text = "Login";
            btnEnter.Location = new Point(75, 290);
            btnEnter.Size = new Size(100, 40);
            btnEnter.ForeColor = Color.Black;
            btnEnter.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            btnEnter.Click += BtnEnter_Click;
            panel.Controls.Add(btnEnter);
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            string[] user = new string[100];
            int brojac = 0;
            var dataBase = new DataBase();
            string query = "SELECT `id`, `Username`, `Password`, `Role` FROM `korisnik`";
            dataBase.Connect_db();
            MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user[brojac] += reader.GetString("Username");
                brojac++;
            }
            reader.Close();
            dataBase.Close_db();

            foreach (string u in user)
            {
                if (Username.Text == u)
                {
                    var dataBase1 = new DataBase();
                    dataBase1.Connect_db();
                    query = "SELECT * FROM `korisnik` WHERE `Username` = '" + u + "'";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, dataBase1.conn); 
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = mySqlCommand;
                    DataTable dataTable = new DataTable();
                    mySqlDataAdapter.Fill(dataTable);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                    if (mySqlDataReader.Read())
                    {
                        if(Password.Text == mySqlDataReader.GetString("Password"))
                        {
                            string predmet = Predmet(u);
                            HomePage page = new HomePage(Username.Text, mySqlDataReader.GetString("Role"), predmet, this);
                            dataBase1.Close_db();
                            page.Show();
                            Username.Text = "";
                            Password.Text = "";
                            this.Hide();
                        }
                    }
                }
            }
        }

        private string Predmet(string username)
        {
            string id = "";
            var dataBase = new DataBase();
            string query = "SELECT `id` FROM `korisnik` WHERE `Username` = '" + username + "'";
            dataBase.Connect_db();
            MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                id = reader.GetString("Id");
            reader.Close();
            dataBase.Close_db();

            var dataBase1 = new DataBase();
            dataBase1.Connect_db();
            query = "SELECT `Naziv` FROM `predmeti` WHERE `Predavac` = '" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(query, dataBase1.conn);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = mySqlCommand;
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            if (mySqlDataReader.Read())
                return mySqlDataReader.GetString("Naziv");
            return "";
        }
        private void Password_Click(object sender, EventArgs e)
        {
            if (Password.Text == "Unesite šifru")
            {
                Password.Text = "";
                Password.ForeColor = Color.Black;
            }
            if (Username.Text == "")
            {
                Username.Text = "Unesite ime";
                Username.ForeColor = Color.LightGray;
                panel.Focus();
                return;
            }
        }

        private void Evrithing_Click(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                Username.Text = "Unesite ime";
                Username.ForeColor = Color.LightGray;
                panel.Focus();
                return;
            }
            if (Password.Text == "")
            {
                Password.Text = "Unesite šifru";
                Password.ForeColor = Color.LightGray;
                panel.Focus();
                return;
            }
        }

        private void Username_Click(object sender, EventArgs e)
        {
            if (Username.Text == "Unesite ime")
            {
                Username.Text = "";
                Username.ForeColor = Color.Black;
            }
            if (Password.Text == "")
            {
                Password.Text = "Unesite šifru";
                Password.ForeColor = Color.LightGray;
                panel.Focus();
                return;
            }
        }
    }
}
