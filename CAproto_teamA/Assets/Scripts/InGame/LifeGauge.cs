using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGauge : MonoBehaviour
{
	[SerializeField] private GameObject lifeObj;

	public void SetLifeGauge(int life)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		for (int i = 0; i < life; i++)
		{
			Instantiate<GameObject>(lifeObj, transform);
		}
	}
	public void DamageSetLifeGauge()
	{
		Destroy(transform.GetChild(0).gameObject);
	}
}
