using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.CompilerServices;

public class NetworkManager : MonoBehaviour
{ 
    public int port; // = 55600;
	private string IP; // = "192.168.1.149";
    private UdpClient server;
    private IPEndPoint remoteEndPoint;
    
    [Serializable]
    private struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", x, y, z);
        }
        public static implicit operator Vector3(SerializableVector3 val) => new Vector3(val.x, val.y, val.z);
        public static implicit operator SerializableVector3(Vector3 val) => new SerializableVector3(val.x, val.y, val.z);
    }

    [Serializable]
    private struct SerializableQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public SerializableQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1}, {2}, {3})", x, y, z, w);
        }
        public static implicit operator Quaternion(SerializableQuaternion val) => new Quaternion(val.x, val.y, val.z, val.w);
        public static implicit operator SerializableQuaternion(Quaternion val) => new SerializableQuaternion(val.x, val.y, val.z, val.w);

    }
    
    [Serializable]
    private struct SerializableTransform
    {
        public SerializableVector3 position;
        public SerializableQuaternion rotation;
    }
    private SerializableTransform robotTransform;
    private byte[] data = null;

    // Start is called before the first frame update
    void Start()
    {	
		IP = PlayerPrefs.GetString("IP","192.168.1.149");
		//UnityEngine.Debug.Log(IP);
        
		robotTransform = new SerializableTransform();
        
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        server = new UdpClient();
        
        StartCoroutine(sendData());	
    }
    
    // Update is called once per frame
    void Update()
    {
        robotTransform.position = transform.position;
        robotTransform.position.y = 0.0f;
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(0.0f, q.eulerAngles.y, 0.0f);
        robotTransform.rotation = q;
       
        //UnityEngine.Debug.Log(robotTransform.position);
        //UnityEngine.Debug.Log(robotTransform.rotation);
		
        data = ToByteArray(robotTransform);
    }

    IEnumerator sendData() {
        while (true) {
            try
            {
                server.Send(data, data.Length, remoteEndPoint); 
                UnityEngine.Debug.Log("successfully sent");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log(ex.ToString());
            }
            yield return null;
        }
    }

    private byte[] ToByteArray(SerializableTransform trans)
    {

        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, trans);
            return ms.ToArray();
        }
    }
}
