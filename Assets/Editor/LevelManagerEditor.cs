using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (LevelManager))]
public class LevelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        LevelManager level = (LevelManager)target;
        if(GUILayout.Button("Create Enemy"))
        {
            // Create an enemy
            Enemy newEnemy = EnemyManager.createEnemy("test", 1);
        }
    }
}
