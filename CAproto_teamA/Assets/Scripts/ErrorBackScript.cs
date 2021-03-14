using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorBackScript : MonoBehaviour
{
    [SerializeField] private Button eroorButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        eroorButton.onClick.AddListener(LoadTitle);
    }

    private void LoadTitle()
    {
        SceneController.Instance.LoadTitleScene();

    }

}
