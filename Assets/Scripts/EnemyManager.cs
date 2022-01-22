using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    // TODO: Unity script where we can press a button to create an enemy
    public static List<GameObject> enemies = new List<GameObject>();
    public static int enemyCount = 0;
    public static GameObject enemyPrefab;
    public static LevelManager level = (LevelManager)GameObject.FindObjectOfType(typeof(LevelManager));

    public static Enemy createEnemy(string name_, float health_, Vector3 pos_)
    {
        



        return null;
    }
    public static Enemy createEnemy(string name_, float health_)
    {
        // Calls the proper createEnemy, but with pos_ equal to start point
        return createEnemy(name_, health_, level.startTile.transform.position);
    }

}
