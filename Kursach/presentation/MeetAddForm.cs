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
    public partial class MeetAddForm : Form
    {
        private IRepository repository;
        public MeetAddForm(IRepository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var firstName = textBox1.Text;
            if (string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Поле Фамилия не может быть пустым");
                return;
            }
            var lastName = textBox2.Text;
            if (string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Поле Имя не может быть пустым");
                return;
            }
            var middleName = textBox3.Text;
            if (string.IsNullOrEmpty(middleName))
            {
                MessageBox.Show("Поле Отчество не может быть пустым");
                return;
            }

            if (!DateTime.TryParseExact(textBox4.Text, "d", CultureInfo.CurrentCulture,
                              DateTimeStyles.None, out var date))
            {
                MessageBox.Show("Некорректная дата");
                return;
            }
            var dateS = date.ToString("d");
            var desc = textBox5.Text;

            repository.Add("Meets", firstName, lastName, middleName, dateS, desc);
            var mainForm = (System.Windows.Forms.Application.OpenForms["form1"] as Form1);
            mainForm.UpdateGrids(mainForm.lastDate);
            this.Close();
        }
    }
}
