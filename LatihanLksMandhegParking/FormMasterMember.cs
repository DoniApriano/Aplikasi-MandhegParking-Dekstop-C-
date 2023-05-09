using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LatihanLksMandhegParking
{
    public partial class FormMasterMember : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BK8I0G1;Initial Catalog=MandhegParkingSystem;Integrated Security=True");
        public FormMasterMember()
        {
            InitializeComponent();
        }

        private void showData()
        {
            conn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Member", conn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void normalField()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            richTextBox1.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;

            dateTimePicker1.Value = DateTime.Now;
        }

        private void FormMasterMember_Load(object sender, EventArgs e)
        {
            normalField();
            showData();
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand("select name from membership", conn);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                comboBox1.Items.Add(sqlDataReader["name"].ToString());
            }
            conn.Close();
            
            label9.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            normalField();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled= true;
            button3.Enabled= true;
            button4.Enabled = true; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                conn.Open();
                string memberId = null;
                if (comboBox1.Text == "Non Member")
                {
                    memberId = "1";
                } 
                else if (comboBox1.Text == "Regular")
                {
                    memberId= "2";
                }
                else if (comboBox1.Text == "VIP")
                {
                    memberId = "3";
                }


                string gender = null;
                if (radioButton1.Checked == true)
                {
                    gender = radioButton1.Text;
                } else if (radioButton2.Checked == true)
                {
                    gender = radioButton2.Text;
                }
                SqlCommand cmd = new SqlCommand("insert into member(membership_id,name,email,phone_number,address,date_of_birth,gender,created_at) values('" + memberId + "','" + textBox1.Text + "','" + textBox2.Text + "','"+textBox3.Text+"','"+richTextBox1.Text+"','"+dateTimePicker1.Value.ToString()+"','"+gender+"','"+DateTime.Now.ToString()+"')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Insert Data");
                conn.Close();
                normalField();
                showData();
            } 
            else if (button2.Enabled == false)
            {
                conn.Open();
                string memberId = null;
                if (comboBox1.Text == "Non Member")
                {
                    memberId = "1";
                }
                else if (comboBox1.Text == "Regular")
                {
                    memberId = "2";
                }
                else if (comboBox1.Text == "VIP")
                {
                    memberId = "3";
                }


                string gender = null;
                if (radioButton1.Checked == true)
                {
                    gender = radioButton1.Text;
                }
                else if (radioButton2.Checked == true)
                {
                    gender = radioButton2.Text;
                }
                SqlCommand cmd = new SqlCommand("update member set membership_id = '" + memberId + "', name = '" + textBox1.Text + "', email = '" + textBox2.Text + "', phone_number = '" + textBox3.Text + "', address = '" + richTextBox1.Text + "', date_of_birth = '" + dateTimePicker1.Value.ToString() + "', gender = '" + gender + "', last_updated_at = '" + DateTime.Now.ToString() + "' where id = '"+label9.Text+"'", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Update Data");
                conn.Close();
                normalField();
                showData();
            }
            else if (button3.Enabled == false)
            {
                conn.Open();
                
                SqlCommand cmd = new SqlCommand("delete from member where id = '" + label9.Text + "'", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Hapus Data");
                conn.Close();
                normalField();
                showData();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;

                label9.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();

                if (dataGridView1.Rows[e.RowIndex].Cells["membership_id"].FormattedValue.ToString() == "Non Member")
                {
                    comboBox1.SelectedIndex = 0;
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells["membership_id"].FormattedValue.ToString() == "Regular")
                {
                    comboBox1.SelectedIndex = 1;
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells["membership_id"].FormattedValue.ToString() == "VIP")
                {
                    comboBox1.SelectedIndex = 2;
                }

                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["email"].FormattedValue.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["phone_number"].FormattedValue.ToString();
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["address"].FormattedValue.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells["date_of_birth"].FormattedValue.ToString();

                if (dataGridView1.Rows[e.RowIndex].Cells["gender"].FormattedValue.ToString() == "Male")
                {
                    radioButton1.Checked = true;
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells["gender"].FormattedValue.ToString() == "Female")
                {
                    radioButton2.Checked = true;
                }
            }
        }
    }
}
