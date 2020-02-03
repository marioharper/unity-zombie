using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public int damage = 10;
	public int ammo = 0;
	public int maxAmmo = 17;
	public float soundRange = 10f;
	public AudioSource shootingAudio;
	public AudioClip fireClip;
	public float effectSpawnRate = 10f;
	public LayerMask whatToHit;
//	public Transform BulletHitPrefab;

	// returns true if weapon fired
	public bool PullTrigger() {
		if (ammo == 0) {
			return false;
		} else {
			Fire ();
			return true;
		}
	}

	private void Fire() {
		Transform firePoint = GameObject.Find("FirePoint").transform;
		Transform firePoint2 = GameObject.Find("FirePoint2").transform;
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		Vector2 firePointPosition2 = new Vector2(firePoint2.position.x, firePoint2.position.y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, firePointPosition2, 100, whatToHit);

		MakeNoise ();

		// Debug.DrawLine(firePointPosition, firePointPosition2, Color.white);

		//we hit something
		if (hit.collider != null)
		{
			//we hit an enemy
			if(hit.collider.tag == "Enemy"){ 
				//get the enemies Enemy script
				EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
				float d = .2f;
				//randomize the hit location
				Vector2 randomPoint = new Vector2(hit.point.x, Random.Range(hit.point.y-d, hit.point.y+d));

				//create a hit
//				Transform bulletHitClone = 	Instantiate(BulletHitPrefab, randomPoint, firePoint.rotation) as Transform;
//				bulletHitClone.parent = hit.collider.transform;
//
				//damage the enemy by weapon damage
				enemyHealth.TakeDamage(damage);
			}
		}

		ammo -= 1;
		GameManager.instance.UpdateAmmoText(ammo, maxAmmo);
	}

	private void MakeNoise()
	{
		shootingAudio.clip = fireClip;
		shootingAudio.Play ();

		AlertEnemies ();
	}

	private void AlertEnemies() {
		Collider2D[] enemiesWhoHeard = Physics2D.OverlapCircleAll(transform.position, soundRange, whatToHit);

		foreach (Collider2D enemy in enemiesWhoHeard )
		{
			EnemyController enemyController = enemy.gameObject.GetComponentInParent(typeof(EnemyController)) as EnemyController;
			enemyController.Target = transform;
		}
	}
}
