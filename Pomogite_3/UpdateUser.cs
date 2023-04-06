using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pomogite_3
{
    public partial class UpdateUser : Form
    {

        DBconnect connection = new DBconnect();

        public UpdateUser()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;

            connection.ConOpen();

            SqlCommand command_1 = new SqlCommand("select * from USER_;", connection.GetConnection());

            using (var reader = command_1.ExecuteReader())
            {

                while (reader.Read())
                {

                    comboBox1.Items.Add(reader["FIO"].ToString());

                }

            }

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

            textBox1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command_2 = new SqlCommand($"UPDATE USER_ SET {comboBox2.Text} = '{textBox1.Text}' WHERE FIO = '{comboBox1.Text}';", connection.GetConnection());
                command_2.ExecuteNonQuery();
                MessageBox.Show("Вы успешно изсмнили данные");
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            catch (Exception)
            {

                MessageBox.Show("У вас не выбрано поле с именем или рдактируемой колонкой");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }
    }
}
