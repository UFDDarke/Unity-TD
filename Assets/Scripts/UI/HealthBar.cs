using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    private Enemy enemy; // Which enemy this healthbar is attached to
    public Image image;


    public void Initialize(Enemy enemy_)
	{
        enemy = enemy_;

        UpdateValues();
    }

    public void UpdateValues()
	{
        image.fillAmount = (enemy.health / enemy.healthMax);
        // Hide healthbar if it is full
        if (image.fillAmount >= 1) this.gameObject.SetActive(false); else this.gameObject.SetActive(true);
	}
}
