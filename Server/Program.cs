using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new Dictionary<string, string>()
                {
                    {"red", "красный" },
                    {"blue", "синий" },
                    {"green", "зеленый" }
                };

            var tcpListener = new TcpListener(IPAddress.Any, 8888);

            tcpListener.Start();    // запускаем сервер
            Console.WriteLine("Сервер запущен. Ожидание подключений... ");

            while (true)
            {
                // получаем подключение в виде TcpClient
                var tcpClient = tcpListener.AcceptTcpClient();
                // получаем объект NetworkStream для взаимодействия с клиентом
                var stream = tcpClient.GetStream();
                // буфер для входящих данных
                var response = new List<byte>();
                int bytesRead = 10;
                while (true)
                {
                    // считываем данные до конечного символа
                    while ((bytesRead = stream.ReadByte()) != '\n')
                    {
                        // добавляем в буфер
                        response.Add((byte)bytesRead);
                    }
                    var word = Encoding.UTF8.GetString(response.ToArray());

                    // если прислан маркер окончания взаимодействия,
                    // выходим из цикла и завершаем взаимодействие с клиентом
                    if (word == "END") break;

                    string[] commang = word.Split('|');
                    string result="";

                    switch (commang[0]) 
                    {
                        case "Login":
                            if (commang[1].Length>0 && commang[2].Length>0)
                            {
                                result = "LOGIN";
                            }
                            Console.WriteLine($"Вход пользователя {commang[1]}");
                            break;
                        default:
                            break;
                    }


                   
                    
                    result += '\n';
                    // отправляем результат
                    byte[] s = Encoding.UTF8.GetBytes(result);
                    stream.Write(s,0, s.Length);
                    response.Clear();
                }
            }
            tcpListener.Stop();
        }
    }
}
