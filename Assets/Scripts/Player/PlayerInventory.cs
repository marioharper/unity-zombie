using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory {
	private List<Weapon> weapons = new List<Weapon>();
	private int equippedWeaponIdx = -1;
	
	public void Add(Weapon weapon) {
		weapons.Add(weapon);
	}

	public void Equip(int weaponIdx) {
		equippedWeaponIdx = weaponIdx;
		Weapon equippedWeapon = GetEquippedWeapon();
		GameManager.instance.UpdateEquippedWeaponSprite(equippedWeapon.gameObject.GetComponent<SpriteRenderer>().sprite);
		GameManager.instance.UpdateAmmoText(equippedWeapon.ammo, equippedWeapon.maxAmmo);
	}

	public Weapon GetEquippedWeapon() {
		Weapon weapon = null;

		try {
			weapon = weapons[equippedWeaponIdx];
		} catch (System.ArgumentOutOfRangeException e) {}

		return weapon;
	}

	public Weapon GetWeapon(int weaponIdx) {
		return weapons[weaponIdx];
	}

	public void EquipNextWeapon() {
		int nextWeaponIdx = equippedWeaponIdx + 1;

		if (nextWeaponIdx >= weapons.Count) {
			return;
		}

		Equip(nextWeaponIdx);
	}

	public void EquipPreviousWeapon() {
		int nextWeaponIdx = equippedWeaponIdx - 1;

		if (nextWeaponIdx < 0) {
			return;
		}

		Equip(nextWeaponIdx);
	}
}

public class PlayerInventory : MonoBehaviour {
	private Inventory inventory = new Inventory();
	
	private void Update() {
		EquipNextWeapon ();
		EquipPreviousWeapon ();
	}

	public Weapon GetEquippedWeapon() {
		return inventory.GetEquippedWeapon();
	}

	public void Add(Weapon weapon) {
		inventory.Add(weapon);
	}

	public void Equip(int weaponIdx) {
		inventory.Equip(weaponIdx);
	}

	private void EquipNextWeapon() {
		if (Input.GetKeyDown(KeyCode.E)) {
			inventory.EquipNextWeapon();
		}
	}

	private void EquipPreviousWeapon() {
		if (Input.GetKeyDown(KeyCode.Q)) {
			inventory.EquipPreviousWeapon();
		}
	}
}
