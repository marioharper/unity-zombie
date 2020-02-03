using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour {

	private EnemyController enemyController;

	void Start () {
		enemyController = gameObject.GetComponentInParent(typeof(EnemyController)) as EnemyController;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") enemyController.Target = other.transform;
	}

	void OnTriggerExit2D(Collider2D other) {
		// if (other.transform == target) target = null;
	}
}

