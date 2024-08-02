using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShooting : MonoBehaviour
{
    private Shooting shooting;

    public bool isAimingMortar = false;

    [SerializeField] GameObject aimingMortarGameObject, topMortarGameObject, bottomMortarGameObject;

    [SerializeField] [Range(1, 100)] private float sensivity, reloadTimeMortar, amountMortarBalls;

    [SerializeField] private Object cannonballPrefab;

    private Vector2 previousMousePosition;
    private float reloadTimer;
    private bool readyToFireMortar;

    private Vector2 mousePosition;
    private bool leftClick, rightClick, rightClickDown;

    private void Start()
    {
        reloadTimer = reloadTimeMortar;
        shooting = gameObject.GetComponent<Shooting>();
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

    public void MortarFire(Vector3 mortarPos, Vector3 cameraPosition, Vector3 cameraRotation)
    {
        Vector2 div = mousePosition - previousMousePosition;
        if (rightClick)
        {
            if (rightClickDown)
            {
                aimingMortarGameObject.transform.localEulerAngles = new Vector3(0, 180 + cameraRotation.y, 0);
                aimingMortarGameObject.transform.GetChild(0).localPosition = Vector3.forward * -60f;
            }

            
            if ((mortarPos - cameraPosition).magnitude <= shooting.shootingRange)
            {
                isAimingMortar = true;
                aimingMortarGameObject.SetActive(true);
                

                aimingMortarGameObject.transform.Rotate(Vector3.up * div.x * sensivity * Time.deltaTime);

                aimingMortarGameObject.transform.GetChild(0).localPosition -= Vector3.forward * div.y * sensivity * Time.deltaTime * 1.5f;

                if (leftClick && readyToFireMortar)
                {
                    readyToFireMortar = false;

                    for (int i = 0; i < amountMortarBalls; i++)
                    {
                        Vector3 fireDir = bottomMortarGameObject.transform.position - topMortarGameObject.transform.position;
                        Vector3 randomPos = new Vector3(Random.Range(topMortarGameObject.transform.position.x - 3f, topMortarGameObject.transform.position.x + 3f), Random.Range(topMortarGameObject.transform.position.y - 3f, topMortarGameObject.transform.position.y + 3f), Random.Range(topMortarGameObject.transform.position.z - 3f, topMortarGameObject.transform.position.z + 3f));
                        GameObject cannonball = Instantiate(cannonballPrefab, randomPos, Quaternion.Euler(fireDir.normalized), GameObject.Find("Cannonballs").transform) as GameObject;
                        cannonball.GetComponent<Rigidbody>().AddRelativeForce(-Vector3.up * 1000f);

                        //firePointsLeft[i].GetComponent<ParticleSystem>().Play();
                        Destroy(cannonball, 8f);
                    }
                }
            }
        }
        else
        {
            isAimingMortar = false;
            aimingMortarGameObject.SetActive(false);
        }

        previousMousePosition = mousePosition;
    }

    private void Update()
    {
        if (!readyToFireMortar && reloadTimer >= 0)
        {
            reloadTimer -= Time.deltaTime;
        }
        else
        {
            readyToFireMortar = true;
            reloadTimer = reloadTimeMortar;
        }
    }
}
