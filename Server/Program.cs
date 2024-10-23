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
                                SqlQueriesRun sql = new SqlQueriesRun();
                                int col = Convert.ToInt32( sql.RunCalcQuery($"select count(AccountID) from Accounts where Login='{commang[1]}' and Password='{commang[2]}'"));
                                if (col > 0) result = "LOGIN";
                                else result = "NoLogin";
                            }
                            Console.WriteLine($"Вход пользователя {commang[1]}");
                            break;

                        case "F1":
                            if (commang.Length==4)
                            {
                                result = ( Int32.Parse(commang[1]) + Int32.Parse(commang[2])+Int32.Parse(commang[3])).ToString();
                                Console.WriteLine($"Подсчет функции F1");
                            }
                            break;
                        case "F2":
                            if (commang.Length == 4)
                            {
                                result = (Int32.Parse(commang[1]) - Int32.Parse(commang[2]) - Int32.Parse(commang[3])).ToString();
                                Console.WriteLine($"Подсчет функции F2");
                            }
                            break;
                        case "F3":
                            if (commang.Length == 4)
                            {
                                result = (Int32.Parse(commang[1]) * Int32.Parse(commang[2]) + Int32.Parse(commang[3])).ToString();
                                Console.WriteLine($"Подсчет функции F3");
                            }
                            break;
                        default:
                            result = "Произошла ошибка!";
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
