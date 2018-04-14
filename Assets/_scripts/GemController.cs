using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour {

    public ParticleSystem explosion;
    public int colour;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player" || other.name == "undersidecollider") {
            explosion.Play();
            PreventExplodeLinkedDeath();
            Destroy(gameObject);
            switch (colour)
            {
                case 0:
                    GameObject.FindGameObjectWithTag("blueCrystal").GetComponent<UICrystalScript>().triggerOn();
                    break;
                case 1:
                    GameObject.FindGameObjectWithTag("greenCrystal").GetComponent<UICrystalScript>().triggerOn();
                    break;
                case 2:
                    GameObject.FindGameObjectWithTag("purpleCrystal").GetComponent<UICrystalScript>().triggerOn();
                    break;
                case 3:
                    GameObject.FindGameObjectWithTag("redCrystal").GetComponent<UICrystalScript>().triggerOn();
                    break;
                case 4:
                    GameObject.FindGameObjectWithTag("yellowCrystal").GetComponent<UICrystalScript>().triggerOn();
                    break;
            }
        }
    }

    public void PreventExplodeLinkedDeath()
    {
        explosion.transform.parent = null;
        Destroy(explosion.gameObject, explosion.main.duration);
    }
    void OnDestroy(){
        GameObject.FindGameObjectWithTag("warning").GetComponent<WarningTextScript>().collectedCrystals++;
    
    }
    // Update is called once per frame
    void Update () {
		
	}
}
