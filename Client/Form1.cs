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
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 8888);
            // слова для отправки для получения перевода
            var words = new string[] { "red", "yellow", "blue" };
            // получаем NetworkStream для взаимодействия с сервером
            var stream = tcpClient.GetStream();

            // буфер для входящих данных
            var response = new List<byte>();
            int bytesRead = 10; // для считывания байтов из потока
            foreach (var word in words)
            {
                // считыванием строку в массив байт
                // при отправке добавляем маркер завершения сообщения - \n
                byte[] data = Encoding.UTF8.GetBytes(word + '\n');
                // отправляем данные
                stream.Write(data, 0, data.Length);

                // считываем данные до конечного символа
                while ((bytesRead = stream.ReadByte()) != '\n')
                {
                    // добавляем в буфер
                    response.Add((byte)bytesRead);
                }
                var translation = Encoding.UTF8.GetString(response.ToArray());
                textBox1.Text += ($"{Environment.NewLine}Слово {word}: {translation}");
                response.Clear();
            }

            // отправляем маркер завершения подключения - END
            stream.Write(Encoding.UTF8.GetBytes("END\n"), 0, Encoding.UTF8.GetBytes("END\n").Length);
            textBox1.Text += ($"{Environment.NewLine}Все сообщения отправлены");

        }
    }
}
