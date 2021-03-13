using System.Collections;
using ProjectConnect.Network;
using ProjectConnect.Network.RequestDto;
using ProjectConnect.Network.ResponseDto;
using UnityEngine;
using UnityEngine.UI;

public class LoginNetWork : MonoBehaviour
{

	public InputField userIdInputField;
	public InputField passwordInputField;
	public Text userIdTestText;

	//チーム用のサーバー
	private const string ServerAddress = "http://52.198.128.17:8080/";

	//サンプル用のサーバー
	//private const string ServerAddress = "http://3.138.103.250:8080/";

	/// <summary>
	/// メソッドタイプ
	/// </summary>
	public enum MethodType
	{
		// Postメソッド系
		PostUserLogin,

		// Getメソッド系
		GetUser
	}

	void Start()
	{
		//InputFieldのコンポーネントを呼び出す
		userIdInputField = userIdInputField.GetComponent<InputField>();
		passwordInputField = passwordInputField.GetComponent<InputField>();


		//WebGLテスト用のテキスト
		userIdTestText = userIdTestText.GetComponent<Text>();
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

		// サーバーアドレスを設定する
		webRequest.SetServerAddress(ServerAddress);

		// ユーザデータ作成
		yield return LoginUser(webRequest);

		// ユーザー情報取得
		yield return GetUserInfo(webRequest);

	}


	/// <summary>
	/// ユーザログイン
	/// </summary>
	/// <param name="webRequest">Webリクエスト</param>
	/// <returns></returns>
	private IEnumerator LoginUser(WebRequest webRequest)
	{

		// ユーザーログイン時の情報
		var userLoginRequestDto = new UserLoginRequestDto();
		userLoginRequestDto.userId = userIdInputField.text;
		userLoginRequestDto.password = passwordInputField.text;

		// トークンを受け取る変数
		// レスポンスで受け取ったトークンはここに設定してください。
		string token = null;

		Debug.Log("UserID:"+userLoginRequestDto.userId);
		Debug.Log("Password:"+userLoginRequestDto.password);

		// TODO: ユーザ作成の通信
		//       更新や情報取得など他の通信を参考に、ユーザ作成の通信処理を記載してください。
		yield return webRequest.Post<UserLoginRequestDto, UserLoginResponseDto>(          //Postリクエスト
			GetMethod(MethodType.PostUserLogin),                                           //リクエスト先のURL
			userLoginRequestDto,                                                           //Postするデータ（今回はuserId,password)
			userLoginResonseDto =>                                                                            //通信成功時の処理
			{
			
				token = userLoginResonseDto.token;
				Debug.Log("ログインPost完了!最高だぜ!!");
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
		// 成功時: userIdの情報を出力する
		// 失敗時: エラーの内容をDebug.LogErrorで出力する
		yield return webRequest.Get<UserGetResponseDto>(GetMethod(MethodType.GetUser), userGetResponseDto =>
		{
			userIdTestText.text = userGetResponseDto.userId;
			Debug.Log("userId:" + userGetResponseDto.userId);
		
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
			//case MethodType.PostUserCreate:
			//	return "/user/create";

			case MethodType.PostUserLogin:
				return "/user/login";

			// ユーザ情報取得用HTTPメソッド
			case MethodType.GetUser:
				return "/user/get";
		}

		return null;
	}

}
