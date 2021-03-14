using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingButtonController : MonoBehaviour
{
    [SerializeField] private Button buttonOK;

    // Start is called before the first frame update
    void Start()
    {
        buttonOK.onClick.AddListener(LoadLoginedTitle);
    }

    private void LoadLoginedTitle()
	{
        SceneController.Instance.LoadLoginedTitleScene();
	}
}
