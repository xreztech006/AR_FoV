using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerCollider : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    public int[] array = new int[3];
    public int SumArray;
    public int ctr = 0;
    public int ctr2 = 0;
    public int SumArrayOld;
    Camera1 camera1object;
    Camera2 camera2object;
    Camera3 camera3object;
    Vector3[] robotscale;
    Vector3 robotscale_2;
    Vector3 robotscale_3;
    Vector3 origin;
    Vector3 Pos;
    Vector3 Pos1;
    Vector3 Pos2;
    Vector3 Pos3;
    Vector3 Pos4;
    Vector3 Pos5;
    Vector3 Pos6;
    Vector3 Pos7;
    Vector3 Pos8;
    Vector3 PosTwo;
    Vector3 PosThree;
    Vector3 PosEdit;
    Vector3 CameraPos;
    Vector3 CameraPosTwo;
    Vector3 CameraPosTwo2;
    Vector3 CameraPosThree;
    Vector3 CameraPosThree3;
    Vector3 CameraPos1;
    Vector3 CameraPos2;
    Vector3[] scale;
    Vector3 sc1;
    Vector3 sc2;
    Vector3 sc3;
    Vector3 sc_1;
    Vector3 sc_2;
    Vector3 sc_3;
    Vector3 Scale;
    Vector3 end;
    Vector3 look;
    Vector3 looktwo;
    Vector3 lookthree;
    Vector3 bb1;
    Vector3 bb2;
    Vector3 bb3;
    Vector3 bb4;
    Vector3 bb5;
    Vector3 bb6;
    Vector3 bb7;
    Vector3 bb8;
    public float Two5;
    public float Five0;
    public float Seven5;
    public int xvalue;
    public int yvalue;
    public int zvalue;
    public int Cxvalue;
    public int Cyvalue;
    public int Czvalue;
    public bool Answer;
    public float y1;
    public float angle;
    public float angletwo;
    public float anglethree;
    public string one;
    public string two;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

        // angle = 180;
        // angletwo = 180;
        // anglethree = 180;

        SumArrayOld = 0;

        Two5 = (float).25f;
        Five0 = (float).5f;
        Seven5 = (float).75;
        //sc1 = new Vector3(.33f, 0f, .33f); // 1 to 0
        // sc2 = new Vector3(.33f, 0f, .33f); // 2 to 1
        // sc3 = new Vector3(..5ff, 0f, ..5ff); // 3 to 2

        scale[0] = new Vector3(.2f, .01f, .2f);
        scale[1] = new Vector3(.15f, .01f, .15f);
        scale[2] = new Vector3(.1f, .01f, .1f);
        scale[3] = new Vector3(.05f, .01f, .05f);
        //SumArray = 0;
        //SumArray = 0;
        y1 = 0;

        robotscale = new Vector3[4];

        robotscale[1] = transform.localScale + sc_1; //0 to 1
        robotscale[2] = transform.localScale + sc_2; //1 to 2
        robotscale[3] = transform.localScale + sc_3; //2 to 3
        robotscale[0] = transform.localScale; // to 0
        // robotscale_2 = transform.localScale + sc2; //2 to 1
        // robotscale_3 = transform.localScale + sc3; //3 to 2


        //array[0] = 0;
        //array[1] = 0;
    }

    // Update is called once per frame
    //get the position of the camera and get the position of the robot and then
    //create the triangle for the camera and then using the radius, test if any of the
    //four points of the robot are inside the triangle
    //Questions: size of triangle?
    //how do i know what direction the camera is pointing? using rotation i think, but
    //what does rotation give you exactly?




    void FixedUpdate()
    {
        //start off buy creating the triangle
        //checking if the area matches
        //camera1object = Instantiate(camera1object);
        //GameObject CameraObject = GameObject.Find ("Camera1");


        camera1object = FindObjectOfType<Camera1>();
        camera2object = FindObjectOfType<Camera2>();
        camera3object = FindObjectOfType<Camera3>();

        //Vector3 CameraPos;
        CameraPos = camera1object.transform.position + .5f * camera1object.transform.forward;
        CameraPosTwo = camera2object.transform.position + .5f * camera1object.transform.forward;
        CameraPosThree = camera3object.transform.position + .5f * camera1object.transform.forward;
        //CameraPos[1] = y1;

        bb1 = transform.position + .5f * transform.up + .2f * transform.right + .25f * transform.forward;
        bb2 = transform.position + .5f * transform.up - .2f * transform.right + .25f * transform.forward;
        bb3 = transform.position + .5f * transform.up + .2f * transform.right - .2f * transform.forward;
        bb4 = transform.position + .5f * transform.up - .2f * transform.right - .2f * transform.forward;


        bb5 = transform.position - .5f * transform.up + .2f * transform.right + .25f * transform.forward;
        bb6 = transform.position - .5f * transform.up - .2f * transform.right + .25f * transform.forward;
        bb7 = transform.position - .5f * transform.up + .2f * transform.right - .2f * transform.forward;
        bb8 = transform.position - .5f * transform.up - .2f * transform.right - .2f * transform.forward;
        //this is from the origin and this is NOT the right vecotr
        //camera1object.transform.position = CameraPos;
        //two = camera1object.transform.position.ToString();
        //Debug.Log("We are after the CameraPos[1] and CameraPos[1] = ");
        //Debug.Log(two);



        Pos = transform.position;
        PosTwo = transform.position;
        PosThree = transform.position;

        PosEdit = transform.position;
        PosEdit[1] = PosEdit[1] + .5f;
        //the x y and z values are floats
        //Debug.Log("Before");
        //Debug.Log(Pos.ToString());
        Pos[1] = Pos.y - CameraPos.y;
        Pos[0] = Pos.x - CameraPos.x;
        Pos[2] = Pos.z - CameraPos.z;

        /*
        Pos1[0] = bb1.x - CameraPos.x;
        Pos1[1] = bb1.y - CameraPos.y;
        Pos1[2] = bb1.z - CameraPos.z;
        */

        PosTwo[1] = PosTwo.y - CameraPosTwo.y;
        PosTwo[0] = PosTwo.x - CameraPosTwo.x;
        PosTwo[2] = PosTwo.z - CameraPosTwo.z;

        PosThree[1] = PosThree.y - CameraPosThree.y;
        PosThree[0] = PosThree.x - CameraPosThree.x;
        PosThree[2] = PosThree.z - CameraPosThree.z;


        //maybe you dont need to make the pos of the robot the same as the changed vector values
        //transform.position = Pos;
        one = Pos.ToString();
        //Debug.Log("We are after the Pos[1] and pos[1] = ");
        //Debug.Log(one);

        //transform.TransformDirection(Vector3.forward)
        //CameraPos1 = CameraPos; //+ Vector3.up * 15;
        CameraPos2 = CameraPos - 1f * camera1object.transform.forward;

        CameraPosTwo2 = CameraPosTwo - 1f * camera2object.transform.forward;

        CameraPosThree3 = CameraPosThree - 1f * camera3object.transform.forward;

        look[0] = CameraPos2[0] - CameraPos[0];
        look[1] = CameraPos2[1] - CameraPos[1];
        look[2] = CameraPos2[2] - CameraPos[2];

        looktwo[0] = CameraPosTwo2[0] - CameraPosTwo[0];
        looktwo[1] = CameraPosTwo2[1] - CameraPosTwo[1];
        looktwo[2] = CameraPosTwo2[2] - CameraPosTwo[2];

        lookthree[0] = CameraPosThree3[0] - CameraPosThree[0];
        lookthree[1] = CameraPosThree3[1] - CameraPosThree[1];
        lookthree[2] = CameraPosThree3[2] - CameraPosThree[2];

        //Physics.Raycast(camera1object.transform.position + Vector3.left + Vector3.down*15, camera1object.transform.position + Vector3.left + Vector3.down*15 + camera1object.transform.forward, 1f);

        //Debug.DrawLine(PosEdit, transform.position + .5f*Vector3.forward + 1f*transform.forward, Color.yellow);

        Debug.DrawLine(camera1object.transform.position + .5f * camera1object.transform.forward, camera1object.transform.position - 1f * camera1object.transform.forward, Color.red);
        Debug.DrawLine(camera2object.transform.position + .5f * camera2object.transform.forward, camera2object.transform.position - 1f * camera2object.transform.forward, Color.red);
        Debug.DrawLine(camera3object.transform.position + .5f * camera3object.transform.forward, camera3object.transform.position - 1f * camera3object.transform.forward, Color.red);
        //Debug.DrawLine(camera2object.transform.position, camera2object.transform.position + 1f*camera2object.transform.forward, Color.blue);
        //Debug.DrawLine(camera3object.transform.position, camera3object.transform.position + 1f*camera3object.transform.forward, Color.yellow);


        Debug.DrawLine(transform.position + .5f * transform.up + .2f * transform.right + .25f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.red);
        Debug.DrawLine(transform.position + .5f * transform.up - .2f * transform.right + .25f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.white);
        Debug.DrawLine(transform.position + .5f * transform.up + .2f * transform.right - .2f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.blue);
        Debug.DrawLine(transform.position + .5f * transform.up - .2f * transform.right - .2f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.yellow);


        Debug.DrawLine(transform.position - .5f * transform.up + .2f * transform.right + .25f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.red);
        Debug.DrawLine(transform.position - .5f * transform.up - .2f * transform.right + .25f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.white);
        Debug.DrawLine(transform.position - .5f * transform.up + .2f * transform.right - .2f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.blue);
        Debug.DrawLine(transform.position - .5f * transform.up - .2f * transform.right - .2f * transform.forward, transform.position - 1f * transform.forward + .5f * transform.up, Color.yellow);

        //Debug.DrawLine(transform.position, transform.position + 1f*Vector3.back, Color.white);
        //Debug.DrawLine(transform.position, transform.position + 1f*Vector3.right, Color.blue);
        //Debug.DrawLine(transform.position, transform.position + 1f*Vector3.left, Color.yellow);


        //Debug.DrawLine(camera1object.transform.position , camera1object.transform.position + Vector3.forward + 1f*camera1object.transform.forward, Color.blue);
        //Debug.Log(CameraPos1.ToString());
        //Debug.Log(CameraPos2.ToString());



        angle = Vector3.Angle(look, Pos);
        //angle = Vector3.Angle(look, Pos1);
        angletwo = Vector3.Angle(looktwo, PosTwo);
        anglethree = Vector3.Angle(lookthree, PosThree);



        //Debug.Log("The angle between the two is");

        //Debug.Log(angle);
        //Debug.Log(180-angle);

        if (angle <= 45)
        {
            array[0] = 1;
        }
        else
        {
            array[0] = 0;
        }
        Debug.Log("Angle is the following: ");
        Debug.Log(angle);

        if (angletwo <= 45)
        {
            array[1] = 1;
        }
        else
        {
            array[1] = 0;
        }
        //Debug.Log("Angle two is the following: ");
        //Debug.Log(angletwo);


        if (anglethree <= 45)
        {
            //Debug.Log("we are being seen by the camera");
            array[2] = 1;
        }
        else
        {
            //Debug.Log("we are not being seen by the camera");
            array[2] = 0;
        }
        Debug.Log("Angle three is the following: ");
        Debug.Log(anglethree);
        /*
              SumArray = array[0] + array[1] + array[2];
                if (SumArray == 1) {
                  array[0] = 1;
                  if (transform.localScale.x != .75 && ctr-ctr2 == 0) {
                  transform.localScale += sc;
                  Debug.Log(transform.localScale.x);
                  Debug.Log("transform.localScale.x value is first");
                  Debug.Log(transform.localScale.z);
                  Debug.Log("transform.localScale.z value is second");
                  ctr += 1;
                  }
                }
                else {
                  array[0] = 0;
                  //transform.localScale.x = 1;
                  //transform.localScale.z = 1;
                  if (transform.localScale.x != 1 && ctr2 == 0) {
                  Debug.Log("Were in");
                  transform.localScale += sc2;
                  Debug.Log(transform.localScale.x);
                  Debug.Log("transform.localScale.x value is first");
                  Debug.Log(transform.localScale.z);
                  Debug.Log("transform.localScale.z value is second");
                  ctr2 += 1;
                  }
                }
        */

        SumArray = array[0] + array[1] + array[2];
        rend.sharedMaterial = material[SumArray];
        transform.localScale = scale[SumArray];

        //if (SumArray != SumArrayOld)
        //{
        //    rend.sharedMaterial = material[SumArray];
        //    transform.localScale = scale[SumArray];
        //}
        //SumArrayOld = array[0] + array[1] + array[2];




        /*
        
        //2 to 3
        if (SumArray == 3 && SumArrayOld == 2)
        {
            Debug.Log("SumArray is equal to 3");
            rend.sharedMaterial = material[3];
            transform.localScale = scale[3];
        }

        else if (SumArray == 3 && SumArrayOld == 3)
        {
            Debug.Log("SumArray is equal to 3");
            rend.sharedMaterial = material[3];
        }

        //1 to 2
        else if (SumArray == 2 && SumArrayOld == 1)
        {
            Debug.Log("SumArray is equal to 2");
            rend.sharedMaterial = material[2];
            transform.localScale = scale[2];
        }

        else if (SumArray == 2 && SumArrayOld == 2)
        {
            Debug.Log("SumArray is equal to 2");
            rend.sharedMaterial = material[2];
        }
        //3 to 2
        else if (SumArray == 2 && SumArrayOld == 3)
        {
            Debug.Log("SumArray is equal to 2");
            rend.sharedMaterial = material[2];
            transform.localScale = scale[2];
        }


        //0 to 1
        else if (SumArray == 1 && SumArrayOld == 0)
        {
            //Debug.Log("SumArray is equal to 1");
            rend.sharedMaterial = material[1];
            transform.localScale = scale[1];
        }

        else if (SumArray == 1 && SumArrayOld == 1)
        {
            //Debug.Log("SumArray is equal to 1");
            rend.sharedMaterial = material[1];
        }
        //2 to 1
        else if (SumArray == 1 && SumArrayOld == 2)
        {
            //Debug.Log("SumArray is equal to 1");
            rend.sharedMaterial = material[1];
            transform.localScale = scale[1];
        }

        //1 to 0
        else if (SumArray == 0 && SumArrayOld == 1)
        {
            Debug.Log("SumArray is equal to 0");
            rend.sharedMaterial = material[0];
            //transform.localScale += sc1;
            transform.localScale = scale[0];
        }
        else if (SumArray == 0 && SumArrayOld == 0)
        {
            Debug.Log("SumArray is equal to 0");
            rend.sharedMaterial = material[0];
        }

        SumArrayOld = array[0] + array[1] + array[2];


        //SumArray = array[0] + array[1];


        //Debug.Log(SumArray);
    }
    */
        /*void OnCollisionEnter ( Collision collisionInfo)
        {
            if (collisionInfo.collider.name == "Camera1")
            {
              Debug.Log("We are being seen by camera 1");
              array[0] = 1;
              Debug.Log("array[0] value is the following:");
              Debug.Log(array[0]);
            }

            if (collisionInfo.collider.name == "Camera2")
            {
              Debug.Log("We are being seen by camera 2");
              array[1] = 1;
              Debug.Log("array[1] value is the following:");
              Debug.Log(array[1]);
            }

            if (collisionInfo.collider.name == "Camera3")
            {
              Debug.Log("We are being seen by camera 3");
              array[2] = 1;
              Debug.Log("array[2] value is the following:");
              Debug.Log(array[2]);
            }
        }

        void OnCollisionExit ( Collision collisionInfo) {
          if (collisionInfo.collider.name == "Camera1")
          {
            Debug.Log("We are no longer being seen by camera 1");
            array[0] = 0;
            Debug.Log("array[0] value is the following:");
            Debug.Log(array[0]);
          }

          if (collisionInfo.collider.name == "Camera2")
          {
            Debug.Log("We are no longer being seen by camera 2");
            array[1] = 0;
            Debug.Log("array[1] value is the following:");
            Debug.Log(array[1]);
          }

          if (collisionInfo.collider.name == "Camera3")
          {
            Debug.Log("We are no longer being seen by camera 3");
            array[2] = 0;
            Debug.Log("array[2] value is the following:");
            Debug.Log(array[2]);
          }
        }*/
    }
}
