using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public float startingHealth = 100f;
	public Slider slider;                        
	public Image fillImage;   
	public Color fullHealthColor = Color.green;  
	public Color zeroHealthColor = Color.red;    

	private float currentHealth;
	private bool dead;

	private void OnEnable()
	{
		currentHealth = startingHealth;
		dead = false;

		SetHealthUI();
	}

	private void SetHealthUI()
	{
		// Adjust the value and color of the slider.
		slider.value = currentHealth;

		fillImage.color = Color.Lerp (zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
	}

	public void TakeDamage(float amount)
	{
		// Adjust the player's current health, update the UI based on the new health and check whether or not the tank is dead.
		currentHealth -= amount;
		SetHealthUI();

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
