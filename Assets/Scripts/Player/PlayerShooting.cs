using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public float lowerArmWaitTime = 2.0f;         

	private Animator anim;
	private IEnumerator lowerAimCoroutine;
	private Rigidbody2D body;
	private PlayerInventory inventory;

	private void Start () {
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
		
	private void Update() {
		Shoot ();
	}

	private void Awake ()
	{
		inventory = GetComponent<PlayerInventory>();
	}

	private void Shoot() {
		bool isPlayerMoving = Mathf.Abs (body.velocity.x) > 0.01;
		bool canShoot = !isPlayerMoving;

		if (canShoot && Input.GetButtonDown ("Fire1")) {
			Weapon gun = inventory.GetEquippedWeapon();

			if (!gun) return;

			anim.SetBool ("aiming", true);

			if (gun && gun.PullTrigger ()) {
				anim.SetTrigger ("shoot");
			}

			if (lowerAimCoroutine != null) StopCoroutine (lowerAimCoroutine);
			lowerAimCoroutine = LowerAim ();
			StartCoroutine (lowerAimCoroutine);
		}
	}

	private IEnumerator LowerAim()
	{
		yield return new WaitForSeconds(lowerArmWaitTime);

		anim.SetBool ("aiming", false);
	}
}
