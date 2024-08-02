using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject[] firePointsLeft, firePointsRight;

    private Vector2 previousMousePosition;
    private float refreshTimeNormalLeft, refreshTimeNormalRight, refreshTimeStrongLeft, refreshTimeStrongRight;
    private int indexLeft, indexRight;

    [SerializeField] private Object cannonballPrefab;
    [SerializeField] private GameObject aimingCurveLeft, aimingCurveRight, aimerLeft, aimerRight;

    [SerializeField] [Range(0, 100)] private float sensivity, shootingAngle, fireStrength, reloadTimeNormal, reloadTimeStrong, volleyAmount, volleyTime;
    [Range(1, 100)] public float shootingRange;

    public bool isAmingLeft = false, isAmingRight = false;

    private bool readyToFireLeft, readyToFireRight, readyToStrongFireLeft, readyToStrongFireRight;

    private Vector2 mousePosition;
    private bool leftClick, rightClick, rightClickDown;

   

    private void Start()
    {

        refreshTimeNormalLeft = reloadTimeNormal;
        refreshTimeNormalRight = reloadTimeNormal;
        refreshTimeStrongLeft = reloadTimeStrong;
        refreshTimeStrongRight = reloadTimeStrong;
    }

    public void GetMousePosition(Vector2 otherMousePosition)
    {
        mousePosition = otherMousePosition;
    }

    public void GetInputs(bool otherLeftClick, bool otherRightClick, bool otherRightClickDown)
    {
        leftClick = otherLeftClick;
        rightClick = otherRightClick;
        rightClickDown = otherRightClickDown;
    }

    public void LeftFire(Vector3 leftCannonSidePos, Vector3 cameraPosition)
    {
        Vector2 div = mousePosition - previousMousePosition;

        if (rightClick)
        {
            if (rightClickDown)
            {
                aimingCurveLeft.transform.localEulerAngles = Vector3.zero;
                Transform child = aimingCurveLeft.transform.GetChild(0);
                child.localScale = new Vector3(60.05271f, 3.761446f, 5.059998f);
                child.localPosition = Vector3.right * 34.35f;
            }

            if (Vector3.Distance(cameraPosition, leftCannonSidePos) <= shootingRange)
            {
                isAmingLeft = true;
                aimingCurveLeft.SetActive(true);

                //Left and Right
                aimingCurveLeft.transform.Rotate(Vector3.up * div.x * sensivity * Time.deltaTime);
                //aimingCurveLeft.transform.localEulerAngles = Vector3.up * Mathf.Clamp(aimingCurveLeft.transform.eulerAngles.y, -shootingAngle, shootingAngle);


                //Up and down
                Transform child = aimingCurveLeft.transform.GetChild(0);

                child.localScale += Vector3.right * div.y * sensivity * Time.deltaTime;
                child.localPosition -= Vector3.right * -div.y * sensivity * Time.deltaTime * 0.5f;
                if (leftClick && readyToFireLeft)
                {
                    readyToFireLeft = false;
              
                    for (int i = 0; i < firePointsLeft.Length; i++)
                    {
                        Vector3 fireDir = aimerLeft.transform.position - firePointsLeft[i].transform.position;
                        GameObject cannonball = Instantiate(cannonballPrefab, firePointsLeft[i].transform.position, Quaternion.Euler(fireDir.normalized), GameObject.Find("Cannonballs").transform) as GameObject;
                        cannonball.GetComponent<Rigidbody>().AddRelativeForce(fireDir * 1.2f * fireStrength + Vector3.up * fireDir.magnitude * 0.2f * fireStrength);

                        firePointsLeft[i].GetComponent<ParticleSystem>().Play();
                        Destroy(cannonball, 3);
                    }
                }

            }
        }
        else
        {
            isAmingLeft = false;
            aimingCurveLeft.SetActive(false);
        }
        if (leftClick && !rightClick && readyToStrongFireLeft)
        {
            readyToStrongFireLeft = false;
            SpawnNextCannonballLeft();
            
        }
        previousMousePosition = mousePosition;
    }

    private void SpawnNextCannonballLeft()
    {
        //print(indexLeft);
        for (int i = 0; i < firePointsLeft.Length; i++)
        {
            GameObject cannonball = Instantiate(cannonballPrefab, firePointsLeft[i].transform.position, firePointsLeft[i].transform.rotation, GameObject.Find("Cannonballs").transform) as GameObject;
            cannonball.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * fireStrength * 300f);

            firePointsLeft[i].GetComponent<ParticleSystem>().Play();
            Destroy(cannonball, 3);
        }
        if (indexLeft + 2 <= volleyAmount)
        {
            indexLeft++;
            Invoke("SpawnNextCannonballLeft", volleyTime);
        }
        else
            indexLeft = 0;
    }



    public void RightFire(Vector3 rightCannonSidePos, Vector3 cameraPosition)
    {
        Vector2 div = mousePosition - previousMousePosition;

        if (rightClick)
        {
            if (rightClickDown)
            {
                aimingCurveRight.transform.localEulerAngles = Vector3.zero;
                Transform child = aimingCurveRight.transform.GetChild(0);
                child.localScale = new Vector3(60.05271f, 3.761446f, 5.059998f);
                child.localPosition = Vector3.right * -34.35f;
            }

            if (Vector3.Distance(cameraPosition, rightCannonSidePos) <= shootingRange)
            {
                isAmingRight = true;
                aimingCurveRight.SetActive(true);

                //Left and Right
                aimingCurveRight.transform.Rotate(Vector3.up * div.x * sensivity * Time.deltaTime);
                


                //Up and down
                Transform child = aimingCurveRight.transform.GetChild(0);

                child.localScale += Vector3.right * div.y * sensivity * Time.deltaTime;
                child.localPosition += Vector3.right * -div.y * sensivity * Time.deltaTime * 0.5f;
                if (leftClick && readyToFireRight)
                {
                    readyToFireRight = false;
                    for (int i = 0; i < firePointsRight.Length; i++)
                    {
                        Vector3 fireDir = aimerRight.transform.position - firePointsRight[i].transform.position;
                        GameObject cannonball = Instantiate(cannonballPrefab, firePointsRight[i].transform.position, Quaternion.Euler(fireDir.normalized), GameObject.Find("Cannonballs").transform) as GameObject;
                        cannonball.GetComponent<Rigidbody>().AddRelativeForce(fireDir * 1.2f * fireStrength + Vector3.up * fireDir.magnitude * 0.2f * fireStrength);

                        firePointsRight[i].GetComponent<ParticleSystem>().Play();
                        Destroy(cannonball, 3);
                    }
                }

            }
        }
        else
        {
            isAmingRight = false;
            aimingCurveRight.SetActive(false);
        }
        if (leftClick && !rightClick && readyToStrongFireRight)
        {
 
            readyToStrongFireRight = false;
            SpawnNextCannonballRight();
        }
        previousMousePosition = mousePosition;
    }

    private void SpawnNextCannonballRight()
    {
        print(indexRight);
        for (int i = 0; i < firePointsRight.Length; i++)
        {
            GameObject cannonball = Instantiate(cannonballPrefab, firePointsRight[i].transform.position, firePointsRight[i].transform.rotation, GameObject.Find("Cannonballs").transform) as GameObject;
            cannonball.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * fireStrength * 300f);

            firePointsRight[i].GetComponent<ParticleSystem>().Play();
            Destroy(cannonball, 3);
        }
        if (indexRight + 2 <= volleyAmount)
        {
            indexRight++;
            Invoke("SpawnNextCannonballRight", volleyTime);
        }
        else
            indexRight = 0;
    }

    private void Update()
    {
        if (!readyToFireLeft && refreshTimeNormalLeft >= 0)
        {
            refreshTimeNormalLeft -= Time.deltaTime;
        }
        else
        {
            readyToFireLeft = true;
            refreshTimeNormalLeft = reloadTimeNormal;
        }

        if (!readyToFireRight && refreshTimeNormalRight >= 0)
        {
            refreshTimeNormalRight -= Time.deltaTime;
        }
        else
        {
            readyToFireRight = true;
            refreshTimeNormalRight = reloadTimeNormal;
        }


        if (!readyToStrongFireLeft && refreshTimeStrongLeft >= 0)
        {
            refreshTimeStrongLeft -= Time.deltaTime;
        }
        else
        {
            readyToStrongFireLeft = true;
            refreshTimeStrongLeft = reloadTimeStrong;
        }

        if (!readyToStrongFireRight && refreshTimeStrongRight >= 0)
        {
            refreshTimeStrongRight -= Time.deltaTime;
        }
        else
        {
            readyToStrongFireRight = true;
            refreshTimeStrongRight = reloadTimeStrong;
        }


    }
}
