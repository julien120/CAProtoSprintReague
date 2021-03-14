using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button howToPlay;
        [SerializeField] private Button signUp;
        [SerializeField] private Button login;

        private AudioSource audioSound;

        /// <summary>
        /// Titleでは、GameManagerにUIの処理を書いてます。
        /// </summary>
        void Start()
        {
            audioSound = GetComponent<AudioSource>();
            signUp.onClick.AddListener(LoadSignUp);
            login.onClick.AddListener(LoadSignIn);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void LoadSignUp()
        {
            audioSound.PlayOneShot(audioSound.clip);
            SceneController.Instance.LoadSignUpScene();
        }

        private void LoadSignIn()
	    {
            audioSound.PlayOneShot(audioSound.clip);
            SceneController.Instance.LoadLoginScene();
        }

        public void ClickSound()
        {
            audioSound.PlayOneShot(audioSound.clip);

        }


    }
}