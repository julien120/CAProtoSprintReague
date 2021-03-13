using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace InGame
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSourceSE;
		[SerializeField] private AudioSource _audioSourceBGM;

		[SerializeField] private AudioClip _audioClipGun;
		[SerializeField] private AudioClip _audioClipDamaged;
		[SerializeField] private AudioClip _audioClipShot;

		[SerializeField, Range(0, 1)] private float _audioVolumeGun = 0.3f;
		[SerializeField, Range(0, 1)] private float _audioVolumeDamaged = 0.6f;
		[SerializeField, Range(0, 1)] private float _audioVolumeShot = 0.6f;
		[SerializeField, Range(0, 1)] private float _audioVolumeBGM = 0.5f;

		[SerializeField, Range(0, 1)] private float _audioPitchBGM = 0.8f;

		private BulletsManager _bulletsManager;
		private GameManager _gameManager;
		void Start()
		{
			_bulletsManager = this.GetComponent<BulletsManager>();
			_gameManager = this.GetComponent<GameManager>();
		}

		void Update()
		{
			if(_gameManager.isPlaying && !_gameManager.isGameOver)
			if (_bulletsManager.isShootingMoment)
			{
				//ビーム音を出す
				_audioSourceSE.volume = _audioVolumeGun;
				_audioSourceSE.PlayOneShot(_audioClipGun);
				_bulletsManager.isShootingMoment = false;
			}

			if (_bulletsManager.isShootingSucess)
			{
				//敵の球を打った音を出す
				_audioSourceSE.volume = _audioVolumeShot;
				_audioSourceSE.PlayOneShot(_audioClipShot);
				_bulletsManager.isShootingSucess = false;
			}

			if (_bulletsManager.isDamageReceived)
			{
				//ダメージをくらった音を出す
				_audioSourceSE.volume = _audioVolumeDamaged;
				_audioSourceSE.PlayOneShot(_audioClipDamaged);
				_bulletsManager.isDamageReceived = false;
			}

			if (_gameManager.isPressSpaceKey && _gameManager.isMatrixAvailable)
			{
				//Matrix時の処理
				_audioSourceBGM.volume = _audioVolumeBGM;
				_audioSourceBGM.pitch = _audioPitchBGM;
			}
			else
			{
				_audioSourceBGM.volume = 1f;
				_audioSourceBGM.pitch = 1f;
			}
		}
	}
}