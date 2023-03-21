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

namespace Pedagoška_sveska.Forme
{
    public partial class PrikazAdmin : Form
    {
        private Panel pnlOcene = new Panel();
        private Panel pnlAktivnost = new Panel();
        private Panel pnlOceneIzabran = new Panel();
        private Panel pnlAktivnostIzabran = new Panel();

        private Label lblIme = new Label();
        public Label lblDatum = new Label();
        public Label lblAktivnost = new Label();
        public Label lblAktivnostIzabran = new Label();

        private Button btnAdd = new Button();
        private Button btnPlus = new Button();
        private Button btnMinus = new Button();
        private Button btnClose = new Button();
        private Button btnKalendar = new Button();
        private Button btnPlusIzabran = new Button();
        private Button btnMinusIzabran = new Button();

        private ComboBox cbOcene = new ComboBox();
        private ComboBox cbOceneIzabran = new ComboBox();

        private ListBox lbOceneIzabran = new ListBox();
        private ListBox lbAktivnostIzabran = new ListBox();
        private ListBox lbUceniciOcene = new ListBox();
        private ListBox lbUceniciAktivnost = new ListBox();

        private DataGridView dgvOceneIzabran = new DataGridView();
        /*private DataGridView dgvOceneIzabran1 = new DataGridView();
        private DataGridView dgvOceneIzabran2 = new DataGridView();
        private DataGridView dgvOceneIzabran3 = new DataGridView();*/

