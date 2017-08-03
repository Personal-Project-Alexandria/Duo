using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	protected void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Line"))
		{
			if (!col.GetComponent<Line>().hit)
			{
				col.GetComponent<Line>().OnHit();
				GameManager.Instance.AddScore();
			}
		}
		else if (col.CompareTag("Challenge"))
		{
			if (!GameManager.Instance.end)
			{
				GameManager.Instance.EndGame();
			}
		}
	}
}
