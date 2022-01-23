using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEndEditor : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject tileParent;
    //public List<GameObject> tiles = new List<GameObject>();
    //public List<GameObject> path = new List<GameObject>();
    public Material buildableMaterial;
    public Material emptyMaterial;
    public Material roadMaterial;
    public Camera levelCam;

    public int length;
    public int height;

    public bool autoUpdate;
    public bool autoGenerate;

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

        if(autoGenerate)
        {
            LevelManager.Reset();
            LevelManager.Start();
        }

    }
}
