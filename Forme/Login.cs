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
        private Label LabelPassword = new Label();
        private Label LabelUsername = new Label();
        private Button btnEnter = new Button();
        private TextBox Username = new TextBox();
        private TextBox Password = new TextBox();

        public Login()
        {
            Dizajn();   
        }
        private void Dizajn()
        {
            //
            //  Forma
            //
            MaximizeBox = false;
            MinimizeBox = false;
            Click += Evrithing_Click;
            ClientSize = new Size(500,500);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;

            //
            //  panel
            //
            panel.Click += Evrithing_Click;
            panel.Size = new Size(250, 340);
            panel.Location = new Point(125, 75);
            Controls.Add(panel);

            //
            //  Label
            //
            lblDizajn(Label, "Login");
            panel.Controls.Add(Label);

            //
            //  LabelUsername
            //
            lblDizajn(LabelUsername, "Username");
            panel.Controls.Add(LabelUsername);

            //
            //  Username
            //
            Username.MaxLength = 20;
            Username.Text = "Unesite ime";
            Username.Click += Username_Click;
            Username.Size = new Size(172, 60);
            Username.ForeColor = Color.LightGray;
            Username.Location = new Point(39, 120);
            Username.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            panel.Controls.Add(Username);

            //
            //  LabelPassword
            //
            lblDizajn(LabelPassword, "Password");
            panel.Controls.Add(LabelPassword);

            //
            //  Password
            //
            Password.MaxLength = 12;
            Password.PasswordChar = '*';
            Password.ForeColor = Color.Black;
            Password.Click += Password_Click;
            Password.Size = new Size(172, 60);
            Password.Location = new Point(39, 230);
            Password.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            panel.Controls.Add(Password);

            //
            //  btnEnter
            //
            btnEnter.Text = "Login";
            btnEnter.ForeColor = Color.Black;
            btnEnter.Click += BtnEnter_Click;
            btnEnter.Size = new Size(100, 40);
            btnEnter.Location = new Point(75, 290);
            btnEnter.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            panel.Controls.Add(btnEnter);
        }
        private void lblDizajn(Label label, string element)
        {
            label.ForeColor = Color.Black;
            label.Click += Evrithing_Click;
            label.Size = new Size((int)panel.Width, 60);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Times New Roman", 20F, FontStyle.Regular);
            if (element == "Login")
            {
                label.Text = "Login";
                label.Location = new Point(0, 0);
            }
            else if (element == "Username")
            {
                label.Text = "Username";
                label.Location = new Point(0, 60);
            }
            else if (element == "Password")
            {
                label.Text = "Password";
                label.Location = new Point(0, 170);
            }
        }
        private void BtnEnter_Click(object sender, EventArgs e)
        {
            string[] user = new string[100];
            int brojac = 0;
            var dataBase = new DataBase();
            string query = "SELECT `id`, `Username`, `Password`, `Role` FROM `korisnik`";
            dataBase.conn.Open();
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
            dataBase.conn.Close();

            foreach (string u in user)
            {
                if (Username.Text == u)
                {
                    var dataBase1 = new DataBase();
                    dataBase1.conn.Open();
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
                            dataBase1.conn.Close();
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
            dataBase.conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                id = reader.GetString("Id");
            reader.Close();
            dataBase.conn.Close();

            var dataBase1 = new DataBase();
            dataBase1.conn.Open();
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
            if (Username.Text == "")
            {
                Username.Text = "Unesite ime";
                Username.ForeColor = Color.LightGray;
            }
        }
        private void Evrithing_Click(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                Username.Text = "Unesite ime";
                Username.ForeColor = Color.LightGray;
            }
                panel.Focus();
                return;
        }
        private void Username_Click(object sender, EventArgs e)
        {
            if (Username.Text == "Unesite ime")
            {
                Username.Text = "";
                Username.ForeColor = Color.Black;
            }
        }
    }
}
