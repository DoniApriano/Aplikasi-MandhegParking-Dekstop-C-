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
using System.Security.Cryptography;
using System.Net.Http.Headers;

namespace LatihanLksMandhegParking
{
    public partial class Form1 : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BK8I0G1;Initial Catalog=MandhegParkingSystem;Integrated Security=True");
        MD5 md5 = MD5.Create();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "pellentesque.massa.lobortis@fringillacursus.net";
            textBox2.Text = "admin";
        }

        static string sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string password = sha256(textBox2.Text);
            conn.Open();
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Isi Semua Field");
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Employee where email = '" + textBox1.Text + "' and password = '" + password + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SqlCommand cmd = new SqlCommand("select * from Employee where email = '"+textBox1.Text+"'", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (dt.Rows.Count > 0)
                { 
                    rdr.Read();
                    string name = rdr["name"].ToString();
                    FormUtama formUtama = new FormUtama(name);
                    this.Hide();
                    formUtama.Show();
                }
                else
                {
                    MessageBox.Show("nama nggak ada");
                }
                label4.Text = sha256(textBox2.Text);
            }
            conn.Close();
        }
    }
}
