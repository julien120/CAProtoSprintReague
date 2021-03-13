using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
	public class BulletsManager : MonoBehaviour
	{
		public float totalPoint = 0;
		private GameManager gameManager;
		private MatrixObjectManager matrixObjectManager;
		private StageManager stagemanager;
		public GameObject BulletsPrefab;
		public GameObject cameraObject;
		public GameObject pointTextObject;
		public GameObject pointMultipleTextObject;

		private Text _pointText;
		private Text _pointMultipleText;

		private GameObject bullet;
		//private List<GameObject> bullets;
		private GameObject[] bullets;
		private float _spawnTimer;
		[SerializeField] private float _enemyShootInterval = 2; //発射間隔
		//[SerializeField] private float _intervalRangeOfReduction = 0.5f;
		[SerializeField] private float _speedRangeOfReduction;
		[SerializeField] private int _eachPoint = 100;
		private Vector3 _enemyBulletVector;
		private Vector3 _enemyBulletSpawnPosition;
		private Vector3 _enemyBulletForce;
		private Transform _cameraPosition;
		[SerializeField] private float _enemyBulletSpeed = 2;

		private bool _isDestroy;
		//private bool _isChangeSpeed;

		[SerializeField, Range(0, 10)] private float _enemyRange = 5f;

		private Vector3 END_ENEMY { get { return new Vector3(0, 0, -3); } }
		///
		public bool isShootingMoment;
		public bool isShootingSucess;
		public bool isDamageReceived;





		// Start is called before the first frame update
		void Start()
		{
			isShootingSucess = false;
			isDamageReceived = false;

			_isDestroy = false;
			//_isChangeSpeed = false;

			gameManager = this.GetComponent<GameManager>();
			stagemanager = this.GetComponent<StageManager>();
			matrixObjectManager = this.GetComponent<MatrixObjectManager>();

			_pointText =pointTextObject.GetComponent<Text>();
			_pointMultipleText = pointMultipleTextObject.GetComponent<Text>();
			pointMultipleTextObject.SetActive(false);
			_cameraPosition=cameraObject.GetComponent<Transform>();
			//bullets = new List<GameObject>();

			matrixObjectManager.matrixSpeed = _enemyBulletSpeed;
		}

		// Update is called once per frame
		void Update()
		{

			if (_isDestroy)
			{
				bullets = GameObject.FindGameObjectsWithTag("Bullet");
				_isDestroy = false;
			}

			if (stagemanager._stageChangeMoment)
			{
				DifficultLevel(stagemanager.currentStageNumber);
				stagemanager._stageChangeMoment = false;
			}


			//update関数のここから下をif(gameManager.isPlaying)で囲って下さい

			//Debug.Log(_spawnTimer);
			if (stagemanager.oneStageTakeTime - stagemanager.eachStageProgressTime > 2 && !stagemanager.isNowInterval)   //ステージ終了3秒前になったら弾生成させなくする
			{
				matrixObjectManager.isSpawn = false;
				matrixObjectManager.isExist = false;
				if(gameManager.isPressSpaceKey && gameManager.isMatrixAvailable)
				{
					_spawnTimer += Time.deltaTime * gameManager.timeSpeedMultiplier;
				}
				else
				{
					_spawnTimer += Time.deltaTime;
				}
			}
			else
			{
				matrixObjectManager.isSpawn = true;
			}
			
			if (_spawnTimer > _enemyShootInterval && gameManager.isPlaying && !gameManager.isGameOver)
			{
				_spawnTimer = 0;
				_enemyBulletSpawnPosition = new Vector3(Random.Range(-_enemyRange, _enemyRange), Random.Range(-_enemyRange, _enemyRange), 40);
				bullet = Instantiate(BulletsPrefab, _enemyBulletSpawnPosition, Quaternion.identity);
				bullets = GameObject.FindGameObjectsWithTag("Bullet");
				//_enemyBulletVector = new Vector3(0, 0, -1);
				_enemyBulletVector = (END_ENEMY - _enemyBulletSpawnPosition).normalized; //pattern 2
				if (gameManager.isPressSpaceKey && gameManager.isMatrixAvailable)
				{
					bullet.GetComponent<Rigidbody>().velocity = _enemyBulletVector * _enemyBulletSpeed * gameManager.timeSpeedMultiplier;
				}
				else
				{
					bullet.GetComponent<Rigidbody>().velocity = _enemyBulletVector * _enemyBulletSpeed;
				}
			}
			

			if (bullet != null)
			{
				foreach (GameObject bullet in bullets)
				{
					if (Input.GetKeyDown(KeyCode.Space) && gameManager.isMatrixAvailable) 
					{
						bullet.GetComponent<Rigidbody>().velocity *= gameManager.timeSpeedMultiplier;
					}
					if (Input.GetKeyUp(KeyCode.Space) && gameManager.isMatrixAvailable)
					{
						bullet.GetComponent<Rigidbody>().velocity /= gameManager.timeSpeedMultiplier;
					}
					if(!gameManager.isPlaying || gameManager.isGameOver)
					{
						bullet.GetComponent<Rigidbody>().velocity *= 0;
					}

					//Debug.Log(_enemyBulletForce);

					if (bullet.GetComponent<Transform>().position.z < 0)
					{
						Destroy(bullet);
						//bullets = GameObject.FindGameObjectsWithTag("Bullet");
						//Destroy(bullet);
						_isDestroy = true;
						isDamageReceived = true;
						gameManager.life -= 1;
					}
				}
				


			}

			if (Input.GetMouseButtonDown(0))
			{
				Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
				Ray ray = Camera.main.ScreenPointToRay(center);  //pattern 2
				//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 50)&& bullet != null)
				{
					
					foreach (GameObject bullet in bullets)
					{
							
						isShootingSucess = true;
						_isDestroy = true;
						Destroy(hit.collider.gameObject);

						if (bullet.GetComponent<Transform>().position.z-_cameraPosition.position.z<4 && bullet.GetComponent<Transform>().position.z - _cameraPosition.position.z > 0)
						{
							totalPoint += _eachPoint*4;
							pointMultipleTextObject.SetActive(true);
							_pointMultipleText.text = "× 4";
							_pointText.text = totalPoint.ToString();
							return;
						}						
						else
						{
							totalPoint += _eachPoint;
							_pointText.text = totalPoint.ToString();
						}
						
						
					}

				}

			}

			if (Input.GetMouseButtonDown(0))
			{
				isShootingMoment = true;
			}
			else
			{
				isShootingMoment = false;
			}


		}

		private void DifficultLevel(int _roundNumber)
		{
			matrixObjectManager.matrixSpeed += _speedRangeOfReduction;
			_enemyBulletSpeed += _speedRangeOfReduction;
			_enemyShootInterval -= 1 / (2 * _roundNumber);

			if(_enemyShootInterval<0.3)
			{
				_enemyShootInterval = 0.3f;
			}
			//_enemyShootInterval -= _intervalRangeOfReduction;			

		}
	}
}