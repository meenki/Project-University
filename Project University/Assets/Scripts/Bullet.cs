using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Enemy target;
    public float speed = 1f;
    public Dice info;
    public Vector3 startPos = Vector3.zero;

    float elapsed = 0;

    public void Shot(Dice dice, Enemy target)
    {
        elapsed = 0;
        this.target = target;
        info = dice;
        transform.position = startPos = info.transform.position;
        gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(!target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        elapsed += Time.deltaTime * speed;
        
        transform.position = Vector3.Lerp
            (
            transform.position,
            target.transform.position,
            elapsed);

        if((transform.position - target.transform.position).magnitude < 0.01)
        {
            target.Hit(info);
            gameObject.SetActive(false);
        }
    }
}
