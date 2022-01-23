using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEndEditor : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject tileParent;
    public GameObject enemyPrefab;
    //public List<GameObject> tiles = new List<GameObject>();
    //public List<GameObject> path = new List<GameObject>();
    public Material buildableMaterial;
    public Material emptyMaterial;
    public Material roadMaterial;
    public Camera levelCam;

    [Range(2, 20)]
    public int length = 18;
    [Range(2, 14)]
    public int height = 9;

    public bool autoUpdate;

    public void Awake()
    {
        
    }

    public void Sync()
    {
        Debug.Log("Syncing!");
        LevelManager.tilePrefab = tilePrefab;
        LevelManager.tileParent = tileParent;
        LevelManager.buildableMaterial = buildableMaterial;
        LevelManager.emptyMaterial = emptyMaterial;
        LevelManager.roadMaterial = roadMaterial;
        LevelManager.levelCam = levelCam;
        LevelManager.length = length;
        LevelManager.height = height;
        EnemyManager.enemyPrefab = enemyPrefab;

        EnemyManager.Reset();
        LevelManager.Reset();
        LevelManager.Start();

    }
}
