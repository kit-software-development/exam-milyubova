using System;
using System.Text;
using System.Net.Sockets;

namespace ClientTaxiUI {

    class Client {

        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";

        TcpClient client;

        public void Start() {
            //---create a TCPClient object at the IP and port no.---
            client = new TcpClient(SERVER_IP, PORT_NO);

           
        }
        
        public void SendToServerMsg(string msg) {
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(msg);

            //---Send message to server---
            Console.WriteLine("Sending : " + msg);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
        }

        public void StopClient() {
            client.Close();
        }
    }
}
