using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
	public class AudioManagerForEnemy : MonoBehaviour
	{

		private AudioSource _audioSource;
		private BulletsManager _bulletsManager;
		public AudioClip clip;


		void Start()
		{
			_audioSource = this.GetComponent<AudioSource>();
			_bulletsManager = GameObject.Find("GameManager").GetComponent<BulletsManager>();
		}

		// Update is called once per frame
		void Update()
		{
			Debug.Log(_bulletsManager.isShootingSucess);
			if (_bulletsManager.isShootingSucess)
			{
				Destroy(gameObject);
				//弾が当たった音を出す
				AudioSource.PlayClipAtPoint(clip, transform.position);
				//_audioSource.Play();
				_bulletsManager.isShootingSucess = false;
			}
		}
	}
}