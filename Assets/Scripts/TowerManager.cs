using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TowerManager
{
	public static List<GameObject> towers = new List<GameObject>();

	public static Tower CreateTower(Vector3 position, GameObject prefab)
	{
		GameObject createdObj = UnityEngine.Object.Instantiate(prefab);
		Tower createdTower = createdObj.GetComponent<Tower>();

		createdObj.transform.position = position;

		towers.Add(createdObj);

		return createdTower;

	}


}
