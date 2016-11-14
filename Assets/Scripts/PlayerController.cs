using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float health = 150f;
    public GameObject laserPrefab;
    public float padding = 1f;

     float speed = 10.0f;
     float xMin, xMax;

    // Use this for initialization
    void Start () {

        //find z distance
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", .000001f, 1f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 5f, 0);
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        laser missile = col.gameObject.GetComponent<laser>();
        if (missile)
        {
            health -= missile.getDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        Debug.Log(col);
    }
}
