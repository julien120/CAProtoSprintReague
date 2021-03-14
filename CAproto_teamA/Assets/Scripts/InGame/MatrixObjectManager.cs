using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
	public class MatrixObjectManager : MonoBehaviour
	{
		public bool isShootingSucess;

		public GameObject matrixPrefab;
		private GameObject matrixObject;

		private GameManager _gameManager;
		private StageManager _stageManager;
		private MatrixGaugeManager _matrixGaugeManager;

		private float _spawnTimer;

		public bool isSpawn;
		public bool isExist;

		private Vector3 _enemyMatrixSpawnPosition;
		private Vector3 _enemyMatrixVector;
		private Vector3 END_MATRIX { get { return new Vector3(0, 0, -3); } }

		[SerializeField, Range(0, 10)] private float _matrixRange = 5f;
		[SerializeField] public float enemyShootInterval = 2; //発射間隔

		public float matrixSpeed;


		void Start()
		{
			_gameManager = this.GetComponent<GameManager>();
			_stageManager = this.GetComponent<StageManager>();
			_matrixGaugeManager = this.GetComponent<MatrixGaugeManager>();
			isShootingSucess = false;
			isSpawn = false;
			isExist = false;

		}

		void SpawnMatrix()
		{
			_enemyMatrixSpawnPosition = new Vector3(Random.Range(-_matrixRange, _matrixRange), Random.Range(-_matrixRange, _matrixRange), 40);
			matrixObject = Instantiate(matrixPrefab, _enemyMatrixSpawnPosition, Quaternion.identity);

			//_enemyMatrixSpawnPosition = new Vector3(Random.Range(-_matrixRange, _matrixRange), positionY, _positionZ);
			//matrixObject = Instantiate(matrixPrefab, _enemyMatrixSpawnPosition, Quaternion.identity);
			//rb = matrixObject.GetComponent<Rigidbody>();
			_enemyMatrixVector = (END_MATRIX - _enemyMatrixSpawnPosition).normalized; //pattern 2
			matrixObject.GetComponent<Rigidbody>().velocity = _enemyMatrixVector * matrixSpeed;
		}

		void Update()
		{
			if (_gameManager.isPlaying || !_gameManager.isGameOver)
			{
				if (isSpawn && !isExist)
				{
					{
						_spawnTimer += Time.deltaTime;
					}
					SpawnMatrix();
					isExist = true;
				}

				if (matrixObject != null)
				{
					if (matrixObject.GetComponent<Transform>().position.z < 0)
					{
						Destroy(matrixObject);
					}
				}

				if (Input.GetMouseButton(0))
				{
					Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
					Ray ray = Camera.main.ScreenPointToRay(center);  //pattern 2
																	 //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit, 50) && matrixObject != null)
					{
						isShootingSucess = true;

						if (hit.collider.gameObject.CompareTag("Matrix"))
						{
							_matrixGaugeManager.isRecovery = true;
							isShootingSucess = true;
							Destroy(hit.collider.gameObject);
						}
					}

				}
			}
		}
	}
}