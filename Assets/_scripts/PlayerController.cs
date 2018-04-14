using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    private float moveHorizontal;

    private bool touchingWall;

    public bool wallBelow = false;
    public bool nearWall = false;

    //stuff seems to crash through other objects at 100 speed or more
    public float maxSpeed = 99f;
    public float speedScale = 3f;
    public float airScale = 1.5f;
    public float jumpScale = 40f;

  //  public Collider underSideCollider;

    public float manualGravity;

    private GameObject touchingBrick;

    public GameObject highlighter;

    //[HideInInspector]
    public bool undersideTouching;

    private Ray topleftRay, toprightRay, bottomleftRay, bottomrightRay, upRay, downRay;

    private int framesSinceJump;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();

        topleftRay = new Ray();
        toprightRay = new Ray();
        bottomleftRay = new Ray();
        bottomrightRay = new Ray();
        upRay = new Ray();
        downRay = new Ray();

	}

    private void OnCollisionEnter(Collision collision)
    {
        touchingWall = true;
        touchingBrick = collision.gameObject;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("bricc") || collision.gameObject.CompareTag("indestructbricc")){
            touchingWall = true;
            if (collision.gameObject.CompareTag("bricc"))
            {
                touchingBrick = collision.gameObject;
            }
        } else touchingWall = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingWall = false;
        touchingBrick = new GameObject();
    }

    private bool isPlayerTouchingBrick()
    {
        if (touchingBrick != null)
        {
            if (touchingBrick.CompareTag("bricc"))
            {
                return true;
            }
            else
            {
                touchingWall = false;
                return false;
            }
        }
        else
        {
            touchingWall = false;
            return false;
        }
    }

    private void sideColliding()
    {
        if (touchingWall)
        {
            bool flag1, flag2, flag3, flag4, flag5, flag6;
            flag1 = flag2 = flag3 = flag4 = flag5 = flag6 = false;

            GameObject toDestroy = null;

            Vector3 UpperOrigin = new Vector3(rb.transform.position.x, rb.transform.position.y + 0.07f);
            Vector3 LowerOrigin = new Vector3(rb.transform.position.x, rb.transform.position.y);
            topleftRay = new Ray(UpperOrigin, new Vector3(-0.1f, 0, 0));
            toprightRay = new Ray(UpperOrigin, new Vector3(0.1f, 0, 0));
            bottomleftRay = new Ray(LowerOrigin, new Vector3(-0.1f, 0, 0));
            bottomrightRay = new Ray(LowerOrigin, new Vector3(0.1f, 0, 0));
            upRay = new Ray(UpperOrigin, new Vector3(0, 0.1f));
            downRay = new Ray(LowerOrigin, new Vector3(0, -0.1f));
            RaycastHit tlhit = new RaycastHit();
            RaycastHit blhit = new RaycastHit();
            RaycastHit trhit = new RaycastHit();
            RaycastHit brhit = new RaycastHit();
            RaycastHit uhit = new RaycastHit();
            RaycastHit dhit = new RaycastHit();
            wallBelow = false;
            nearWall = true;
            if (Physics.Raycast(topleftRay, out tlhit, 0.1f) && Input.GetAxis("Horizontal") ==-1)
            {
                if (tlhit.collider.gameObject.CompareTag("bricc"))
                {
                    flag1 = true;
                    toDestroy = tlhit.collider.gameObject;
                    Vector3 coverupPos = new Vector3(tlhit.collider.gameObject.transform.position.x, tlhit.collider.gameObject.transform.position.y, -0.1f);
                    highlighter.transform.position = coverupPos;
                    // Debug.Log("TOP LEFT RAY HIT");
                }
            }
            else if (Physics.Raycast(toprightRay, out trhit, 0.1f) && Input.GetAxis("Horizontal") == 1)
            {
                if (trhit.collider.gameObject.CompareTag("bricc"))
                {
                    flag2 = true;
                    toDestroy = trhit.collider.gameObject;
                    Vector3 coverupPos = new Vector3(trhit.collider.gameObject.transform.position.x, trhit.collider.gameObject.transform.position.y, -0.1f);
                    highlighter.transform.position = coverupPos;
                    //  Debug.Log("TOP RIGHT RAY HIT");
                    //  return 'r';
                }
            }
            if (Physics.Raycast(bottomleftRay, out blhit, 0.1f) && Input.GetAxis("Horizontal") ==-1 )
            {
                if (blhit.collider.gameObject.CompareTag("bricc"))
                {
                    flag3 = true;
                    toDestroy = blhit.collider.gameObject;
                    Vector3 coverupPos = new Vector3(blhit.collider.gameObject.transform.position.x, blhit.collider.gameObject.transform.position.y, -0.1f);
                    highlighter.transform.position = coverupPos;
                    //  Debug.Log("BOTTOM LEFT RAY HIT");
                    //  return 'l';
                }
            }
            else if (Physics.Raycast(bottomrightRay, out brhit, 0.1f) && Input.GetAxis("Horizontal") ==1)
            {
                if (brhit.collider.gameObject.CompareTag("bricc"))
                {
                    flag4 = true;
                    toDestroy = brhit.collider.gameObject;
                    Vector3 coverupPos = new Vector3(brhit.collider.gameObject.transform.position.x, brhit.collider.gameObject.transform.position.y, -0.1f);
                    highlighter.transform.position = coverupPos;
                    //  Debug.Log("BOTTOM RIGHT RAY HIT");
                    //  return 'r';
                }
            }
            else if (Physics.Raycast(downRay, out dhit, 0.1f) && Input.GetAxis("Vertical") ==-1 && Input.GetAxis("Horizontal") == 0)
            {
                if (dhit.collider.gameObject.CompareTag("bricc"))
                {
                    flag5 = true;
                    toDestroy = dhit.collider.gameObject;
                    Vector3 coverupPos = new Vector3(dhit.collider.gameObject.transform.position.x, dhit.collider.gameObject.transform.position.y, -0.1f);
                    highlighter.transform.position = coverupPos;
                    //  Debug.Log("BOTTOM RIGHT RAY HIT");
                    //  return 'r';
                    wallBelow = true;
                    //Debug.Log("Set wallBelow");
                }
            }
            else if (Physics.Raycast(upRay, out uhit, 0.1f) && Input.GetAxis("Vertical") ==1 && Input.GetAxis("Horizontal") == 0)
            {
                if (uhit.collider.gameObject.CompareTag("bricc"))
                {
                    flag6 = true;
                    toDestroy = uhit.collider.gameObject;
                    Vector3 coverupPos = new Vector3(uhit.collider.gameObject.transform.position.x, uhit.collider.gameObject.transform.position.y, -0.1f);
                    highlighter.transform.position = coverupPos;
                    //  Debug.Log("BOTTOM RIGHT RAY HIT");
                    //  return 'r';
                }
            }
            else if (!flag1 && !flag2 && !flag3 && !flag4 && !flag5 && !flag6)
            {
                
                highlighter.transform.position = new Vector3(200, 200, -0.1f);
                nearWall = false;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (toDestroy != null)
                {
                    Destroy(toDestroy);
                }
            }

        }
    }


    // Update is called once per frame
    void Update () {
        
        //manual gravity
        // rb.AddRelativeForce(new Vector3(0, -5000, 0));

        // Debug.Log(undersideTouching.ToString());
        //Debug.Log(isPlayerTouchingBrick().ToString());
        //if (isPlayerTouchingBrick() && Input.GetButtonDown("Fire1"))
        //{
         //   Destroy(touchingBrick);
         //   undersideTouching = false;
        //}

    }

    private void FixedUpdate()
    {
        sideColliding();
        /*
        Debug.Log(touchingWall.ToString());

        if (rb.velocity.sqrMagnitude > maxSpeed)
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
           // rb.velocity *= 0.5f;
        }

        if (!touchingWall)
        {
           // Vector3 tempVel = rb.velocity;
           // tempVel.x *= 0.3f;
           // rb.velocity = tempVel;
           
        }*/

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveVertical < 0) moveVertical = 0;
        //Debug.Log(moveHorizontal.ToString());

        //if (!undersideTouching)
        //{
            rb.AddForce(new Vector3(0, -manualGravity));
        //}
        
        /*
        if (Input.GetButtonDown("Jump"))
        {
         //   framesSinceJump = 10;
            rb.velocity = (new Vector3(0, 0, 0));
            if (sideTouching == 'l' && moveHorizontal < -0.9f)
            {
                rb.AddForce(new Vector3(jumpScale*0.5f, jumpScale, 0), ForceMode.Force);
            } else if (sideTouching == 'r' && moveHorizontal > 0.9f)
            {
                rb.AddForce(new Vector3(-jumpScale*0.5f, jumpScale, 0), ForceMode.Force);
            } else
            {
                rb.AddForce(new Vector3(jumpScale*moveHorizontal, jumpScale, 0), ForceMode.Force);
            }
        }
        */
        //rb.AddForce(new Vector3(moveHorizontal*speedScale, 0));

        //THIS WORKS BUT JITTER WHEN LOW
        rb.velocity = (new Vector3(moveHorizontal * speedScale, (moveVertical)*speedScale+1, 0));

        //rb.velocity = new Vector3(moveHorizontal * 50, 0, 0);
    }
}
