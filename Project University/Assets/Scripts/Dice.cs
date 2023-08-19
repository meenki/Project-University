using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dice : MonoBehaviour
{
    public SpriteRenderer sprite;

    public DiceType type;
    public int level;
    public float damage;
    public float attackSpeed;

    public int index;

    float elapsed = 0;
    Enemy target = null;

    public void InitDice(DiceInfo info)
    {
        switch (info.Type)
        {
            case DiceType.Fire:
                sprite.sprite = GameManager.Instance.fireDice[0];
                break;
            case DiceType.Wind:
                sprite.sprite = GameManager.Instance.windDice[0];
                break;
            case DiceType.Volt:
                sprite.sprite = GameManager.Instance.voltDice[0];
                break;
            case DiceType.Ice:
                sprite.sprite = GameManager.Instance.iceDice[0];
                break;
            case DiceType.Boss:
                sprite.sprite = GameManager.Instance.bossDice[0];
                break;
        }

        this.level = 1;
        this.type = info.Type;
        this.damage = info.damage;
        this.attackSpeed = info.attackSpeed;
        elapsed = 0;
        target = null;

        gameObject.SetActive(true);
    }

    public void LevelUpDice()
    {

    }

    public void Update()
    {
        elapsed += Time.deltaTime * attackSpeed;
        if(elapsed > 1)
        {
            if(target == null || !target.gameObject.activeSelf)
            {
                var enemy = GameManager.Instance.GetTarget();
                if (enemy == null)
                    return;
                else
                {
                    target = enemy;
                }
            }

            Attack();
            elapsed = 0;
        }
    }

    public void Attack()
    {
        GameManager.Instance.Attack(this, target);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
