using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System;





public class carController : MonoBehaviour
{
    public float speed = 6.0f;


    bool socketReady = false;
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    public String Host = "192.168.1.102";
    public Int32 Port = 50001;

    public float forward;
    public float turn;
    // Start is called before the first frame update
    void Start()
    {
        setupSocket();
    }

    // Update is called once per frame
    void Update()
    {
        forward = Input.GetAxisRaw("Vertical");
        turn = Input.GetAxisRaw("Horizontal");


        //gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0, forward*speed*Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime  * speed);
        gameObject.transform.RotateAroundLocal(new Vector3(0, 1, 0),turn*Time.deltaTime);


        RaycastHit hit1,hit2,hit3,hit4,hit5;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit1, Mathf.Infinity);
        Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(5, 0, 0)), out hit2, Mathf.Infinity);
        Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-5, 0, 0)), out hit3, Mathf.Infinity);
        Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(5, 0, 5)), out hit4, Mathf.Infinity);
        Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-5, 0, 5)), out hit5, Mathf.Infinity);
        

        String features_label = hit1.distance + "," + hit2.distance + "," + hit3.distance + "," + hit4.distance + "," + hit5.distance+","+turn+"\n";
        

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.red);
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(5,0,0) ) * 10f, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-5, 0, 0)) * 10f, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(5, 0, 5)) * 10f, Color.cyan);
            Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-5, 0, 5)) * 10f, Color.cyan);

        //String data = transform.position.x+","+ transform.position.y + "," + transform.position.y+"\n";
        theWriter.Write(features_label);
        theWriter.Flush();
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

}
