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
	public Text errorText;

	//チーム用のサーバー
	
	//Unity上でテストする際のサーバーURL
	private const string ServerAddress = "http://52.198.128.17:8080/";

	//サンプル用のサーバー
	//private const string ServerAddress = "http://3.138.103.250:8080/";

	private bool isCreateUserNetWorkError;
	private bool isGetInfoNetWorkError;

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

		errorText = errorText.GetComponent<Text>();

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

		if(isCreateUserNetWorkError == true)
		{
			//タイトルに戻るなどの操作
			errorText.gameObject.SetActive(true);
			errorText.text = "ユーザー登録に失敗しました.";
		}
		else
		{
			yield return GetUserInfo(webRequest);
			if(isGetInfoNetWorkError==true)
			{
				//なんかの処理
				errorText.gameObject.SetActive(true);
				errorText.text = "ユーザー登録に失敗しました.";
			}
			else
			{
				LoadLoginedTitleScene();
			}
		}

		// ユーザー情報取得
	
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
			onCreateUserError,                                                                 //通信失敗時の処理
			false                                                                            //トークンを使用するか
			);


		// トークンの設定
		webRequest.SetToken(token);
		TokenManager.token = token;


	}


	//エラー時の処理
	private void onCreateUserError(string message)
	{
		isCreateUserNetWorkError = true;
	}

	private void onGetUserInfoError(string message)
	{
		isGetInfoNetWorkError = true;
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
			
		}, onGetUserInfoError);

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
