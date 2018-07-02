using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System;

public class Client
{

    Socket _socket;
    string ip = "127.0.0.1";
    int port = 8888;
    byte[] buffer = new byte[1024];

    public void Connect()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ipadd = IPAddress.Parse(ip);
        IPEndPoint endPoint = new IPEndPoint(ipadd, port);
        _socket.BeginConnect(endPoint, ConnectAsyn, _socket);
    }
    void ConnectAsyn(IAsyncResult ar)
    {
        Socket  socket = ar.AsyncState as Socket;
   
        Debug.LogError("主动链接成功==>>" + socket.RemoteEndPoint.ToString());
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, socket);
    }
    void Receive(IAsyncResult ar)
    {
      Socket  socket = ar.AsyncState as Socket;
        int ReceiveCount = socket.EndReceive(ar);
        if (ReceiveCount <= 0)
            Debug.LogError("接收出错");

        string str = System.Text.Encoding.UTF8.GetString(buffer, 0, ReceiveCount);
        Debug.LogError("接收到了==>>" + str);
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, socket);
    }

   public void Send()
    {
        string str = "奶奶个腿";
        byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
        _socket.Send(data);
    }

}
