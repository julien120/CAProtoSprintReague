using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Result
{
	public class GameManager : MonoBehaviour
	{
		public void LoadLoginedTitle()
		{
			SceneController.Instance.LoadLoginedTitleScene();
		}

		public void LoadInGame()
		{
			SceneController.Instance.LoadInGameScene();
		}
	}
}
