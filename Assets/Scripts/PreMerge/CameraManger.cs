using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class CameraManger : MonoBehaviour
{
	public int port; // = 55601;
	private string IP; // = "192.168.1.149";
    private UdpClient server;
    private IPEndPoint remoteEndPoint;
	private byte[] data = new byte[1];
    
	// Start is called before the first frame update
    void Start()
    {
        IP = PlayerPrefs.GetString("IP","192.168.1.149");
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        server = new UdpClient();
    }

    // Update is called once per frame
    void Update()
    {
		foreach (Touch touch in Input.touches)
		{
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
    
			if (touch.phase == TouchPhase.Began)
			{
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit, 1000))
				{
					Debug.Log("touched " + hit.transform.name);
					switch (hit.transform.name) 
					{
						case "Camera1":
							sendData(BitConverter.GetBytes(1));
							break;
						case "Camera2":
							sendData(BitConverter.GetBytes(2));
							break;
						case "Camera3":
							sendData(BitConverter.GetBytes(3));
							break;
						case "RobotTargetCylinder":
							sendData(BitConverter.GetBytes(4));
							break;
					}
					
				}
			}
		}
        
    }
	
	void sendData(byte[] data) {
		//while (true) {
        if (BitConverter.IsLittleEndian) {
			Array.Reverse(data);
		}
		
		try
        {
            server.Send(data, data.Length, remoteEndPoint); 
            UnityEngine.Debug.Log("successfully sent camera number");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log(ex.ToString());
        }
            //yield return null;
        //}
	}
}
