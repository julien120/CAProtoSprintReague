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

		public GameObject BulletsPrefab;
		private GameObject bullets;
		private float _spawnTimer;
		private float _enemyShootInterval = 3; //発射間隔
		private Vector3 _enemyBulletVector;
		private Vector3 _enemyBulletSpawnPosition;
		private Vector3 _enemyBulletForce;
		private float _enemyBulletSpeed = 10;
		private Vector3 END_ENEMY { get { return new Vector3(0, 0, -2); } }
		///
		public bool isShootingMoment;
		public bool isShootingSucess;
		public bool isDamageReceived;





		// Start is called before the first frame update
		void Start()
		{
			isShootingSucess = false;
			isDamageReceived = false;

			gameManager = this.GetComponent<GameManager>();

		}

		// Update is called once per frame
		void Update()
		{

			_spawnTimer += Time.deltaTime;

			if (_spawnTimer > _enemyShootInterval)
			{
				_spawnTimer = 0;
				_enemyBulletSpawnPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 40);
				bullets = Instantiate(BulletsPrefab, _enemyBulletSpawnPosition, Quaternion.identity);
				_enemyBulletVector = (END_ENEMY - _enemyBulletSpawnPosition).normalized;
				_enemyBulletForce = _enemyBulletVector * _enemyBulletSpeed;
			}

			if (bullets != null)
			{
				bullets.GetComponent<Rigidbody>().AddForce(_enemyBulletForce);

				if (bullets.GetComponent<Transform>().position.z < 0)
				{
					Destroy(bullets);
					isDamageReceived = true;
					gameManager.life -= 1;
					Debug.Log("くらった");
				}


			}

			if (Input.GetMouseButton(0))
			{
				Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
				Ray ray = Camera.main.ScreenPointToRay(center);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 10))
				{
					if (hit.collider.CompareTag("Bullet"))
					{
						//もしhitのタグが"Block"と一致していた場合．．．の処理内容
						Destroy(bullets);
						Debug.Log("破壊したよ");
						isShootingSucess = true;
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
	}
}