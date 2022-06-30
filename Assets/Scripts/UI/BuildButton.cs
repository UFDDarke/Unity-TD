using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public TowerData data;
    public Image icon;
    public Text cost;

    public void initialize(TowerData _data)
	{
        data = _data;
        //icon = this.gameObject.GetComponent<Image>();

        icon.sprite = data.icon;
        cost.text = data.cost.ToString();
	}
}
