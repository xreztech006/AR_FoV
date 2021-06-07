using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ModeSwitch : MonoBehaviour
{
    Camera _camera;
    public Text text;
    string _text, ls, fv;
    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<Camera>();
        text.text = _text = ls = "Laser Scan";
        fv = "Field of View";
        toggle("laserscan");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void toggle(string name)
    {
        _camera.cullingMask ^= 1 << LayerMask.NameToLayer(name);
    }
    public void switchModes()
    {
        toggle("fov");
        toggle("laserscan");
        text.text = _text == ls ? fv : ls;
    }
}
