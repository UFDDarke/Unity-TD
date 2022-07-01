using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TowerManager
{
	public static List<GameObject> towers = new List<GameObject>();
	public static Object[] types;

	public static Tower CreateTower(Vector3 position, GameObject prefab, TowerData data)
	{
		GameObject createdObj = UnityEngine.Object.Instantiate(prefab);
		Tower createdTower = createdObj.GetComponent<Tower>();

		createdObj.transform.position = position;

		towers.Add(createdObj);

		createdTower.initialize(data);

		return createdTower;

	}

	// Alternative to Start() and Awake() for static classes
	static TowerManager()
	{
		// Load in all tower data from resource folder
		types = Resources.LoadAll("Tower", typeof(TowerData));
		Debug.Log(types.Length);
	}


}
