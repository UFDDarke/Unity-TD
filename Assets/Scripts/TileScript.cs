using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 pos;
    public int index;
    public Tower tower; // This is the tower currently placed on this tile
    public TileType type = TileType.WALL;

	private void OnMouseOver()
	{
		//this.gameObject.
	}

	private void OnMouseExit()
	{
		
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
