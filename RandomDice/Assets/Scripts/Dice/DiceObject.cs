using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceObject : MonoBehaviour
{

    [SerializeField] protected string bulletID;
    [SerializeField] protected float attackDelay = 1f;

    protected int rank;

    protected float attackCounter;

    protected virtual void Start()
    {
        rank = 1;
    }

    public virtual void Progress()
    {
        attackCounter += Time.deltaTime;
    }

    public abstract void Attack(EnemyBase target);

}
