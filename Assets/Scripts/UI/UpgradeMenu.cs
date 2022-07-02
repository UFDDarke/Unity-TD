using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is the panel that appears upon clicking a tile already occupied by a tower
public class UpgradeMenu : MonoBehaviour
{
	public TileScript clickedTile;
	public GameObject upgradePanel;
	[SerializeField] public Tooltip tooltip;


	public Text towerName;
	public Text damageText;
	public Text attackSpeedText;
	public Text rangeText;

	public void Start()
	{
		HideMenu();
	}

	public void MoveToCursor()
	{
		Vector3 offset = new Vector3(0, 0, 0);
		RectTransform popupObject = upgradePanel.GetComponent<RectTransform>();
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

	public void Update()
	{
		UpdateValues();
	}

	public void UpdateValues()
	{
		if (this.gameObject.activeSelf && clickedTile != null)
		{
			towerName.text = clickedTile.tower.Data.name.ToString();
			damageText.text = clickedTile.tower.damage.ToString();
			attackSpeedText.text = clickedTile.tower.atkSpeed.ToString();
			rangeText.text = clickedTile.tower.range.ToString();
		}
	}

	public void ShowMenu()
	{
		this.gameObject.SetActive(true);
		UpdateValues();
		MoveToCursor();
		Canvas.ForceUpdateCanvases();
	}

	public void HideMenu()
	{
		this.gameObject.SetActive(false);
		clickedTile = null;
	}

}
