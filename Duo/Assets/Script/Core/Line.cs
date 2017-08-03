using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Line : MonoBehaviour {

	public GameObject dotPrefab;
	public int count;
	public float distance;
	public bool hit = false;
	public float h = 1.5f;

	private List<GameObject> dots;
	private BoxCollider2D boxCollider;
	private SpriteRenderer sprite;
	private GameManager.GameInfo info;

	protected void Start()
	{
		dots = new List<GameObject>();
		boxCollider = GetComponent<BoxCollider2D>();
		sprite = dotPrefab.GetComponent<SpriteRenderer>();
		CreateDots_2();
	}

	public void CreateDots()
	{
		float width = Camera.main.orthographicSize * Camera.main.aspect;
		float distance = (2 * width) / (count - 1);	
		for (int i = 0; i < count; i++)
		{
			float x = -width + distance * i;
			float y = transform.position.y;
			GameObject dot = Instantiate(dotPrefab, new Vector3(x, y), Quaternion.identity, transform);
			dots.Add(dot);
		}

		boxCollider.size = new Vector2(2 * width, sprite.bounds.size.y);
	}

	public void CreateDots_2()
	{
		info = GameManager.Instance.info;

		do
		{
			float x = transform.position.x - info.width + distance * dots.Count;
			float y = transform.position.y;
			GameObject dot = Instantiate(dotPrefab, new Vector3(x, y), Quaternion.identity, transform);
			dots.Add(dot);
			if (dots.Count > 100)
			{
				break;
			}
		} while (dots[dots.Count - 1].transform.position.x < info.width);

		boxCollider.size = new Vector2(2 * info.width, sprite.bounds.size.y);
	}

	public void OnHit()
	{
		hit = true;
		for (int i = 0; i < dots.Count; i++)
		{
			SpriteRenderer sprite = dots[i].GetComponent<SpriteRenderer>();
			dots[i].transform.DOMoveY(transform.position.y + h, 0.5f + 0.025f * i)
				.SetLoops(2, LoopType.Yoyo)
				.OnComplete(delegate { OnFinishLoop(sprite); });
		}
	}

	void OnFinishLoop(SpriteRenderer sprite)
	{
		sprite.color = new Color32(40, 40, 40, 255);
	}
}
