using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TowerManager
{
	public static List<Tower> towers = new List<Tower>();
	public static Object[] types;

	public static Tower CreateTower(TileScript tile, GameObject prefab, TowerData data)
	{
		GameObject createdObj = UnityEngine.Object.Instantiate(prefab);
		Tower createdTower = createdObj.GetComponent<Tower>();

		//createdObj.transform.position = position;

		towers.Add(createdTower);

		createdTower.initialize(data, tile);

		return createdTower;

	}

	public static List<Tower> GetTowersInRange(Vector2 pos, float radius)
	{
		List<Tower> towersInRange = new List<Tower>();

		foreach(Tower tower in towers)
		{
			if (tower.getDistance(pos) <= radius) towersInRange.Add(tower);
		}

		return towersInRange;
	}






	// Alternative to Start() and Awake() for static classes
	static TowerManager()
	{
		// Load in all tower data from resource folder
		types = Resources.LoadAll("Tower", typeof(TowerData));
	}


}
