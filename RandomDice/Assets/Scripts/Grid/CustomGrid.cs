using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid
{
    public static readonly int Width = 5;
    public static readonly int Height = 3;

    public static readonly float CellSize = 1.2f;

    private DiceObject[,] _gridArray;

    private int _numOfObjects;

    public CustomGrid()
    {
        _gridArray = new DiceObject[Height, Width];
    }

    public static Vector3 RowColumnToPoint(int row, int column, Vector3 originPosition)
    {
        float x = (column - Width / 2) * CellSize;
        float y = (row - Height / 2) * CellSize;

        Vector3 newVec = originPosition + new Vector3(x, -y);

        return newVec;
    }

    public bool IsValidRowColumn(int row, int column)
    {
        return (row >= Height) || (row < 0) || (column >= Width) || (column < 0);
    }

    public void SetObject(int row, int column, DiceObject obj)
    {
        if(IsValidRowColumn(row, column))
        {
            return;
        }


        if(IsObjectExists(row, column))
        {
            return;
        }

        _gridArray[row, column] = obj;
        ++_numOfObjects;
    }

    public bool IsObjectExists(int row, int column)
    {
        return _gridArray[row, column];
    }

    public bool IsGridFull()
    {
        int size = Width * Height;

        return _numOfObjects >= size;
    }


}
