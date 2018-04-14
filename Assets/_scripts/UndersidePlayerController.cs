using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndersidePlayerController : MonoBehaviour {

    private Rigidbody rb;
    private PlayerController parentscript; 
	// Use this for initialization
	void Start () {
        parentscript = this.transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        string h = collision.GetComponent<Collider>().name;
        if(h != "BlueCrystal" && h != "GreenCrystal" && h != "YellowCrystal" && h != "PurpleCrystal" && h != "RedCrystal"){
            parentscript.undersideTouching = true;
        }
      //  Debug.Log("UNDERSIDE ON ENTER");
    }

    private void OnTriggerStay(Collider collision)
    {
        string h = collision.GetComponent<Collider>().name;
        if (h != "BlueCrystal" && h != "GreenCrystal" && h != "YellowCrystal" && h != "PurpleCrystal" && h != "RedCrystal")
        {
            if (collision.gameObject.CompareTag("bricc") || collision.gameObject.CompareTag("indestructbricc"))
            {
                parentscript.undersideTouching = true;
            }
            else parentscript.undersideTouching = false;
        }
     //   Debug.Log("UNDERSIDE ON STAY");
    }

    private void OnTriggerExit(Collider collision)
    {
        parentscript.undersideTouching = false;
      //  Debug.Log("UNDERSIDE ON LEAVE");
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("killme");
	}
}
