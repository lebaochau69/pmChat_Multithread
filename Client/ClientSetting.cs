﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class ClientSetting
    {
        readonly Socket _s;
        public delegate void ReceivedEventHandler(ClientSetting cs, string received);
        public event ReceivedEventHandler Received = delegate { };
        public event EventHandler Connected = delegate { };
        public delegate void DisconnectedEventHandler(ClientSetting cs);
        public event DisconnectedEventHandler Disconnected = delegate { };
        bool _connected;

        public bool Connected1 { get => _connected; set => _connected = value; }

        public ClientSetting()
        {
            _s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            try
            {
                var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
                _s.BeginConnect(ep, ConnectCallback, _s);
            }
            catch { }
        }

        public void Close()
        {
            _s.Dispose();
            _s.Close();
        }

        void ConnectCallback(IAsyncResult ar)
        {
            _s.EndConnect(ar);  // No connect could be made
            Connected1 = true;
            Connected(this, EventArgs.Empty);
            var buffer = new byte[_s.ReceiveBufferSize];
            _s.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReadCallback, buffer);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            var buffer = (byte[])ar.AsyncState;
            var rec = _s.EndReceive(ar);
            if (rec != 0)
            {
                var data = Encoding.UTF8.GetString(buffer, 0, rec);
                Received(this, data);
            }
            else
            {
                Disconnected(this);
                Connected1 = false;
                Close();
                return;
            }
            _s.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReadCallback, buffer);
        }

        public void Send(string data)
        {
            try
            {
                var buffer = Encoding.UTF8.GetBytes(data);
                _s.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, buffer);
            }
            catch { Disconnected(this); }
        }

        void SendCallback(IAsyncResult ar)
        {
            _s.EndSend(ar);
        }
    }
}
