using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Vector3 windDirection;

    [SerializeField] private GameObject airsock;

    private void Start()
    {
        Debug.DrawLine(Vector3.up * 15f, windDirection * 15f + Vector3.up * 15f, Color.blue, Mathf.Infinity);
        airsock.transform.rotation = Quaternion.Euler(windDirection);
    }

    private void Update()
    {
        airsock.transform.rotation = Quaternion.Euler(windDirection);
    }
}
