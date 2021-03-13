using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneName;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;

    public static SceneController Instance
    {

        get
        {
            if (instance == null)
            {

                GameObject single = new GameObject();
                //instanceに格納されてる値を管理する
                instance = single.AddComponent<SceneController>();
                //scene跨いでもインスタンスが残るのでnull処理に行かない
                DontDestroyOnLoad(instance);

            }
            return instance;

        }
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene(SceneName.SceneName.ResultScene);
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene(SceneName.SceneName.InGameScene);
    }

    public void LoadRankingScene()
    {
        SceneManager.LoadScene(SceneName.SceneName.RankingScene);
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(SceneName.SceneName.TitleScene);
    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene(SceneName.SceneName.LoginScene);
    }

    public void LoadSignUpScene()
    {
        SceneManager.LoadScene(SceneName.SceneName.SignUpScene);
    }
}
