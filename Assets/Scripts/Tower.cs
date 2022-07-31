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
    public Vector3 projectileOffset; // UNUSED; to be used to determine a projectile's starting point

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
    private float criticalDamage;

    [Header("Misc")]
    [SerializeField] private AttackEvent onAttack;

    public float Damage
	{
        get
		{
            return buffs.processDamage(damage);
		}
	}

    public float Range
    {
        get
        {
            return buffs.processRange(range);
        }
    }

    public float AtkSpeed
	{
        get
		{
            return buffs.processSpeed(atkSpeed);
		}
	}

    public float CriticalChance
	{
        get
		{
            return buffs.processCritChance(criticalChance);
		}
	}

    public float CriticalDamage
    {
        get
        {
            return buffs.processCritDamage(criticalDamage);
        }
    }

    public float ProjectileSpeed
	{
        get
		{
            return buffs.processProjSpeed(projectileSpeed);
		}
	}

    [SerializeField]
    // TODO: Automatically adjust size of target array if maxNumberTargets changes
    private int maxNumberTargets; // How many targets can this tower fire at per attack?
    private bool canAcquireSameTarget; // If tower can fire at multiple targets, is tower able to fire at the same target?
    public DamageInfo lastDamageInfo; // This is alwa

    public BuffComponentTower buffs;

    public TowerData Data { get; private set; }

	public void initialize(TowerData data_, TileScript tile_)
	{
        obj = this.gameObject;
        Data = data_;
        //print("Tower initialized!");

        damage = Data.damage;
        range = Data.range;
        atkSpeed = Data.atkSpeed;
        projectileSpeed = Data.projectileSpeed;
        projectileOffset = Data.projectileOffset;
        maxNumberTargets = Data.maxTargets;
        canAcquireSameTarget = Data.canHitSameTarget;
        targets = new Enemy[maxNumberTargets];
        criticalChance = Data.criticalChance;
        criticalDamage = Data.criticalDamage;
       

        tile = tile_;
        tile.tower = this;

        buffs = this.gameObject.GetComponent<BuffComponentTower>();

        this.gameObject.transform.position = tile.gameObject.transform.position;

        StartCoroutine(TowerLoop());
	}

    public bool withinRange(Transform point)
	{
        // Temporarily adding 0.1f range for some wiggle room.
        return getDistance(point.transform) < Range + 0.05f;
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
                    if(enemy != null && withinRange(enemy.transform))
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
                yield return new WaitForSeconds(AtkSpeed);
                continue;
			}

            // No valid targets within range, let's hibernate for 0.1s
            yield return new WaitForSeconds(0.1f);
        }
	}
}
