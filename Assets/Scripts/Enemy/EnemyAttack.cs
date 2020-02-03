using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public int damage = 10;

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") DamagePlayer (other.gameObject);
	}

	private void DamagePlayer (GameObject player) {
		PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
		playerHealth.TakeDamage (damage);
	}
}
