using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tuteScreen2 : MonoBehaviour {
    public bool isEnabled = false;

    public bool pauseflag = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isEnabled) {
            if (Input.GetKeyUp(KeyCode.Space)) pauseflag = true;
            if (Input.GetKeyDown(KeyCode.Space) && pauseflag)
                SceneManager.LoadScene("Main");
        }	
	}
}
