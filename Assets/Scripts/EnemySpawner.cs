using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;

    float width = 10f;
    float height = 5f;

    float xMin, xMax;
    bool movingRight = true;
    public  float speed = 30f;
	// Use this for initialization
	void Start () {

        foreach(Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x ;
        xMax = rightmost.x;

    }


    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}

    void Update()
    {
        if (movingRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        }
        else
            transform.position += new Vector3(-speed * Time.deltaTime, 0);

        float rightEdgeOfFormation = transform.position.x + (.5f * width);
        float leftEdgeOfFormation = transform.position.x - (.5f * width);

        if(leftEdgeOfFormation < xMin)  
        {
            movingRight = true;
        }else if(rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }
    }
}
