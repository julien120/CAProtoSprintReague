using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
	public class GameManager : MonoBehaviour
	{

		[SerializeField] public float timeSpeedMultiplier = 0.1f;
		[SerializeField] public int life = 3;
		[SerializeField] private Text _cdtext;
		[SerializeField] private Text _gameOverText;

		MatrixGaugeManager matrixGaugeManager;

		private BulletsManager bulletsManager;
		public float currentTime;
		public static int totalScore;	           /////リザルト	
		public GameObject totalScoreTextObject;
		public GameObject okButton;
		private Text _totalScoreText;
		public bool isPressSpaceKey;

		private float _currentSecond;
		private int _currentMinute;
		public bool isGameOver;

		private Text _timePrint;
		private string _second;

		public bool isMatrixAvailable;
		public bool isPlaying;

		void Start()
		{
			bulletsManager = this.GetComponent<BulletsManager>();
			_totalScoreText = totalScoreTextObject.GetComponent<Text>();
			totalScoreTextObject.SetActive(false);
			matrixGaugeManager = this.GetComponent<MatrixGaugeManager>();
			_currentSecond = 0f;
			_currentMinute = 0;
			isPressSpaceKey = false;
			_timePrint = GameObject.Find("TimerText").GetComponent<Text>();

			okButton.SetActive(false);
			_gameOverText.text = "";
			isMatrixAvailable = true;
			isPlaying = false;
			StartCoroutine(CountdownC());
		}

		public static int GetTotalScore()
		{
			return totalScore;
		}

		private void Playing()
		{
			if (matrixGaugeManager.MatrixTime == 0)
			{
				isMatrixAvailable = false;
			}
			else if (Input.GetKey(KeyCode.Space))
			{
				isPressSpaceKey = true;
			}
			else
			{
				isPressSpaceKey = false;

			}
		}

		private void getCurrentTime(bool isPressSpaceKey, bool isMatrixAvailable)
		{
			if (isPressSpaceKey && isMatrixAvailable)
			{
				//Debug.Log(isPressSpaceKey);
				//Matrix処理

				currentTime += Time.deltaTime * timeSpeedMultiplier;
				_currentSecond += Time.deltaTime * timeSpeedMultiplier;
			}
			else
			{
				currentTime += Time.deltaTime;
				_currentSecond += Time.deltaTime;
			}
		}

		private void gameOver()
		{
			isGameOver = false;
			isPressSpaceKey = false;
			isMatrixAvailable = false;
			totalScoreTextObject.SetActive(true);
			totalScore = (int)(currentTime * bulletsManager.totalPoint);
			_totalScoreText.text = totalScore.ToString();
			StartCoroutine(WaitTwoSecs());
		}

		private void GetCurrentTimeForText()
		{
			if (_currentSecond > 60f)
			{
				_currentMinute++;
				_currentSecond -= 60f;
			}

			_second = ((int)_currentSecond).ToString("00");
			_second += (_currentSecond - (int)_currentSecond).ToString("F2").TrimStart('0');
			_timePrint.text = _currentMinute.ToString("00") + ":" + _second;
		}

		IEnumerator CountdownC()
		{
			_cdtext.text = "3";
			yield return new WaitForSeconds(0.93f);

			_cdtext.text = "2";
			yield return new WaitForSeconds(0.93f);

			_cdtext.text = "1";
			yield return new WaitForSeconds(0.93f);

			_cdtext.text = "START";
			isPlaying = true;
			yield return new WaitForSeconds(0.93f);

			_cdtext.text = "";
		}

		IEnumerator WaitTwoSecs()
		{
			_gameOverText.text = "GAME OVER";
			yield return new WaitForSeconds(2f);
			isPlaying = false;
			okButton.SetActive(true);
		}

		void Update()
		{
			if (isPlaying)
			{
				if (!isGameOver)
				{
					//Playing時の処理

					Playing();
					getCurrentTime(isPressSpaceKey, isMatrixAvailable);
				}
				else
				{
					//GameOver直後の処理

					gameOver();
				}
			}
			else
			{
				if (!isGameOver)
				{
					//Pause時の処理
				}
				else
				{
					//GameOver後の処理

					StartCoroutine(WaitTwoSecs());
					_gameOverText.text = "";
				}
			}

			GetCurrentTimeForText();

			if (life == 0)
			{
				isGameOver = true;
			}
		}
	}
}