using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasUtility : MonoBehaviour
{
    public GameObject[] hideItems;
    public GameObject button;
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
        foreach (GameObject item in hideItems)
        {
            item.SetActive(uiActive);
        }
    }

    public void showFoV()
    {
        cam.rect = (_cur == a ? b : a);
        _cur = cam.rect;
    }
    void toggleMode(string name)
    {
        int mask = _mask;
        mask ^= 1 << LayerMask.NameToLayer(name);
        cam.cullingMask = mask;
    }
    public void switchModes()
    {
        bool fov = _text != fv;
        text.text = fov ? fv : ls;
        _text = text.text;
        toggleMode(fov? "fov" : "laserscan");
        //button.SetActive(fov ? true : false);
        hideItems[3].SetActive(fov ? true : false);
        hideItems[4].SetActive(fov ? true : false);
        hideItems[5].SetActive(!fov ? true : false);
        hideItems[6].SetActive(!fov ? true : false);
    }
}
