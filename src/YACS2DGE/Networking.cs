using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace YACS2DGE.YACS2DGE{
    class Networking{
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