using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //public Enemy[] target = new Enemy[30]; // Tower's current target.
    //public List<Enemy> targets = new List<Enemy>();
    public Enemy[] targets;

    public GameObject obj; // GameObject that Tower.cs is attached to
    public GameObject prefab; // Which projectile prefab the tower will use
    protected TileScript tile; // The tile that this tower currently resides on

    /*
    [SerializeField]
    private float damage;
    [SerializeField]
    private float range;
    [SerializeField]
    private float atkSpeed; // How much time, in seconds, this tower has to wait to make another attack.
    [SerializeField]
    private float projectileSpeed; // How fast the tower's projectiles are
    [SerializeField]
    private float criticalChance;
    [SerializeField]
    private float criticalDamage;*/

    [SerializeField]
    private Attribute damage;
    [SerializeField]
    private Attribute range;
    [SerializeField]
    private Attribute atkSpeed;
    [SerializeField]
    private Attribute projectileSpeed;
    [SerializeField]
    private Attribute criticalChance;
    [SerializeField]
    private Attribute criticalDamage;



    [Header("Misc")]
    [SerializeField] private AttackEvent onAttack;

    public Attribute Damage
	{
        get
		{
            return damage;
		}
	}

    public Attribute Range
    {
        get
        {
            return range;
        }
    }

    public Attribute AtkSpeed
	{
        get
		{
            return atkSpeed;
        }
	}

    public Attribute CriticalChance
	{
        get
		{
            return criticalChance;
        }
	}

    public Attribute CriticalDamage
    {
        get
        {
            return criticalDamage;
        }
    }

    public Attribute ProjectileSpeed
	{
        get
		{
            return projectileSpeed;
        }
	}

    // TODO: Automatically adjust size of target array if maxNumberTargets changes, maybe use properties to make a new array
    private int maxNumberTargets; // How many targets can this tower fire at per attack?
    private bool canAcquireSameTarget; // If tower can fire at multiple targets, is tower able to fire at the same target?
    public DamageInfo lastDamageInfo; // This is used during damage events so scripts can get the parameters of the last damage event

    public BuffComponentTower buffs;

    public TowerData Data { get; private set; }

	public void initialize(TowerData data_, TileScript tile_)
	{
        obj = this.gameObject;
        Data = data_;
        //print("Tower initialized!");

        /*
        damage = Data.damage;
        range = Data.range;
        atkSpeed = Data.atkSpeed;
        projectileSpeed = Data.projectileSpeed;
        criticalChance = Data.criticalChance;
        criticalDamage = Data.criticalDamage;
        */

        damage = new Attribute(Data.damage);
        range = new Attribute(Data.range);
        atkSpeed = new AdditiveAttribute(Data.atkSpeed);
        projectileSpeed = new Attribute(Data.projectileSpeed);
        criticalChance = new Attribute(Data.criticalChance);
        criticalDamage = new Attribute(Data.criticalDamage);


        maxNumberTargets = Data.maxTargets;
        canAcquireSameTarget = Data.canHitSameTarget;
        targets = new Enemy[maxNumberTargets];

       

        tile = tile_;
        tile.tower = this;

        buffs = this.gameObject.GetComponent<BuffComponentTower>();

        this.gameObject.transform.position = tile.gameObject.transform.position;

        StartCoroutine(TowerLoop());
	}

    public bool withinRange(Transform point)
	{
        // Temporarily adding 0.1f range for some wiggle room.
        return getDistance(point.transform) < Range.getFinalValue() + 0.05f;
	}

    public float getDistance(Transform point)
	{
        return Vector2.Distance(this.gameObject.transform.position, point.gameObject.transform.position);
	}

    public void fire(Enemy target_) // Can override to fire at a specific target.
	{
        // TODO: Instantiate new projectile, set its target.
        //print("Tower fired!");
        ProjectileManager.CreateProjectile(this, target_);
        onAttack.Raise(this);
    }

    public void fire() // With no parameters, this fires at this tower's current target.
	{
        fire(targets[0]);
	}

    public void sellTower()
	{
        TowerManager.towers.Remove(this.gameObject);
        tile.tower = null;
        Destroy(this.gameObject);
	}

    private void CheckTargets()
	{
        // Scans all of the tower's current targets to find any that are invalid

        for(int i = 0; i < maxNumberTargets; i++)
		{
            if (targets[i] == null || !withinRange(targets[i].transform)) {
                //Debug.Log("Acquiring new target for index " + i);
                AcquireNewTarget(i);
			}
		}
	}

    public void AttackEvent(Tower tower)
	{
        if (tower != this) return;

        foreach (ScriptableAction action in Data.onAttackActions)
        {
            action.PerformAction(this.gameObject);
        }
    }

    public void DamageEvent(DamageInfo info)
	{
        if (info.attacker != this) return;

        lastDamageInfo = info;

        foreach(ScriptableAction action in Data.onDamageActions)
		{
            action.PerformAction(this.gameObject);
		}




        lastDamageInfo = null;
	}

    private void AcquireNewTarget(int index)
	{
        // Since we know our current target is no longer valid, let's remove it for now
        targets[index] = null;

        // Looking for a new target. Let's search all enemies until we find one in range.

        foreach (GameObject enemy in EnemyManager.enemies)
		{
            switch(canAcquireSameTarget)
			{
                case true:
                    // TODO: Change targetting logic, so that the tower only targets the same enemy as a last resort
                    if (enemy != null && withinRange(enemy.transform))
					{
                        // Target acquired!
                        targets[index] = enemy.GetComponent<Enemy>();
                        return;
					}
                    break;
                case false:
                    if(enemy != null && withinRange(enemy.transform) && !targets.Contains(enemy.GetComponent<Enemy>()))
					{
                        // Target acquired!
                        targets[index] = enemy.GetComponent<Enemy>();
                        return;
                    }
                    break;
			}
		}

        return;
	}

    public IEnumerator TowerLoop()
	{
		while (true)
		{
            CheckTargets();

            // Attempt attack on all targets in list
            bool attackSuccess = false;

            foreach(Enemy target in targets)
			{
                if(target != null)
				{
                    fire(target);
                    attackSuccess = true;
                }

			}

            if(attackSuccess)
			{
                yield return new WaitForSeconds(AtkSpeed.getFinalValue());
                continue;
			}

            // No valid targets within range, let's hibernate for 0.1s
            yield return new WaitForSeconds(0.1f);
        }
	}
}
