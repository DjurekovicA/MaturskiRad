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
        private Panel pnlUpisOcene = new Panel();
        private Panel pnlPregledOcene = new Panel();
        private Panel pnlUpisAktivnost = new Panel();
        private Panel pnlPregledAktivnost = new Panel();

        private Panel pnlUpisOceneIzabran = new Panel();
        private Panel pnlPregledOceneIzabran = new Panel();
        private Panel pnlUpisAktivnostIzabran = new Panel();
        private Panel pnlPregledAktivnostIzabran = new Panel();

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

        private ListBox lbUceniciOcene = new ListBox();
        private ListBox lbUceniciAktivnost = new ListBox();

        private DataGridView dgvOcene = new DataGridView();
        private DataGridView dgvAktivnost = new DataGridView();
        private DataGridView dgvOceneIzabran = new DataGridView();
        private DataGridView dgvAktivnostIzabran = new DataGridView();

        string ucenik = "";
        string prikaz = "";
        int panel = 0;
        string predmet = "";
        string Znak = "";
        string upisPregled = "";
        public PrikazAdmin(string Ucenik, string Prikaz, string Predmet, string UpisPregled)
        {
            upisPregled = UpisPregled;
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
            pnlUpisOcene.Visible = false;
            pnlUpisAktivnost.Visible = false;
            pnlUpisOceneIzabran.Visible = false;
            pnlUpisAktivnostIzabran.Visible = false;
            pnlPregledOcene.Visible = false;
            pnlPregledAktivnost.Visible = false;
            pnlPregledOceneIzabran.Visible = false;
            pnlPregledAktivnostIzabran.Visible = false;

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
            lblDatum.Size = new Size(btnPlus.Width * 2 - 20, 20);
            lblDatum.Location = new Point(252, 225 + btnKalendar.Height);
            lblDatum.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lblDatum.SendToBack();
            Controls.Add(lblDatum);

            //
            //  pnlUpisOcene
            //
            {
                pnlDizajn(pnlUpisOcene);
                Controls.Add(pnlUpisOcene);

                //
                //  cbOcene
                //
                cbOcenaDizajn(cbOcene);
                pnlUpisOcene.Controls.Add(cbOcene);

            }

            //
            //  pnlUpisOceneIzabran
            //
            {
                pnlDizajn(pnlUpisOceneIzabran);
                Controls.Add(pnlUpisOceneIzabran);

                //
                //  cbOceneIzabran
                //
                cbOcenaDizajn(cbOceneIzabran);
                pnlUpisOceneIzabran.Controls.Add(cbOceneIzabran);

                //
                //  dgvOceneIzabran
                //
                dgvAktivnostDizajn(dgvOceneIzabran);
                dgvOceneIzabran.Columns[0].Name = "Ocena";
                dgvOceneIzabran.Columns[1].Name = "Datum";
                pnlUpisOceneIzabran.Controls.Add(dgvOceneIzabran);
            }

            //
            //  pnlUpisAktivnost
            //
            {
                pnlDizajn(pnlUpisAktivnost);
                Controls.Add(pnlUpisAktivnost);

                //
                //  lblAktivnost
                //
                lblAktivnostDizajn(lblAktivnost);
                pnlUpisAktivnost.Controls.Add(lblAktivnost);

                //
                //  lbUceniciAktivnost
                //
                lbUceniciAktivnostDizajn(lbUceniciAktivnost);
                lbUceniciAktivnost.Click += LbUceniciAktivnost_Click;
                lbUceniciAktivnost.BackColor = Color.Red;
                pnlUpisAktivnost.Controls.Add(lbUceniciAktivnost);

                //
                //  btnPlus
                //
                btnAktivnostDizajn(btnPlus, "+");
                pnlUpisAktivnost.Controls.Add(btnPlus);

                //
                //  btnMinus
                //
                btnAktivnostDizajn(btnMinus, "-");
                pnlUpisAktivnost.Controls.Add(btnMinus);
            }

            //
            //  pnlUpisAktivnostIzabran
            //
            {
                pnlDizajn(pnlUpisAktivnostIzabran);
                Controls.Add(pnlUpisAktivnostIzabran);

                //
                //  dgvAktivnost
                //
                dgvAktivnostDizajn(dgvAktivnostIzabran);
                dgvAktivnostIzabran.Columns[0].Name = "Aktivnost";
                dgvAktivnostIzabran.Columns[1].Name = "Datum";
                pnlUpisAktivnostIzabran.Controls.Add(dgvAktivnostIzabran);

                //
                //  lblAktivnostIzabran
                //
                lblAktivnostDizajn(lblAktivnostIzabran);
                pnlUpisAktivnostIzabran.Controls.Add(lblAktivnostIzabran);

                //
                //  btnPlusIzabran
                //
                btnAktivnostDizajn(btnPlusIzabran, "+");
                pnlUpisAktivnostIzabran.Controls.Add(btnPlusIzabran);

                //
                //  btnMinusIzabran
                //
                btnAktivnostDizajn(btnMinusIzabran, "-");
                pnlUpisAktivnostIzabran.Controls.Add(btnMinusIzabran);
            }

            //
            //  pnlPregledOcene
            //
            {
                pnlDizajn(pnlPregledOcene);
                Controls.Add(pnlPregledOcene);
            }

            //
            //  pnlPregledOceneIzabran
            //
            {
                pnlDizajn(pnlPregledOceneIzabran);
                Controls.Add(pnlPregledOceneIzabran);
            }

            //
            //  pnlPregledAktivnost
            //
            {
                pnlDizajn(pnlPregledAktivnost);
                Controls.Add(pnlPregledAktivnost);

            }

            //
            //  pnlPregledAktivnostIzabran
            //
            {
                pnlDizajn(pnlPregledAktivnostIzabran);
                Controls.Add(pnlPregledAktivnostIzabran);

            }
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
            lbUceniciOcene.Size = new Size(pnlUpisOcene.Width / 2 - 20, pnlUpisOcene.Height - 20);
            pnlUpisOcene.Controls.Add(lbUceniciOcene);

            //
            //  lbOceneIzabran
            //
            dgvOceneIzabran.ColumnCount = 2;
            dgvOceneIzabran.Columns[0].Width = 70;
            dgvOceneIzabran.Columns[1].Width = 98;
            dgvOceneIzabran.RowTemplate.Height = 30;
            dgvOceneIzabran.ColumnHeadersHeight = 30;
            dgvOceneIzabran.Columns[0].Name = "Ocena";
            dgvOceneIzabran.Columns[1].Name = "Datum";
            dgvOceneIzabran.Location = new Point(10, 10);
            dgvOceneIzabran.BorderStyle = BorderStyle.None;
            dgvOceneIzabran.Size = new Size(pnlUpisOcene.Width / 2 + 14, 52);
            dgvOceneIzabran.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            pnlOceneIzabran.Controls.Add(dgvOceneIzabran);

            btnKalendar.Size = new Size(btnMinus.Width, btnMinus.Height);
            /*
            if (ucenik == "")
            {
                if (prikaz == "Aktivnost")
                {
                    panel = 1;
                    pnlUpisOcene.Visible = false;
                    pnlPregledAktivnost.Visible = true;
                    pnlOceneIzabran.Visible = false;
                    pnlAktivnostIzabran.Visible = false;
                }
                else if (prikaz == "Ocene")
                {
                    panel = 2; 
                    pnlUpisOcene.Visible = true;
                    pnlPregledAktivnost.Visible = false;
                    pnlOceneIzabran.Visible = false;
                    pnlAktivnostIzabran.Visible = false;
                }
            }
            else
            {
                if (prikaz == "Aktivnost")
                {
                    panel = 3;
                    pnlUpisOcene.Visible = false;
                    pnlPregledAktivnost.Visible = false;
                    pnlOceneIzabran.Visible = false;
                    pnlAktivnostIzabran.Visible = true;
                }
                else if (prikaz == "Ocene")
                {
                    panel = 4;
                    pnlUpisOcene.Visible = false;
                    pnlPregledAktivnost.Visible = false;
                    pnlOceneIzabran.Visible = true;
                    pnlAktivnostIzabran.Visible = false;
                }
            }
            */
            if (ucenik != "")
            {
                if (upisPregled == "Pregled")
                {
                    if (prikaz == "Aktivnost")
                    {
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = true;

                    }
                    else if (prikaz == "Ocene")
                    {
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;

                    }
                }
                else if (upisPregled == "Upis")
                {
                    if (prikaz == "Aktivnost")
                    {
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = true;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;

                    }
                    else if (prikaz == "Ocene")
                    {
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = true;
                        pnlPregledAktivnost.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;

                    }
                }
            }
            else if (ucenik == "")
            {
                if (upisPregled == "Pregled")
                {
                    if (prikaz == "Aktivnost")
                    {
                        panel = 2;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlPregledAktivnost.Visible = true;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;

                    }
                    else if (prikaz == "Ocene")
                    {
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;

                    }
                }
                else if (upisPregled == "Upis")
                {
                    if (prikaz == "Aktivnost")
                    {
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = true;
                        pnlPregledAktivnostIzabran.Visible = false;

                    }
                    else if (prikaz == "Ocene")
                    {
                        pnlUpisOcene.Visible = true;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;

                    }
                }
            }
        }
        private void pnlDizajn(Panel panel)
        {
            panel.Click += Deselect_Click;
            panel.Location = new Point(30, 50);
            panel.Size = new Size(ClientSize.Width - 60, ClientSize.Height - 95);
        }
        private void lblAktivnostDizajn(Label label)
        {
            label.Text = "Plusevi: - Minusevi: -";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Size = new Size(btnPlus.Width * 2 + 10, 50);
            label.Location = new Point((ClientSize.Width - 60) / 2, 45);
            label.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
        }
        private void lbUceniciAktivnostDizajn(ListBox listBox)
        {
            listBox.BackColor = Color.Red;
            listBox.Location = new Point(10, 10);
            listBox.BorderStyle = BorderStyle.None;
            listBox.BackColor = Color.FromArgb(241, 242, 243);
            listBox.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            listBox.Size = new Size((ClientSize.Width - 60) / 2 - 20, (ClientSize.Height - 95) - 20);
        }
        private void btnAktivnostDizajn(Button button, string znak)
        {
            if(znak == "+")
            {
                button.Text = "+";
                button.Click += BtnPlus_Click;
                button.Location = new Point((5 * (ClientSize.Width - 60) / 8) - (button.Width / 2 + 5), (ClientSize.Height - 95) / 2);
            }
            else
            {
                button.Text = "-";
                button.Click += BtnMinus_Click;
                button.Location = new Point((5 * (ClientSize.Width - 60) / 8) + 5 + button.Width / 2, (ClientSize.Height - 95) / 2);
            }
            button.AutoSize = true;
            button.Font = new Font("Times New Roman", 15F, FontStyle.Bold);
        }
        private void BtnPlus_Click(object sender, EventArgs e)
        {
            Znak = "+";
            
            btnPlus.ForeColor = Color.DarkGreen;
            btnPlus.BackColor = Color.LightGreen;
            btnPlusIzabran.ForeColor = Color.DarkGreen;
            btnPlusIzabran.BackColor = Color.LightGreen;

            btnMinus.ForeColor = Color.Black;
            btnMinus.BackColor = Color.Transparent;
            btnMinusIzabran.ForeColor = Color.Black;
            btnMinusIzabran.BackColor = Color.Transparent;
        }
        private void BtnMinus_Click(object sender, EventArgs e)
        {
            Znak = "-";

            btnPlus.ForeColor = Color.Black;
            btnPlus.BackColor = Color.Transparent;
            btnPlusIzabran.ForeColor = Color.Black;
            btnPlusIzabran.BackColor = Color.Transparent;
            
            btnMinus.ForeColor = Color.DarkRed;
            btnMinus.BackColor = Color.FromArgb(255, 133, 133);
            btnMinusIzabran.ForeColor = Color.DarkRed;
            btnMinusIzabran.BackColor = Color.FromArgb(255, 133, 133);
        }
        private void cbOcenaDizajn(ComboBox comboBox)
        {
            comboBox.Text = "Izaberite ocenu";
            comboBox.Items.Add("Izaberite ocenu");
            comboBox.Items.Add("1");
            comboBox.Items.Add("2");
            comboBox.Items.Add("3");
            comboBox.Items.Add("4");
            comboBox.Items.Add("5");
            comboBox.Font = new Font("Times New Roman", 12F, FontStyle.Regular);
            comboBox.Location = new Point((5 * (ClientSize.Width - 60) / 8) - (btnPlus.Width / 8 + 5), (ClientSize.Height - 95) / 2 - comboBox.Height);
        }
        private void dgvAktivnostDizajn(DataGridView dataGridView)
        {
            dataGridView.ColumnCount = 2;
            dataGridView.Columns[0].Width = 90;
            dataGridView.Columns[1].Width = 98;
            dataGridView.RowTemplate.Height = 30;
            dataGridView.ColumnHeadersHeight = 30;
            dataGridView.Location = new Point(10, 10);
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.Size = new Size((ClientSize.Width - 20) / 2 + 14, 152);
            dataGridView.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
        }
        private void LbUceniciAktivnost_Click(object sender, EventArgs e)
        {
            var dataBase = new DataBase();
            int brojacPlus = 0;
            int brojacMinus = 0;
            dataBase.conn.Open();
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
            dataBase.conn.Close();

            lblAktivnost.Text = "Plusevi: " + brojacPlus + " Minusevi: " + brojacMinus;
        }
        private void loadData()
        {
            var dataBase = new DataBase();
            dataBase.conn.Open();
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
                dataBase.conn.Close();
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
                {
                    lbUceniciOcene.Items.Add(reader.GetString("Username"));

                }
                reader.Close();
                dataBase.conn.Close();
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
                    //lbAktivnostIzabran.Items.Add("'" + reader.GetString("Aktivnost") + "' - " + reader.GetString("Datum"));

                    if (reader.GetString("Aktivnost") == "+")
                        brojacPlus++;
                    else if (reader.GetString("Aktivnost") == "-")
                        brojacMinus++;

                }
                reader.Close();
                dataBase.conn.Close();
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
                {
                    dgvOceneIzabran.Rows.Add(reader.GetString("Ocena"), reader.GetString("Datum"));
                    dgvOceneIzabran.Height += 30;
                }
                reader.Close();
                dataBase.conn.Close();
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (panel == 1)
            {
                if (Znak != "" && (string) lbUceniciAktivnost.SelectedItem != "" && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.conn.Open();
                    string query = "INSERT INTO `aktivnost`(`id`, `Ucenik`, `Aktivnost`, `Predmet`, `Datum`) VALUES ('" + null + "','" + lbUceniciAktivnost.SelectedItem + "','" + Znak + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.conn.Close();
                }
            }
            else if (panel == 2)
            {
                if (cbOcene.SelectedIndex != -1 && (string) lbUceniciOcene.SelectedItem != "" && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.conn.Open();
                    string query = "INSERT INTO `ocene`(`id`, `Ocena`, `Ucenik`, `Predmet`, `Datum`) VALUES ('" + null + "','" + cbOcene.SelectedIndex + "','" + lbUceniciOcene.SelectedItem + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.conn.Close();
                }
            }
            else if (panel == 3)
            {
                if (Znak != "" && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.conn.Open();
                    string query = "INSERT INTO `aktivnost`(`id`, `Ucenik`, `Aktivnost`, `Predmet`, `Datum`) VALUES ('" + null + "','" + ucenik + "','" + Znak + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.conn.Close();
                    //lbAktivnostIzabran.Items.Clear();
                    loadData();
                }
            }
            else if (panel == 4)
            {
                if (cbOceneIzabran.SelectedIndex != -1 && lblDatum.Text != "")
                {
                    var dataBase = new DataBase();
                    dataBase.conn.Open();
                    string query = "INSERT INTO `ocene`(`id`, `Ocena`, `Ucenik`, `Predmet`, `Datum`) VALUES ('" + null + "','" + cbOceneIzabran.SelectedIndex + "','" + ucenik + "','" + predmet + "','" + lblDatum.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    cmd.CommandText = query;
                    adapter.Fill(dt);
                    dataBase.conn.Close();
                    dgvOceneIzabran.Rows.Clear();
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
