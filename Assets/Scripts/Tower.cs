using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float damage;
    public float range;
    public float atkSpeed; // How much time, in seconds, this tower has to wait to make another attack.
    public float projectileSpeed; // How fast the tower's projectiles are
    public Enemy target; // Tower's current target.
    public GameObject obj; // GameObject that Tower.cs is attached to

    public GameObject prefab; // Which projectile prefab the tower will use
    public Vector3 projectileOffset; // UNUSED; to be used to determine a projectile's starting point

	// TODO: Use Coroutine for the tower loop

	public void Awake()
	{
        obj = this.gameObject;
        print("Tower initialized!");


        StartCoroutine(TowerLoop());
	}

    public bool withinRange(Transform point)
	{
        return (Vector3.Distance(point.position, obj.transform.position) < range);
	}

    public void fire(Enemy target_) // Can override to fire at a specific target.
	{
        // TODO: Instantiate new projectile, set its target.
        //print("Tower fired!");
        ProjectileManager.CreateProjectile(this, target);
	}

    public void fire() // With no parameters, this fires at this tower's current target.
	{
        fire(target);
	}

    public IEnumerator TowerLoop()
	{
		while (true)
		{
            // Check if we need a new target (we have no target, or current target not in range)
            if (target == null || !withinRange(target.transform))
            {
                //print("Looking for new target..");
                // Tower has no target, let's search for a potential target in range
                foreach (GameObject enemy in EnemyManager.enemies)
                {
                    if (enemy != null && withinRange(enemy.transform))
                    {
                        //print("Found one!");
                        // Found a valid target
                        target = enemy.GetComponent<Enemy>();
                        break;
                    }
                }
            }



            // Check if we have a target
            if (target != null)
            {
                //print("Firing at current target!");
                fire();
                yield return new WaitForSeconds(atkSpeed);
                continue;
            }

            // No valid targets within range, let's hibernate for 0.1s
            //print("No targets. Hibernating..");
            yield return new WaitForSeconds(0.1f);
        }
	}

}
