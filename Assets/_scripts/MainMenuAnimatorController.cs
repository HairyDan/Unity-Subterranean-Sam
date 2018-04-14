using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimatorController : MonoBehaviour {

    public GameObject gem1, gem2, gem3, gem4, gem5;

    public Light l1, l2, l3, l4, l5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        l1.intensity = Mathf.PingPong(Time.time * 9, 10);
        l2.intensity = Mathf.PingPong(Time.time * 9, 10);
        l3.intensity = Mathf.PingPong(Time.time * 9, 10);
        l4.intensity = Mathf.PingPong(Time.time * 9, 10);
        l5.intensity = Mathf.PingPong(Time.time * 9, 10);

        gem1.transform.position = new Vector3(gem1.transform.position.x, Mathf.PingPong(Time.time * 0.2f, 1)+1);
        gem2.transform.position = new Vector3(gem2.transform.position.x, Mathf.PingPong(Time.time * 0.24f, 1)+1);
        gem3.transform.position = new Vector3(gem3.transform.position.x, Mathf.PingPong(Time.time * 0.18f, 1)+1);
        gem4.transform.position = new Vector3(gem4.transform.position.x, Mathf.PingPong(Time.time * 0.26f, 1)+1);
        gem5.transform.position = new Vector3(gem5.transform.position.x, Mathf.PingPong(Time.time * 0.16f, 1)+1);

    }
}
