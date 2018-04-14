using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }

    public void loadMainScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
