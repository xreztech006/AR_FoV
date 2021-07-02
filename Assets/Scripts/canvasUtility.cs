//==========================================
// Title:  canvasUtility
// Author: HDH
// Date:   24 Jun 2021
//==========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasUtility : MonoBehaviour
{
    [Tooltip("Match the name to the UI element")]
    public GameObject modeText, modeButton, FoVButton, CountText, Cams, SpreadSlider, CountSlider, LinePanel, flipButton;
    // triad of rectangles to set the main camera to show the FoV camera underneath 
    private Rect a, b, _cur;
    // holder var for the main camera
    private Camera cam;
    [Tooltip("the actual text component within the modeText object")]
    public Text text;
    // triad of text for the two modes (laserscan and fov repectively)
    string _text, ls, fv;
    // cinch point:: maintains the initial state of the ARCameras masks for easy adjusting
    // the good functionality of this script requires that the Fov and laserscan layers be disabled on the AR camera by default
    int _mask;
    bool uiActive, _inverted = true;
    [Tooltip("0:O1, 1:V1, 2:V2, 3:O2, 4:OS, 5:VS")]
    public GameObject[] walls;
    Vector3[] _tw;
    float[] _rw;
    int _wallMod = 1;

    void Start()
    {
        a = new Rect(0, 0, 1f, 1f); //  the whole screen
        b = new Rect(0.5f, 0, 1f, 1f);  // the right half of the screen
        cam = Camera.main;
        _mask = cam.cullingMask; // hold the cameras default mask
        _cur = cam.rect = a; // set the whole screen
        text.text = _text = fv = "Field of View"; // default to FOV on load arbitrary
        ls = "Laser Scan"; // cont
        toggleMode("fov");  // cont
        _tw = new Vector3[4];
        _rw = new float[4];
        for (int i = 0; i < 4; i++)
        {
            _tw[i] = walls[i].transform.position;
            _rw[i] = walls[i].transform.rotation.eulerAngles.y;
        }
    }
    // the function which en/disables the whole UI, uses _text to test mode so as not to turn on the wrong buttons
    public void toggleUI()
    {
        uiActive = !uiActive;
        // use a string we need anyway to track the mode
        bool fov = _text == fv; //attention, equals here because we are returning
        
        modeText.SetActive(uiActive);
        modeButton.SetActive(uiActive);
        FoVButton.SetActive(uiActive);
        CountText.SetActive(fov && uiActive ? true : false);
        Cams.SetActive(fov && uiActive ? true : false);
        SpreadSlider.SetActive(!fov && uiActive ? true : false);
        CountSlider.SetActive(!fov && uiActive ? true : false);
        flipButton.SetActive(!fov && uiActive ? true : false);
        //LinePanel.SetActive(!fov && uiActive ? true : false);
    }
    // splits the screen to show current fov camera
    public void showFoV()
    {
        cam.rect = (_cur == a ? b : a);
        _cur = cam.rect;
    }
    // changes the layermask to hide objects depending on the mode
    // the good functionality of this script requires that the Fov and laserscan layers be disabled on the AR camera by default
    void toggleMode(string name)
    {
        int mask = _mask;
        mask ^= 1 << LayerMask.NameToLayer(name);
        cam.cullingMask = mask;
    }
    // handles UI elements for mode switching : shell togglemode
    public void switchModes()
    {
        // use a string we need anyway to track the mode
        bool fov = _text != fv; //attention, NOT equals here because we are switching
        text.text = fov ? fv : ls;
        _text = text.text;
        toggleMode(fov? "fov" : "laserscan");
        //button.SetActive(fov ? true : false);
        CountText.SetActive(fov ? true : false);
        Cams.SetActive(fov ? true : false);
        SpreadSlider.SetActive(!fov ? true : false);
        CountSlider.SetActive(!fov ? true : false);
        //LinePanel.SetActive(!fov ? true : false);
        flipButton.SetActive(!fov ? true : false);
    }
    public void flipHospital()
    {
        /*
        foreach (GameObject wall in oest)
        {
            var p = wall.transform.position;
            wall.transform.position = new Vector3(_inverted? west : east, p.y, p.z);
            wall.transform.Rotate(0, 180, 0);
        }
        foreach (GameObject wall in vest)
        {
            var p = wall.transform.position;
            wall.transform.position = new Vector3(_inverted ? east : west, p.y, p.z);
            wall.transform.Rotate(0, 180, 0);
        }
        _inverted = !_inverted;*/
        for (int i = 0; i < 4; i++)
        {
            walls[i].transform.position = _tw[(i + _wallMod)%4] ;
            walls[i].transform.rotation = Quaternion.Euler(0, _rw[(i + _wallMod) % 4], 0);
        }
        var p = walls[5].transform.position;
        walls[4].transform.position = new Vector3(_inverted ? _tw[2].x : _tw[0].x, p.y, p.z);
        walls[5].transform.position = new Vector3(_inverted ? _tw[0].x : _tw[2].x, p.y, p.z);
        _wallMod++;
        if(_wallMod > 3)
        {
            _wallMod = 0;
            _inverted = !_inverted;
        }

    }
    public void toggleGraph()
    {
        LinePanel.SetActive(LinePanel.activeInHierarchy ? false : true);
    }
}
