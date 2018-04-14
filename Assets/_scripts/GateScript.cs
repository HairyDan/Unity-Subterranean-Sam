using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

    public bool endgame =false;

    public Sprite on, off;
    public bool isEnabled = true;
    private bool initial = true;
    private float turnOffTime;
    public SpriteRenderer rend;
	// Use this for initialization
	void Start () {
        turnOffTime = Time.time + 5.0f;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && endgame)
        {
            //  Debug.Log("YOU WON");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > turnOffTime && initial)
        {
            isEnabled = false;
            initial = false;
        }
		if(isEnabled){
            rend.sprite = on;
        }else{
            rend.sprite = off;
        }
	}
}
