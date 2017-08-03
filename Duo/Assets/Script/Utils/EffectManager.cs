using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager> {

	public GameObject ballExplode;
	
	public void Explode(Vector3 position, float scale = 1)
	{
		GameObject effect = (GameObject)Instantiate(ballExplode, position, Quaternion.identity, transform);
		effect.transform.localScale = new Vector3(scale, scale, scale);
	}
}
