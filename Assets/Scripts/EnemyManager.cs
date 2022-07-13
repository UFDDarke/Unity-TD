using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    // TODO: Unity script where we can press a button to create an enemy
    public static List<GameObject> enemies = new List<GameObject>();
    public static int enemyCount = 0;
    public static GameObject enemyPrefab;

    public static Enemy createEnemy(EnemyData data, float baseHealth, Vector3 pos_)
    {
        // TODO: Make sure that this newly created enemy adheres to the path
        GameObject created = Object.Instantiate(enemyPrefab);
        Enemy createdEnemy = created.GetComponent<Enemy>();


        createdEnemy.Init(data, baseHealth, pos_);
        return createdEnemy;
    }
    public static Enemy createEnemy(EnemyData data, float baseHealth)
    {
        // Calls the proper createEnemy, but with pos_ equal to start point
        return createEnemy(data, baseHealth, LevelManager.startTile.transform.position);
    }

    public static void Reset()
    {
        foreach(GameObject enemy in enemies)
        {
            UnityEngine.Object.Destroy(enemy);
        }
        enemies.Clear();
    }

}
