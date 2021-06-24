﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasUtility : MonoBehaviour
{
    public GameObject modeText, modeButton, FoVButton, CountText, Cams, SpreadSlider, CountSlider;
    private Rect a, b, _cur;
    private Camera cam;
    public Text text;
    string _text, ls, fv;
    int _mask;
    bool uiActive = true;

    // Start is called before the first frame update
    void Start()
    {
        a = new Rect(0, 0, 1f, 1f);
        b = new Rect(0.5f, 0, 1f, 1f);
        cam = Camera.main;
        _mask = cam.cullingMask;
        _cur = cam.rect = a;
        text.text = _text = fv = "Field of View";
        ls = "Laser Scan";
        toggleMode("fov");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggleUI()
    {
        uiActive = !uiActive;
        bool fov = _text == fv; //attention, equals here because we are returning
        /*foreach (GameObject item in hideItems)
        {
            item.SetActive(uiActive);
        }*/
        modeText.SetActive(uiActive);
        modeButton.SetActive(uiActive);
        FoVButton.SetActive(uiActive);
        CountText.SetActive(fov && uiActive ? true : false);
        Cams.SetActive(fov && uiActive ? true : false);
        SpreadSlider.SetActive(!fov && uiActive ? true : false);
        CountSlider.SetActive(!fov && uiActive ? true : false);
    }

    public void showFoV()
    {
        cam.rect = (_cur == a ? b : a);
        _cur = cam.rect;
    }
    //the good functionality of this script requires that the Fov and laserscan layers be disabled on the AR camera by default
    void toggleMode(string name)
    {
        int mask = _mask;
        mask ^= 1 << LayerMask.NameToLayer(name);
        cam.cullingMask = mask;
    }
    public void switchModes()
    {
        bool fov = _text != fv; //attention, NOT equals here because we are switching
        text.text = fov ? fv : ls;
        _text = text.text;
        toggleMode(fov? "fov" : "laserscan");
        //button.SetActive(fov ? true : false);
        CountText.SetActive(fov ? true : false);
        Cams.SetActive(fov ? true : false);
        SpreadSlider.SetActive(!fov ? true : false);
        CountSlider.SetActive(!fov ? true : false);
    }
}
