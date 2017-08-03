using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : Challenge {

	public GameObject obstacle;
	public float distance;
	public float speed;

	private List<GameObject> obstacles;
	private GameManager.GameInfo info;
	private float start;
	private float end;
	private int wait;

	public override void Create()
	{
		base.Create();
		obstacles = new List<GameObject>();
		info = GameManager.Instance.info;

		do
		{
			Vector3 pos = new Vector3(P.x + obstacles.Count * distance - info.width - distance, P.y);
			GameObject obs = (GameObject)Instantiate(obstacle, pos, Quaternion.identity, transform);
			obstacles.Add(obs);
		} while (obstacles[obstacles.Count - 1].transform.position.x < info.width);

		if (speed >= 0)
		{
			start = obstacles[0].transform.position.x;
			end = obstacles[obstacles.Count - 1].transform.position.x;
			wait = obstacles.Count - 1;
		}
		else
		{
			end = obstacles[0].transform.position.x;
			start = obstacles[obstacles.Count - 1].transform.position.x;
			wait = 0;
		}
	}

	public override void Process(float time)
	{
		base.Process(time);
		for (int i = 0; i < obstacles.Count; i++)
		{
			if (i == wait)
			{
				continue;
			}
			obstacles[i].transform.position += time * new Vector3(speed, 0);
			if ((obstacles[i].transform.position.x > end && speed >= 0) ||
				(obstacles[i].transform.position.x < end && speed < 0))
			{
				obstacles[i].transform.position = new Vector3(start, P.y);
				obstacles[wait].transform.position += time * new Vector3(speed, 0);
				wait = i;
			}
		}
	}
}
