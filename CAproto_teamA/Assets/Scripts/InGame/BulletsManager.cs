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
		private GameObject[] lifeImages;

		[SerializeField] private LifeGauge lifeGauge;

		private float _spawnTimer;
		private float _enemyShootInterval = 2; //ηΊε°ιι
		//[SerializeField] private float _intervalRangeOfReduction = 0.5f;
		[SerializeField] private float _speedRangeOfReduction;
		[SerializeField] private int _eachPoint = 100;
		private Vector3 _enemyBulletVector;
		private Vector3 _enemyBulletSpawnPosition;
		private Vector3 _enemyBulletForce;
		private Transform _cameraPosition;
		private bool _isKeyDown;
		private float _duringTime;
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
			_isKeyDown = false;

			lifeGauge.SetLifeGauge(gameManager.life);
			_duringTime = 0.3f;

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
				DifficultLevel();
				stagemanager._stageChangeMoment = false;
			}


			//updateι’ζ°γ?γγγγδΈγif(gameManager.isPlaying)γ§ε²γ£γ¦δΈγγ

			//Debug.Log(_spawnTimer);
			if (stagemanager.oneStageTakeTime - stagemanager.eachStageProgressTime > 2 && !stagemanager.isNowInterval)   //γΉγγΌγΈη΅δΊ3η§εγ«γͺγ£γγεΌΎηζγγγͺγγγ
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
				Debug.Log("AAAAAAA"+_enemyShootInterval);
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
						_isKeyDown = true;
					}
					if (Input.GetKeyUp(KeyCode.Space) && gameManager.isMatrixAvailable)
					{
						bullet.GetComponent<Rigidbody>().velocity /= gameManager.timeSpeedMultiplier;
						_isKeyDown = false;
					}
					if(!gameManager.isPlaying || gameManager.isGameOver)
					{
						bullet.GetComponent<Rigidbody>().velocity *= 0;
					}

					if (_isKeyDown && !gameManager.isMatrixAvailable)
					{
						bullet.GetComponent<Rigidbody>().velocity /= gameManager.timeSpeedMultiplier;
						_isKeyDown = false;
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
						lifeGauge.DamageSetLifeGauge();
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
						if (hit.collider.gameObject.CompareTag("Bullet")){
							Destroy(hit.collider.gameObject);

						}

						if (bullet.GetComponent<Transform>().position.z-_cameraPosition.position.z<4 && bullet.GetComponent<Transform>().position.z - _cameraPosition.position.z > 0)
						{
							totalPoint += _eachPoint*4;
							pointMultipleTextObject.SetActive(true);
							_pointMultipleText.text = "400";
							_pointText.text = totalPoint.ToString();
							return;
						}						
						else
						{
							totalPoint += _eachPoint;
							_pointMultipleText.text = "100";
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

		private void DifficultLevel()
		{
			matrixObjectManager.matrixSpeed += _speedRangeOfReduction;
			_enemyBulletSpeed += _speedRangeOfReduction;
			_enemyShootInterval -= _duringTime;
			Debug.Log(_enemyShootInterval);
			if (_enemyShootInterval > 0.3f) _duringTime = 0.05f;
			if (_enemyShootInterval > 0.4f) _duringTime = 0.1f;
			if (_enemyShootInterval > 0.8f) _duringTime = 0.2f;
			if (_enemyShootInterval > 1.2f) _duringTime = 0.3f;
			//_enemyShootInterval -= _intervalRangeOfReduction;			

		}
	}
}