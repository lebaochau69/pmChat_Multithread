﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Listener
    {
        private Socket _socket;

        public bool Listening { get; private set; }

        public int Port { get; private set; }

        public Listener(int port)
        {
            Port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (Listening) return;
            _socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port)); // Port = 9000
            _socket.Listen(0);
            _socket.BeginAccept(Callback, null);
            Listening = true;
        }

        public void Stop()
        {
            if (!Listening) return;
            if (_socket.Connected)
                _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;
        void Callback(IAsyncResult ar)
        {
            try
            {
                var s = _socket.EndAccept(ar);
                if (SocketAccepted != null) SocketAccepted(s);
                _socket.BeginAccept(Callback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
