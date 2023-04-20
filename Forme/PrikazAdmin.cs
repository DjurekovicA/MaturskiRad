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

        private DataGridView dgvOceneIzabranUpis = new DataGridView();
        private DataGridView dgvAktivnostIzabranUpis = new DataGridView();

        string ucenik = "";
        string prikaz = "";
        string predmet = "";
        string Znak = "";
        string upisPregled = "";
        int panel = 0;
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
        private void Prikaz()
        {
            if (ucenik != "")
            {
                if (upisPregled == "Pregled")
                {
                    btnAdd.Hide();
                    lblDatum.Hide();
                    btnKalendar.Hide();
                    if (prikaz == "Aktivnost")
                    {
                        panel = 1;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledOcene.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlPregledOceneIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = true;
                    }
                    else if (prikaz == "Ocene")
                    {
                        panel = 2;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledOcene.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlPregledOceneIzabran.Visible = true;
                        pnlPregledAktivnostIzabran.Visible = false;
                    }
                }
                else if (upisPregled == "Upis")
                {
                    if (prikaz == "Aktivnost")
                    {
                        panel = 3;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = true;
                        pnlPregledOcene.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlPregledOceneIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;
                    }
                    else if (prikaz == "Ocene")
                    {
                        panel = 4;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = true;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledOcene.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlPregledOceneIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;
                    }
                }
            }
            else if (ucenik == "")
            {
                if (upisPregled == "Pregled")
                {
                    btnAdd.Hide();
                    lblDatum.Hide();
                    btnKalendar.Hide();
                    if (prikaz == "Aktivnost")
                    {
                        panel = 5;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledOcene.Visible = false;
                        pnlPregledAktivnost.Visible = true;
                        pnlPregledOceneIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;
                    }
                    else if (prikaz == "Ocene")
                    {
                        panel = 6;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledOcene.Visible = true;
                        pnlPregledAktivnost.Visible = false;
                        pnlPregledOceneIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;
                    }
                }
                else if (upisPregled == "Upis")
                {
                    if (prikaz == "Aktivnost")
                    {
                        panel = 7;
                        pnlUpisOcene.Visible = false;
                        pnlUpisAktivnost.Visible = true;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledOcene.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlPregledOceneIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;
                    }
                    else if (prikaz == "Ocene")
                    {
                        panel = 8;
                        pnlUpisOcene.Visible = true; 
                        pnlUpisAktivnost.Visible = false;
                        pnlUpisOceneIzabran.Visible = false;
                        pnlUpisAktivnostIzabran.Visible = false;
                        pnlPregledOcene.Visible = false;
                        pnlPregledAktivnost.Visible = false;
                        pnlPregledOceneIzabran.Visible = false;
                        pnlPregledAktivnostIzabran.Visible = false;
                    }
                }
            }
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

            Prikaz();

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
            btnKalendar.Location = new Point(300, 215);
            btnKalendar.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            Controls.Add(btnKalendar);

            //
            //  lblDatum
            //
            lblDatum.TextAlign = ContentAlignment.MiddleCenter;
            lblDatum.Size = new Size(btnPlus.Width * 2 - 20, 20);
            lblDatum.Location = new Point(274, 225 + btnKalendar.Height);
            lblDatum.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            lblDatum.SendToBack();
            Controls.Add(lblDatum);

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

                //
                //  lbUceniciOcene
                //
                lbUceniciAktivnostDizajn(lbUceniciOcene);
                pnlUpisOcene.Controls.Add(lbUceniciOcene);
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
                //  dgvOceneIzabranUpis
                //
                dodajDGV(pnlUpisOceneIzabran, dgvOceneIzabranUpis, 0);
                dgvOceneIzabranUpis.Columns[0].Name = "Ocena";
            }
            
            //
            //  pnlUpisAktivnostIzabran
            //
            {
                pnlDizajn(pnlUpisAktivnostIzabran);
                Controls.Add(pnlUpisAktivnostIzabran);

                //
                //  dgvAktivnostIzabranUpis
                //
                dodajDGV(pnlUpisAktivnostIzabran, dgvAktivnostIzabranUpis, 0);
                dgvAktivnostIzabranUpis.Columns[0].Name = "Aktivnost";

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

                //
                //  dgvOcene
                //
                dodajDGV(pnlPregledOcene, dgvOcene, 13);
            }

            //
            //  pnlPregledAktivnost
            //
            {
                pnlDizajn(pnlPregledAktivnost);
                Controls.Add(pnlPregledAktivnost);

                //
                //  dgvAktivnost
                //
                dodajDGV(pnlPregledAktivnost, dgvAktivnost, 23);
            }

            //
            //  pnlPregledOceneIzabran
            //
            {
                pnlDizajn(pnlPregledOceneIzabran);
                Controls.Add(pnlPregledOceneIzabran);

                //
                //  dgvOceneIzabran
                //
                dodajDGV(pnlPregledOceneIzabran, dgvOceneIzabran, 12);
            }

            //
            //  pnlPregledAktivnostIzabran
            //
            {
                pnlDizajn(pnlPregledAktivnostIzabran);
                Controls.Add(pnlPregledAktivnostIzabran);

                //
                //  dgvAktivnostIzabran
                //
                dodajDGV(pnlPregledAktivnostIzabran, dgvAktivnostIzabran, 22);
            }

            btnKalendar.Size = new Size(btnMinus.Width, btnMinus.Height);
        }
        private void pnlDizajn(Panel panel)
        {
            panel.Click += Deselect_Click;
            panel.Location = new Point(15, 50);
            panel.Size = new Size(ClientSize.Width - 30, ClientSize.Height - 95);
        }
        private void lblAktivnostDizajn(Label label)
        {
            label.Text = "Plusevi: - Minusevi: -";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Size = new Size(btnPlus.Width * 2 + 10, 50);
            label.Location = new Point((ClientSize.Width - 60) / 2 + 50, 45);
            label.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
        }
        private void lbUceniciAktivnostDizajn(ListBox listBox)
        {
            loadUcenici(listBox);
            listBox.Click += LbUcenici_Click;
            listBox.Location = new Point(25, 10);
            listBox.BorderStyle = BorderStyle.None;
            listBox.BackColor = Color.FromArgb(241, 242, 243);
            listBox.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            listBox.Size = new Size((ClientSize.Width - 60) / 2 - 10, (ClientSize.Height - 95) - 20);
        }
        private void btnAktivnostDizajn(Button button, string znak)
        {
            if(znak == "+")
            {
                button.Text = "+";
                button.Click += BtnPlus_Click;
                button.Location = new Point((5 * (ClientSize.Width - 60) / 8) - (button.Width / 2 + 5) + 45, (ClientSize.Height - 95) / 2);
            }
            else if (znak == "-")
            {
                button.Text = "-";
                button.Click += BtnMinus_Click;
                button.Location = new Point((5 * (ClientSize.Width - 60) / 8) + 5 + button.Width / 2 + 45, (ClientSize.Height - 95) / 2);
            }
            button.AutoSize = true;
            button.Font = new Font("Times New Roman", 15F, FontStyle.Bold);
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
            comboBox.Location = new Point((5 * (ClientSize.Width - 60) / 8) - (btnPlus.Width / 8 + 5) + 30, (ClientSize.Height - 95) / 2 - comboBox.Height);
        }
        private void dgvDizajn(DataGridView dataGridView, int br)
        {
            dataGridView.ColumnCount = 2;
            dataGridView.Columns[0].Width = 90;
            dataGridView.Columns[1].Width = 98;
            dataGridView.RowTemplate.Height = 30;
            dataGridView.ColumnHeadersHeight = 30;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.Font = new Font("Times New Roman", 15F, FontStyle.Regular);
            if (br == 0)
            {
                dataGridView.Location = new Point(10, 10);
                dataGridView.Columns[1].Name = "Datum";
            }
            else if (br == 12)
            {
                dataGridView.Columns[0].Name = "Ocene";
                dataGridView.Columns[1].Name = "Datum";
            }
            else if (br == 13)
            {
                dataGridView.ColumnCount = 3;
                dataGridView.Columns[0].Width = 120;
                dataGridView.Columns[0].Name = "Učenik";
                dataGridView.Columns[1].Name = "Ocene";
                dataGridView.Columns[2].Name = "Datum";

            }
            else if (br == 22)
            {
                dataGridView.Columns[0].Name = "Aktivnost";
                dataGridView.Columns[1].Name = "Datum";
            }
            else if (br == 23)
            {
                dataGridView.ColumnCount = 3;
                dataGridView.Columns[0].Width = 120;
                dataGridView.Columns[1].Width = 63;
                dataGridView.Columns[2].Width = 58;
                dataGridView.Columns[0].Name = "Učenik";
                dataGridView.Columns[1].Name = "Br.  +";
                dataGridView.Columns[2].Name = "Br. -";

            }
            if (br == 0 || br == 12 || br == 22)
                dataGridView.Size = new Size(dataGridView.Columns[0].Width + dataGridView.Columns[1].Width + 41, 53);
            else if (br == 13 || br == 23)
                dataGridView.Size = new Size(dataGridView.Columns[0].Width + dataGridView.Columns[1].Width + dataGridView.Columns[2].Width + 41, 53);
            if (br != 0)
            {
                dataGridView.Location = new Point((pnlPregledOcene.Width - dataGridView.Width) / 2, 10);
                dataGridView.MaximumSize = new Size(pnlPregledOcene.Width - 20, pnlPregledOcene.Height - 20);
            }
        }
        private void dodajDGV(Panel panel, DataGridView dataGridView, int broj)
        {
            dgvDizajn(dataGridView, broj);
            panel.Controls.Add(dataGridView);
        } 
        private void LbUcenici_Click(object sender, EventArgs e)
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
        private void PrikazAdmin_Load(object sender, EventArgs e)
        {

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
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (panel == 3)
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
                }
                pnlUpisAktivnostIzabran.Controls.Remove(dgvAktivnostIzabranUpis);
                dodajDGV(pnlUpisAktivnostIzabran, dgvAktivnostIzabranUpis, 0);
                Znak = "";
                lblDatum.Text = "";
                btnPlusIzabran.ForeColor = Color.Black;
                btnPlusIzabran.BackColor = Color.Transparent;
                btnMinusIzabran.ForeColor = Color.Black;
                btnMinusIzabran.BackColor = Color.Transparent;
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
                }
                pnlUpisOceneIzabran.Controls.Remove(dgvOceneIzabranUpis);
                dodajDGV(pnlUpisOceneIzabran, dgvOceneIzabranUpis, 0);
                lblDatum.Text = "";
                cbOceneIzabran.SelectedItem = 0;
            }
            else if (panel == 7)
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
                Znak = "";
                lblDatum.Text = "";
                btnPlus.ForeColor = Color.Black;
                btnPlus.BackColor = Color.Transparent;
                btnMinus.ForeColor = Color.Black;
                btnMinus.BackColor = Color.Transparent;
            }
            else if (panel == 8)
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
                lblDatum.Text = "";
                cbOcene.SelectedItem = 0;
            }
            loadData();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void loadUcenici(ListBox listBox)
        {
            var dataBase = new DataBase();
            dataBase.conn.Open();
            string query = "SELECT `Username` FROM `korisnik` WHERE `role` = 'user'";
            MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                listBox.Items.Add(reader.GetString("Username"));
            reader.Close();
            dataBase.conn.Close();
        }
        private void loadData()
        {
            var dataBase = new DataBase();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            if (panel == 1)
            {
                dataBase.conn.Open();
                string query = "SELECT * FROM `aktivnost` WHERE `Ucenik` = '" + ucenik + "' AND `Predmet` = '" + predmet + "'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dgvAktivnostIzabran.Rows.Add(reader.GetString("Aktivnost"), reader.GetString("Datum"));
                    if (dgvAktivnostIzabran.Height < dgvAktivnostIzabran.MaximumSize.Height)
                        dgvAktivnostIzabran.Height += 30;
                }
                if (dgvAktivnostIzabran.Height >= dgvAktivnostIzabran.MaximumSize.Height)
                    dgvAktivnostIzabran.Width += 17;
                reader.Close();
                dataBase.conn.Close();
            }
            else if (panel == 2)
            {
                dataBase.conn.Open();
                string query = "SELECT `Ocena`, `Ucenik`, `Datum` FROM `ocene` WHERE `Ucenik`= '" + ucenik + "' AND `Predmet` = '" + predmet + "'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dgvOceneIzabran.Rows.Add(reader.GetString("Ocena"), reader.GetString("Datum"));
                    if (dgvOceneIzabran.Height < dgvOceneIzabran.MaximumSize.Height)
                        dgvOceneIzabran.Height += 30;
                }
                if (dgvOceneIzabran.Height >= dgvOceneIzabran.MaximumSize.Height)
                    dgvOceneIzabran.Width += 17;
                reader.Close();
                dataBase.conn.Close();
            }
            else if (panel == 3)
            {
                int brPlus = 0;
                int brMinus = 0;
                dataBase.conn.Open();
                string query = "SELECT * FROM `aktivnost` WHERE `Ucenik`= '" + ucenik + "' AND `Predmet` = '" + predmet + "'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString("Aktivnost") == "+")
                        brPlus++;
                    else if (reader.GetString("Aktivnost") == "-")
                        brMinus++;
                    dgvAktivnostIzabranUpis.Rows.Add(reader.GetString("Aktivnost"), reader.GetString("Datum"));
                    if (dgvAktivnostIzabranUpis.Height < 210)
                        dgvAktivnostIzabranUpis.Height += 30;
                }
                if (dgvAktivnostIzabranUpis.Height < 210)
                {
                    dgvAktivnostIzabranUpis.Width += 17;
                    dgvAktivnostIzabranUpis.Location = new Point(0, 10);
                }
                lblAktivnostIzabran.Text = "Plusevi: " + brPlus + " Minusevi: " + brMinus;
                reader.Close();
                dataBase.conn.Close();
            }
            else if (panel == 4)
            {
                dataBase.conn.Open();
                string query = "SELECT `Ocena`, `Ucenik`, `Datum` FROM `ocene` WHERE `Ucenik`= '" + ucenik + "' AND `Predmet` = '" + predmet + "'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dgvOceneIzabranUpis.Rows.Add(reader.GetString("Ocena"), reader.GetString("Datum"));
                    if (dgvOceneIzabranUpis.Height < 210)
                        dgvOceneIzabranUpis.Height += 30;
                }
                if (dgvOceneIzabranUpis.Height >= 210)
                    dgvOceneIzabranUpis.Width += 17;
                reader.Close();
                dataBase.conn.Close();
            }
            else if (panel == 5)
            {
                dataBase.conn.Open();
                string query = "SELECT * FROM `korisnik`WHERE `Role` = 'user'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int brPlus = 0;
                    int brMinus = 0;
                    var dbase = new DataBase();
                    dbase.conn.Open();
                    string Query = "SELECT * FROM `aktivnost` WHERE `Ucenik`= '" + reader.GetString("Username") + "' AND `Predmet` = '" + predmet + "'";
                    MySqlCommand mycmd = new MySqlCommand(Query, dbase.conn);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(mycmd);
                    DataTable dataTable = new DataTable();
                    myAdapter.Fill(dataTable);
                    MySqlDataReader myreader = mycmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        if (myreader.GetString("Aktivnost") == "+")
                            brPlus++;
                        else if (myreader.GetString("Aktivnost") == "-")
                            brMinus++;
                    }
                    dgvAktivnost.Rows.Add(reader.GetString("Username"), brPlus.ToString(), brMinus.ToString());
                    if (dgvAktivnost.Height < dgvAktivnost.MaximumSize.Height)
                        dgvAktivnost.Height += 30;
                    myreader.Close();
                    dbase.conn.Close();
                }

                if (dgvAktivnost.Height >= dgvAktivnost.MaximumSize.Height)
                    dgvAktivnost.Width += 17;
                reader.Close();
                dataBase.conn.Close();
            }
            else if (panel == 6)
            {
                dataBase.conn.Open();
                string query = "SELECT `Ocena`, `Ucenik`, `Datum` FROM `ocene` WHERE `Predmet` = '" + predmet + "'";
                MySqlCommand cmd = new MySqlCommand(query, dataBase.conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dgvOcene.Rows.Add(reader.GetString("Ucenik"), reader.GetString("Ocena"), reader.GetString("Datum"));
                    if (dgvOcene.Height < dgvOcene.MaximumSize.Height)
                        dgvOcene.Height += 30;
                }
                if (dgvOcene.Height >= dgvOcene.MaximumSize.Height)
                    dgvOcene.Width += 17;
                reader.Close();
                dataBase.conn.Close();
            } 
        }
    }
}
