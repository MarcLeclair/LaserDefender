using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float spawnDelay = .5f;


    float width = 10f;
    float height = 5f;

    float xMin, xMax;
    bool movingRight = true;
    public  float speed = 30f;


	// Use this for initialization
	void Start () {

        SpawnEnemies();
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x;
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

        if (AllMembersDead()) SpawnUntilFull();
    }

    bool AllMembersDead()
    {
        foreach(Transform childPosition in transform)
        {
            if (childPosition.childCount > 0) return false;  
        }
        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount <= 0) return childPosition;
        }
        return null;
    }

    

    void SpawnUntilFull()
    {
        Transform position = NextFreePosition();
        if (position)
        {
            GameObject enemy = Instantiate(enemyPrefab, position.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = position;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform position in transform)
        {
           GameObject enemy = Instantiate(enemyPrefab, position.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = position;
         }
    }

  
}
