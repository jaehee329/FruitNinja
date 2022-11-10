// 1초에 한 번 서버와 커넥션을 생성해 받아온 메세지를 출력한다.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Client : MonoBehaviour {
    private TcpClient clientSocket;
    private string HOST = "127.0.0.1";
    private int PORT = 8080;
    private float time = 0f;
    private float period = 1f;

    public void Start()
    {
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time >= period) {
            string message = FetchMessage();
            Debug.Log(message);
            time = 0;
        }
    }

    private string FetchMessage()
    {
        MakeConnection();
        if (clientSocket == null)
        {
            return "";
        }
        string msg = "";
        try
        {
            NetworkStream stream = clientSocket.GetStream();
            byte[] buffer = new byte [1024];
            int nbytes = stream.Read(buffer, 0, buffer.Length);
            msg = Encoding.ASCII.GetString(buffer, 0, nbytes);
            stream.Close();
            clientSocket.Close();
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
        return msg;
    }

    private void MakeConnection()
    {
        try
        {
            clientSocket = new TcpClient(HOST, PORT);
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
}