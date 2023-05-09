using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LatihanLksMandhegParking
{
    public partial class FormUtama : Form
    {
        public FormUtama(string value)
        {
            InitializeComponent();
            //label1.Text = value;
            this.Value = value;
        }

        public string Value { get; set; }

        private void FormUtama_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = Value;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMasterMember frmMasterMember = new FormMasterMember();
            this.Hide();
            frmMasterMember.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMasterVehicle frmMasterVehicle = new FormMasterVehicle();   
            this.Hide();
            frmMasterVehicle.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormParkingPayment formParkingPayment = new FormParkingPayment();
            this.Hide();
            formParkingPayment.Show();
        }
    }
}
