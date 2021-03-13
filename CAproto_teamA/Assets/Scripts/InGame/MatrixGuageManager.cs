namespace InGame
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class MatricksGageManager : MonoBehaviour
	{
		protected bool isPressSpaceKey;
		private const float MATRIX_TIME = 20;
		private float _MatrixTime;
		public float _remainingMatrixGuage;
		// Start is called before the first frame update
		void Start()
		{
			_MatrixTime = MATRIX_TIME;
		}

		// Update is called once per frame
		void Update()
		{

			if (isPressSpaceKey)
			{
				_MatrixTime -= Time.deltaTime;
				_remainingMatrixGuage=Mathf.Lerp(0, 100, _MatrixTime / MATRIX_TIME);
				Debug.Log(_remainingMatrixGuage);

			}
		}
	}
}

