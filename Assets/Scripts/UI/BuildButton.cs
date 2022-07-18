using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Tooltip tooltip;
    public TowerData data;
    public Image icon;
    public Text cost;

	public BuildMenu buildMenu;

	public GameObject prefab;

    public void initialize(TowerData _data, Tooltip tooltip_)
	{
        data = _data;
		tooltip = tooltip_;
		prefab = (GameObject)Resources.Load("Prefabs/Tower");
		//icon = this.gameObject.GetComponent<Image>();

		icon.sprite = data.icon;
        cost.text = data.cost.ToString();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		tooltip.DisplayInfo(data.name, data.GetTooltipInfo(), data.description);
		buildMenu.rangeIndicator.SetActive(true);
		buildMenu.rangeIndicator.transform.position = buildMenu.clickedTile.transform.position;
		buildMenu.rangeIndicator.transform.localScale = new Vector3(data.range * 2, data.range * 2, 1);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.HidePopup();
		buildMenu.rangeIndicator.SetActive(false);
	}

	public void BuildTower()
	{
		Tower newTower = TowerManager.CreateTower(buildMenu.clickedTile, prefab, data);
		buildMenu.HideMenu();
		tooltip.HidePopup();
		buildMenu.rangeIndicator.SetActive(false);
	}

}
