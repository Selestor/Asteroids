using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public AudioClip destruction;
    public GameObject asteroid;
    public GameObject asteroidExplosionFX;

    public int points;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(0f, 150.0f));
        
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(0f, 90.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Missle")
        {
            Destroy(collision.gameObject);
            GameManager.instance.AdjustScore(points);

            if (tag == "AsteroidLarge" || tag == "AsteroidMedium")
            {
                GameObject instance1 = Instantiate(asteroid, new Vector3(transform.position.x - .5f, transform.position.y - .5f, 0), Quaternion.Euler(0, 0, 45));
                GameObject instance2 = Instantiate(asteroid, new Vector3(transform.position.x + .5f, transform.position.y + .5f, 0), Quaternion.Euler(0, 0, 135));
                instance1.transform.SetParent(GameManager.instance.asteroidHolder);
                instance2.transform.SetParent(GameManager.instance.asteroidHolder);
            }
            AudioSource.PlayClipAtPoint(destruction, Camera.main.transform.position);
            Instantiate(asteroidExplosionFX, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.SubstractLife();
            GameManager.instance.GotHit();
        }
    }
}
