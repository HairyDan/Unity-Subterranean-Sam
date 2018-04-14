using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tuteScreen1 : MonoBehaviour {
    public Image im;
    public tuteScreen2 tute;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            im.enabled = false;
            tute.isEnabled = true;
        }
	}
}
