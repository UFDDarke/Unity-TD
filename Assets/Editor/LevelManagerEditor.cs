using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FrontEndEditor))]
public class LevelManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        

        //LevelManager level = (LevelManager)target;
        FrontEndEditor frontEnd = (FrontEndEditor)target;
        if(GUILayout.Button("Init Level") && Application.isPlaying)
        {
            frontEnd.Sync();
            //Debug.Log("The map is " + LevelManager.length + "x" + LevelManager.height);
        }


        if(GUILayout.Button("Create Enemy") && Application.isPlaying)
        {
            // Create an enemy
            //Enemy newEnemy = EnemyManager.createEnemy("test", 1);
        }


        if (DrawDefaultInspector() && frontEnd.autoUpdate && Application.isPlaying)
        {
            frontEnd.Sync();
        }

    }
}
