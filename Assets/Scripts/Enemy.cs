using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


// TODO:
// Visual effect on enemy damage



public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public float health;
    public float healthMax;
    public GameObject obj; // The GameObject that Enemy.cs is attached to
    private int curNode = 0;
    private HealthBar healthBar;
    public Attribute damageTakenModifier;
    public Attribute moveSpeed;
    [SerializeField] private DamageEvent onDamaged;

    public BuffComponentEnemy buffs;

    public void Init(EnemyData data_, float health_, Vector3 pos_)
    {
        EnemyManager.enemyCount++;
        EnemyManager.enemies.Add(this.gameObject);

        data = data_;

        healthMax = data.healthModifier * health_;
        health = healthMax;

        healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        healthBar.Initialize(this);

        damageTakenModifier = new Attribute(0f);
        moveSpeed = new Attribute(data.speed);


        obj = this.gameObject;
        obj.transform.position = pos_;
        obj.name = data.name;
        obj.GetComponentInChildren<Renderer>().material.color = data.enemyColor;

        buffs = this.gameObject.GetComponent<BuffComponentEnemy>();

        StartCoroutine(movementTest());
    }

    public void takeDamage(float damage, Tower owner)
	{
        DamageInfo damageInfo = new DamageInfo();
        damageInfo.attacker = owner;
        damageInfo.victim = this;
        damageInfo.baseDamage = damage;

        float modifiedDamage = damage;
        StringBuilder builder = new StringBuilder();
        int critCount = 0;


        modifiedDamage = damageTakenModifier.processValue(modifiedDamage);


        // Check for a critical strike TODO: Multicrit support
        if (Random.Range(0f, 1f) < owner.CriticalChance.getFinalValue())
		{
            // Critical strike!
            modifiedDamage *= (1 + (owner.CriticalDamage.getFinalValue()));
            critCount++;
            damageInfo.wasCritical = true;
		}


        builder.Append(modifiedDamage.ToString());
        for(int i = 0; i < critCount; i++)
		{
            // Appending an exclamation mark for crits
            builder.Append("!");
		}

        // TODO: Instead of creating damage text directly, have the DamageText listen for a damage event
        DamageText.CreateFloatingText(builder.ToString(), gameObject.transform.position);
        health -= modifiedDamage;
        healthBar.UpdateValues();

        damageInfo.wasLethal = (health < 0);
        onDamaged.Raise(damageInfo);

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

            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.position, Mathf.Max(moveSpeed.getFinalValue(), 0.35f) * Time.deltaTime);

            yield return null;
        }

        // Reached exit goal
        Cleanup();

        yield return new WaitForSeconds(3f);
    }

    public void OnDestroy()
    {
        EnemyManager.enemyCount--;
    }

}

public class DamageInfo {
    public Tower attacker;
    public Enemy victim;

    public float baseDamage;
    public float finalDamage;
    public bool wasLethal = false;
    public bool wasCritical = false;
}