using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {
    public Transform trans;
    public float rotateSpeed = 1.0f;
	// Use this for initialization
	void Start () {
        //trans = GameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 scale = trans.localScale;
        trans.localScale = new Vector3(scale.x * 0.98f, scale.y * 0.98f, scale.z * 0.98f);
        trans.rotation = new Quaternion(0.0f, 0.0f, trans.rotation.z + Random.Range(0, 5),1.0f);
        if (scale.x < 0.075)
            Destroy(gameObject);
	}
}
