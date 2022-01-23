using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float health;
    public float healthMax;
    public GameObject obj; // The GameObject that Enemy.cs is attached to
    private static float BASESPEED = 1.0f;
    private int curNode = 0;

    public void Init(string name_, float health_, Vector3 pos_)
    {
        EnemyManager.enemyCount++;
        EnemyManager.enemies.Add(this.gameObject);

        enemyName = name_;
        health = health_;
        healthMax = health_;
        obj = this.gameObject;
        obj.transform.position = pos_;
        StartCoroutine(movementTest());
    }

    public IEnumerator movementTest()
    {
        Transform target = LevelManager.path[0].transform;

        while(Vector3.Distance(obj.transform.position, LevelManager.endTile.transform.position) > 0.05f)
        {
            if(Vector3.Distance(obj.transform.position, target.position) <= 0.05f)
            {
                curNode++;
                target = LevelManager.path[curNode].transform;
            }


            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.position, BASESPEED * Time.deltaTime);

            // LevelManager.path.Count < curNode

            yield return null;
        }

        print("we out here");

        yield return new WaitForSeconds(3f);
    }

    public void OnDestroy()
    {
        EnemyManager.enemyCount--;
    }

}
