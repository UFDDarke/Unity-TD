using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// This is the panel that appears upon clicking a tile already occupied by a tower
public class UpgradeMenu : MonoBehaviour
{
	public TileScript clickedTile;
	public Tower clickedTower;
	public GameObject upgradePanel;
	[SerializeField] public Tooltip tooltip;


	public Text towerName;
	public Text damageText;
	public Text multishotText;
	public Text attackSpeedText;
	public Text rangeText;
	public Text extraTraits;
	public GameObject rangeIndicator;

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
		rangeIndicator.transform.position = clickedTile.pos;
	}

	public void Update()
	{
		UpdateValues();
	}

	public void UpdateValues()
	{
		if (this.gameObject.activeSelf && clickedTile != null)
		{
			Tower tower = clickedTile.tower;
			towerName.text = tower.Data.name.ToString();
			damageText.text = tower.damage.ToString();

			if(tower.Data.maxTargets > 1)
			{
				// show multishot text
				multishotText.gameObject.SetActive(true);
				multishotText.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = tower.Data.maxTargets.ToString();
			} else
			{
				// hide multishot text
				multishotText.gameObject.SetActive(false);
			}


			attackSpeedText.text = tower.atkSpeed.ToString();
			rangeText.text = tower.range.ToString();
			rangeIndicator.transform.localScale = new Vector2(2 * tower.range, 2 * tower.range);

			StringBuilder builder = new StringBuilder();
			bool needNewline = false;

			if(tower.Data.criticalChance > 0 && tower.Data.criticalDamage > 0)
			{
				builder.Append("<b>" + (tower.criticalChance * 100).ToString() + "%</b> chance for <b>+" + (tower.criticalDamage * 100).ToString() + "</b>% extra damage.");
				needNewline = true;
			}

			if(tower.Data.canHitSameTarget)
			{
				if(needNewline)
				{
					builder.AppendLine();
				}
				builder.Append("Excess projectiles strike main target.");
				needNewline = true;
			}


			string builtString = builder.ToString();
			if (builtString.Length == 0)
			{
				// No extra traits on tower, so hide the extra traits
				extraTraits.gameObject.transform.parent.gameObject.SetActive(false);
			} else
			{
				// Built string has content, so let's show it.
				extraTraits.text = builtString;
				extraTraits.gameObject.transform.parent.gameObject.SetActive(true);
			}

			LayoutRebuilder.ForceRebuildLayoutImmediate(upgradePanel.GetComponent<RectTransform>());

		}
	}

	public void ShowMenu()
	{
		this.gameObject.SetActive(true);
		rangeIndicator.SetActive(true);
		UpdateValues();
		MoveToCursor();
		this.clickedTower = clickedTile.tower;
		Canvas.ForceUpdateCanvases();
	}

	public void Sell()
	{
		clickedTower.sellTower();
		HideMenu();
	}

	public void HideMenu()
	{
		this.gameObject.SetActive(false);
		clickedTile = null;
		rangeIndicator.SetActive(false);
	}

}
