using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public float speed = 1f;

	private float minRange = 0.4f;
	private Transform transform;
	private Rigidbody2D body;
	private Animator anim;
	private Transform target;
	private bool facingRight = true;

	public Transform Target
	{
		get { return target; }
		set { target = value; }
	}

	private void Start () {
		transform = GetComponent<Transform> (); 
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}

	private void Update () {
		MoveTowardsTarget ();
	}

	private void MoveTowardsTarget() {
		if (target == null) return;

		FaceTarget ();

		float distance = Vector2.Distance(transform.position, target.position);
		bool tooClose = distance < minRange;

		if (!tooClose) {
			Vector2 velocity = new Vector2 (Mathf.Sign (target.position.x - transform.position.x) * speed, body.velocity.y);
			anim.SetFloat ("velocityX", Mathf.Abs (velocity.x));
			body.velocity = velocity;
		}

	}

	private void FaceTarget() {
		//transform.right = target.position - transform.position;

		if (target.position.x < transform.position.x && facingRight) {
			Flip ();
		} else if (target.position.x > transform.position.x && !facingRight) {
			Flip ();
		}
	}

	private void Flip() {
		Vector3 theScale = transform.localScale;

		facingRight = !facingRight;

		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

