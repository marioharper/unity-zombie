using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

	public int damage = 10;
	public float distance = 1.0f;
	public LayerMask enemy;

	void Update () {
		Melee ();
	}
		
	private void Melee() {
		if (Input.GetButtonDown ("Fire2")) {
			Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, distance, enemy); 

			foreach (Collider2D hit in hits)
			{
				EnemyHealth enemyHealth = hit.gameObject.GetComponent<EnemyHealth>();
				enemyHealth.TakeDamage(damage);
			}
		}
	}

//	void OnDrawGizmos() {
//		Gizmos.color = Color.yellow;
//		Gizmos.DrawWireSphere(transform.position, distance);
//	}

}
