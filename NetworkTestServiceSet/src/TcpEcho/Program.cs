using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Common;

namespace TcpEcho
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            if (args.Length > 0)
            {
                port = int.Parse(args[0]);
            }
            else
            {
                try
                {
                    var json = File.ReadAllText(Util.GetFullPathOnApplicationDir("Setting.json"));
                    var elem = JsonSerializer.Deserialize<JsonElement>(json);
                    port = elem.GetProperty("port").GetInt32();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    port = 50001;
                }
            }
            var listener = new TcpListener(new IPEndPoint(IPAddress.Any, port));
            Console.WriteLine($"waiting port:{port}");

            listener.Start();
            while (true)
            {
                try
                {
                    using var client = listener.AcceptTcpClient();
                    using var stream = client.GetStream();
                    var buffer = new byte[1024];
                    var length = stream.Read(buffer);
                    var str = Encoding.UTF8.GetString(buffer[..length]);
                    Console.WriteLine($"RECV:{str}");
                    var retStr = $"Re:{str}";
                    Console.WriteLine($"SEND:{retStr}");
                    stream.Write(Encoding.UTF8.GetBytes(retStr));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
