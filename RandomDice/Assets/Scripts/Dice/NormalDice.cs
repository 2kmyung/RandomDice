using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NormalDice : DiceObject
{
    [SerializeField]

    protected override void Start()
    {
        base.Start();
    }

    public override void Attack(EnemyBase target)
    {
        if(attackCounter < attackDelay)
        {
            return;
        }

        attackCounter -= attackDelay;

        string id = bulletID;

        var bullet = BulletHandler.Instance.GetBulletByID(id);

        bullet.transform.position = transform.position;
        bullet.Move(target.transform);

    }


}
