using MySql.Data.MySqlClient;
using Pedagoška_sveska.Resursi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using WECPOFLogic;

namespace Pedagoška_sveska.Forme
{
    public partial class HomePage : Form
    {
        private Label lblIme = new Label();
        private ListBox lbPredmeti = new ListBox();
        private Button btnOcene = new Button();
        private Button btnAktivnost = new Button();
        private Button btnClose = new Button();
        private RadioButton rbAdd = new RadioButton();
        private RadioButton rbView = new RadioButton();


        string username = "";
        string role = "";
        string predmet = "";
        string UpisPregled = "";
        Form parent;
        public HomePage(string Username, string Role, string Predmet, Form Parent)
        {
            parent = Parent;
            predmet = Predmet;
            role = Role;
            username = Username;
            Dizajn();
        }
        public void Dizajn()
        {
            //  
            //  Forma
            //
            Click += Deselect_Click;
            ClientSize = new Size(650, 450);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(241, 242, 243);

            //
            //  rbAdd
            //
            rbAdd.AutoSize = true;
            rbAdd.Text = "Upis";
            rbAdd.Location = new Point(425, 95);
            rbAdd.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            if (role == "admin")
                Controls.Add(rbAdd);

            //
            //  rbView
            //
            rbView.AutoSize = true;
            rbView.Text = "Pregled";
            rbView.Location = new Point(425, 95 + rbAdd.Height);
            rbView.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            if (role == "admin")
                Controls.Add(rbView);

            //
            //  lblIme
            //
            lblIme.Text = username;
            lblIme.AutoSize = true;
            lblIme.Click += Deselect_Click;
            lblIme.Location = new Point(5, 10);
            lblIme.Font = new Font("Times New Roman", 15, FontStyle.Regular);
            Controls.Add(lblIme);

            //
            //  btnOcene
            //
            btnOcene.Text = "Ocene";
            btnOcene.Click += BtnOcene_Click;
            btnOcene.Size = new Size(100, 30);
            btnOcene.Location = new Point(425,150);
            btnOcene.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            Controls.Add(btnOcene);

            //
            //  btnAktivnost
            //
            btnAktivnost.Text = "Aktivnost";
            btnAktivnost.Size = new Size(100, 30);
            btnAktivnost.Click += BtnAktivnost_Click;
            btnAktivnost.Location = new Point(425, 200);
            btnAktivnost.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            Controls.Add(btnAktivnost);

            //
            //  btnClose
            //
            btnClose.Text = "X";
            btnClose.Size = new Size(40, 40);
            btnClose.Click += BtnClose_Click;
            btnClose.Location = new Point(600, 10);
            btnClose.Font = new Font("Times New Roman", 10F, FontStyle.Regular);
            Controls.Add(btnClose);

            //
            //  lbPredmeti
            //
            lbPredmeti.Size = new Size(240, 260);
            lbPredmeti.Location = new Point(140, 95);
            lbPredmeti.BorderStyle = BorderStyle.None;
            lbPredmeti.BackColor = Color.FromArgb(241, 242, 243);
            lbPredmeti.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            loadData(lbPredmeti, role);
            Controls.Add(lbPredmeti);

        }
        private void loadData(ListBox lb, string role)
        {
            var dataBase = new DataBase();
            dataBase.conn.Open();
            string query = "";
            if(role == "user")
                query = "SELECT `Naziv` FROM `predmeti`";
            if(role == "admin")
                query = "SELECT `Username` FROM `korisnik` WHERE `Role` = 'user'";
            MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(role == "user")
                    lb.Items.Add(reader.GetString("Naziv"));
                if (role == "admin")
                    lb.Items.Add(reader.GetString("Username"));
            }
            reader.Close();
            dataBase.conn.Close();
        }
        private void BtnOcene_Click(object sender, EventArgs e)
        {
            if (rbView.Checked)
                UpisPregled = "Pregled";
            if (rbAdd.Checked)
                UpisPregled = "Upis";
            if (UpisPregled != "")
            {
                if (lbPredmeti.SelectedIndex.ToString() != "-1")
                {
                    if (role == "admin")
                    {
                        PrikazAdmin prikazAdmin = new PrikazAdmin(lbPredmeti.SelectedItem.ToString(), "Ocene", predmet, UpisPregled);
                        prikazAdmin.ShowDialog();
                    }
                }
                else if (lbPredmeti.SelectedIndex.ToString() == "-1")
                {
                    if (role == "admin")
                    {
                        PrikazAdmin prikazAdmin = new PrikazAdmin("", "Ocene", predmet, UpisPregled);
                        prikazAdmin.ShowDialog();
                    }
                }
            }
            else if (UpisPregled == "")
            {
                if (role == "admin")
                    MessageBox.Show("Izaberite sve opcije");
                else
                {
                    if (lbPredmeti.SelectedIndex.ToString() != "-1")
                    {
                        PrikazUser prikazUser = new PrikazUser(lbPredmeti.SelectedItem.ToString(), "Ocene", username);
                        prikazUser.ShowDialog();
                    }
                    else if (lbPredmeti.SelectedIndex.ToString() == "-1")
                    {
                        PrikazUser prikazUser = new PrikazUser("", "Ocene", username);
                        prikazUser.ShowDialog();
                    }
                }
            }
        }
        private void BtnAktivnost_Click(object sender, EventArgs e)
        {
            if (rbView.Checked)
                UpisPregled = "Pregled";
            if (rbAdd.Checked)
                UpisPregled = "Upis";

            if (UpisPregled != "")
            {
                if (lbPredmeti.SelectedIndex.ToString() != "-1")
            {
                if (role == "admin")
                {
                    PrikazAdmin prikazAdmin = new PrikazAdmin(lbPredmeti.SelectedItem.ToString(), "Aktivnost", predmet, UpisPregled);
                    prikazAdmin.ShowDialog();
                }
            }
            else if (lbPredmeti.SelectedIndex.ToString() == "-1")
            {
                if (role == "admin")
                {
                    PrikazAdmin prikazAdmin = new PrikazAdmin("", "Aktivnost", predmet, UpisPregled);
                    prikazAdmin.ShowDialog();
                }
            }
            else return;
            }
            else if (UpisPregled == "")
            {
                if (role == "admin")
                    MessageBox.Show("Izaberite sve opcije");
                else
                {
                    if (lbPredmeti.SelectedIndex.ToString() != "-1")
                    {
                        PrikazUser prikazUser = new PrikazUser(lbPredmeti.SelectedItem.ToString(), "Aktivnost", username);
                        prikazUser.ShowDialog();
                    }
                    else if (lbPredmeti.SelectedIndex.ToString() == "-1")
                    {
                        PrikazUser prikazUser = new PrikazUser("", "Aktivnost", username);
                        prikazUser.ShowDialog();
                    }
                }
            }
        }

        private void Deselect_Click(object sender, EventArgs e)
        {
            lbPredmeti.SelectedIndex = -1;
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            MessageBoxManager.Yes = "LogOut";
            MessageBoxManager.No = "Exit";
            MessageBoxManager.Register();
            DialogResult result = MessageBox.Show("", "Exit", MessageBoxButtons.YesNoCancel);
            MessageBoxManager.Unregister();

            if (result == DialogResult.Yes) {
                parent.Show();
                this.Close();
            }
            else if (result == DialogResult.No) 
                Application.Exit();
        }
    }
}
