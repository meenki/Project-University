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
    public float speed;
    public EnemyType type;

    public float distance;

    int targetNum = 0;

    GameObject attackEffect;
    public void Awake()
    {
        attackEffect = Instantiate(GameManager.Instance.killPrefab, transform);
        attackEffect.SetActive(false);  
    }
    public void ActiveEnemy(EnemyType type, float hp, float speed)
    {
        distance = 0;
        transform.position = enemyWay[0].position;
        this.type = type;

        this.hp = hp;
        this.speed = speed;

        switch (type)
        {
            case EnemyType.Normal:
                sprite.sprite = normalRat;
                break;
            case EnemyType.Speed:
                sprite.sprite = speedRat;
                this.hp = hp / 2;
                this.speed = speed * 2;
                break;
            case EnemyType.Boss:
                sprite.sprite = bossRat;
                break;
        }

        targetNum = 1;
        gameObject.SetActive(true);
    }

    public void Hit(Dice info)
    {
        var damage = info.damage * GameManager.Instance.levelDamage[GameManager.Instance.diceLevel[(int)info.type]] * GameManager.Instance.upgradeDamage[info.level];
        attackEffect.SetActive(false);

        switch (info.type)
        {
            case DiceType.Fire:
                attackEffect.SetActive(true);
                break;
            case DiceType.Wind:
                attackEffect.SetActive(true);

                break;
            case DiceType.Volt:
                attackEffect.SetActive(true);

                break;
            case DiceType.Ice:
                speed *= 0.5f;
                if (speed < 0.1)
                    speed = 0.1f;
                attackEffect.SetActive(true);

                break;
            case DiceType.Boss:
                if(this.type == EnemyType.Boss)
                {
                    damage *= 2;
                }
                attackEffect.SetActive(true);

                break;
        }

        hp -= damage;

        if (hp < 0)
        {
            GameManager.Instance.KillEnemy();
            gameObject.SetActive(false);
        }
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
                this.gameObject.SetActive(false);
                GameManager.Instance.DecreaseLife();
            }
        }
        distance += speed * Time.deltaTime;
    }
}
