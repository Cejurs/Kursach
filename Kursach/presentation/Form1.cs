using Kursach.domain;
using Kursach.presentation;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Kursach
{
    public partial class Form1 : Form
    {
        private IRepository repository;
        public string lastDate;
        public Form1(IRepository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        public void UpdateGrids(string date)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            var calls = repository.GetItemsByDate("Calls", date).Select(x => (Call)x).ToArray();

            foreach (var call in calls)
            {
                dataGridView1.Rows.Add(call.Id, call.FirstName, call.LastName, call.MidleName,
                    call.PhoneNumber, call.Description, call.Status, call.Date);
            }
            var meets = repository.GetItemsByDate("Meets", date).Select(x => (Meet)x).ToArray();
            foreach (var meet in meets)
            {
                dataGridView2.Rows.Add(meet.Id, meet.FirstName, meet.LastName, meet.MidleName,
                    meet.Description, meet.Status,meet.Date);
            }
            var toDoList = repository.GetItemsByDate("ToDoList", date).Select(x => (ToDo)x).ToArray();
            foreach(var toDo in toDoList)
            {
                dataGridView3.Rows.Add(toDo.Id, toDo.Description, toDo.Status, toDo.Date);
            }
            var other = repository.GetItemsByDate("OtherThings", date).Select(x => (OtherThing)x).ToArray();
            foreach(var otherThing in other)
            {
                dataGridView4.Rows.Add(otherThing.Id, otherThing.Description, otherThing.Status, otherThing.Date);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lastDate = DateTime.Now.ToString("d");
            toolStripTextBox1.Text = lastDate;
            UpdateGrids(lastDate);
        }

        private void звонокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var callAddForm = new CallAddForm(repository);
            callAddForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var date = toolStripTextBox1.Text;
            if (!DateTime.TryParseExact(date,"d",CultureInfo.CurrentCulture,
                              DateTimeStyles.None,out var value))
            {
                MessageBox.Show("Некоректнаяя дата", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lastDate = date;
            UpdateGrids(date);

        }
        
        private void DeleteRow(string tableName,int id)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                repository.Delete(tableName,id);
            }
            UpdateGrids(lastDate);
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            var id = int.Parse(e.Row.Cells[0].Value.ToString());
            DeleteRow("Calls", id);
        }

        private void dataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            var id = int.Parse(e.Row.Cells[0].Value.ToString());
            DeleteRow("Meets", id);
        }

        private void dataGridView3_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            var id = int.Parse(e.Row.Cells[0].Value.ToString());
            DeleteRow("ToDoList", id);
        }

        private void dataGridView4_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            var id = int.Parse(e.Row.Cells[0].Value.ToString());
            DeleteRow("OtherThings", id);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var call = new Call()
            {
                Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()), // xскрытое поле
                FirstName = (dataGridView1.Rows[rowIndex].Cells[1].Value?? "").ToString(),
                LastName= (dataGridView1.Rows[rowIndex].Cells[2].Value?? "").ToString(),
                MidleName= (dataGridView1.Rows[rowIndex].Cells[3].Value?? "").ToString(),
                PhoneNumber= (dataGridView1.Rows[rowIndex].Cells[4].Value?? "").ToString(),
                Description= (dataGridView1.Rows[rowIndex].Cells[5].Value?? "").ToString(),
                Status= bool.Parse(dataGridView1.Rows[rowIndex].Cells[6].Value.ToString()),
                Date= dataGridView1.Rows[rowIndex].Cells[7].Value.ToString(), // скрытое поле
            };
            repository.Update("Calls", call as object);
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var meet = new Meet()
            {
                Id = int.Parse(dataGridView2.Rows[rowIndex].Cells[0].Value.ToString()), // скрытое поле
                FirstName = (dataGridView2.Rows[rowIndex].Cells[1].Value?? "").ToString(),
                LastName = (dataGridView2.Rows[rowIndex].Cells[2].Value?? "").ToString(),
                MidleName = (dataGridView2.Rows[rowIndex].Cells[3].Value?? "").ToString(),
                Description = (dataGridView2.Rows[rowIndex].Cells[4].Value?? "").ToString(),
                Status = bool.Parse(dataGridView2.Rows[rowIndex].Cells[5].Value.ToString()),
                Date = (dataGridView2.Rows[rowIndex].Cells[6].Value?? "").ToString(), // скрытое поле
            };
            repository.Update("Meets", meet as object);
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var toDo = new ToDo()
            {
                Id = int.Parse(dataGridView3.Rows[rowIndex].Cells[0].Value.ToString()), // скрытое поле
                Description = (dataGridView3.Rows[rowIndex].Cells[1].Value?? "").ToString(),
                Status = bool.Parse(dataGridView3.Rows[rowIndex].Cells[2].Value.ToString()),
                Date = (dataGridView3.Rows[rowIndex].Cells[3].Value?? "").ToString(), // скрытое поле
            };
            repository.Update("ToDoList", toDo as object);
        }

        private void встречаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var meetAddForm = new MeetAddForm(repository);
            meetAddForm.Show();
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var other = new OtherThing()
            {
                Id = int.Parse(dataGridView4.Rows[rowIndex].Cells[0].Value.ToString()), // скрытое поле
                Description = (dataGridView4.Rows[rowIndex].Cells[1].Value ?? "").ToString(),
                Status = bool.Parse(dataGridView4.Rows[rowIndex].Cells[2].Value.ToString()),
                Date = (dataGridView4.Rows[rowIndex].Cells[3].Value ?? "").ToString(), // скрытое поле
            };
            repository.Update("OtherThings", other as object);
        }

        private void делоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toDoAddForm = new ToDoAddForm(repository);
            toDoAddForm.Show();
        }

        private void прочееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var otherAddForm = new OtherAddForm(repository);
            otherAddForm.Show();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для добавления записи - клинуть на клавишу добавить запись \n Для удаления записи - выделить строку и нажать на клавиатуре кнопку delete \n Для редактирования записи - дважды щелкнуть ЛКМ",
                "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
