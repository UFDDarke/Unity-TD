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
                    // We clicked on a tile. Check if it's a buildable tile.
                    TileScript clickedTile = obj.GetComponent<TileScript>();

                    if(clickedTile.type == TileType.WALL)
					{
                        switch(clickedTile.tower)
						{
                            case null:
                                Debug.Log("Clicked on a tile with a null tower");
                                buildMenu.clickedTile = clickedTile;
                                buildMenu.ShowMenu();
                                upgradeMenu.HideMenu();

                                break;
                            default:
                                Debug.Log("Clicked on a tile with a tower present");
                                upgradeMenu.clickedTile = clickedTile;
                                upgradeMenu.ShowMenu();
                                buildMenu.HideMenu();
                                break;
						}
					}

                    if(clickedTile.type == TileType.PATH)
					{
                        buildMenu.HideMenu();
                        upgradeMenu.HideMenu();
                    }

				}
            }
            else
            {
                // Didn't click on anything, so hide the menus
                buildMenu.HideMenu();
                upgradeMenu.HideMenu();
            }
        }
    }
}
