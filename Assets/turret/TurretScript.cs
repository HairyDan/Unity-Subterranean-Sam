using UnityEngine;

public class TurretScript : MonoBehaviour {

    public GameObject turret;

    public GameObject gun;

    public GameObject lookAtTarget;

    public GameObject bullet;

    public int maxRange;


    public bool canSeePlayer()
    {
        //Debug.DrawRay(turret.transform.position, lookAtTarget.transform.position - turret.transform.position);
        
        //Debug.DrawRay(turret.transform.position, new Vector3();
        RaycastHit hitinfo;
        if (Physics.Raycast(gun.transform.position, lookAtTarget.transform.position - turret.transform.position, out hitinfo, maxRange))
        {
            //Debug.Log("HIT SOMETHING");
            //Debug.Log(hitinfo.collider.ToString());
            if(hitinfo.collider.name == "player")
            {
                //Debug.Log("hit the player");
                fire();
            }
        }
        else
        {
            //Debug.Log("didnt hit");
        }
        return false;
    }

    void fire()
    {
        Vector3 correctedRotation = turret.transform.rotation.eulerAngles;

        correctedRotation = new Vector3(correctedRotation.x, correctedRotation.y, correctedRotation.z + 90);
        GameObject instanceBullet = Instantiate(bullet, gun.transform.position, Quaternion.Euler(correctedRotation)) as GameObject;
        Rigidbody laserBullet = instanceBullet.GetComponent<Rigidbody>();
        laserBullet.AddRelativeForce(new Vector3(0.0f, -800.0f, 0.0f));
    }
    // Use this for initialization
    void Start () {
        InvokeRepeating("canSeePlayer", 2.0f, 0.8f);
    }



    // Update is called once per frame
    void Update() {
        //float num = Mathf.PingPong(Time.time, 2);
        //num -= 1;
        //Debug.Log(num);
        //turret.transform.rotation = new Quaternion(0,0,(Mathf.PingPong(Time.time, 10)-5f)*36,0);
        //turret.transform.rotation = new Quaternion(0, 0, num, turret.transform.rotation.w);
        //turret.transform.position = new Vector3(Mathf.PingPong(Time.time, 1), 0);
        //Debug.Log(framesSinceShot);
        

        Quaternion newRotation;
        if (turret.transform.position.x <= lookAtTarget.transform.position.x)
        {
            newRotation = Quaternion.LookRotation(turret.transform.position - lookAtTarget.transform.position, Vector3.up);
        }
        else
        {
            newRotation = Quaternion.LookRotation(turret.transform.position - lookAtTarget.transform.position, Vector3.down);
        }
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        //newRotation.z -= 90.0f;
        //Debug.Log(newRotation.z);
       // if (newRotation.z > 0) newRotation.z -= 0.5f;
       // else newRotation.z += 0.5f;
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, newRotation, Time.deltaTime * 8);
        
    }
}
