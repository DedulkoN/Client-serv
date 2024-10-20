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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 8888);

            var stream = tcpClient.GetStream();
            var response = new List<byte>();
            int bytesRead = 10; // для считывания байтов из потока

            byte[] data = Encoding.UTF8.GetBytes($"Login|{textBoxLogin.Text}|{textBoxPass.Text}" + '\n');
            // отправляем данные
            stream.Write(data, 0, data.Length);

            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                // добавляем в буфер
                response.Add((byte)bytesRead);
            }
            var result = Encoding.UTF8.GetString(response.ToArray());

            stream.Write(Encoding.UTF8.GetBytes("END\n"), 0, Encoding.UTF8.GetBytes("END\n").Length);
            if (result == "LOGIN")
                this.DialogResult = DialogResult.OK;

        }
    }
}
