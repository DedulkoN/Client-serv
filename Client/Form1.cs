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
            } else {
                Application.Exit();               
            }

            InitializeComponent();
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 8888);

            var stream = tcpClient.GetStream();
            var response = new List<byte>();
            int bytesRead = 10; // для считывания байтов из потока
            byte[] data = new byte[10];
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    data = Encoding.UTF8.GetBytes($"F1|{textBox1.Text}|{textBox2.Text}|{textBox3.Text}" + '\n');
                    break;
                case 1:
                    data = Encoding.UTF8.GetBytes($"F2|{textBox4.Text}|{textBox5.Text}|{textBox6.Text}" + '\n');
                    break;
                case 2:
                    data = Encoding.UTF8.GetBytes($"F3|{textBox7.Text}|{textBox8.Text}|{textBox9.Text}" + '\n');
                    break;
            }
            stream.Write(data, 0, data.Length);

            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                // добавляем в буфер
                response.Add((byte)bytesRead);
            }
            var result = Encoding.UTF8.GetString(response.ToArray());

            stream.Write(Encoding.UTF8.GetBytes("END\n"), 0, Encoding.UTF8.GetBytes("END\n").Length);
            tcpClient.Close();
            MessageBox.Show($"Результат:{result} ");
        }
    }
}
