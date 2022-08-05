using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileManager
{
	public static List<GameObject> projectiles;

	public static void CreateProjectile(Tower tower, Enemy target)
	{
		GameObject newObj = UnityEngine.Object.Instantiate(tower.prefab);
		Projectile newProjectile = newObj.GetComponent<Projectile>();
		newObj.transform.parent = tower.transform;
		newObj.transform.position = tower.obj.transform.position;
		newProjectile.Init(tower, target, tower.ProjectileSpeed.getFinalValue(), tower.Damage.getFinalValue());
	}


}
