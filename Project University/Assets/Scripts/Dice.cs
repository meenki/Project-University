using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dice : MonoBehaviour
{
    public DiceType type;
    public int level;
    public float damage;
    public float attackSpeed;

    public int index;

    float elapsed = 0;

    public void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > attackSpeed)
        {
            Attack();
            elapsed = 0;
        }
    }

    public void Attack()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
