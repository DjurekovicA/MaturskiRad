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
    public partial class PrikazUser : Form
    {
        private Label lblIme = new Label();
        private Button btnClose = new Button();
        private ListBox lbPredmetiO = new ListBox();
        private ListBox lbPredmetiA = new ListBox();

        private Panel pnlOcene = new Panel();
        private Panel pnlAktivnost = new Panel();

        string predmet = "";
        string prikaz = "";
        string korisnik = "";
        public PrikazUser(string Predmet, string Prikaz, string Korisnik)
        {
            predmet = Predmet;
            prikaz = Prikaz;
            korisnik = Korisnik;
            Dizajn();
            LoadData();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.LightBlue, ButtonBorderStyle.Solid);
        }
        private void Dizajn()
        {
            //  
            //  Forma
            //
            ClientSize = new Size(350, 370);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(241, 242, 243);
            StartPosition = FormStartPosition.CenterScreen;

            //
            //  Label
            //
            lblIme.Text = predmet;
            lblIme.AutoSize = true;
            lblIme.Location = new Point(5, 10);
            lblIme.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            Controls.Add(lblIme);

            //
            //  btnClose
            //
            btnClose.Text = "X";
            btnClose.Size = new Size(30, 30);
            btnClose.Click += BtnClose_Click;
            btnClose.Location = new Point(310, 10);
            btnClose.Font = new Font("Times New Roman", 10F, FontStyle.Regular);
            Controls.Add(btnClose);

            //
            //  lbPredmetiA  
            //
            lbPredmetiA.BorderStyle = BorderStyle.None;
            lbPredmetiA.BackColor = Color.FromArgb(241, 242, 243);
            lbPredmetiA.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lbPredmetiA.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            pnlAktivnost.Controls.Add(lbPredmetiA);

            //
            //  lbPredmetiO
            //
            lbPredmetiO.BorderStyle = BorderStyle.None;
            lbPredmetiO.BackColor = Color.FromArgb(241, 242, 243);
            lbPredmetiO.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lbPredmetiO.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            pnlOcene.Controls.Add(lbPredmetiO);

            //
            //  pnlOcene
            //
            if (prikaz == "Ocene")
            {
                pnlAktivnost.Visible = false;
                pnlOcene.Visible = true;
            }
            pnlOcene.Location = new Point(20, 50);
            pnlOcene.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            pnlOcene.AutoScroll = true;
            Controls.Add(pnlOcene);

            //
            //  pnlAktivnost
            //
            if (prikaz == "Aktivnost"){
                pnlOcene.Visible = false;
                pnlAktivnost.Visible = true;
            }
            pnlAktivnost.Location = new Point(20, 50);
            pnlAktivnost.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            pnlAktivnost.AutoScroll = true;
            Controls.Add(pnlAktivnost);
        }

        private void LoadData()
        {
            if (predmet == "")
            {
                lblIme.Text = korisnik;
                if (prikaz == "Ocene")
                {
                    var dataBase = new DataBase();
                    string query = "SELECT `Ocena`,`Predmet`, `Datum` FROM `ocene` WHERE `Ucenik`= '" + korisnik + "'";
                    dataBase.Connect_db();
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        lbPredmetiO.Items.Add(reader.GetString("Predmet") + "   " + reader.GetString("Ocena") + "   " + reader.GetString("Datum"));

                    reader.Close();
                    dataBase.Close_db();
                }
                else if (prikaz == "Aktivnost")
                {
                    var dataBase = new DataBase();
                    string query = "SELECT `Ucenik`, `Aktivnost`, `Predmet`, `Datum` FROM `aktivnost` WHERE `Ucenik` = '" + korisnik + "'";
                    dataBase.Connect_db();
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        lbPredmetiA.Items.Add(reader.GetString("Predmet") + "   '" + reader.GetString("Aktivnost") + "'   " + reader.GetString("Datum"));

                    reader.Close();
                    dataBase.Close_db();
                }
            }
            else if (predmet != "")
            {
                if (prikaz == "Ocene")
                {
                    var dataBase = new DataBase();
                    string query = "SELECT `Ocena`,`Predmet`, `Datum` FROM `ocene` WHERE `Ucenik`= '" + korisnik + "' AND `Predmet`= '" + predmet + "'";
                    dataBase.Connect_db();
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        lbPredmetiO.Items.Add(reader.GetString("Ocena") + "   " + reader.GetString("Datum"));

                    reader.Close();
                    dataBase.Close_db();
                }
                else if (prikaz == "Aktivnost")
                {
                    var dataBase = new DataBase();
                    string query = "SELECT `Ucenik`, `Aktivnost`, `Predmet`, `Datum` FROM `aktivnost` WHERE `Ucenik` = '" + korisnik + "' AND `Predmet`= '" + predmet + "'";
                    dataBase.Connect_db();
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        lbPredmetiA.Items.Add("'" + reader.GetString("Aktivnost") + "'   " + reader.GetString("Datum"));

                    reader.Close();
                    dataBase.Close_db();
                }
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
