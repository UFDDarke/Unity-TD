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

    public static List<Enemy> GetEnemiesInRange(Vector2 pos, float range)
	{
        List<Enemy> withinRange = new List<Enemy>();

        foreach(GameObject enemy in enemies)
		{
            if (!enemy) continue;


            if(Vector2.Distance(pos, enemy.transform.position) <= range)
			{
                withinRange.Add(enemy.GetComponent<Enemy>());
			}
		}

        if (withinRange.Count <= 0) return null;

        return withinRange;
	}
}
