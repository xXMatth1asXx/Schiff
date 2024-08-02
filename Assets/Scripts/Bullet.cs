using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0, 100)] public float bulletDamage;
    [SerializeField] private Object[] explosion;

    float timer = 0.1f;

    private void Update()
    {
        if (timer < 0)
        {
            gameObject.tag = "Bullet";
        }
        else
            timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("AIShip")) && gameObject.CompareTag("Bullet"))
        {
            GameObject explosionGameObject = Instantiate(explosion[Random.Range(0, explosion.Length - 1)], gameObject.transform.position, Quaternion.identity, other.transform) as GameObject;
            explosionGameObject.GetComponent<ParticleSystem>().Play();
            Destroy(explosionGameObject, 3f);
        }
    }
}
