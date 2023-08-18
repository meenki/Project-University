using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] enemyWay;
    public SpriteRenderer sprite;

    public Sprite normalRat;
    public Sprite speedRat;
    public Sprite bossRat;

    public float hp;
    public float speed = 2;
    public EnemyType type;

    int targetNum = 0;
    Transform direction;

    public void ActiveEnemy(EnemyType type, float hp, float speed)
    {
        transform.position = enemyWay[0].position;
        this.type = type;
        this.hp = hp;
        this.speed = speed;

        targetNum = 1;
    }

    public void Update()
    {
        if (targetNum == 1)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if(transform.position.x > enemyWay[targetNum].position.x)
            {
                transform.position = enemyWay[targetNum].position;
                targetNum = 2;
            }
        }
        else if (targetNum == 2)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if (transform.position.y < enemyWay[targetNum].position.y)
            {
                transform.position = enemyWay[targetNum].position;
                targetNum = 3;
            }
        }
        else if(targetNum == 3)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x < enemyWay[targetNum].position.x)
            {
                transform.position = enemyWay[targetNum].position;
            }
        }
    }
}
