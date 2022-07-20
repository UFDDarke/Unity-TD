using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO:
// Visual effect on enemy damage



public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public float health;
    public float healthMax;
    public GameObject obj; // The GameObject that Enemy.cs is attached to
    private static float BASESPEED = 1.0f;
    private int curNode = 0;
    private HealthBar healthBar;

    public void Init(EnemyData data_, float health_, Vector3 pos_)
    {
        EnemyManager.enemyCount++;
        EnemyManager.enemies.Add(this.gameObject);

        data = data_;

        healthMax = data.healthModifier * health_;
        health = healthMax;

        healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        healthBar.Initialize(this);


        obj = this.gameObject;
        obj.transform.position = pos_;
        obj.name = data.name;
        obj.GetComponentInChildren<Renderer>().material.color = data.enemyColor;

        StartCoroutine(movementTest());
    }

    public void takeDamage(float damage)
	{
        DamageText.CreateFloatingText(damage, gameObject.transform.position);
        health -= damage;
        healthBar.UpdateValues();
        if(health <= 0)
		{
            Cleanup();
		}
	}

    public void Cleanup()
	{
        Destroy(obj);
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

        //print("we out here");
        Cleanup();

        yield return new WaitForSeconds(3f);
    }

    public void OnDestroy()
    {
        EnemyManager.enemyCount--;
    }

}
