using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

    public GameObject laserPrefab;
    public ParticleSystem thrusters;
    public float health = 150f;
    public float shotsPerSec = .5f;
    public int valueOfenemy;
    public AudioClip explosion, firing;

    ScoreKeeper score;
   

    void Awake()
    {
        thrusters.Play();
    }
    
    void Start()
    {
        score = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
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
                score.Score(valueOfenemy);
                AudioSource.PlayClipAtPoint(explosion, transform.position);
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
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5f, 0);
        AudioSource.PlayClipAtPoint(firing, transform.position);
    }

    
}

