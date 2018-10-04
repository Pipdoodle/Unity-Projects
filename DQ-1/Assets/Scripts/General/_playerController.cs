using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _playerController : MonoBehaviour {

	public float moveSpeed;
	public static bool playerMoving;
	public static bool busy;

	private Vector2 lastMove;
	private Rigidbody2D myRigidbody;
	private static bool playerExists; //will be false by default

	private Animator anim;

	// Use this for initialization
	void Start () {
		playerMoving = true;
		if (!playerExists) {
			playerExists = true;
			transform.gameObject.DontDestroyOnLoad();
		} else
		{Destroy (gameObject);}
		//anim = GetComponent<Animator>(); //for animations later
		myRigidbody = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();
		if (anim == null){
			Debug.Log("cannot find animator!");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerMoving && !busy) {
			Debug.Log("player can move");
			if (Input.GetAxisRaw ("Horizontal") > 0.5f
			  || Input.GetAxisRaw ("Horizontal") < -0.5f) {
				myRigidbody.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal") * moveSpeed, myRigidbody.velocity.y);
				playerMoving = true;
				//lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
				//for animations later
			}	

			if (Input.GetAxisRaw ("Vertical") > 0.5f
			  || Input.GetAxisRaw ("Vertical") < -0.5f) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, Input.GetAxisRaw ("Vertical") * moveSpeed);
				playerMoving = true;
				//lastMove = new Vector2 (Input.GetAxisRaw ("Vertical"), 0f);
				//for animations later
			}

			if (Input.GetAxisRaw ("Horizontal") < 0.5f
			  && Input.GetAxisRaw ("Horizontal") > -0.5f) {
				myRigidbody.velocity = new Vector2 (0f, myRigidbody.velocity.y);
			}

			if (Input.GetAxisRaw ("Vertical") < 0.5f
			  && Input.GetAxisRaw ("Vertical") > -0.5f) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0f);
			}

		anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));

		}

	}

}