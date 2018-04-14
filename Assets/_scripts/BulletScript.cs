using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public ParticleSystem explosion;

    public GameObject player;

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("bricc") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("indestructbricc"))
        {

            explosion.Play();
            PreventExplodeLinkedDeath();
            Destroy(gameObject);

            if (other.gameObject.CompareTag("Player"))
            {
                // Debug.Log("YOU LOST!");
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            }
        }

    }

    public void PreventExplodeLinkedDeath()
    {
        explosion.transform.parent = null;
        Destroy(explosion.gameObject, explosion.main.duration);
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("ASDADSADADSADSDAD");
	}
}
