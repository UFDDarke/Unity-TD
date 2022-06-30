using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSegment : MonoBehaviour
{
	public Image image;
    public Color hovered, normal;
    public bool isHovered = false;

	public void toggleSelect()
	{
        // Swaps color between hovered and normal
        if(isHovered)
		{
			// hovered changes to normal
			image.color = normal;
		} else
		{
			// normal changes to hovered
			image.color = hovered;

		}
		isHovered = !isHovered;
	}

	public void setActive()
	{
		if (!isHovered)
		{
			toggleSelect();
		}
	}

	public void setInactive()
	{
		if(isHovered)
		{
			toggleSelect();
		}
	}

	public void Awake()
	{
		image = this.gameObject.GetComponent<Image>();
		image.color = normal;
		setInactive();
	}
}
