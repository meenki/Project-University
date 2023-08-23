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

    public bool isSelected = false;

    List<Dice> collDiceList = new List<Dice>();

    public void InitDice(DiceInfo info)
    {
        this.level = 1;
        this.type = info.Type;
        this.damage = info.damage;
        this.attackSpeed = info.attackSpeed;
        elapsed = 0;
        target = null;

        switch (type)
        {
            case DiceType.Fire:
                sprite.sprite = GameManager.Instance.fireDice[level - 1];
                break;
            case DiceType.Wind:
                sprite.sprite = GameManager.Instance.windDice[level - 1];
                break;
            case DiceType.Volt:
                sprite.sprite = GameManager.Instance.voltDice[level - 1];
                break;
            case DiceType.Ice:
                sprite.sprite = GameManager.Instance.iceDice[level - 1];
                break;
            case DiceType.Boss:
                sprite.sprite = GameManager.Instance.bossDice[level - 1];
                break;
        }

        gameObject.SetActive(true);
    }

    public void LevelUpDice()
    {
        level++;
        switch (type)
        {
            case DiceType.Fire:
                sprite.sprite = GameManager.Instance.fireDice[level - 1];
                break;
            case DiceType.Wind:
                sprite.sprite = GameManager.Instance.windDice[level - 1];
                break;
            case DiceType.Volt:
                sprite.sprite = GameManager.Instance.voltDice[level - 1];
                break;
            case DiceType.Ice:
                sprite.sprite = GameManager.Instance.iceDice[level - 1];
                break;
            case DiceType.Boss:
                sprite.sprite = GameManager.Instance.bossDice[level - 1];
                break;
        }
    }

    public void DropDice()
    {

        if (collDiceList.Count > 0)
        {
            float distacne = float.MaxValue;

            Dice target = null;
            for(int i = 0; i < collDiceList.Count; i++)
            {
                if (distacne > Vector3.Distance(collDiceList[i].transform.position, transform.position))
                {
                    target = collDiceList[i];
                }
            }

            if (target.type == this.type && target.level < 5 && target.level == this.level)
            {
                target.LevelUpDice();
                this.gameObject.SetActive(false);
            }

            collDiceList.Clear();
        }
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
        if (!isSelected) return;
        var obj = collision.gameObject.GetComponent<Dice>();
        if (obj != null)
        {
            collDiceList.Add(obj);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!isSelected) return;
        var obj = collision.gameObject.GetComponent<Dice>();
        if(obj != null)
        {
            collDiceList.Remove(obj);

        }
    }
}
