using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletBase : MonoBehaviour
{
    [field: SerializeField]

    public string ID { get; private set; }

    [field: SerializeField]

    public int Damage { get; private set; }

    [SerializeField] 
    
    protected float speed;

    public void Move(Transform target)
    {
        StartCoroutine(TrackPosition(target));
    }

    private IEnumerator TrackPosition(Transform target)
    {
        while(gameObject)
        {
            if(!target || !target.gameObject.activeSelf)
            {
                break;
            }

            Vector3 dir = target.position - transform.position;
            dir = dir.normalized;

            transform.position += dir * speed * Time.deltaTime;

            yield return null;
        }

        RemoveSelf();
    }

    public void RemoveSelf()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

}
