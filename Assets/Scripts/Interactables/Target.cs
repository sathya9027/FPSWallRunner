using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public float health = 100f;

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void PlaceDynamite()
    {
        throw new System.NotImplementedException();
    }
}
