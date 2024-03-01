using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server{
    class ServerClass{
        private List<Socket> clients = new List<Socket>();
        private Socket listener; 

        public void Start(){
            // Create a TCP/IP socket
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections
            listener.Bind(new IPEndPoint(IPAddress.Any, 6969));
            listener.Listen(10);

            Console.WriteLine($"Server listening on port {6969}...");

            Thread _acceptThead = new Thread(AcceptConnections);
            _acceptThead.Start();
        }

        private void AcceptConnections(){
            while (true){
                if (clients.Count() < 4){
                    Socket handler = listener.Accept();
                    clients.Add(handler);

                    Thread _clientThread = new Thread(() => HandleClient(handler));
                    _clientThread.Start();
                }else{
                    Console.WriteLine("Too many connected clients!");
                }
            }
        }
        private void HandleClient(Socket _client)
        {
            string _clientIP = ((IPEndPoint)_client.RemoteEndPoint).Address.ToString();
            Console.WriteLine($"Connected to {_clientIP}");

            byte[] buffer = new byte[1024];
            int totalBytesReceived = 0;
            while (true)
            {
                int bytesRead = _client.Receive(buffer, totalBytesReceived, buffer.Length - totalBytesReceived, SocketFlags.None);
                if (bytesRead == 0)
                {
                    break; // Exit the loop when the client disconnects
                }

                totalBytesReceived += bytesRead;
                Console.WriteLine($"Received {bytesRead} bytes from client {_clientIP}, Total received: {totalBytesReceived}");

                if (totalBytesReceived >= buffer.Length)
                {
                    // Expand the buffer if needed
                    Array.Resize(ref buffer, buffer.Length * 2);
                }
            }

            string msg = System.Text.Encoding.ASCII.GetString(buffer, 0, totalBytesReceived);
            Console.WriteLine($"{_clientIP}: {msg}");

            Console.WriteLine($"{_clientIP} disconnected!");
            clients.Remove(_client);
            _client.Shutdown(SocketShutdown.Both);
            _client.Close();
        }
    }
}