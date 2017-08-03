using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomSquare : Challenge {

	public float zoomSpeed;
	public float minScale;
	public float waitTime;

	private float k = 1f;
	private float wait;

	public override void Create()
	{
		base.Create();
		this.wait = 0f;
	}

	public override void Process(float time)
	{
		base.Process(time);
		
		// If there's wait then reduce wait and return
		if (wait > 0)
		{
			wait -= time;
			return;
		}

		if (transform.localScale.x >= 1 && k == +1f)
		{
			transform.localScale = new Vector3(1, 1, 1);
			k = -1f;
			this.wait = waitTime;
		}
		else if (transform.localScale.x <= minScale && k == -1f)
		{
			transform.localScale = new Vector3(minScale, minScale, 0);
			k = +1f;
			this.wait = waitTime;
		}
		else
		{
			transform.localScale += k * time * zoomSpeed * new Vector3(1f, 1f, 0f);
		}
	}

}
