using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace NetworkingTest{
    class Program{
        public static void Main(string[] args){
            bool running = true;
            while (running){
                Console.WriteLine("\nwrite 'quit' to close\n");
                Console.Write("send a msg /> ");
                string msg = Console.ReadLine();
                if (msg == "quit"){
                    running = false;
                    return;
                }else{
                    SendData(msg, 6969, "127.0.0.1");
                }
            }
        }

        public static void SendData(string data, Int32 port, string ip)
        {
            IPAddress host = IPAddress.Parse(ip);  
            IPEndPoint ipendpoint = new IPEndPoint(host, port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipendpoint);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                socket.Close();
                return;
            }
            try
            {
                socket.Send(System.Text.Encoding.ASCII.GetBytes(data));
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                socket.Close();
                return;
            }
            socket.Close();
        }
    }
}