using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Title_afterLogin
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button buttonStartGame;

        [SerializeField] private Button buttonRanking;

        // Start is called before the first frame update
        void Start()
        {
            buttonStartGame.onClick.AddListener(LoadInGameScene);
            buttonRanking.onClick.AddListener(LoadRankingScene);
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void LoadInGameScene()
		{
            SceneController.Instance.LoadInGameScene();
		}

        private void LoadRankingScene()
        {
            SceneController.Instance.LoadRankingScene();
        }

        /// <summary>
		/// シーン移動しないならDotweenかSetActiveで
		/// </summary>
        private void LoadSettingScene()
        {
            //SceneController.Instance.LoadInGameScene();
        }
    }
}