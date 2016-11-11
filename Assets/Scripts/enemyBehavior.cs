using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

    public GameObject laserPrefab;
    public ParticleSystem thrusters;
    public float health = 150f;
    public float shotsPerSec = .5f;

    void Awake()
    {
        thrusters.Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        laser missile = col.gameObject.GetComponent<laser>();
        if (missile)
        {
            health -= missile.getDamage();
            if(health <= 0)
            {
                Destroy(gameObject);
                Destroy(col.gameObject);
            }
        }
        Debug.Log(col);
    }

    void Update()
    {
       
        float probability = Time.deltaTime * shotsPerSec;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 startPos = transform.position + new Vector3(0, -1f, 0);
        GameObject laser = Instantiate(laserPrefab, startPos, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5f, 0);
    }

    
}

