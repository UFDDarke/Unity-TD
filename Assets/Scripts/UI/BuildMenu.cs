using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
	public GameObject buttonPrefab;
	public GameObject buttonGrid;

	public void Start()
	{
		foreach (var t in TowerManager.types)
		{
			BuildButton newButton = Instantiate(buttonPrefab).GetComponent<BuildButton>();
			newButton.initialize((TowerData) t);
			newButton.transform.SetParent(buttonGrid.transform);
			newButton.transform.localScale = new Vector3(1, 1, 1);
		}
		
	}
}
