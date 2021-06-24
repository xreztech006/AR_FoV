using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public Text ipdisplay;
	private string ipkey = "IP";
    
	// Start is called before the first frame update
    void Start()
    {
        ipdisplay.text = PlayerPrefs.GetString(ipkey, "None");
    }
	
	public void SetIP(string newip) {
		ipdisplay.text = newip;
		PlayerPrefs.SetString(ipkey, newip);
	}
	
	public void ChangeScene() {
		SceneManager.LoadScene(1);
	}
}
