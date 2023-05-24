using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]

    private DiceHandler diceHandler;

    [SerializeField]

    private PathHandler pathHandler;

    GameObject enemyPrefab;

    private List<EnemyBase> enemyList = new List<EnemyBase>();

    private List<DiceObject> diceList = new List<DiceObject>();

    int life;

    void Start()
    {


        enemyPrefab = Resources.Load<GameObject>("Enemy/Enemy");

        StartCoroutine(SpawnEnemy());

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var dice = diceHandler.SpawnDiceInRandomPosition();
            diceList.Add(dice);
        }

        EnemyUpdate();
        DiceUpdate();
    }

    private void DiceUpdate()
    {
        for (int i = 0; i < diceList.Count; i++)
        {
            DiceObject dice = diceList[i];

            dice.Progress();

            if (enemyList.Count > 0)
            {

                int targetIndex = Random.Range(0, enemyList.Count);
                EnemyBase enemy = enemyList[targetIndex];


                if (enemy)
                {
                    dice.Attack(enemy);
                }

            }



        }
    }

    private void EnemyUpdate()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            EnemyBase enemy = enemyList[i];

            if(!enemy.gameObject.activeSelf)
            {
                Destroy(enemy.gameObject);
                enemyList.RemoveAt(i--);
                continue;

            }

            if (enemy.IsLifeTimeEnd())
            {
                --life;
                Destroy(enemy.gameObject);
                enemyList.RemoveAt(i--);
                continue;
            }

            Vector3 enemyPoint = pathHandler.GetEnemyPoint(enemy.TimeAgo, enemy.LifeTime);
            enemy.transform.position = enemyPoint;
            enemy.Progress();

        }
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds enemySpawnDelay = new WaitForSeconds(3);

        while (true)
        {
            Vector3 pos = pathHandler.GetEnemyPoint(0f, 1f);

            var enemy = Instantiate(enemyPrefab).GetComponent<EnemyBase>();

            enemyList.Add(enemy);

            yield return enemySpawnDelay;
        }

    }
}
