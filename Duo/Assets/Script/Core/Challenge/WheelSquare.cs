using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSquare : Challenge {

	public float waitTime;
	public float rotateTime;
	public float distance;
	public GameObject obstacle;

	public override void Create()
	{
		base.Create();

		Vector3 p = transform.position;
		GameObject top = Instantiate(obstacle, new Vector3(p.x + 0, p.y + distance), Quaternion.identity, transform);
		GameObject bottom = Instantiate(obstacle, new Vector3(p.x + 0, p.y - distance), Quaternion.identity, transform);
		GameObject left = Instantiate(obstacle, new Vector3(p.x - distance, p.y + 0), Quaternion.identity, transform);
		GameObject right = Instantiate(obstacle, new Vector3(p.x + distance, p.y + 0), Quaternion.identity, transform);
	}

	public override void Process(float time)
	{
		base.Process(time);
		transform.Rotate(new Vector3(0f, 0f, time * rotateTime));
	}
}
