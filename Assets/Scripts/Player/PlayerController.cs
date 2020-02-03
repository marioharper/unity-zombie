using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	private Transform playerGraphics;
	private Rigidbody2D body;
	private Animator anim;
	private bool facingRight;
	private float move;
	private bool grounded;
	private float groundRadius = 0.2f;
	private PlayerInventory inventory;
	
	private void Start () {
		playerGraphics = GetComponent<Transform> (); 
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		facingRight = playerGraphics.localScale.x > 0;
	}

	private void Awake ()
	{
		inventory = GetComponent<PlayerInventory>();
	}
	
	private void FixedUpdate () {
		Move ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		PickUpItem(other);
	}

	private void Update() {
		Crouch ();
		Jump ();
		GroundCheck ();
		Flip ();
		Falling ();
	}
		
	//////
	
	private void GroundCheck() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("grounded", grounded);
	}

	private void Move() {
		move = Input.GetAxisRaw ("Horizontal");
		anim.SetFloat ("velocityX", Mathf.Abs (move));

		body.velocity = new Vector2 (move * maxSpeed, body.velocity.y);
	}

	private void Crouch () {
		float vertical = Input.GetAxisRaw ("Vertical");

		if (grounded && vertical < 0) {
			anim.SetBool("crouch", true);
		} else {
			anim.SetBool("crouch", false);
		}
	}

	private void Jump () {
		if (Input.GetButtonDown ("Jump") && grounded) {
			body.AddForce (new Vector2 (0, jumpForce)); 
			anim.SetTrigger("jump");
		}
	}

	private void Falling () {
		bool falling = !grounded && body.velocity.y < -0.1;
		anim.SetBool ("falling", falling);
	}

	private void Flip() {
		Vector3 theScale = playerGraphics.localScale;

		if (move > 0 && !facingRight || move < 0 && facingRight) {
			facingRight = !facingRight;

			theScale.x *= -1;
			playerGraphics.localScale = theScale;
		}
	}

	private void PickUpItem(Collider2D other) {
		if (other.gameObject.CompareTag ("Weapon Pick Up")) {
				other.gameObject.SetActive (false);
				inventory.Add (other.gameObject.GetComponent<Weapon>());
			} 
		}
}
