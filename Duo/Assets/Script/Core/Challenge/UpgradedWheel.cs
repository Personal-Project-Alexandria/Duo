using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedWheel : Challenge {

	public GameObject wheelPrefab;
	public float speed;
	public int count;
	public bool sameDirection = true;
	public bool sameSpeed = true;
	public float decrease = 0.1f;
	private List<Circle> wheels;

	public override void Create()
	{
		base.Create();
		wheels = new List<Circle>();
		for (int i = 0; i < count; i++)
		{
			Vector3 pos = new Vector3(P.x, P.y);
			GameObject wheelObject = Instantiate(wheelPrefab, pos, Quaternion.identity, transform);
			Circle wheel = wheelObject.GetComponent<Circle>();
			wheel.xradius = wheel.yradius = 1.5f + 0.3f * i;
			wheels.Add(wheel);
		}
	}

	public override void Process(float time)
	{
		base.Process(time);
		for (int i = 0; i < wheels.Count; i++)
		{
			float v = speed * time;
			if (!sameDirection && i % 2 != 0)
			{
				v = -v;
			}
			if (!sameSpeed)
			{
				v *= (1 - i * decrease);
			}

			wheels[i].transform.Rotate(new Vector3(0f, 0f, v));
		}
	}
}
