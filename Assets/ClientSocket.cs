using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class ClientSocket : MonoBehaviour
{

    bool socketReady = false;
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    public String Host = "192.168.1.102";
    public Int32 Port = 50001;

    void Start()
    {
        setupSocket();
    }


    public void setupSocket()
    {                            // Socket setup here
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);                // catch any exceptions
        }
      
    }

    private void Update()
    {
        if (socketReady)
        {
            //theWriter.Write("Hello World");
            //theWriter.Flush();
            Byte[] data = new Byte[1024];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = theStream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Log(responseData);
        }
    }


}