using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletControl : MonoBehaviour
{

    //Base speed values
    public float baseForwardForce, baseHorizontalForce, baseVerticalForce, rotateSpeed, rollSpeed;

    //How much to slow time upon bullet's creation
    public int timeIndex;

    //Can the bullet speed up
    public bool boostEnabled;
    public float boostMultiplier;

    //Acceleration values per direciton
    public float forwardAcceleration, horizontalAcceleration, verticalAcceleration, rollAcceleration;

    //Mouse detection vectors
    private Vector2 lookInput, screenCenter, mouseDistance;

    //Calculated speed values
    private float activeForwardForce, activeHorizontalForce, activeVerticalForce;

    //Q - E input value
    private float rollInput;

    //Referenced out of object components
    public GameManager manager;

    //Variables for distance checking at high speed
    public float collisionCheckDistance;
    public LayerMask collisionCheckTargets;
    private bool impacted = false;
    public bool checkBoost, boosting = false, destroyOnTouch = false;

    public string[] collisionTagList;
    public int ignoreOnBoost;
    

    private void Start()
    {

        //Collect relevant components 
        //gun = GameObject.Find("Gun").GetComponent<GunFire>();
        manager = GameObject.Find("Manager").GetComponent<GameManager>();

        //Find the center of the screen
        screenCenter = new Vector2(Screen.width * .5f, Screen.height * .5f);

        //Free up the cursor to move
        Cursor.lockState = CursorLockMode.Confined;

        //Slow time
        TimeManager.SlowTime(timeIndex);
    }
    void Update()
    {
        //Use player mouse location to rotate bullet
        //Find where player is looking
        lookInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //Calculate mouse distance from screen center
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        //Clamp mouse distance range for regular speed
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        //Receive and calculate input from roll axis( Q - E )
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        //Rotate bullet by final values
        transform.Rotate(-mouseDistance.y * rotateSpeed * Time.deltaTime, mouseDistance.x * rotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);





        if (Input.GetKey(KeyCode.Space) && boostEnabled)
        {
            //Acceleration if enabled
            activeForwardForce = Mathf.Lerp(activeForwardForce, baseForwardForce * boostMultiplier, forwardAcceleration * Time.deltaTime);
            boosting = true;
            /*
            RaycastHit hit;
            if ((body.SweepTest(transform.forward, out hit, collisionCheckDistance)))
            {
                
                if(hit.collider.CompareTag("Ground"))
                {
                    BulletImpact();
                }

            }
            */
            if (Physics.CheckSphere(transform.position, collisionCheckDistance, collisionCheckTargets) && checkBoost)
            {
                Debug.Log("About to hit");
                if (!impacted)
                {
                    BulletImpact();
                }

            }
        }
        else
        {
            //Static forward speed
            activeForwardForce = Mathf.Lerp(activeForwardForce, baseForwardForce, forwardAcceleration * Time.deltaTime);
            boosting = false;
        }

        //Grab inputs and modify by base speed
        activeHorizontalForce = Mathf.Lerp(activeHorizontalForce, Input.GetAxisRaw("Horizontal") * baseHorizontalForce, horizontalAcceleration * Time.deltaTime);
        activeVerticalForce = Mathf.Lerp(activeVerticalForce, Input.GetAxisRaw("Vertical") * baseVerticalForce, verticalAcceleration * Time.deltaTime);


        //Move Bullet according to modified forces
        transform.position += (transform.forward * activeForwardForce * Time.deltaTime) + (transform.right * activeHorizontalForce * Time.deltaTime) + (transform.up * activeVerticalForce * Time.deltaTime);



    }

    private void OnTriggerEnter(Collider other)
    {
        foreach( string tag in collisionTagList)
        {
            if(other.gameObject.CompareTag(tag) && !impacted)
            {
                if(other.gameObject.CompareTag(collisionTagList[ignoreOnBoost]) && boosting && destroyOnTouch)
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    BulletImpact();
                }
                

            }
        }
    }

    public void BulletImpact()
    {
        Debug.Log("Impact");
        impacted = true;

        //Return time value to standard
        TimeManager.ResetTime();

        //Allow player to fire again
        GunFire.canFire = true;
        GunFire.controlling = false;

        //Separate camera to avoid deletion
        gameObject.transform.DetachChildren();

        //Return control to player character
        manager.SwitchControl();

        //Return to player camera
        CameraSwitcher.SwitchCamera(manager.playerCamera);

        //Return the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        //Delete bullet
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, collisionCheckDistance);
    }

    private void OnBecameInvisible()
    {
        if (!impacted)
        {
            BulletImpact();
        }

    }

}

