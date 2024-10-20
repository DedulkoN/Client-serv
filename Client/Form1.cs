using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            FormLogin formLogin = new FormLogin();
            if(formLogin.ShowDialog()==DialogResult.OK)
            {
                MessageBox.Show("Успешный вход");
            } else { this.Close(); }

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
