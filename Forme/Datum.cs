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
    public partial class Datum : Form
    {
        private Button btnSave = new Button();
        private Button btnClose = new Button();
        private DateTimePicker MonthCalendar = new DateTimePicker();

        public string ReturnValue { get; set; }

        public Datum()
        {
            Dizajn();
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
            ClientSize = new Size(300, 300);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;

            //
            //  btnClose
            //
            btnClose.Text = "X";
            btnClose.Size = new Size(30, 30);
            btnClose.Click += BtnClose_Click;
            btnClose.Font = new Font("Times New Roman", 10F, FontStyle.Regular);
            btnClose.Location = new Point(ClientSize.Width - btnClose.Width - 10, 10);
            Controls.Add(btnClose);

            //
            //  btnSave
            //
            btnSave.AutoSize = true;
            btnSave.Text = "Sačuvaj";
            btnSave.Click += BtnSave_Click;
            btnSave.Font = new Font("Times New Roman", 12F, FontStyle.Regular);
            btnSave.Location = new Point(ClientSize.Width - btnSave.Width - 10, ClientSize.Height - btnSave.Height - 15);
            Controls.Add(btnSave);

            //
            //  MonthCalendar
            //
            MonthCalendar.Location = new Point((ClientSize.Width - MonthCalendar.Width) / 2, ClientSize.Height/ 4);
            Controls.Add(MonthCalendar);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            ReturnValue = MonthCalendar.Value.ToShortDateString();
            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
