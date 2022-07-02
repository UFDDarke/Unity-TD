using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserControl : MonoBehaviour
{
    public GameObject towerPrefab;
    public BuildMenu buildMenu;
    public UpgradeMenu upgradeMenu;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
            if (EventSystem.current.IsPointerOverGameObject())
			{
                return;
			}



            // On Mouse Click
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //print("clicked");

            if(hit.collider != null && hit.collider.gameObject != null)
			{
                GameObject obj = hit.collider.gameObject;
                // Check if clicked object is a tile
                if(obj.GetComponent<TileScript>() != null)
				{
                    // We clicked on a tile. Now, check if there is a tower currently placed on that tile

                    if(obj.GetComponent<TileScript>().tower != null)
					{
                        // There's a tower on this tile, so bring up the upgrade panel
                        upgradeMenu.clickedTile = obj.GetComponent<TileScript>();
                        upgradeMenu.ShowMenu();
                        buildMenu.HideMenu();
					} else
					{
                        // No tower on the tile, bring up the build panel

                        buildMenu.clickedTile = obj.GetComponent<TileScript>();
                        buildMenu.ShowMenu();
                        upgradeMenu.HideMenu();
                    }
				}
            }
            else
            {
                // Didn't click on anything, so hide the build menu
                buildMenu.HideMenu();
                upgradeMenu.HideMenu();
            }
        }
    }
}
