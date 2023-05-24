using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHandler : MonoBehaviour
{

    private DiceObject[] dicePrefabArray;

    private Vector3 originPosition;

    private CustomGrid grid;

    private void Awake()
    {
        dicePrefabArray = Resources.LoadAll<DiceObject>("Dice/");
        grid = new CustomGrid();

        originPosition = Vector3.zero;
    }



    private DiceObject GetRandomPrefab()
    {
        int index = Random.Range(0, dicePrefabArray.Length);

        return dicePrefabArray[index];
    }

    public DiceObject SpawnDice(int row, int column)
    {
        if(grid.IsValidRowColumn(row, column))
        {
            return null;
        }

        if (grid.IsObjectExists(row, column))
        {
            return null;
        }

        Vector3 spawnPosition = CustomGrid.RowColumnToPoint(row, column, originPosition);

        var dice = Instantiate(GetRandomPrefab(), spawnPosition, Quaternion.identity);

        grid.SetObject(row, column, dice);

        return dice;

    }

    public DiceObject SpawnDiceInRandomPosition()
    {

        if(grid.IsGridFull())
        {
            return null;
        }

        int row = -1;
        int column = -1;

        while(true)
        {
            row = Random.Range(0, CustomGrid.Height);
            column = Random.Range(0, CustomGrid.Width);

            if(!grid.IsObjectExists(row, column))
            {
                break;
            }
        }

        return SpawnDice(row, column);
    }


}
