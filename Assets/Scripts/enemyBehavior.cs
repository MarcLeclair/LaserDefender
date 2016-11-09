using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

    public float health = 150f;
    void OnTriggerEnter2D(Collider2D col)
    {
        laser missile = col.gameObject.GetComponent<laser>();
        if (missile)
        {
            health -= missile.getDamage();
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
        Debug.Log(col);
    }
}