        string ucenik = "";
        string prikaz = "";
        int panel = 0;
        string predmet = "";
        string znak = "";
        public PrikazAdmin(string Ucenik, string Prikaz, string Predmet)
        {
            predmet = Predmet;
            ucenik = Ucenik;
            prikaz = Prikaz;
            Dizajn();
            loadData();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.LightBlue, ButtonBorderStyle.Solid);
        }
        private void Dizajn()
        {
            pnlOcene.Visible = false;
            pnlAktivnost.Visible = false;
            pnlOceneIzabran.Visible = false;
            pnlAktivnostIzabran.Visible = false;
            //  
            //  Forma
            //
            ClientSize = new Size(450, 350);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(241, 242, 243);

            //
            //  lblIme
            //
            lblIme.Text = ucenik;
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
            btnClose.Location = new Point(410, 10);
            btnClose.Font = new Font("Times New Roman", 10F, FontStyle.Regular);
            Controls.Add(btnClose);

            //
            //  btnKalendar
            //
            btnKalendar.Text = "Datum";
            btnKalendar.Click += BtnKalendar_Click;
            btnKalendar.Location = new Point(275, 215);
            btnKalendar.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            Controls.Add(btnKalendar);

            //
            //  lblDatum
            //
            lblDatum.TextAlign = ContentAlignment.MiddleCenter;
            lblDatum.Size = new Size(btnPlus.Width * 2 + 10, 20);
            lblDatum.Location = new Point(232, 225 + btnKalendar.Height);
            lblDatum.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            Controls.Add(lblDatum);

            //
            //  lblAktivnost
            //
            lblAktivnost.Text = "Plusevi: - Minusevi: -";
            lblAktivnost.TextAlign = ContentAlignment.MiddleCenter;
            lblAktivnost.Size = new Size(btnPlus.Width * 2 + 10, 50);
            lblAktivnost.Location = new Point(pnlAktivnost.Width, 45);
            lblAktivnost.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            pnlAktivnost.Controls.Add(lblAktivnost);

            //
            //  lblAktivnostIzabran
            //
            lblAktivnostIzabran.TextAlign = ContentAlignment.MiddleCenter;
            lblAktivnostIzabran.Size = new Size(btnPlus.Width * 2 + 10, 50);
            lblAktivnostIzabran.Location = new Point(pnlAktivnostIzabran.Width, 45);
            lblAktivnostIzabran.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            pnlAktivnostIzabran.Controls.Add(lblAktivnostIzabran);

            //
            //  pnlOcene
            //
            pnlOcene.Click += Deselect_Click;
            pnlOcene.Location = new Point(30, 50);
            pnlOcene.Size = new Size(ClientSize.Width - 60, ClientSize.Height - 95);
            Controls.Add(pnlOcene);

            //
            //   pnlOceneIzabran
            //
            pnlOceneIzabran.Location = new Point(30, 50);
            pnlOceneIzabran.Size = new Size(ClientSize.Width - 60, ClientSize.Height - 95);
            Controls.Add(pnlOceneIzabran);

            //
            //   pnlAktivnost
            //
            pnlAktivnost.Click += Deselect_Click;
            pnlAktivnost.Location = new Point(30, 50);
            pnlAktivnost.Size = new Size(ClientSize.Width - 60, ClientSize.Height - 95);
            Controls.Add(pnlAktivnost);

            //
            //   pnlAktivnostIzabran
            //
            pnlAktivnostIzabran.Enabled = true;
            pnlAktivnostIzabran.Location = new Point(30, 50);
            pnlAktivnostIzabran.Size = new Size(ClientSize.Width - 60, ClientSize.Height - 95);
            Controls.Add(pnlAktivnostIzabran);

            //
            //  btnPlus
            //
            btnPlus.Text = "+";
            btnPlus.AutoSize = true;
            btnPlus.Click += BtnPlus_Click;
            btnPlus.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            btnPlus.Location = new Point((5 * pnlAktivnost.Width / 8) - (btnPlus.Width / 2 + 5), pnlAktivnost.Height / 2);
            pnlAktivnost.Controls.Add(btnPlus);

            //
            //  btnPlusIzabran
            //
            btnPlusIzabran.Text = "+";
            btnPlusIzabran.AutoSize = true;
            btnPlusIzabran.Click += BtnPlus_Click;
            btnPlusIzabran.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            btnPlusIzabran.Location = new Point((5 * pnlAktivnostIzabran.Width / 8) - (btnPlusIzabran.Width / 2 + 5), pnlAktivnostIzabran.Height / 2);
            pnlAktivnostIzabran.Controls.Add(btnPlusIzabran);

            //
            //  btnMinus
            //
            btnMinus.Text = "-";
            btnMinus.AutoSize = true;
            btnMinus.Click += BtnMinus_Click;
            btnMinus.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            btnMinus.Location = new Point((5 * pnlAktivnost.Width / 8) + 5 + btnMinus.Width / 2, pnlAktivnost.Height / 2);
            pnlAktivnost.Controls.Add(btnMinus);

            //
            //  btnMinusIzabran
            //
            btnMinusIzabran.Text = "-";
            btnMinusIzabran.AutoSize = true;
            btnMinusIzabran.Click += BtnMinus_Click;
            btnMinusIzabran.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            btnMinusIzabran.Location = new Point((5 * pnlAktivnostIzabran.Width / 8) + 5 + btnMinusIzabran.Width / 2, pnlAktivnostIzabran.Height / 2);
            pnlAktivnostIzabran.Controls.Add(btnMinusIzabran);

            //
            //  cbOcene
            //
            cbOcene.Text = "Izaberite ocenu";
            cbOcene.Items.Add("Izaberite ocenu");
            cbOcene.Items.Add("1");
            cbOcene.Items.Add("2");
            cbOcene.Items.Add("3");
            cbOcene.Items.Add("4");
            cbOcene.Items.Add("5");
            cbOcene.Font = new Font("Times New Roman", 12F, FontStyle.Regular);
            cbOcene.Location = new Point((5 * pnlAktivnostIzabran.Width / 8) - (btnPlusIzabran.Width / 8 + 5), pnlOcene.Height / 2 - cbOcene.Height);
            pnlOcene.Controls.Add(cbOcene);

            //
            //  cbOcene
            //
            cbOceneIzabran.Text = "Izaberite ocenu";
            cbOceneIzabran.Items.Add("Izaberite ocenu");
            cbOceneIzabran.Items.Add("1");
            cbOceneIzabran.Items.Add("2");
            cbOceneIzabran.Items.Add("3");
            cbOceneIzabran.Items.Add("4");
            cbOceneIzabran.Items.Add("5");
            cbOceneIzabran.Font = new Font("Times New Roman", 12F, FontStyle.Regular);
            cbOceneIzabran.Location = new Point((5 * pnlAktivnostIzabran.Width / 8) - (btnPlusIzabran.Width / 8 + 5), pnlOcene.Height / 2 - cbOceneIzabran.Height);
            pnlOceneIzabran.Controls.Add(cbOceneIzabran);

            //
            //  btnAdd
            //
            btnAdd.AutoSize = true;
            btnAdd.Text = "Sačuvaj";
            btnAdd.Click += BtnAdd_Click;
            btnAdd.Font = new Font("Times New Roman", 12F, FontStyle.Regular);
            btnAdd.Location = new Point(ClientSize.Width - btnAdd.Width - 10, ClientSize.Height - btnAdd.Height - 15);
            Controls.Add(btnAdd);

            //
            //  lbUceniciOcene
            //
            lbUceniciOcene.BackColor = Color.White;
            lbUceniciOcene.Location = new Point(0, 10);
            lbUceniciOcene.BorderStyle = BorderStyle.None;
            lbUceniciOcene.BackColor = Color.FromArgb(241, 242, 243);
            lbUceniciOcene.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lbUceniciOcene.Size = new Size(pnlOcene.Width / 2 - 20, pnlOcene.Height - 20);
            pnlOcene.Controls.Add(lbUceniciOcene);

            //
            //  lbUceniciAktivnost
            //
            lbUceniciAktivnost.Click += LbUceniciAktivnost_Click;
            lbUceniciAktivnost.BackColor = Color.White;
            lbUceniciAktivnost.Location = new Point(10, 10);
            lbUceniciAktivnost.BorderStyle = BorderStyle.None;
            lbUceniciAktivnost.BackColor = Color.FromArgb(241, 242, 243);
            lbUceniciAktivnost.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lbUceniciAktivnost.Size = new Size(pnlAktivnost.Width / 2 - 20, pnlAktivnost.Height - 20);
            pnlAktivnost.Controls.Add(lbUceniciAktivnost);

            //
            //  lbOceneIzabran
            //
            lbOceneIzabran.BackColor = Color.White;
            lbOceneIzabran.Location = new Point(10, 10);
            lbOceneIzabran.BorderStyle = BorderStyle.None;
            lbOceneIzabran.BackColor = Color.FromArgb(241, 242, 243);
            lbOceneIzabran.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lbOceneIzabran.Size = new Size(pnlOcene.Width / 2 - 20, pnlOcene.Height - 20);
            pnlOceneIzabran.Controls.Add(lbOceneIzabran);

            //
            //  lbAktivnostIzabran
            //
            lbAktivnostIzabran.BackColor = Color.White;
            lbAktivnostIzabran.Location = new Point(10, 10);
            lbAktivnostIzabran.BorderStyle = BorderStyle.None;
            lbAktivnostIzabran.BackColor = Color.FromArgb(241, 242, 243);
            lbAktivnostIzabran.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lbAktivnostIzabran.Size = new Size(pnlOcene.Width / 2 - 20, pnlOcene.Height - 20);
            pnlAktivnostIzabran.Controls.Add(lbAktivnostIzabran);

            btnKalendar.Size = new Size(btnMinus.Width, btnMinus.Height);

            if (ucenik == "")
            {
                if (prikaz == "Aktivnost")
                {
                    panel = 1;
                    pnlOcene.Visible = false;
                    pnlAktivnost.Visible = true;
                    pnlOceneIzabran.Visible = false;
                    pnlAktivnostIzabran.Visible = false;
                }
                else if (prikaz == "Ocene")
                {
                    panel = 2;
                    pnlOcene.Visible = true;
                    pnlAktivnost.Visible = false;
                    pnlOceneIzabran.Visible = false;
                    pnlAktivnostIzabran.Visible = false;
                }
            }
            else
            {
                if (prikaz == "Aktivnost")
                {
                    panel = 3;
                    pnlOcene.Visible = false;
                    pnlAktivnost.Visible = false;
                    pnlOceneIzabran.Visible = false;
                    pnlAktivnostIzabran.Visible = true;
                }
                else if (prikaz == "Ocene")
                {
                    panel = 4;
                    pnlOcene.Visible = false;
                    pnlAktivnost.Visible = false;
                    pnlOceneIzabran.Visible = true;
                    pnlAktivnostIzabran.Visible = false;
                }
            }
        }
        private void BtnMinus_Click(object sender, EventArgs e)
        {
            znak = "-";
        }
        private void BtnPlus_Click(object sender, EventArgs e)
        {
            znak = "+";
        }
        private void LbUceniciAktivnost_Click(object sender, EventArgs e)
        {
            var dataBase = new DataBase();
            int brojacPlus = 0;
            int brojacMinus = 0;
            dataBase.Connect_db();
            string query = "SELECT `Aktivnost` FROM `aktivnost` WHERE `Predmet` = '" + predmet + "' AND `Ucenik` = '" + lbUceniciAktivnost.SelectedItem + "'";
            MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString("Aktivnost") == "+")
                    brojacPlus++;
                else if (reader.GetString("Aktivnost") == "-")
                    brojacMinus++;

            }
            reader.Close();
            dataBase.Close_db();

            lblAktivnost.Text = "Plusevi: " + brojacPlus + " Minusevi: " + brojacMinus;
        }
        private void loadData()
        {
            var dataBase = new DataBase();
            dataBase.Connect_db();
            if (panel == 1)
            {
                string query = "SELECT `Username` FROM `korisnik` WHERE `Role` = 'user'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lbUceniciAktivnost.Items.Add(reader.GetString("Username"));
                reader.Close();
                dataBase.Close_db();
            }
            else if (panel == 2)
            {
                string query = "SELECT `Username` FROM `korisnik` WHERE `Role` = 'user'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lbUceniciOcene.Items.Add(reader.GetString("Username"));
                reader.Close();
                dataBase.Close_db();
            }
            else if (panel == 3)
            {
                int brojacPlus = 0;
                int brojacMinus = 0;
                string query = "SELECT `Ucenik`, `Aktivnost`, `Predmet`, `Datum` FROM `aktivnost` WHERE `Ucenik`= '" + ucenik + "' AND `Predmet` = '" + predmet + "'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lbAktivnostIzabran.Items.Add("'" + reader.GetString("Aktivnost") + "' - " + reader.GetString("Datum"));

                    if (reader.GetString("Aktivnost") == "+")
                        brojacPlus++;
                    else if (reader.GetString("Aktivnost") == "-")
                        brojacMinus++;

                }
                reader.Close();
                dataBase.Close_db();
                lblAktivnostIzabran.Text = "Plusevi: " + brojacPlus + " Minusevi: " + brojacMinus;
            }
            else if (panel == 4)
            {
                string query = "SELECT `Ocena`, `Ucenik`, `Datum` FROM `ocene` WHERE `Ucenik`= '" + ucenik + "' AND `Predmet` = '" + predmet + "'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lbOceneIzabran.Items.Add(reader.GetString("Ocena") + " - " + reader.GetString("Datum"));
                reader.Close();
                dataBase.Close_db();
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (panel == 1)
            {
                if (znak != "" && lbUceniciAktivnost.SelectedItem != "" && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.Connect_db();
                    string query = "INSERT INTO `aktivnost`(`id`, `Ucenik`, `Aktivnost`, `Predmet`, `Datum`) VALUES ('" + null + "','" + lbUceniciAktivnost.SelectedItem + "','" + znak + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.Close_db();
                }
            }
            else if (panel == 2)
            {
                if (cbOcene.SelectedIndex != -1 && lbUceniciOcene.SelectedItem != "" && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.Connect_db();
                    string query = "INSERT INTO `ocene`(`id`, `Ocena`, `Ucenik`, `Predmet`, `Datum`) VALUES ('" + null + "','" + cbOcene.SelectedIndex + "','" + lbUceniciOcene.SelectedItem + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.Close_db();
                }
            }
            else if (panel == 3)
            {
                if (znak != "" && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.Connect_db();
                    string query = "INSERT INTO `aktivnost`(`id`, `Ucenik`, `Aktivnost`, `Predmet`, `Datum`) VALUES ('" + null + "','" + ucenik + "','" + znak + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.Close_db();
                    lbAktivnostIzabran.Items.Clear();
                    loadData();
                }
            }
            else if (panel == 4)
            {
                if (cbOceneIzabran.SelectedIndex != -1 && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.Connect_db();
                    string query = "INSERT INTO `ocene`(`id`, `Ocena`, `Ucenik`, `Predmet`, `Datum`) VALUES ('" + null + "','" + cbOceneIzabran.SelectedIndex + "','" + ucenik + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.Close_db();
                    lbOceneIzabran.Items.Clear();
                    loadData();
                }
            }

        }

        private void BtnKalendar_Click(object sender, EventArgs e)
        {
            Datum datum = new Datum();
            datum.ShowDialog();
            lblDatum.Text = datum.ReturnValue;
        }

        private void Deselect_Click(object sender, EventArgs e)
        {
            lbUceniciOcene.SelectedIndex = -1;
            lbUceniciAktivnost.SelectedIndex = -1;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
