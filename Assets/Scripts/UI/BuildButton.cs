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

	private BuildMenu buildMenu;

	public GameObject prefab;

    public void initialize(TowerData _data, Tooltip tooltip_)
	{
        data = _data;
		tooltip = tooltip_;
		buildMenu = GetComponentInParent<BuildMenu>();
		prefab = (GameObject)Resources.Load("Prefabs/Tower");
		//icon = this.gameObject.GetComponent<Image>();

		icon.sprite = data.icon;
        cost.text = data.cost.ToString();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		tooltip.DisplayInfo(data.name, data.GetTooltipInfo(), data.description);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.HidePopup();
	}

	public void BuildTower()
	{
		Debug.Log(buildMenu.clickedTIle == null); // TODO: clickedTile, or buildMenu is null. Figure this out
		Tower newTower = TowerManager.CreateTower(buildMenu.clickedTIle.transform.position, prefab);
	}

}
