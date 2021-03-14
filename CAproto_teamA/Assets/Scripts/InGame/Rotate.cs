using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
	public class Rotate : MonoBehaviour
	{

		private GameObject _gameManager;
		private GameManager _gameManagerScript;

		private void Start()
		{
			_gameManager = GameObject.Find("GameManager");
			_gameManagerScript = _gameManager.GetComponent<GameManager>();
		}

		void Update()
		{
			if(_gameManagerScript.isMatrixAvailable && _gameManagerScript.isPressSpaceKey)
			{
				transform.Rotate(new Vector3(30, 45, 60) * Time.deltaTime * _gameManagerScript.timeSpeedMultiplier);
			}
			else
			{
				transform.Rotate(new Vector3(30, 45, 60) * Time.deltaTime);
			}
		}
	}
}