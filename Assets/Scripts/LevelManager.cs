using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Git Test
enum Direction
{
    NORTH, // 0
    EAST, // 1
    SOUTH, // 2
    WEST // 3
}

public enum TileType
{
    WALL, // 0
    PATH, // 1
    START,
    END
}

// TODO: Convert into a static class (we don't need to instantiate this + would be nice to access from anywhere)
public static class LevelManager
{
    public static GameObject tilePrefab;
    public static GameObject tileParent;
    public static List<GameObject> tiles = new List<GameObject>();
    public static List<GameObject> path = new List<GameObject>();
    public static Material buildableMaterial;
    public static Material emptyMaterial;
    public static Material roadMaterial;
    public static Camera levelCam;

    // Start is called before the first frame update

    public static int length;
    public static int height;

    public static GameObject startTile;
    public static GameObject endTile;

    public static List<GameObject> towers;

    public static void Start()
    {
        Debug.Log("Test!");
        #region Create Tiles
        for (int l = 0; l < length; l++)
        {
            for (int h = 0; h < height; h++)
            {
                //GameObject newTile = Instantiate(tilePrefab, new Vector3(l - (length / 2), h - (height / 2), 10.0f), transform.rotation);
                GameObject newTile = Transform.Instantiate(tilePrefab, new Vector3(l, h, 0.0f), Quaternion.identity);
                tiles.Add(newTile);
                newTile.transform.parent = tileParent.transform;
                //newTile.GetComponent<TileScript>().x = l + 1;
                //newTile.GetComponent<TileScript>().y = h + 1;
                newTile.GetComponent<TileScript>().pos = new Vector2(l, h );
                newTile.GetComponent<TileScript>().index = l + h;
            }
        }
        startTile = tiles[0];
        endTile = tiles[tiles.Count - 1];

        startTile.GetComponentInChildren<SpriteRenderer>().material = buildableMaterial;
        startTile.GetComponent<TileScript>().type = TileType.START;
        endTile.GetComponentInChildren<SpriteRenderer>().material = emptyMaterial;
        endTile.GetComponent<TileScript>().type = TileType.END;

        #endregion

        #region Build Path
        // Build Random Path
        // This should select a random direction to take the path from the leading node, checking all possible directions.
        // Invalid nodes are: Directly next to a wall, adjacent to another path

        path.Add(tiles[0]);

        for(int i = 1; i < height; i++)
        {
            GameObject pathTile = getTileAtCoordinate(0, i);
            pathTile.GetComponentInChildren<SpriteRenderer>().material = roadMaterial;
            path.Add(pathTile);
            pathTile.GetComponent<TileScript>().type = TileType.PATH;
        }

        for (int i = 0; i < length - 1; i++)
        {
            GameObject pathTile = getTileAtCoordinate(i, height - 1);
            pathTile.GetComponentInChildren<SpriteRenderer>().material = roadMaterial;
            path.Add(pathTile);
            pathTile.GetComponent<TileScript>().type = TileType.PATH;
        }

        path.Add(tiles[tiles.Count - 1]);

        #endregion

        #region Camera Position
        // Position camera
        levelCam.transform.position = new Vector3(length / 2.0f, height / 2.0f, -10.0f);
        #endregion

        #region Cosmetic
        // COSMETIC: Tiles snap together if they are of the same type
        /*
        foreach (GameObject tile in tiles)
        {
            // TODO: instead of each direction individually, do a foreach direction
            int thisType = tile.GetComponent<TileScript>().type;
            GameObject cardinalTile = getTileInDirection(tile, Direction.NORTH);
            if(cardinalTile != null && cardinalTile.GetComponent<TileScript>().type == thisType)
            {
                tile.transform.localScale += new Vector3(0, 0.1f);
                tile.transform.position += new Vector3(0, 0.045f);
            }

            cardinalTile = getTileInDirection(tile, Direction.SOUTH);
            if (cardinalTile != null && cardinalTile.GetComponent<TileScript>().type == thisType)
            {
                tile.transform.localScale += new Vector3(0, 0.1f);
                tile.transform.position -= new Vector3(0, 0.045f);
            }

            cardinalTile = getTileInDirection(tile, Direction.EAST);
            if (cardinalTile != null && cardinalTile.GetComponent<TileScript>().type == thisType)
            {
                tile.transform.localScale += new Vector3(0.1f, 0);
                tile.transform.position += new Vector3(0.045f, 0);
            }

            cardinalTile = getTileInDirection(tile, Direction.WEST);
            if (cardinalTile != null && cardinalTile.GetComponent<TileScript>().type == thisType)
            {
                tile.transform.localScale += new Vector3(0.1f, 0);
                tile.transform.position -= new Vector3(0.045f, 0);
            }
        }*/
        #endregion
    }

    public static void Reset()
    {
        foreach(GameObject tile in tiles)
        {
            UnityEngine.Object.Destroy(tile);
        }
        tiles.Clear();
        path.Clear();

    }

    // Update is called once per frame
    static void Update()
    {
        
    }

    public static void CreateTower(GameObject tile, GameObject prefab)
	{
        // Creates a tower of type 'prefab' at given 'tile'
	}



    static GameObject getTileAtCoordinate(int x, int y)
    {
        // To get X:1 Y:0
        // The tile index is 9
        // So each X value is +9 to the index, while each Y is +1

        return tiles[(x * height) + y];
    }

    static GameObject getTileInDirection(GameObject initial, Direction direct)
    {
        Vector2 pos = initial.GetComponent<TileScript>().pos;
        Vector2 newPos = pos;

        switch (direct)
        {
            case Direction.NORTH:
                // add 1 to y, unless y == (height - 1)
                if (pos.y == height - 1) return null;
                newPos.y += 1;

                break;

            case Direction.EAST:
                // add 1 to x, unless x == (length - 1)
                if (pos.x == length - 1) return null;
                newPos.x += 1;

                break;

            case Direction.SOUTH:
                // subtract 1 from y, unless y == 0
                if (pos.y == 0) return null;
                newPos.y -= 1;

                break;

            case Direction.WEST:
                // subtract 1 from x, unless x == 0
                if (pos.x == 0) return null;
                newPos.x -= 1;

                break;
        }

        return getTileAtCoordinate((int)newPos.x, (int)newPos.y);
    }
}
