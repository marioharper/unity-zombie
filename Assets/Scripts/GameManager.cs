using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public Text ammoText;
	public Image equippedWeaponImage;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance == this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
		InitGame ();
	}

	void InitGame () {
		ammoText.text = "";
		equippedWeaponImage.enabled = false;
	}

	public void UpdateEquippedWeaponSprite(Sprite sprite) {
		equippedWeaponImage.sprite = sprite;
		equippedWeaponImage.enabled = true;
	}

	public void UpdateAmmoText(int curr, int cap) {
		ammoText.text = "Ammo: " + curr.ToString () + " / " + cap.ToString();
	}
}
