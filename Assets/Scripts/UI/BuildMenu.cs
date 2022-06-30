using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
	public GameObject buttonPrefab;
	public GameObject buttonGrid;
	public GameObject buildPanel;
	[SerializeField] public Tooltip tooltip;

	public TileScript clickedTIle;


	public void Start()
	{
		foreach (var t in TowerManager.types)
		{
			BuildButton newButton = Instantiate(buttonPrefab).GetComponent<BuildButton>();
			newButton.initialize((TowerData) t, tooltip);
			newButton.transform.SetParent(buttonGrid.transform);
			newButton.transform.localScale = new Vector3(1, 1, 1);
		}
		
	}

	public void MoveToCursor()
	{
		Vector3 offset = new Vector3(0, 0, 0);
		RectTransform popupObject = buildPanel.GetComponent<RectTransform>();
		Canvas masterCanvas = tooltip.masterCanvas;

		Vector3 newPos = Input.mousePosition + offset;
		newPos.z = 0f;
		float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * masterCanvas.scaleFactor / 2);
		if (rightEdgeToScreenEdgeDistance < 0)
		{
			newPos.x += rightEdgeToScreenEdgeDistance;
		}
		float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * masterCanvas.scaleFactor / 2);
		if (leftEdgeToScreenEdgeDistance > 0)
		{
			newPos.x += leftEdgeToScreenEdgeDistance;
		}
		float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * masterCanvas.scaleFactor);
		if (topEdgeToScreenEdgeDistance < 0)
		{
			newPos.y += topEdgeToScreenEdgeDistance;
		}
		popupObject.transform.position = newPos;
	}
	public void ShowMenu()
	{
		this.gameObject.SetActive(true);
		MoveToCursor();
	}

	public void HideMenu()
	{
		this.gameObject.SetActive(false);
		clickedTIle = null;
	}

}