using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    [SerializeField] Vector3[] enemyPathArray = null;
    private float[] progressArray = null;

    private void Awake()
    {
        int enemyPathLength = enemyPathArray.Length;
        progressArray = new float[enemyPathLength - 1];

        print(progressArray);

        float sum = 0.0f;

        for (int index = 0; index < enemyPathLength - 1; ++index)
        {
            Vector3[] arr = enemyPathArray;
            Vector3 diff = arr[index] - arr[index + 1];
            float length = diff.magnitude;
            sum += length;
        }

        for (int index = 0; index < enemyPathLength - 1; ++index)
        {
            Vector3[] arr = enemyPathArray;
            Vector3 diff = arr[index] - arr[index + 1];
            float length = diff.magnitude;

            progressArray[index] = length / sum;

            if (index > 0)
            {
                progressArray[index] += progressArray[index - 1];
            }
        }
    }

    private int GetPointIndex(float timeAgo, float lifeTime)
    {
        float progress = timeAgo / lifeTime;

        int index = 0;

        for (int i = 0; i < progressArray.Length -1; i++)
        {
            if (progress >= progressArray[i])
            {
                index = i + 1;
            }
            else { break; }
        }
        return index;
    }

    public Vector3 GetEnemyPoint(float t, float lifeTime)
    {
        int index = GetPointIndex(t, lifeTime);
        float targetTime = lifeTime * progressArray[index];

        if (index > 0)
        {
            float timeAgo = lifeTime * progressArray[index - 1];
            t -= timeAgo;
            targetTime -= timeAgo;

        }

        Vector3 start = enemyPathArray[index];
        Vector3 end = enemyPathArray[index + 1];

        return Vector3.Lerp(start, end, t / targetTime);  
    }

    private void OnDrawGizmos()
    {
        int enemyPathLength = enemyPathArray.Length;

        for(int i = 0; i < enemyPathLength - 1; i++)
        {
            Vector3[] arr = enemyPathArray;

            Color preColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(arr[i], arr[i + 1]);
            Gizmos.color = preColor;
        }
    }
}