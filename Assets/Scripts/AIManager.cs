using UnityEngine;

public class AIManager : MonoBehaviour
{
    private BoatScript boatScript;
    private Shooting shooting;

    private RaycastHit hitForward;
    private GameObject playerShip;
    [SerializeField] private GameObject target1, target2;
    private Vector3 relative;
    private float speed;

    //[SerializeField] [Range(1, 100)] float targetDistance;

    public bool forward1, forward2, forward3, forwardLeft, forwardRight, left1, left2, left3, right1, right2, right3, leftBack, rightBack, back1, back2, back3, trackingTheTarget, start;
    private void Start()
    {
        boatScript = gameObject.GetComponent<BoatScript>();
        shooting = gameObject.GetComponent<Shooting>();
        playerShip = GameObject.Find("Ship");
        target1 = GameObject.Find("Target1");
        target2 = GameObject.Find("Target2");
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0.5f, 0, -1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(0.5f, 0, -1)) * hitForward.distance, Color.red);
            forward1 = true;
        }
        else
            forward1 = false;

        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(-Vector3.forward) * hitForward.distance, Color.red);
            forward2 = true;
        }
        else
            forward2 = false;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-0.5f, 0, -1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(-0.5f, 0, -1)) * hitForward.distance, Color.red);
            forward3 = true;
        }
        else
            forward3= false;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, -1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(1, 0, -1)) * hitForward.distance, Color.red);
            forwardLeft = true;
        }
        else
            forwardLeft = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, -1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(-1, 0, -1)) * hitForward.distance, Color.red);
            forwardRight = true;
        }
        else
            forwardRight = false;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), transform.TransformDirection(Vector3.right), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z - 10), transform.TransformDirection(Vector3.right) * hitForward.distance, Color.red);
            left1 = true;
        }
        else
            left1 = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(Vector3.right) * hitForward.distance, Color.red);
            left2 = true;
        }
        else
            left2 = false;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 10), transform.TransformDirection(Vector3.right), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z + 10), transform.TransformDirection(Vector3.right) * hitForward.distance, Color.red);
            left3 = true;
        }
        else
            left3 = false;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 10), transform.TransformDirection(-Vector3.right), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z + 10), transform.TransformDirection(-Vector3.right) * hitForward.distance, Color.red);
            right1 = true;
        }
        else
            right1 = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.right), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(-Vector3.right) * hitForward.distance, Color.red);
            right2 = true;
        }
        else
            right2 = false;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), transform.TransformDirection(-Vector3.right), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z - 10), transform.TransformDirection(-Vector3.right) * hitForward.distance, Color.red);
            right3 = true;
        }
        else
            right3 = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(1, 0, 1)) * hitForward.distance, Color.red);
            leftBack = true;
        }
        else
            leftBack = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(-1, 0, 1)) * hitForward.distance, Color.red);
            rightBack = true;
        }
        else
            rightBack = false;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0.5f, 0, 1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(0.5f, 0, 1)) * hitForward.distance, Color.red);
            back1 = true;
        }
        else
            back1 = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(Vector3.forward) * hitForward.distance, Color.red);
            back2 = true;
        }
        else
            back2 = false;
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-0.5f, 0, 1)), out hitForward, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z), transform.TransformDirection(new Vector3(-0.5f, 0, 1)) * hitForward.distance, Color.red);
            back3 = true;
        }
        else
            back3 = false;




        /*if (forward1 && forwardLeft)
            boatScript.GetHorizontalAndVertical(new Vector2(1, 0.6f));
        else
            boatScript.GetHorizontalAndVertical(Vector2.up);*/

        /* float angleZ = Vector3.SignedAngle(transform.position, target1.transform.position, Vector3.up);
         float angleX = Vector3.SignedAngle(transform.position, target1.transform.position, Vector3.forward);*/




        float disShip = Vector3.Distance(transform.position, playerShip.transform.position);
        float disTarget1 = Vector3.Distance(transform.position, target1.transform.position);
        float disTarget2 = Vector3.Distance(transform.position, target2.transform.position);
 

        if (disShip <= 60 || disTarget1 <= 60 || disTarget2 <= 60)
        {
            trackingTheTarget = true;
            if (disTarget1 <= disTarget2)
            {
                relative = -transform.InverseTransformPoint(target1.transform.position);
                Debug.DrawLine(transform.position, target1.transform.position, Color.blue, 0.1f);
                speed = disTarget1;
            }
            else
            {
                relative = -transform.InverseTransformPoint(target2.transform.position);
                Debug.DrawLine(transform.position, target2.transform.position, Color.gray, 0.1f);
                speed = disTarget2;
            }
            
        }
        else
        {
            trackingTheTarget = false;
            relative = -transform.InverseTransformPoint(playerShip.transform.position);
            Debug.DrawLine(transform.position, playerShip.transform.position, Color.green, 0.1f);
        }



        if (back1 || back2 || back3)
            speed = 0.2f;
        else if (leftBack || rightBack)
            speed = 0.5f;
        else if ((forwardLeft || forwardRight))
            speed = 1.2f;
        else if ((forward1 || forward2 || forward3) && !trackingTheTarget)
            speed = 3f;
        else if ((left1 && left2 && left3) || (right1 && right2 && right3)){
            speed = 1f;
            shooting.GetInputs(true, true, false);
            shooting.GetInputs(true, false, false);
            shooting.LeftFire(Vector3.zero, Vector3.zero);
            shooting.RightFire(Vector3.zero, Vector3.zero);
        }
        else if (((left1 && left2) || (right1 && right2)) && trackingTheTarget)
        {
            speed = 0.8f;
            shooting.GetInputs(true, true, false);
            shooting.LeftFire(Vector3.zero, Vector3.zero);
            shooting.RightFire(Vector3.zero, Vector3.zero);
        }
        else if (((left2 && left3) || (right2 && right3)) && trackingTheTarget)
        {
            speed = 1.2f;
            shooting.GetInputs(true, true, false);
            shooting.LeftFire(Vector3.zero, Vector3.zero);
            shooting.RightFire(Vector3.zero, Vector3.zero);
        }
        else if ((left1 || right1) && trackingTheTarget)
        {
            speed = 0.7f;
        }
        else if ((left3 || right3) && trackingTheTarget)
        {
            speed = 1.3f;
        }
        else if (!trackingTheTarget)
            speed = 0f;
        else
            speed = 1.1f;

        if (!start)
        {
            speed = 1.2f;
            start = true;
        }
        //print("Steering: " + relative.normalized.x + " Speed: " + speed + " DisShip: " + disShip + " DisTargetLeft: " + disTarget1  + " DisTargetRight: " + disTarget2 );
        boatScript.GetHorizontalAndVertical(new Vector3(relative.normalized.x, speed));


    }


}
 