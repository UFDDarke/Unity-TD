using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Tower Defense/New Tower")]
public class TowerData : ScriptableObject
{
    public new string name = "New Tower";
    public string description = "Tower description here";
    public Sprite icon;

    public int cost;
    public float damage;
    public float range;
    public float atkSpeed; // How much time, in seconds, this tower has to wait to make another attack. Lower is faster.
    public float projectileSpeed; // How fast the tower's projectiles are

    public GameObject prefab; // Which projectile prefab the tower will use
    public Vector3 projectileOffset; // UNUSED; to be used to determine a projectile's starting point

    [Header("Place tower-specific scripts in this list")]
    public List<Tower> scripts; // place tower-specific scripts in this list, which will override Tower.cs to implement specialized functionality

    public string GetTooltipInfo()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("Attack: " + Math.Round(damage / atkSpeed, 2) + " dps, Physical, " + range + " range").AppendLine();
        return builder.ToString();
    }
}
