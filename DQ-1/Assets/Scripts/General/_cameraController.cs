using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _cameraController : MonoBehaviour {
	public GameObject followTarget;
	private Vector3 targetPosition;
	public float moveSpeed;
	private static bool playerExists;
	// Use this for initialization

	//public BoxCollider2D boundBox;
	private BoxCollider2D boundBox;
	private Vector3 minBounds;
	private Vector3 maxBounds;

	private Camera theCamera;
	private float halfHeight;
	private float halfWidth;

	void Start () {
		if (!playerExists) {
			playerExists = true;
			transform.gameObject.DontDestroyOnLoad();
		} else
		{Destroy (gameObject);}

		//minBounds = boundBox.bounds.min;
		//maxBounds = boundBox.bounds.max;

		theCamera = GetComponent<Camera> ();
		halfHeight = theCamera.orthographicSize;
		halfWidth = halfHeight * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		targetPosition = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, targetPosition, moveSpeed /** Time.deltaTime*/);
		float clampedX = Mathf.Clamp (transform.position.x, minBounds.x + halfWidth,
			                 maxBounds.x - halfWidth);
		float clampedY = Mathf.Clamp (transform.position.y, minBounds.y + halfHeight,
			maxBounds.y - halfHeight);

		transform.position = new Vector3 (clampedX, clampedY, transform.position.x);
	}

	public void SetBounds(BoxCollider2D collid)
	{
		boundBox = collid;
		minBounds = boundBox.bounds.min;
		maxBounds = boundBox.bounds.max;

	}
}
