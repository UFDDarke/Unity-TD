using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name;
    public float health;
    public float healthMax;

    public Enemy init(string name_, float health_, Vector3 pos_)
    {
        EnemyManager.enemyCount++;
        EnemyManager.enemies.Add(this.gameObject);

        name = name_;
        health = health_;
        healthMax = health_;
        this.gameObject.transform.position = pos_;

        return this;
    }

}
