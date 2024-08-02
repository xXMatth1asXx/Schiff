using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private InputManager inputManager;
    private Shooting shooting;
    private MortarShooting mortarShooting;

    [SerializeField] GameObject sails;
    [Header("Positions")]
    [Space]
    [SerializeField] private Vector3 steeringPos, topPos, leftCannonSidePos, rightCannonSidePos, mortarPos;
    [Space]
    [Header("Rotations")]
    [Space]
    [SerializeField] private Vector3 steeringRot, leftCannonSideRot, rightCannonSideRot, otherRot;

    [SerializeField] private float screenDiv;

    private Vector2 mousePosition;

    private void Start()
    {
        inputManager = transform.parent.gameObject.GetComponent<InputManager>();
        shooting = transform.parent.gameObject.GetComponent<Shooting>();
        mortarShooting = transform.parent.gameObject.GetComponent<MortarShooting>();
    }

    public void GetMousePosition(Vector2 otherMousePosition)
    {
        mousePosition = otherMousePosition;
    }

    private void Update()
    {
        float calculation = Mathf.Abs(Screen.width / 2f - mousePosition.x) / (Screen.width / 2f);
        if (inputManager.Space())
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, topPos, Time.deltaTime * 10f);
            if ((topPos - transform.localPosition).magnitude <= shooting.shootingRange)
            {
                if (mousePosition.x < (Screen.width - Screen.width / screenDiv) / 2f)
                    transform.Rotate(Vector3.up * -calculation);
                else if (mousePosition.x >= (Screen.width + Screen.width / screenDiv) / 2f)
                    transform.Rotate(Vector3.up * calculation);
            }
            else        
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, otherRot, calculation * Time.deltaTime * 10000f);
            //print("hoch");    
        }
        else if (inputManager.R() || mortarShooting.isAimingMortar)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, mortarPos, Time.deltaTime * 10f);
            if ((mortarPos - transform.localPosition).magnitude <= shooting.shootingRange){
                sails.SetActive(false);
                if (!mortarShooting.isAimingMortar)
                {
                    if (mousePosition.x < (Screen.width - Screen.width / screenDiv) / 2f)
                        transform.Rotate(Vector3.up * -calculation);
                    else if (mousePosition.x >= (Screen.width + Screen.width / screenDiv) / 2f)
                        transform.Rotate(Vector3.up * calculation);
                }
            }
            else
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, otherRot, calculation * Time.deltaTime * 10000f);
            }

            mortarShooting.MortarFire(mortarPos, transform.localPosition, transform.localRotation.eulerAngles);
            
            //print("vorne");
        }
        else if ((mousePosition.x < (Screen.width - Screen.width / screenDiv) / 2f) && !shooting.isAmingRight && !mortarShooting.isAimingMortar)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, leftCannonSidePos, calculation * Time.deltaTime * 2f);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, leftCannonSideRot, calculation * Time.deltaTime * 2f);

            shooting.LeftFire(leftCannonSidePos, transform.localPosition);

            //print("links");
        }
        else if ((mousePosition.x >= (Screen.width + Screen.width / screenDiv  ) / 2f) && !shooting.isAmingLeft && !mortarShooting.isAimingMortar)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, rightCannonSidePos, calculation * Time.deltaTime * 2f);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, rightCannonSideRot, calculation * Time.deltaTime * 2f);

            shooting.RightFire(rightCannonSidePos, transform.localPosition);
            //print("rechts");
        }
        else if ((transform.localPosition != steeringPos || transform.localEulerAngles != steeringRot) && !shooting.isAmingLeft && !shooting.isAmingRight && !mortarShooting.isAimingMortar)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, steeringPos, Time.deltaTime * 10f);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, steeringRot, Time.deltaTime * 10f);
            sails.SetActive(true);
            //print("zurück");
        }
    }

}
    