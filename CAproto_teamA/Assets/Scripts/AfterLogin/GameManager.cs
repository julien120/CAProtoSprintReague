using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Title_afterLogin
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button buttonStartGame;
        [SerializeField] private Button buttonSetting;
        [SerializeField] private Button buttonRanking;
        [SerializeField] private GameObject settingUI;
        private AudioSource audioSound;
        // Start is called before the first frame update
        void Start()
        {
            audioSound = GetComponent<AudioSource>();
            buttonStartGame.onClick.AddListener(LoadInGameScene);
            buttonRanking.onClick.AddListener(LoadRankingScene);
            buttonSetting.onClick.AddListener(SetSettting);
        }

        private void LoadInGameScene()
		{
            audioSound.PlayOneShot(audioSound.clip);
            SceneController.Instance.LoadInGameScene();
		}

        private void LoadRankingScene()
        {
            audioSound.PlayOneShot(audioSound.clip);
            SceneController.Instance.LoadRankingScene();
        }

        /// <summary>
		/// シーン移動しないならDotweenかSetActiveで
		/// </summary>
        private void SetSettting()
        {
            settingUI.SetActive(true);
        }
    }
}