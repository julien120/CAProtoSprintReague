using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{

	public class MatrixGaugeManager : MonoBehaviour
	{
		private GameObject gameManager;
		private GameManager gameManagerScript;

		[SerializeField ]private Text _matrixGauge;

		protected bool isPressSpaceKey;
		[SerializeField] private const float MATRIX_TIME = 20;
		public float _MatrixTime;
		public float _remainingMatrixGuage;
		// Start is called before the first frame update
		void Start()
		{
			gameManager = GameObject.Find("GameManager");
			gameManagerScript = gameManager.GetComponent<GameManager>();
			_matrixGauge.text = "";
			_MatrixTime = MATRIX_TIME;
			_remainingMatrixGuage = 100;
		}

		// Update is called once per frame
		void Update()
		{
			_matrixGauge.text = _remainingMatrixGuage.ToString("F2");
			if (gameManagerScript.isPressSpaceKey)
			{
				_MatrixTime -= Time.deltaTime;
				_remainingMatrixGuage=Mathf.Lerp(0, 100, _MatrixTime / MATRIX_TIME);

			}
		}
	}
}

