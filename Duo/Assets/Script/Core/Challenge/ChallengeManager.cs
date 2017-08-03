using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour {

	public Transform challengeTransform;
	public Transform lineTransform;

	public int max = 3;
	public Vector3 start;
	public GameObject linePrefab;
	public List<GameObject> challenges;
	public List<Line> lines;
	private List<Challenge> list;
	private GameManager gameManager;

	protected void Start()
	{
		list = new List<Challenge>();
	}
	
	public void OnStart(GameManager gameManager)
	{
		for (int i = 0; i < max; i++)
		{
			CreateChallenge(Random.Range(0, challenges.Count));
		}
		this.gameManager = gameManager;
	}

	public void OnEnd()
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] != null)
			{
				Destroy(list[i].gameObject);
			}
		}

		for (int i = 0; i < lines.Count; i++)
		{
			if (lines[i] != null)
			{
				Destroy(lines[i].gameObject);
			}
		}

		list.Clear();
		lines.Clear();
	}

	public void CreateChallenge(int num)
	{
		if (challenges != null && num < challenges.Count && challenges.Count > 0)
		{
			Vector3 challengePos = Camera.main.transform.position;
			challengePos.z = challenges[num].transform.position.z;

			if (list.Count == 0)
			{
				challengePos += start;
			}
			else
			{
				challengePos = list[list.Count - 1].transform.position + new Vector3(0, list[list.Count - 1].top) + 
					new Vector3(0, challenges[num].GetComponent<Challenge>().bottom);
			}

			GameObject challenge = (GameObject)Instantiate(challenges[num], challengePos, Quaternion.identity, challengeTransform);
			list.Add(challenge.GetComponent<Challenge>());

			Vector3 linePos = list[list.Count - 1].transform.position + new Vector3(0, list[list.Count - 1].top);
			GameObject line = (GameObject)Instantiate(linePrefab, linePos, Quaternion.identity, lineTransform);
			lines.Add(line.GetComponent<Line>());
		}
	}

	public void Renew(int cur)
	{
		if (cur - 2 >= 0)
		{
			CreateChallenge(Random.Range(0, challenges.Count));
			Destroy(list[cur - 2].gameObject);
			Destroy(lines[cur - 2].gameObject);
		}
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			gameManager.PauseGame();
		}
	}
}
