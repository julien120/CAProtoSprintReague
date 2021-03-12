using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
	public class GameManager : MonoBehaviour
	{

		[SerializeField] private float _timeSpeedMultiplier = 0.1f;
		[SerializeField] public int life = 3;

		public float currentTime;
		public bool isPressSpaceKey;

		private float _currentSecond;
		private int _currentMinute;
		private bool _isGameOver;

		private Text _timePrint;
		private string _second;

		void Start()
		{
			_currentSecond = 0f;
			_currentMinute = 0;
			isPressSpaceKey = false;
			_timePrint = GameObject.Find("TimerText").GetComponent<Text>();
		}


		void Update()
		{

			if (_isGameOver)
			{
				//GameOver処理
				Debug.Log("lose");
				_isGameOver = false;
			}
			else
			{
				if (Input.GetKey(KeyCode.Space))
				{
					isPressSpaceKey = true;
				}
				else
				{
					isPressSpaceKey = false;

				}

				if (isPressSpaceKey)
				{
					Debug.Log(isPressSpaceKey);
					//Matrix処理
					currentTime += Time.deltaTime * _timeSpeedMultiplier;
					_currentSecond += Time.deltaTime * _timeSpeedMultiplier;
				}
				else
				{
					currentTime += Time.deltaTime;
					_currentSecond += Time.deltaTime;
				}
				GetCurrentTimeForText();

			}

			if (life == 0)
			{
				_isGameOver = true;
			}
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
	}
}