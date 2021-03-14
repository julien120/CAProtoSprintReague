using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
	[SerializeField] private Button OK;
	[SerializeField] private Button RETRY;
	
	// Start is called before the first frame update
	void Start()
    {
		OK.onClick.AddListener(LoadResultScene);
		RETRY.onClick.AddListener(LoadInGame);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LoadResultScene()
	{
		SceneController.Instance.LoadLoginedTitleScene();
	}

	private void LoadInGame()
	{
		SceneController.Instance.LoadInGameScene();
	}

}
