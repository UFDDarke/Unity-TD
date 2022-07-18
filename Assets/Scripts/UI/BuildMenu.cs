using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
	public GameObject buttonPrefab;
	public GameObject buttonGrid;
	public GameObject buildPanel;
	[SerializeField] public Tooltip tooltip;

	public TileScript clickedTile;
	[SerializeField] public GameObject rangeIndicator;

	public void Start()
	{
		foreach (var t in TowerManager.types)
		{
			BuildButton newButton = Instantiate(buttonPrefab).GetComponent<BuildButton>();
			newButton.buildMenu = this;
			newButton.initialize((TowerData) t, tooltip);
			newButton.transform.SetParent(buttonGrid.transform);
			newButton.transform.localScale = new Vector3(1, 1, 1);
		}

		HideMenu();
		
	}

	public void MoveToCursor()
	{
		Vector3 offset = new Vector3(0, 0, 0);
		RectTransform popupObject = buildPanel.GetComponent<RectTransform>();
		Canvas masterCanvas = tooltip.masterCanvas;

		// Thanks to: 'eses'
		// https://forum.unity.com/threads/create-ui-health-markers-like-in-world-of-tanks.432935/

		Vector3 offsetPos = new Vector3(clickedTile.transform.position.x, clickedTile.transform.position.y, clickedTile.transform.position.z);

		Vector2 canvasPos;
		Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

		RectTransformUtility.ScreenPointToLocalPointInRectangle(masterCanvas.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);




		//Vector3 newPos = Input.mousePosition + offset;
		Vector3 newPos = canvasPos;

		/*
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
		}*/
		popupObject.transform.localPosition = newPos;
	}
	public void ShowMenu()
	{
		this.gameObject.SetActive(true);
		MoveToCursor();
		Canvas.ForceUpdateCanvases();
	}

	public void HideMenu()
	{
		this.gameObject.SetActive(false);
		clickedTile = null;
	}

}
