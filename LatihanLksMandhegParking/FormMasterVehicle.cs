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

namespace LatihanLksMandhegParking
{
    public partial class FormMasterVehicle : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BK8I0G1;Initial Catalog=MandhegParkingSystem;Integrated Security=True");
        public FormMasterVehicle()
        {
            InitializeComponent();
        }

        private void dataCmb()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select name from VehicleType",conn);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                comboBox2.Items.Add(sqlDataReader[0]);
            }
            conn.Close();
        }

        private void dataKosong()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox2.Text = "";
            textBox4.Text = "";
        }

        private void dataEnable()
        {
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox2.Enabled = true;
            textBox4.Enabled = true;
        }

        private void dataDisable()
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox2.Enabled = false;
            textBox4.Enabled = false;
        }

        public void showData()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Vehicle.id, Member.name as Owner, Vehicle.license_plate as license_plate, VehicleType.name as type, Vehicle.notes FROM Vehicle INNER JOIN VehicleType ON Vehicle.vehicle_type_id = VehicleType.id INNER JOIN Member ON Member.id = Vehicle.member_id ", conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            conn.Close();
        }

        private void FormMasterVehicle_Load(object sender, EventArgs e)
        {
            showData();
            dataCmb();
            label6.Visible = false;
            dataDisable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;

            dataEnable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;

            dataDisable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;

            dataEnable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Vehicle(vehicle_type_id,member_id,license_plate,notes,created_at) VALUES((SELECT id FROM VehicleType WHERE name = '" + comboBox2.Text + "'),(SELECT id FROM Member WHERE name = '" + textBox3.Text + "'),'" + textBox2.Text + "','" + textBox4.Text + "','" + DateTime.Now.ToString() + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close() ;
                showData();
                MessageBox.Show("Berhasil Insert Data");
                dataKosong();
            }
            else if (button2.Enabled == false)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Vehicle SET vehicle_type_id = (SELECT id FROM VehicleType WHERE name = '" + comboBox2.Text + "'), member_id = (SELECT id FROM Member WHERE name = '" + textBox3.Text + "'), notes = '" + textBox4.Text + "', last_updated_at = '" + DateTime.Now.ToString() + "' WHERE id = '"+label6.Text+"'" , conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                showData();
                MessageBox.Show("Berhasil Update Data");
                dataKosong();
            }
            else if (button3.Enabled == false)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Vehicle WHERE id = '" + label6.Text + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                showData();
                MessageBox.Show("Berhasil Delete Data");
                dataKosong();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;

            dataKosong();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                label6.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["license_plate"].FormattedValue.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Owner"].FormattedValue.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["notes"].FormattedValue.ToString();
            }
        }
    }
}
