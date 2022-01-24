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
    

    public void Init(Tower owner_, Enemy target_, float speed_, float damage_)
    {
        owner = owner_;
        target = target_;
        speed = speed_;
        damage = damage_;
        obj = this.gameObject;


        StartCoroutine(projectileMovement());
    }

    public void Damage()
	{
        if(target != null)
		{
            target.takeDamage(damage);
        }

        Destroy(obj);
	}

    public IEnumerator projectileMovement()
    {
        // TODO: Projectiles don't disappear when enemy dies, instead they travel to the last position and are destroyed
        while (target != null && Vector3.Distance(obj.transform.position, target.transform.position) > BASERADIUS)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.gameObject.transform.position, speed * Time.deltaTime);

            Vector3 direction = target.gameObject.transform.position - obj.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            obj.transform.rotation = rotation;
            yield return null;
        }

        print("Projectile collision");
        Damage();
    }
}
