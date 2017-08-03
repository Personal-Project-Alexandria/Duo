using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

	public GameObject ballPrefab;			// ball's prefab
	public float distance;					// distace from center
	public float rotateSpeed = 50f;			// default speed
	public float force = 30;				// up force
	public float gravity = -9.8f;			// gravity
	public float fiction = 1;               // fiction
	public Vector3 start;					// start position

	private float speed = 0;
	private float down = 0;				// down force
	private List<Ball> balls;			// balls in game, create 2 on start
	private GameManager gameManager;    // for mother russia
	private float prevY;
	
	// ------------------ MONO_BEHAVIOR -------------------- //

	protected void Start()
	{
		
	}

	protected void Update()
	{
		if (gameManager.pause)
		{
			return;
		}

		transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));
		transform.position += new Vector3(0, Time.deltaTime * down);
		transform.position += new Vector3(0, Time.deltaTime * speed);

		if (speed > 0)
		{
			speed -= Time.deltaTime * fiction;
		}
		else
		{
			speed = 0f;
		}
		
		if (transform.position.y + distance < -Camera.main.orthographicSize + Camera.main.transform.position.y)
		{
			gameManager.EndGame();
		}


		float dif = transform.position.y - prevY;

		if (transform.position.y >= Camera.main.transform.position.y && dif > 0)
		{
			Camera.main.transform.position = new Vector3(transform.position.x, 
				transform.position.y, Camera.main.transform.position.z);
			gameManager.background.Scroll(new Vector2(0, dif), Time.deltaTime);
		}

		prevY = transform.position.y;
	}

	protected void LateUpdate()
	{

	}

	// ------------------ GAME INVOLVES --------------------- //

	public void OnStart()
	{
		transform.position = start;
		Init();
		speed = 0f;
		down = 0f;
		prevY = transform.position.y;
	}

	public void OnStart(GameManager gameManager)
	{
		this.gameManager = gameManager;
		this.OnStart();
	}

	public void OnEnd()
	{
		StartCoroutine(Explode(0.5f));
	}

	public void OnPause()
	{
		
	}

	// ---------------------- OTHERS ------------------------- //

	public void Init()
	{
		if (balls != null)
		{
			balls.Clear();
		}
		else
		{
			balls = new List<Ball>();
		}

		for (int i = 0; i < 2; i++)
		{
			Vector3 dis = new Vector3(distance, 0);
			if (i == 0)
			{
				dis = -dis;
			}

			GameObject ballObject = Instantiate(ballPrefab, transform.position + dis, Quaternion.identity, transform);
			balls.Add(ballObject.GetComponent<Ball>());
		}
	}

	public void Clear()
	{
		if (balls != null)
		{
			for (int i = 0; i < balls.Count; i++)
			{
				Destroy(balls[i].gameObject);
			}
			balls.Clear();
		}
	}

	public void AddForce()
	{
		speed = force;
		down = gravity;
	}

	IEnumerator Explode(float time)
	{
		yield return new WaitForSeconds(time);
		for (int i = 0; i < balls.Count; i++)
		{
			EffectManager.Instance.Explode(balls[i].transform.position);
		}
		Clear();
	}
}
