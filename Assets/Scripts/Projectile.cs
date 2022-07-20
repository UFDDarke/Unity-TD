using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower owner;
    public Enemy target;
    float speed; // how fast the projectile travels
    float damage;
    static float BASERADIUS = 0.1f; // how far from the target to count as a hit
    GameObject obj; // the object that Projectile.cs is attached to
    Vector3 attackLocation; // for non-homing attacks and unparented attacks (last position of target)

    // TODO: Projectiles have a lifetime. If orphaned, they will continue flying in a direction until their lifetime is over.
    // TODO: Enemy exclusion list. For use with multi-hit attacks that should not be able to hit the same target.



    public void Init(Tower owner_, Enemy target_, float speed_, float damage_)
    {
        owner = owner_;
        target = target_;
        speed = speed_;
        damage = damage_;
        obj = this.gameObject;

        attackLocation = target.transform.position;
        StartCoroutine(projectileMovement());
    }

    public void Damage()
	{
        if(target != null)
		{
            target.takeDamage(damage, owner);
        }

        Destroy(obj);
	}

    public IEnumerator projectileMovement()
    {
        // Projectiles currently are not destroyed when their target dies, and instead travel to their target's last location.
        // Considering adding collision to these orphaned projectiles so that they can damage other enemies (prolly not tho)
        while ((target != null || attackLocation != null) && Vector3.Distance(obj.transform.position, attackLocation) > BASERADIUS)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, attackLocation, speed * Time.deltaTime);

            Vector3 direction = attackLocation - obj.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            obj.transform.rotation = rotation;
            
            if(target != null)
			{
                attackLocation = target.gameObject.transform.position;
			}

            yield return null;
        }

        //print("Projectile collision");
        Damage();
    }
}
