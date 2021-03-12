using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace InGame
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;

		[SerializeField] private AudioClip _audioClipGun;
		[SerializeField] private AudioClip _audioClipDamaged;
		[SerializeField] private AudioClip _audioClipShot;

		[SerializeField, Range(0, 1)] private float _audioVolumeGun = 0.3f;
		[SerializeField, Range(0, 1)] private float _audioVolumeDamaged = 0.3f;
		[SerializeField, Range(0, 1)] private float _audioVolumeShot = 0.3f;

		private BulletsManager _bulletsManager;
		void Start()
		{
			_bulletsManager = this.GetComponent<BulletsManager>();
		}

		void Update()
		{
			if (_bulletsManager.isShootingMoment)
			{
				//打った音を出す
				_audioSource.volume = _audioVolumeGun;
				_audioSource.PlayOneShot(_audioClipGun);
				_bulletsManager.isShootingMoment = false;
			}

			if (_bulletsManager.isShootingSucess)
			{
				_audioSource.volume = _audioVolumeShot;
				_audioSource.PlayOneShot(_audioClipShot);
				_bulletsManager.isShootingSucess = false;
			}

			if (_bulletsManager.isDamageReceived)
			{
				//ダメージをくらった音を出す
				_audioSource.volume = _audioVolumeDamaged;
				_audioSource.PlayOneShot(_audioClipDamaged);
				_bulletsManager.isDamageReceived = false;
			}
		}
	}
}