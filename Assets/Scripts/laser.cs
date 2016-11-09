using UnityEngine;
using System.Collections;

public class laser : MonoBehaviour {

    float damage = 100f;

    public float getDamage()
    {
        return damage;
    }

    public void hit()
    {
        Destroy(gameObject);
    }
 
}
