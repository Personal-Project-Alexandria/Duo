using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPause : Challenge {

	public float waitTime;
	public float rotateTime;
	public float distance;
	public GameObject obstacle;
	private List<GameObject> obstacles;
	private float wait;
	private float angle;

	public override void Create()
	{
		base.Create();

		Vector3 p = transform.position;
		GameObject center = Instantiate(obstacle, new Vector3(p.x, p.y), Quaternion.identity, transform);
		GameObject top = Instantiate(obstacle, new Vector3(p.x + 0, p.y + distance), Quaternion.identity, transform);
		GameObject bottom = Instantiate(obstacle, new Vector3(p.x + 0, p.y - distance), Quaternion.identity, transform);
		GameObject left = Instantiate(obstacle, new Vector3(p.x - distance, p.y + 0), Quaternion.identity, transform);
		GameObject right = Instantiate(obstacle, new Vector3(p.x + distance, p.y + 0), Quaternion.identity, transform);

		obstacles = new List<GameObject>() { center, top, bottom, left, right };
		wait = 0f;
		angle = 0f;
	}

	public override void Process(float time)
	{
		base.Process(time);
		if (wait > 0)
		{
			wait -= time;
			return;
		}

		float offset = time * rotateTime;
		angle += offset;

		if (angle >= 90)
		{
			offset = 90 - (angle - offset);
			wait = waitTime;
			angle = 0f;
		}

		transform.Rotate(new Vector3(0f, 0f, offset));
		for (int i = 0; i < obstacles.Count; i++)
		{
			obstacles[i].transform.rotation = Quaternion.identity;
		}
	}
}
