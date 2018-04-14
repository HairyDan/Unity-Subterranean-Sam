using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICrystalScript : MonoBehaviour {
    public Sprite off;
    public Sprite on;
    public RectTransform trans;
    public UnityEngine.UI.Image im;
	// Use this for initialization
	void Start () {
        triggerOff();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void triggerOff()
    {
        im.sprite = off;
        trans.localScale = new Vector3(0.5f, 0.5f, 1.0f);
    }

    public void triggerOn()
    {
        im.sprite = on;
        trans.localScale = new Vector3(0.75f, 0.75f, 1.25f);
    }
}
