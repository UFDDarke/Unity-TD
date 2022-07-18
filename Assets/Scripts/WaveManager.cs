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
		WaveNumber = 1;
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
		float health = Mathf.Pow(waveNumber - 1, 2.0f) + ((waveNumber - 1) * 10) + 50;

		return health;
	}

	private float ParseSizeClass(EnemyData data)
	{
		switch(data.sizeClass)
		{
			case SizeClass.Mass:
				return 0.5f;
			case SizeClass.Normal:
				return 1.0f;
			case SizeClass.Champion:
				return 2.0f;
			case SizeClass.Boss:
				return 10.0f;
			default:
				return 1.0f;

		}
	}


	// Delay between enemies should be: (1 * enemy.HealthModifier)

	private IEnumerator WaveLoop()
	{
		Debug.Log("Wave has begun spawning.");

		EnemyData generatedEnemy = (EnemyData) types[Random.Range(0, types.Length)];

		int enemiesToSpawn = (int)(10.0f / ParseSizeClass(generatedEnemy));

		while(enemiesToSpawn > 0)
		{
			Enemy newEnemy = EnemyManager.createEnemy(generatedEnemy, BaseHealth());
			Debug.Log(generatedEnemy.sizeClass);
			enemiesToSpawn--;

			yield return new WaitForSeconds(1 * newEnemy.data.healthModifier / newEnemy.data.speed);
		}

		isSpawning = false;
		WaveNumber++;
		Debug.Log("Finished spawning enemies.");
	}
}
