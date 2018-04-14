using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTextScript : MonoBehaviour {
    public int collectedCrystals = 0;
    public UnityEngine.UI.Text text;
    private bool triggered = false;
    private float triggerTime;
    public GateScript gate;
	// Use this for initialization
	void Start () {
        text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (collectedCrystals > 4 && !triggered) {
            triggered = true;
            triggerTime = Time.time;
            gate.isEnabled = true;
            gate.endgame = true;
            CamShake.Shake(60.0f, 0.4f);
        }
        if (collectedCrystals > 4 && triggered)
        {
            triggered = true;
            text.text = "! WARNING !" + Environment.NewLine + "!GET BACK TO THE PORTAL!" + Environment.NewLine + "CAVERN DESTRUCTION IN: " + (60.0f - (Time.time - triggerTime)).ToString();
        }
	}
}
