using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour {
    public Animator anim;
    public Transform trans;
    public Sprite sprite;
    public SpriteRenderer rend;
    public PlayerController player;
    bool inAir = false;
    public GameObject cloud;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.undersideTouching)
        {
            anim.SetBool("FlyFall", false);
            if (inAir) {
                inAir = false;
                anim.SetTrigger("Land");
            }
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);
                trans.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool("Left", true);
                anim.SetBool("Right", false);
                trans.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
            }
            else
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
            }

            if(Input.GetButton("Fire1")){
                if (player.wallBelow)
                {
                    anim.SetTrigger("MineDown");
                }
                else if(player.nearWall)
                {
                    anim.SetTrigger("Mine");                    
                }
            }
        }
        else
        {
            anim.SetBool("FlyFall", true);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            rend.sprite = sprite;
            if (Input.GetKey(KeyCode.D))
            {
                trans.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            }else 
            if (Input.GetKey(KeyCode.A))
            {
                trans.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
            }
            inAir = true;
        }
        if (Input.GetKey(KeyCode.W)) {
            GameObject.Instantiate(cloud,trans.position,new Quaternion(0.0f,0.0f,0.0f,1.0f));
        }
	}
}
