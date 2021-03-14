using System.Collections;
using ProjectConnect.Network;
using ProjectConnect.Network.RequestDto;
using ProjectConnect.Network.ResponseDto;
using UnityEngine;
using UnityEngine.UI;

public class ResultNetWork : MonoBehaviour
{
	public Text scoreText;

	//サンプル用
	int userScore = 1000; 
	
	//GameManagerからtotalScoreを呼ぶ
	//int userScore=GameManager.GetTotalScore()

	//チーム用のサーバー
	private const string ServerAddress = "http://52.198.128.17:8080/";


	/// <summary>
	/// メソッドタイプ
	/// </summary>
	public enum MethodType
	{
		// Postメソッド系
		PostGameFinish

		
	}
	// Start is called before the first frame update
	void Start()
    {
		scoreText = scoreText.GetComponent<Text>();
		scoreText.text = userScore.ToString();                  //scoreTextに得点を表示
		StartCoroutine(NetworkCoroutine());
	}

	/// <summary>
	/// 通信サンプル用のCoroutine
	/// </summary>
		private IEnumerator NetworkCoroutine()
	{
		// WebRequestクラスをインスタンス化
		var webRequest = new WebRequest();

		// サーバーアドレスを設定する
		webRequest.SetServerAddress(ServerAddress);

		// ユーザログイン
		yield return ResultUser(webRequest);


	}

	/// <summary>
	/// ユーザログイン
	/// </summary>
	/// <param name="webRequest">Webリクエスト</param>
	/// <returns></returns>
	private IEnumerator ResultUser(WebRequest webRequest)
	{

		// ユーザーログイン時の情報
		var resultRequestDto = new ResultRequestDto();
		resultRequestDto.score = userScore;


		// TODO: ユーザ作成の通信
		//       更新や情報取得など他の通信を参考に、ユーザ作成の通信処理を記載してください。
		yield return webRequest.Post<ResultRequestDto, NoneResponseDto>(          //Postリクエスト
			GetMethod(MethodType.PostGameFinish),                                           //リクエスト先のURL
			resultRequestDto,                                                           //Postするデータ（今回はuserId,password)
			noneResponseDto =>                                                                            //通信成功時の処理
			{
				Debug.Log("YourScore:"+resultRequestDto.score);
			},
			Debug.LogError,                                                                 //通信失敗時の処理
			true                                                                            //トークンを使用する
			);
	}

	/// <summary>
	/// メソッドの取得
	/// </summary>
	/// <param name="type">メソッドタイプ</param>
	/// <returns></returns>
	private string GetMethod(MethodType type)
	{
		switch (type)
		{
			case MethodType.PostGameFinish:
				return "/game/finish";

		}

		return null;
	}
}
