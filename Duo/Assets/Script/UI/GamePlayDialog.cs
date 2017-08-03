using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayDialog : BaseDialog
{
	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		GameManager.Instance.StartGame();
	}
}

