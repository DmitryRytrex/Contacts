using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pomogite_3
{
    public partial class Form1 : Form
    {

        DBconnect connection = new DBconnect();

        public Form1()
        {

            InitializeComponent();

            connection.ConOpen();

            SqlCommand command_data = new SqlCommand("select * from USER_", connection.GetConnection());

            dataGridView1.Columns.Add("S1","ФИО");
            dataGridView1.Columns.Add("S2", "Номер");
            dataGridView1.Columns.Add("S3", "Адресс");
            dataGridView1.Columns.Add("S4", "Паспортные данные");

            using (var reader = command_data.ExecuteReader())
            {

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["FIO"].ToString(), reader["NUMBER"].ToString(), reader["ADRESS"].ToString(), reader["PASS_DAN"].ToString());
                }

            }

            FormBorderStyle = FormBorderStyle.None;

            connection.ConClosed();
        }

        //Добавить
        private void button1_Click(object sender, EventArgs e)
        {

            panel4.Enabled= true;

        }

        //Реактировать
        private void button2_Click(object sender, EventArgs e)
        {

            UpdateUser updateUser = new UpdateUser();
            updateUser.Show();
            this.Hide();

        }

        //Удалить
        private void button3_Click(object sender, EventArgs e)
        {

            connection.ConOpen();

            string name = dataGridView1.CurrentCell.Value.ToString();

            SqlCommand command_4 = new SqlCommand($"DELETE FROM USER_ WHERE FIO = '{name}' or NUMBER ='{name}' or ADRESS = '{name}' or PASS_DAN = '{name}';", connection.GetConnection());
            command_4.ExecuteNonQuery();
            MessageBox.Show("Запись успешно удална");

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            connection.ConOpen();

            SqlCommand command_data = new SqlCommand("select * from USER_", connection.GetConnection());

            dataGridView1.Columns.Add("S1", "ФИО");
            dataGridView1.Columns.Add("S2", "Номер");
            dataGridView1.Columns.Add("S3", "Адресс");
            dataGridView1.Columns.Add("S4", "Паспортные данные");

            using (var reader = command_data.ExecuteReader())
            {

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["FIO"].ToString(), reader["NUMBER"].ToString(), reader["ADRESS"].ToString(), reader["PASS_DAN"].ToString());
                }

            }
            connection.ConClosed();

        }
        //Добавить в панели 4
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                connection.ConOpen();

                SqlCommand command_1 = new SqlCommand($"INSERT INTO USER_ VALUES('{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}');", connection.GetConnection());
                command_1.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена успешно");

                panel4.Enabled = false;

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                connection.ConClosed();

            }
            catch (Exception)
            {

                MessageBox.Show("Программная ошибка");
            }
        }

        //Выйти в панели 4
        private void button4_Click(object sender, EventArgs e)
        {

            panel4.Enabled = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            connection.ConOpen();

            dataGridView1.Rows.Clear();

            SqlCommand command = new SqlCommand($"SELECT * FROM USER_ WHERE FIO LIKE '%{textBox1.Text}%';", connection.GetConnection());
            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["FIO"].ToString(), reader["NUMBER"].ToString(), reader["ADRESS"].ToString(), reader["PASS_DAN"].ToString());
                }

            }

        }
    }
}
