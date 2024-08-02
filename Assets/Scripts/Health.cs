using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Bullet bullet;

    [SerializeField] [Range(0, 1000)] private float health;

    [SerializeField] private ParticleSystem[] particleSystems;

    private float healthStart;

    private void Start()
    {
        healthStart = health;
        print(particleSystems.Length);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            bullet = other.GetComponent<Bullet>();
            health -= bullet.bulletDamage;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        else if (health <= healthStart * 0.1f)
        {
            while (true){
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
       else if (health <= healthStart * 0.2f)
        {
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        else if (health <= healthStart * 0.3f)
        {
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        else if (health <= healthStart * 0.4f)
        {
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        else if (health <= healthStart * 0.5f)
        {
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        else if (health <= healthStart * 0.6f)
        {
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        else if (health <= healthStart * 0.7f)
        {
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        else if (health <= healthStart * 0.8f)
        {
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        else if (health <= healthStart * 0.9f)
        {
            print("test");
            while (true)
            {
                int x = Random.Range(0, particleSystems.Length - 1);
                if (!particleSystems[x].isPlaying)
                {
                    particleSystems[x].Play();
                    break;
                }
            }
        }
        
    }
}
