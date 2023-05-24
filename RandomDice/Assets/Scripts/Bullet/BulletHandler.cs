using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : Singleton<BulletHandler>
{
    private Dictionary<string, BulletBase> bulletPrefabDictionary;


    protected override void Awake()
    {
        base.Awake();
        var bulletPrefabArray = Resources.LoadAll<BulletBase>("Bullet/");
        bulletPrefabDictionary = new Dictionary<string, BulletBase>();

        foreach(var bullet in bulletPrefabArray)
        {
            bulletPrefabDictionary.Add(bullet.ID, bullet);
        }

    }

    public BulletBase GetBulletByID(string id)
    {
        BulletBase bulletPrefab = null;

        if(bulletPrefabDictionary.TryGetValue(id, out bulletPrefab))
        {
            return Instantiate(bulletPrefab);
        }

        return null;
    }
    

}
