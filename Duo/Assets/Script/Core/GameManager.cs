using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager> {

	public class GameInfo
	{
		public float width;
		public float height;
	}

	public Body body;
	public Touch touch;
	public Background background;
	public Camera cam;
	public ChallengeManager challengeManager;

	[HideInInspector]
	public GameInfo info;
	private int score;
	public bool pause;
	public bool end;

	protected void Awake()
	{
		info = new GameInfo();
		info.height = Camera.main.orthographicSize;
		info.width = info.height * Camera.main.aspect;
		DOTween.Init();
	}

	protected void Start()
	{
		GamePlayDialog play = GUIManager.Instance.OnShowDialog<GamePlayDialog>("Play");
	}

	public void StartGame()
	{
		pause = false;
		end = false;
		score = 0;
		cam.transform.position = new Vector3(0, 0, -10);
		body.OnStart(this);
		touch.OnStart(this);
		background.OnStart(this);
		challengeManager.OnStart(this);
	}

	public void RestartGame()
	{
		pause = false;
		end = false;
		body.OnEnd();
		touch.OnEnd();
		challengeManager.OnEnd();
		StartGame();
	}

	public void EndGame()
	{
		end = true;
		StartCoroutine(End());
	}

	public void PauseGame()
	{
		pause = !pause;
	}

	public void AddScore()
	{
		score++;
		challengeManager.Renew(score);
	}

	IEnumerator End()
	{
		PauseGame();
		body.OnEnd();
		yield return new WaitForSeconds(1.5f);
		touch.OnEnd();
		background.OnEnd();
		challengeManager.OnEnd();
		StartGame();
	}
}
