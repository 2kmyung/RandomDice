using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float lifeTime = 10f;

    public float TimeAgo { get; private set; }

    private int hp = 5;

    public float LifeTime
    {
        get { return lifeTime; }
    }
   

    void Start()
    {
        TimeAgo = 0f;
    }



    public void Progress()
    {
        TimeAgo += Time.deltaTime;
    }


    public bool IsLifeTimeEnd()
    {
        return TimeAgo >= LifeTime;
    }

    private void ReceiveDamage(int damage)
    {
        hp -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletBase bullet = other.GetComponent<BulletBase>();

        if(bullet != null)
        {
            ReceiveDamage(bullet.Damage);
            bullet.RemoveSelf();

            if(hp <= 0)
            {
                gameObject.SetActive(false);
            }

        }
    }
}
