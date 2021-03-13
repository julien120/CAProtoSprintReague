using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
	public class BulletsManager : MonoBehaviour
	{

		protected float currentTime; //スコアになるタイム
		protected bool isPressSpaceKey;

		private GameManager gameManager;
		private StageManager stagemanager;
		public GameObject BulletsPrefab;
		private GameObject bullet;
		//private List<GameObject> bullets;
		private GameObject[] bullets;
		private float _spawnTimer;
		[SerializeField] private float _enemyShootInterval = 4; //発射間隔
		[SerializeField] private float _intervalRangeOfReduction = 0.5f;
		[SerializeField] private float _speedRangeOfReduction;
		private Vector3 _enemyBulletVector;
		private Vector3 _enemyBulletSpawnPosition;
		private Vector3 _enemyBulletForce;
		[SerializeField] private float _enemyBulletSpeed = 2;

		private bool _isDestroy;
		private bool _isChangeSpeed;

		[SerializeField, Range(0, 10)] private float _enemyRange = 5f;

		private Vector3 END_ENEMY { get { return new Vector3(0, 0, -1); } }
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
			_isChangeSpeed = false;

			gameManager = this.GetComponent<GameManager>();
			stagemanager = this.GetComponent<StageManager>();


			//bullets = new List<GameObject>();
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


			//update関数のここから下をif(gameManager.isPlaying)で囲って下さい

			//Debug.Log(_spawnTimer);
			if (stagemanager.oneStageTakeTime - stagemanager.eachStageProgressTime > 3 && !stagemanager.isNowInterval)   //ステージ終了3秒前になったら弾生成させなくする
			{

				if (!gameManager.isPressSpaceKey)
				{
					_spawnTimer += Time.deltaTime;
				}
				else
				{
					_spawnTimer += Time.deltaTime * gameManager.timeSpeedMultiplier;
				}
			}
			
			if (_spawnTimer > _enemyShootInterval && gameManager.isPlaying && !gameManager.isGameOver)
			{
				_spawnTimer = 0;
				_enemyBulletSpawnPosition = new Vector3(Random.Range(-_enemyRange, _enemyRange), Random.Range(-_enemyRange, _enemyRange), 40);
				bullet = Instantiate(BulletsPrefab, _enemyBulletSpawnPosition, Quaternion.identity);
				bullets = GameObject.FindGameObjectsWithTag("Bullet");
				//_enemyBulletVector = new Vector3(0, 0, -1);
				_enemyBulletVector = (END_ENEMY - _enemyBulletSpawnPosition).normalized; //pattern 2
				if (gameManager.isPressSpaceKey)
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

			if (Input.GetMouseButton(0))
			{
				Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
				Ray ray = Camera.main.ScreenPointToRay(center);  //pattern 2
				//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 10))
				{
					if (bullet != null)
					{
						foreach (GameObject bullet in bullets)
						{
							//もしhitのタグが"Block"と一致していた場合．．．の処理内容
							isShootingSucess = true;
							_isDestroy = true;
							Destroy(hit.collider.gameObject);
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

			_enemyBulletSpeed += _speedRangeOfReduction;
			///Debug.Log("speed is" + _enemyBulletSpeed);
			_enemyShootInterval -= _intervalRangeOfReduction;
			///Debug.Log("interval is" + _enemyShootInterval);

		}
	}
}