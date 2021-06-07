using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasUtility : MonoBehaviour
{
    public GameObject[] hideItems;
    private Rect a, b, _cur;
    private Camera cam;
    public Text text;
    string _text, ls, fv;
    public GameObject button;
    int _mask;

    // Start is called before the first frame update
    void Start()
    {
        
        a = new Rect(0, 0, 1, 1);
        b = new Rect(0, 0, 1f, 0.5f);
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
        foreach(GameObject item in hideItems)
        {
            item.SetActive(item.activeInHierarchy ? false : true);

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
        button.SetActive(fov ? true : false);
        hideItems[3].SetActive(fov ? true : false);
    }
}
