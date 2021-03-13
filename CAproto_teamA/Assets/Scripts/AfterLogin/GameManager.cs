using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Title_afterLogin
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button button; 

        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(LoadInGameScene);
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void LoadInGameScene()
		{
            SceneController.Instance.LoadInGameScene();
		}
    }
}