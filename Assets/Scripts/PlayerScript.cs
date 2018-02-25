using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject misslePrefab;
    public AudioClip shotsFired;
    public GameObject engineParticle;
    public GameObject breakParticle;

    public int thrust;
    public int maxSpeed;
    public int rotationSpeed;
    public int missleSpeed;

    private Rigidbody2D rb2d;
    private GameObject engine;
    private GameObject breakLeft;
    private GameObject breakRight;

    // Update is called once per frame
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        engine = Instantiate(engineParticle, transform.position, Quaternion.Euler(transform.rotation.x + 90, 0, 0));
        engine.transform.SetParent(gameObject.transform);
        breakLeft = Instantiate(breakParticle, new Vector3(transform.position.x + 0.22f, transform.position.y, 0), Quaternion.Euler(transform.rotation.x - 90, 0, 0));
        breakLeft.transform.SetParent(gameObject.transform);
        breakRight = Instantiate(breakParticle, new Vector3(transform.position.x - 0.22f, transform.position.y, 0), Quaternion.Euler(transform.rotation.x - 90, 0, 0));
        breakRight.transform.SetParent(gameObject.transform);

        if (missleSpeed > 1000) missleSpeed = 1000;
        if(missleSpeed < 200) missleSpeed = 200;
     }

    void FixedUpdate()
    {
        engine.GetComponent<ParticleSystem>().Stop();
        breakLeft.GetComponent<ParticleSystem>().Stop();
        breakRight.GetComponent<ParticleSystem>().Stop();
        LimitSpeed();

#if UNITY_ANDROID
        Debug.Log("Unity Editor");

#elif UNITY_IOS
    Debug.Log("Unity iPhone");

#else
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ThrustUp();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ThrustDown();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
#endif
    }

    public void ThrustUp()
    {
        rb2d.AddForce(transform.up * thrust);
        engine.GetComponent<ParticleSystem>().Play();
    }
    public void ThrustDown()
    {
        rb2d.AddForce(-transform.up * thrust);
        breakLeft.GetComponent<ParticleSystem>().Play();
        breakRight.GetComponent<ParticleSystem>().Play();
    }
    public void RotateLeft()
    {
        gameObject.transform.Rotate(0, 0, rotationSpeed);
    }
    public void RotateRight()
    {
        gameObject.transform.Rotate(0, 0, -rotationSpeed);
    }
    public void Shoot()
    {
        Vector3 playerPos = transform.position;
        Vector3 playerDirection = transform.up;
        Quaternion playerRotation = transform.rotation;
        float spawnDistance = 0.45f;
        Vector3 laserSpawnPosition = playerPos + (playerDirection * spawnDistance);

        GameObject instance = Instantiate(misslePrefab, laserSpawnPosition, transform.rotation);
        instance.GetComponent<Rigidbody2D>().AddForce(instance.transform.up * missleSpeed);
        AudioSource.PlayClipAtPoint(shotsFired, Camera.main.transform.position);

        Destroy(instance, 1f);
    }

    void LimitSpeed()
    {
        if(rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Missle")
        {
            Destroy(collision.gameObject);
            GameManager.instance.SubstractLife();
            GameManager.instance.GotHit();
        }
    }
}
