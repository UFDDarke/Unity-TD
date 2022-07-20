using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Tower Defense/New Tower")]
public class TowerData : ScriptableObject
{
    [Header("Tower Information")]
    public new string name = "New Tower";
    public string description = "Tower description here";
    public Sprite icon;
    [Min(1)]
    public int cost;

    [Header("Attack Stats")]
    [Min(0.5f)]
    [Tooltip("Damage dealt per attack hit.")]
    public float damage;

    [Range(1f, 20f)]
    [Tooltip("Range of the tower, measured roughly in tiles.")]
    public float range;

    [Range(0.03f, 10f)]
    [Tooltip("How long the tower must wait before it may attempt another attack. Lower is faster.")]
    public float atkSpeed;

    [Range(1f, 30f)]
    public float projectileSpeed; // How fast the tower's projectiles are

    [Range(1, 30)]
    [Tooltip("How many targets this tower may fire at per attack.")]
    public int maxTargets = 1; // How many targets this tower can fire at

    [Tooltip("If firing more than one projectile, can additional projectiles target the same enemy?")]
    public bool canHitSameTarget = false; // If maxTargets> > 1, can additional projectiles fire at the same target?


    
    [Header("MISC")]
    [HideInInspector]
    public GameObject prefab; // Which projectile prefab the tower will use
    [HideInInspector]
    public Vector3 projectileOffset; // UNUSED; to be used to determine a projectile's starting point

    //[Header("Place tower-specific scripts in this list")]
    //public List<Tower> scripts; // place tower-specific scripts in this list, which will override Tower.cs to implement specialized functionality

    public string GetTooltipInfo()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("Attack: " + Math.Round(damage / atkSpeed, 2) + " dps, Physical, " + range + " range").AppendLine();
        return builder.ToString();
    }
}
