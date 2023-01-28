using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    private Enemy enemy; // Which enemy this healthbar is attached to
    public Image image;
    public Image ghostImage; // the 'ghost' of the healthbar to show how much health was lost

    public void Initialize(Enemy enemy_)
	{
        enemy = enemy_;

        UpdateValues();
    }

    public void UpdateValues()
	{
        image.fillAmount = (enemy.health / enemy.healthMax);
        ghostImage.fillAmount = Mathf.Max(image.fillAmount, ghostImage.fillAmount * 0.997f);
        // Hide healthbar if it is full
        // TODO: If fillAmount is greater than 1, show a 'shield' over health bar?
        if (image.fillAmount >= 1) this.gameObject.SetActive(false); else this.gameObject.SetActive(true);
	}
}
