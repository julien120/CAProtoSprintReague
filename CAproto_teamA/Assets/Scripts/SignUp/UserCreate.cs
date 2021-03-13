using System.Collections;
using ProjectConnect.Network;
using ProjectConnect.Network.RequestDto;
using ProjectConnect.Network.ResponseDto;
using UnityEngine;
using UnityEngine.UI;

public class UserCreate : MonoBehaviour
{
	public InputField userNameInputField;
	public InputField userIdInputField;
	public InputField passwordInputField;
	

	//チーム用のサーバー
	
	//Unity上でテストする際のサーバーURL
	private const string ServerAddress = "http://52.198.128.17:8080/";

	//サンプル用のサーバー
	//private const string ServerAddress = "http://3.138.103.250:8080/";

	/// <summary>
	/// メソッドタイプ
	/// </summary>
	public enum MethodType
	{
		// Postメソッド系
		PostUserCreate,
		//PostUserUpdate,

		// Getメソッド系
		GetUser
	}

	void Start()
	{
		//InputFieldのコンポーネントを呼び出す
		userNameInputField = userNameInputField.GetComponent<InputField>();
		userIdInputField = userIdInputField.GetComponent<InputField>();
		passwordInputField = passwordInputField.GetComponent<InputField>();

	}

	//OnClickで呼び出すメソッド
	public void StartCoroutine()
	{
		StartCoroutine(NetworkCoroutine());
	}

	//シーン遷移のメソッド
	public void LoadLoginedTitleScene()
	{
		SceneController.Instance.LoadLoginedTitleScene();
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

		// ユーザデータ作成
		yield return CreateUser(webRequest);

		// ユーザー情報取得
		yield return GetUserInfo(webRequest);

		 LoadLoginedTitleScene();
	}


	/// <summary>
	/// ユーザデータの作成
	/// </summary>
	/// <param name="webRequest">Webリクエスト</param>
	/// <returns></returns>
	private IEnumerator CreateUser(WebRequest webRequest)
	{

		// ユーザー作成時の情報
		var userCreateRequestDto = new UserCreateRequestDto();
		userCreateRequestDto.name = userNameInputField.text;
		userCreateRequestDto.userId = userIdInputField.text;
		userCreateRequestDto.password = passwordInputField.text;

		// トークンを受け取る変数
		// レスポンスで受け取ったトークンはここに設定してください。
		string token = null;


		// TODO: ユーザ作成の通信
		//       更新や情報取得など他の通信を参考に、ユーザ作成の通信処理を記載してください。
		yield return webRequest.Post<UserCreateRequestDto, UserCreateResponseDto>(          //Postリクエスト
			GetMethod(MethodType.PostUserCreate),                                           //リクエスト先のURL
			userCreateRequestDto,                                                           //Postするデータ（今回はname)
			userCreateResonseDto =>                                                                            //通信成功時の処理
			{
				//Debug.Log("ユーザー登録完了");
				token = userCreateResonseDto.token;
				Debug.Log("UserID:"+userCreateRequestDto.userId); 
			},
			Debug.LogError,                                                                 //通信失敗時の処理
			false                                                                            //トークンを使用するか
			);


		// トークンの設定
		webRequest.SetToken(token);


	}


	/// <summary>
	/// ユーザ情報取得
	/// </summary>
	/// <param name="webRequest">Webリクエスト</param>
	/// <returns></returns>
	private IEnumerator GetUserInfo(WebRequest webRequest)
	{

		// ユーザー情報取得リクエストを投げる
		// 成功時: name,id,coin,highScoreの情報を出力する
		// 失敗時: エラーの内容をDebug.LogErrorで出力する
		yield return webRequest.Get<UserGetResponseDto>(GetMethod(MethodType.GetUser), userGetResponseDto =>
		{
			Debug.Log("name:" + userGetResponseDto.name);
			
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
			case MethodType.PostUserCreate:
				return "/user/create";

			// ユーザ情報取得用HTTPメソッド
			case MethodType.GetUser:
				return "/user/get";
		}

		return null;
	}

}
