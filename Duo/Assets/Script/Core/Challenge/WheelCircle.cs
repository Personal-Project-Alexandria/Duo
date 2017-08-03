using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCircle : Challenge {

	public GameObject wheelPrefab;
	public float speed; 

	public override void Create()
	{
		base.Create();

		GameObject wheel = Instantiate(wheelPrefab, new Vector3(P.x, P.y), Quaternion.identity, transform);
	}

	public override void Process(float time)
	{
		base.Process(time);
		transform.Rotate(new Vector3(0, 0, time * speed));
	}
}
