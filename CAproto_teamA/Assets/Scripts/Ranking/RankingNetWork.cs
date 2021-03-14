using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectConnect.Network;
using ProjectConnect.Network.RequestDto;
using ProjectConnect.Network.ResponseDto;
using UnityEngine;
using UnityEngine.UI;

public class RankingNetWork : MonoBehaviour
{
	public Text[] userNameText = new Text[10];
	public Text[] scoreText = new Text[10];

	//チーム用のサーバー
	private const string ServerAddress = "http://52.198.128.17:8080/";

	//サンプル用のサーバー
	//private const string ServerAddress = "http://3.138.103.250:8080/";

	/// <summary>
	/// メソッドタイプ
	/// </summary>
	public enum MethodType
	{
		// Getメソッド系
		GetRanking
	}

	void Start()
	{
		for(int i=0;i<10;i++)
		{
			userNameText[i] = userNameText[i].GetComponent<Text>();
			scoreText[i] = scoreText[i].GetComponent<Text>();
		}
	}

	//OnClickで呼び出すメソッド
	public void StartCoroutine()
	{
		StartCoroutine(NetworkCoroutine());
	}

	/// <summary>
	/// 通信サンプル用のCoroutine
	/// </summary>
	private IEnumerator NetworkCoroutine()
	{
		// WebRequestクラスをインスタンス化
		var webRequest = new WebRequest();
		webRequest.SetToken(TokenManager.token);

		// サーバーアドレスを設定する
		webRequest.SetServerAddress(ServerAddress);

		// ユーザー情報取得
		yield return GetRanking(webRequest);

	}



	/// <summary>
	/// ユーザ情報取得
	/// </summary>
	/// <param name="webRequest">Webリクエスト</param>
	/// <returns></returns>
	private IEnumerator GetRanking(WebRequest webRequest)
	{
		
		

		// ユーザー情報取得リクエストを投げる
		// 成功時: userIdの情報を出力する
		// 失敗時: エラーの内容をDebug.LogErrorで出力する
		yield return webRequest.Get<RankingResponseDto>(GetMethod(MethodType.GetRanking), rankingResponseDto =>
		{
			Debug.Log(rankingResponseDto.ranks[0].score);

			for(int i=0;i<rankingResponseDto.ranks.Count;i++)
			{
				userNameText[i].text = rankingResponseDto.ranks[i].userName;
				scoreText[i].text = rankingResponseDto.ranks[i].score;
			}

		}, Debug.LogError);

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
			// ユーザ情報取得用HTTPメソッド
			case MethodType.GetRanking:
				return "/ranking/list";
		}

		return null;
	}

}
