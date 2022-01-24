using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
            // On Mouse Click
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            print("clicked");

            if(hit.collider != null && hit.collider.gameObject != null)
			{
                GameObject obj = hit.collider.gameObject;
                // Check if clicked object is a tile
                if(obj.GetComponent<TileScript>() != null)
				{
                    print("Clicked on a tile at X" + obj.GetComponent<TileScript>().pos.x + " Y" + obj.GetComponent<TileScript>().pos.y);
				}
			}
		}
    }
}
