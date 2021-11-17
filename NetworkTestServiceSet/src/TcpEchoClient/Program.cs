using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpEchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please input IPAddress and port like 127.0.0.1:50001");
                return;
            }
            var splited = args[0].Split(':');
            if (splited.Length != 2)
            {
                Console.WriteLine("Please input IPAddress and port like 127.0.0.1:50001");
                return;
            }
            var addr = splited[0];
            var port = int.Parse(splited[1]);
            using var tcpClient = new TcpClient();
            Console.WriteLine($"Connect to {addr}:{port}");
            tcpClient.Connect(new IPEndPoint(IPAddress.Parse(addr), port));
            using var stream = tcpClient.GetStream();
            var message = "hello";
            Console.WriteLine($"SEND:{message}");
            stream.Write(Encoding.UTF8.GetBytes(message));
            var buffer = new byte[1024];
            var length = stream.Read(buffer);
            Console.WriteLine("RECV:" + Encoding.UTF8.GetString(buffer[..length]));
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
