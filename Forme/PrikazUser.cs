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
        private Panel pnlOcene = new Panel();
        private Button btnClose = new Button();
        private Panel pnlAktivnost = new Panel();
        private DataGridView dgvOcene = new DataGridView();
        private DataGridView dgvAktivnost = new DataGridView();


        string prikaz = "";
        string predmet = "";
        string korisnik = "";
        public PrikazUser(string Predmet, string Prikaz, string Korisnik)
        {
            prikaz = Prikaz;
            predmet = Predmet;
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
            ClientSize = new Size(470, 370);
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
            btnClose.Location = new Point(ClientSize.Width - btnClose.Width - 10, 10);
            btnClose.Font = new Font("Times New Roman", 10F, FontStyle.Regular);
            Controls.Add(btnClose);

            //
            //  dgvAktivnost  
            //
            dgvAktivnost.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvAktivnost.ColumnCount = 3;
            dgvAktivnost.Columns[0].Width = 200;
            dgvAktivnost.Columns[1].Width = 90;
            dgvAktivnost.Columns[2].Width = 99;
            dgvAktivnost.Size = new Size(460, 250);
            dgvAktivnost.Columns[0].Name = "Predmet";
            dgvAktivnost.Columns[1].Name = "Aktivnost";
            dgvAktivnost.Columns[2].Name = "Datum";
            dgvAktivnost.BorderStyle = BorderStyle.None;
            dgvAktivnost.AutoResizeColumnHeadersHeight();
            dgvAktivnost.BackColor = Color.FromArgb(241, 242, 243);
            dgvAktivnost.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            dgvAktivnost.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            pnlAktivnost.Controls.Add(dgvAktivnost);

            //
            //  dgvOcene
            //
            dgvOcene.ColumnCount = 3;
            dgvOcene.Columns[0].Width = 200;
            dgvOcene.Columns[1].Width = 89;
            dgvOcene.Columns[2].Width = 100;
            dgvOcene.Size = new Size(460, 250);
            dgvOcene.Columns[0].Name = "Predmet";
            dgvOcene.Columns[1].Name = "Ocene";
            dgvOcene.Columns[2].Name = "Datum";
            dgvOcene.BorderStyle = BorderStyle.None;
            dgvOcene.AutoResizeColumnHeadersHeight();
            dgvOcene.BackColor = Color.FromArgb(241, 242, 243);
            dgvOcene.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            dgvOcene.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            pnlOcene.Controls.Add(dgvOcene);

            //
            //  pnlOcene
            //
            if (prikaz == "Ocene")
            {
                pnlAktivnost.Visible = false;
                pnlOcene.Visible = true;
            }
            pnlOcene.AutoScroll = true;
            pnlOcene.Location = new Point(20, 50);
            pnlOcene.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            Controls.Add(pnlOcene);

            //
            //  pnlAktivnost
            //
            if (prikaz == "Aktivnost"){
                pnlOcene.Visible = false;
                pnlAktivnost.Visible = true;
            }
            pnlAktivnost.AutoScroll = true;
            pnlAktivnost.Location = new Point(20, 50);
            pnlAktivnost.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 80);
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
                        dgvOcene.Rows.Add(reader.GetString("Predmet"), reader.GetString("Ocena"), reader.GetString("Datum"));

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
                        dgvAktivnost.Rows.Add(reader.GetString("Predmet"), reader.GetString("Aktivnost"), reader.GetString("Datum"));

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
                        dgvOcene.Rows.Add(reader.GetString("Predmet"), reader.GetString("Ocena"), reader.GetString("Datum"));

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
                        dgvAktivnost.Rows.Add(reader.GetString("Predmet"), reader.GetString("Aktivnost"), reader.GetString("Datum"));

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
