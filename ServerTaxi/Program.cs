using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using MySql.Data.MySqlClient;

namespace MultiServer {
    class Program {
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        //я бд делала в dbforge, скрипт прикрепила)
        private static string connStr = "User Id=root;Password=xampp;Host=localhost;Database=database2;Character Set=utf8";
        private static MySqlConnection conn;

        static void Main() {
            Console.Title = "Server";
            ConnectSQL();
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
        }

        public static void ConnectSQL() {
            Console.WriteLine("Init SQL START");
            conn = new MySqlConnection(connStr);
            conn.Open();
            Console.WriteLine("Init SQL OK");
        }

        private static void SetupServer() {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

    
        private static void CloseAllSockets() {
            foreach (Socket socket in clientSockets) {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR) {
            Socket socket;
            try {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) 
            {
                return;
            }
            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult AR) {
            Socket current = (Socket)AR.AsyncState;
            int received;
            try {
                received = current.EndReceive(AR);
            }
            catch (SocketException) {
                Console.WriteLine("Client forcefully disconnected");
                current.Close();
                clientSockets.Remove(current);
                return;
            }
            //ловим то, что передалось от клиента
            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            string[] tokens = text.Split(new[] { "\r\n" }, StringSplitOptions.None);
            CheckResponse(tokens); 

            /*
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                Console.WriteLine("Warning Sent");
            }
            */
            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        public static string name = "";
        public static string phone = "";
        public static int param;
        public static int value;

        public static void CheckResponse(string[] msg) {
            //мы получили строку с индексами, теперь проверяем, разбиваем и сохраняем в переменные, чтоюы передать функции на добавление в бд
           foreach (string token in msg)
            {
                string[] prefixes = { "101 ", "102 ", "103 ", "104 " };
                if (string.IsNullOrWhiteSpace(token))
                {
                    return;
                }
                else if (prefixes.Any(prefix => token.StartsWith(prefix)))
                {
                    if (token.StartsWith("101 "))
                    {
                        name = GetName(token);
                        Console.WriteLine(name);
                    }
                    else if (token.StartsWith("102 "))
                    {
                        phone = GetPhone(token);
                        Console.WriteLine(phone);
                    }
                    else if (token.StartsWith("103 "))
                    {
                        param = GetParam(token);
                        Console.WriteLine(param);
                    }
                    else if (token.StartsWith("104 "))
                    {
                        value = GetValue(token);
                        Console.WriteLine(value);
                    }

                }
                else
                { Console.WriteLine("Wrong request message. = " + msg); }
                Console.WriteLine("HERE");
            }
            
            Console.WriteLine("HERE");
            if (name != "" && phone != "") {
                InsertOrderToDB(name, phone, param, value);
              
            }
        }

        public static void InsertOrderToDB(string name, string phone, int param, int value) {
            Console.WriteLine("Pushing to db");//вставляем полученные значения в таблицу Клиент
            string sql = "INSERT INTO client ( name, phone) VALUES ( \""+name+ "\", \"" + phone + "\")";
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.ExecuteNonQuery();

            //получаем его айди, который создался автоматически
            string sql_id = "SELECT clinet_id FROM client ORDER BY clinet_id DESC LIMIT 1" ;
            MySqlCommand commandValue = new MySqlCommand(sql_id, conn);
            int id = Convert.ToInt32(commandValue.ExecuteScalar());

            //по этому айди заносим в другую таблицу оставшиеся значения
            string sqlcalc = "INSERT INTO calculation ( client_id, param_id, value) VALUES (\"" + id + "\", \"" + param + "\", \"" + value + "\")";
            MySqlCommand commandcalc = new MySqlCommand(sqlcalc, conn);
            commandcalc.ExecuteNonQuery();
            DoCalculation(id);
        }

       


        public static void DoCalculation(int order_id) {
            Console.WriteLine("DoingCalculation");

            List<int> values = new List<int>(); // будем формировать списки цены и значения, чтобы потом перемножать значения
            List<int> params_id = new List<int>();
            List<float> prices = new List<float>();


            string sqlValue = "SELECT value FROM calculation WHERE client_id = " + order_id;
            MySqlCommand commandValue = new MySqlCommand(sqlValue, conn);
            MySqlDataReader valuesRet = commandValue.ExecuteReader();

            while (valuesRet.Read()) {
                int value = 0;
                if (!Int32.TryParse(valuesRet[0].ToString(), out value)) {
                    value = -1;
                }
                values.Add(value);
            }
            valuesRet.Close();

            string sqlParam = "SELECT param_id FROM calculation WHERE client_id = " + order_id;
            MySqlCommand commandParam = new MySqlCommand(sqlParam, conn);
            MySqlDataReader params_idRet = commandParam.ExecuteReader();

            while (params_idRet.Read()) {
                int param_id = 0;
                if (!Int32.TryParse(params_idRet[0].ToString(), out param_id)) {
                    param_id = -1;
                }
                params_id.Add(param_id);
            }
            params_idRet.Close();

            foreach (int param_id in params_id) {
                string sqlPrice = "SELECT price FROM price WHERE param_id = " + param_id;
                MySqlCommand commandPrice = new MySqlCommand(sqlPrice, conn);
                string priceRet = commandPrice.ExecuteScalar().ToString();
                float price = float.Parse(priceRet, CultureInfo.InvariantCulture.NumberFormat);
                prices.Add(price);
            }

            //Умножаем
            float result = 0;
            int i = 0;
            while (i < prices.Count) {
                result = result + (values[i] * prices[i]);
                i++;
            }
            Console.WriteLine(result);
        }

        public static string GetName(string msg) {
            string name = msg.Remove(0, 4);
            return name;
        }

        public static string GetPhone(string msg) {
            string phone = msg.Remove(0, 4);
            return phone;
        }

        public static int GetParam(string msg) {
            string param = msg.Remove(0, 4);
            return Convert.ToInt32(param);
        }

        public static int GetValue(string msg) {
            string value = msg.Remove(0, 4);
            return Convert.ToInt32(value);
        }
        public static string GetDestStreet(string msg)
        {
            string startAddress = msg.Remove(0, 4);
            return startAddress;
        }

        public static string GetDestHouse(string msg)
        {
            string destAddress = msg.Remove(0, 4);
            return destAddress;
        }
    }
}