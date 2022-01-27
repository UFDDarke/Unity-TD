using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject[] entries;

    public Vector2 moveInput;
    void activate()
	{
        // Activates the menu GameObject
        menu.SetActive(true);

	}

    void Update()
    {
        if(menu.activeInHierarchy)
		{
            moveInput.x = Input.mousePosition.x - Screen.width / 2f;
            moveInput.y = Input.mousePosition.y - Screen.height / 2f;
            moveInput.Normalize();

            if(moveInput != Vector2.zero)
			{
                // calculate angle
                float angle = Mathf.Atan2(moveInput.y, -moveInput.x) / Mathf.PI;
                angle *= 180;
                angle += 90f;
                if(angle < 0)
				{
                    angle += 360;
				}



			}
		}
    }
}
