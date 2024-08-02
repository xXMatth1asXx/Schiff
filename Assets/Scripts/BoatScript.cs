using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    private Wind wind;
    private Vector2 inputVector;

    private Rigidbody rb;
    private Vector3 velocity;
    private float vertical, horizontal, timer, inputLimit = 1;

    [SerializeField] [Range(1, 100)] private float speed, waterResistance, maxSpeed;

    [SerializeField] AnimationCurve turningCurve, windAffectness;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        wind = GameObject.Find("GameManager").GetComponent<Wind>();
    }

    private void FixedUpdate()
    {
        CalculateVertical();
        Steering();
        CalculateHorizontal();
        WaterResistance();
    }

    public void GetHorizontalAndVertical(Vector2 otherInputVector)
    {
        inputVector = otherInputVector;
        if (inputVector.y > inputLimit)
            inputLimit = inputVector.y;
    }


    private void CalculateVertical()
    {
        if (inputLimit == 1)
        {
            if (vertical <= 1 && vertical >= 0)
            {
                vertical += inputVector.y * Time.deltaTime;
            }
            else if (vertical > 1)
            {
                vertical = 1;
            }
            else
            {
                vertical = 0;
            }
        }
        else
        {
            vertical = inputVector.y;
        }
    }

    private void CalculateHorizontal()
    {
        if (horizontal <= 1 && horizontal >= -1)
        {
            if (inputVector.x != 0)
            {
                horizontal = inputVector.x;
            }
            else
            {
                if (horizontal != 0)
                {
                    if (horizontal >= 0)
                    {
                        horizontal -= Time.deltaTime * 0.5f;
                    }
                    else
                    {
                        horizontal += Time.deltaTime * 0.5f;
                    }
                }
            }
        }
    }

    private void Steering()
    {
        Gas();
        Turning();
    }

    private void Gas()
    {
        velocity = rb.velocity;
        
        float sinusac = (1f - Mathf.Abs(Mathf.Sin(velocity.magnitude / maxSpeed))) * 10f;

        float windMultiplier = windAffectness.Evaluate( CalculateWind());
        //print(windMultiplier + " | "  + Vector3.Angle(wind.windDirection, transform.forward));
        

        Vector3 addForce = new Vector3(0, 0, -vertical * speed * sinusac * windMultiplier * Time.deltaTime * 10);

        rb.AddRelativeForce(addForce);

        //print(sinusac + " | " + velocity.magnitude);
    }

    private void Turning()
    {
        //transform.rotation = Quaternion.LookRotation(-velocity);

        if (horizontal != 0)
        {
            timer += Time.deltaTime;
            float multiplier = turningCurve.Evaluate(timer);
            
            float sinusac = 1f - Mathf.Abs(Mathf.Sin(velocity.magnitude / maxSpeed));
            
            transform.Rotate(new Vector3(0, horizontal * multiplier * sinusac, 0));
        }
        if (inputVector.x == 0)
        {
            timer = 0;
        }
    }

    private void WaterResistance()
    {
        rb.AddForce(-velocity * waterResistance * Time.deltaTime);
    }

    private float CalculateWind()
    {
        //print( Vector3.Angle(wind.windDirection, transform.forward));
        return Vector3.Angle(wind.windDirection, transform.forward);
    }
}
