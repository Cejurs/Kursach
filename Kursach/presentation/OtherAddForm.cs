using Kursach.domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach.presentation
{
    public partial class OtherAddForm : Form
    {
        private IRepository repository;
        public OtherAddForm(IRepository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var desc = textBox1.Text;
            if (string.IsNullOrEmpty(desc))
            {
                MessageBox.Show("Поле Задача должно быть заполнено");
            }
            if (!DateTime.TryParseExact(textBox2.Text, "d", CultureInfo.CurrentCulture,
                              DateTimeStyles.None, out var date))
            {
                MessageBox.Show("Некорректная дата");
                return;
            }
            var dateS = date.ToString("d");
            repository.Add("OtherThings", desc, dateS);
            var mainForm = (System.Windows.Forms.Application.OpenForms["form1"] as Form1);
            mainForm.UpdateGrids(mainForm.lastDate);
            this.Close();
        }
    }
}
