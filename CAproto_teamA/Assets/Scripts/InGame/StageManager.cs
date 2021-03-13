using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace InGame
{
	
	public class StageManager : MonoBehaviour
	{

		private GameManager gameManager;
		protected float currentTime; //スコアになるタイム
		public int currentStageNumber;
		public float eachStageProgressTime;　　　　//現在のステージに来てから経過している時間
		[SerializeField] public float oneStageTakeTime = 10;   //一ステージ当たりのかかる時間(テストのため短めに設定中）
		public bool isNowInterval=false;
		public GameObject roundNumberTextObject;
		private Text _roundNumberText;
		public bool _stageChangeMoment=false;
		void Start()
		{
			gameManager = this.GetComponent<GameManager>();
			_roundNumberText=roundNumberTextObject.GetComponent<Text>();
			roundNumberTextObject.SetActive(false);
			currentStageNumber = 1;
		}

		// Update is called once per frame
		void Update()
		{
			if (gameManager.isPlaying || !gameManager.isGameOver)
			{
				//Update関数の下の目印までif(gameManager.isPlaying)で囲って下さい

				if (!isNowInterval)
				{
					if (gameManager.isPressSpaceKey)
					{
						eachStageProgressTime += Time.deltaTime * gameManager.timeSpeedMultiplier;
					}
					else
					{
						eachStageProgressTime += Time.deltaTime;
					}
				}

				if (eachStageProgressTime >= oneStageTakeTime)   //ひとつのステージ終了したら
				{

					isNowInterval = true;
					eachStageProgressTime = 0;
					currentStageNumber++;
					roundNumberTextObject.SetActive(true);
					_roundNumberText.text = "Round" + currentStageNumber;
					StartCoroutine(DelayMethod(2.0f, () =>
					{
						isNowInterval = false;
						roundNumberTextObject.SetActive(false);
						_stageChangeMoment = true;

					}));
				}
				//////////////////囲うのはここまで

			}

		}

		

		private IEnumerator DelayMethod(float waitTime, Action action)
		{
			yield return new WaitForSeconds(waitTime);
			action();
		}
		

	}

}
