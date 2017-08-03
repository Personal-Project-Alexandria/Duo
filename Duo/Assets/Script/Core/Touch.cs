using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

	private bool touchable;
	private GameManager gameManager;
	private BoxCollider2D boxCollider;

	// ------------------ MONO_BEHAVIOR -------------------- //

	protected void Start()
	{
		if (boxCollider == null)
		{
			boxCollider = GetComponent<BoxCollider2D>();
		}
		float height = Camera.main.orthographicSize * 2;
		float width =  height * Camera.main.aspect;
		boxCollider.size = new Vector2(width, height);
	}

	protected void Update()
	{
		
	}

	protected void LateUpdate()
	{
		Vector3 pos = gameManager.cam.transform.position;
		transform.position = new Vector3(pos.x, pos.y, transform.position.z);
	}

	protected void OnMouseDown()
	{
		if (!gameManager.pause)
		{
			gameManager.body.AddForce();
		}
	}

	// ------------------ GAME INVOLVES --------------------- //

	public void OnStart()
	{
		touchable = true;
	}

	public void OnStart(GameManager gameManager)
	{
		this.gameManager = gameManager;
		this.OnStart();
	}

	public void OnEnd()
	{
		touchable = false;
	}

	// ---------------------- OTHERS ------------------------- //

}
