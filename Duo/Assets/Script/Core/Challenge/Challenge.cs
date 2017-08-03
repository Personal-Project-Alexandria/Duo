using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour {

	public float top;
	public float bottom;

	public Vector3 P
	{
		get
		{
			return transform.position;
		}
		set
		{
			transform.position = value;
		}
	}

	protected void Start()
	{
		Create();
	}

	protected void Update()
	{
		if (!GameManager.Instance.pause)
		{
			Process(Time.deltaTime);
		}
	}

	public virtual void Create()
	{

	}

	public virtual void Process(float time)
	{

	}
}
