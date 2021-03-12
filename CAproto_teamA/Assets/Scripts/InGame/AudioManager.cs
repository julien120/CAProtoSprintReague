using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
	public class AudioManager : MonoBehaviour
	{
		private BulletsManager bulletsManager;
		void Start()
		{

		}

		void Update()
		{
			if (bulletsManager.isShootingMoment)
			{
				//打った音を出す
			}
			if (bulletsManager.isShootingSucess)
			{
				//弾が当たった音を出す
			}
			if (bulletsManager.isDamageReceived)
			{
				//ダメージをくらった音を出す
			}
		}
	}
}