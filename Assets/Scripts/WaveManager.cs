using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	private bool isSpawning;
	public bool IsSpawning { get => isSpawning; private set => isSpawning = value; }
	public int WaveNumber { get => waveNumber; private set => waveNumber = value; }

	private int waveNumber;

	public Object[] types;

	public void Awake()
	{
		types = Resources.LoadAll("Enemy", typeof(EnemyData));
		IsSpawning = false;
	}

	public void BeginWave()
	{
		Debug.Log("Beginning wave!");
		if (IsSpawning) return; // Can't spawn a wave while another is already spawning.


		IsSpawning = true;
		// TODO: Dynamically choose enemies to populate spawn lists. Enemies shouldn't be 100% random
		StartCoroutine(WaveLoop());
	}


	private float BaseHealth()
	{
		//float health = ((1.1f * waveNumber) * (1.1f * waveNumber)) + (waveNumber * 10) + (50);
		float health = Mathf.Pow(waveNumber, 2.0f) + (waveNumber * 10) + 50;

		return health;
	}


	// Delay between enemies should be: (1 * enemy.HealthModifier)

	private IEnumerator WaveLoop()
	{
		Debug.Log("Wave has begun spawning.");

		int enemiesToSpawn = 20;

		while(enemiesToSpawn > 0)
		{
			Enemy newEnemy = EnemyManager.createEnemy((EnemyData)types[0], BaseHealth());
			enemiesToSpawn--;

			yield return new WaitForSeconds(1 * newEnemy.data.healthModifier);
		}

		isSpawning = false;
		Debug.Log("Finished spawning enemies.");
	}
}
