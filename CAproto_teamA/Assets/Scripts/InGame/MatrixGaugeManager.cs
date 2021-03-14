using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{

	public class MatrixGaugeManager : MonoBehaviour
	{
		private GameObject _gameManager;
		private GameManager _gameManagerScript;

		[SerializeField ]private Text _matrixGauge;
		[SerializeField] public const float MATRIX_TIME = 15;
		[SerializeField] private int _matrixRecovery = 3;
		public float MatrixTime;
		public float remainingMatrixGuage;

		private GameObject image;

		public bool isRecovery;
		// Start is called before the first frame update
		void Start()
		{
			_gameManager = GameObject.Find("GameManager");
			_gameManagerScript = _gameManager.GetComponent<GameManager>();
			
			_matrixGauge.text = "";
			MatrixTime = 0;
			remainingMatrixGuage = 0;

			image = GameObject.Find("MatrixGuage");

			isRecovery = false;
		}

		// Update is called once per frame
		void Update()
		{
			_matrixGauge.text = remainingMatrixGuage.ToString("F2");
			if (_gameManagerScript.isPressSpaceKey && _gameManagerScript.isMatrixAvailable)
			{
				MatrixTime -= Time.deltaTime;
				if (MatrixTime < 0) MatrixTime = 0;
				remainingMatrixGuage=Mathf.Lerp(0, 100, MatrixTime / MATRIX_TIME);
			}
			if (isRecovery)
			{
				MatrixTime += _matrixRecovery;
				if (MatrixTime > MATRIX_TIME)
				{
					MatrixTime = MATRIX_TIME;
				}
				remainingMatrixGuage = Mathf.Lerp(0, 100, MatrixTime / MATRIX_TIME);
				isRecovery = false;
			}
			_gameManagerScript.isMatrixAvailable = true ? MatrixTime > 0 : false;
			image.GetComponent<Image>().fillAmount = remainingMatrixGuage / 100;
		}
	}
}

