using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public float startingHealth = 100f;
	private float currentHealth;
	private bool dead;

	private void OnEnable()
	{
		currentHealth = startingHealth;
		dead = false;
	}

	public void TakeDamage(float amount)
	{
		// Adjust the current health, check whether or not the zombie is dead.
		currentHealth -= amount;

		if (currentHealth <= 0f && !dead) {
			OnDeath ();
		}
	}

	private void OnDeath()
	{
		dead = true;
		gameObject.SetActive (false);
	}
}
