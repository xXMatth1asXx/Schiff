using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum input
    {
        keyboard,
        mobile
    }
    public input InputController;

    private BoatScript boatScript;
    private Shooting shooting;
    private MortarShooting mortarShooting;
    private Camera camera;

    private Vector2 inputVector;
    private Vector2 mousePosition;


    private void Start()
    {
        boatScript = gameObject.GetComponent<BoatScript>();
        shooting = gameObject.GetComponent<Shooting>();
        mortarShooting = gameObject.GetComponent<MortarShooting>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        switch (InputController)
        {
            case input.keyboard:
                Keyboard();
                break;
            case input.mobile:
                Mobile();
                break;
        }
    }

    private void Keyboard()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(horizontal, vertical);

        mousePosition = Input.mousePosition;

        boatScript.GetHorizontalAndVertical(inputVector);

        camera.GetMousePosition(mousePosition);
        shooting.GetMousePosition(mousePosition);
        shooting.GetInputs(LeftClick(), RightClick(), RightClickDown());
        mortarShooting.GetMousePosition(mousePosition);
        mortarShooting.GetInputs(LeftClick(), RightClick(), RightClickDown());
    }


    private void Mobile()
    {

    }

    public bool Space()
    {
        if (Input.GetKey(KeyCode.Space))
            return true;
        else
            return false;
    }

    public bool R()
    {
        if (Input.GetKey(KeyCode.R))
            return true;
        else
            return false;
    }

    public bool LeftClick()
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }

    public bool RightClick()
    {
        if (Input.GetMouseButton(1))
            return true;
        else
            return false;
    }

    public bool RightClickDown()
    {
        if (Input.GetMouseButtonDown(1))
            return true;
        else
            return false;
    }
}
