using System.Collections;
using ProjectConnect.Network;
using ProjectConnect.Network.RequestDto;
using ProjectConnect.Network.ResponseDto;
using UnityEngine;


public class NetworkSample : MonoBehaviour
{
    /// <summary>
    /// サーバの接続先アドレスを設定
    /// TODO: ここに指定したアドレスを入力してください
    ///       インターンでの開発では、バックエンドメンバーが用意したアドレスを設定します。
    ///       このサンプル内でもやっているWebRequest.SetServerAddress()への設定を忘れないようにしてください。
    /// </summary>
    private const string ServerAddress = "http://3.138.103.250:8080/";
    
    /// <summary>
    /// メソッドタイプ
    /// </summary>
    public enum MethodType
    {
        // Postメソッド系
        PostUserCreate,
        PostUserUpdate,
        
        // Getメソッド系
        GetUser
    }
    
    void Start()
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
        yield return CreateUser(webRequest);

        // ユーザー情報取得
        yield return GetUserInfo(webRequest);

        // ユーザ情報更新
        yield return UpdateUser(webRequest);

        // ユーザー情報取得
        // ※名前が更新されてることを確認する
        yield return GetUserInfo(webRequest);
    }


    /// <summary>
    /// ユーザデータの作成
    /// </summary>
    /// <param name="webRequest">Webリクエスト</param>
    /// <returns></returns>
    private IEnumerator CreateUser(WebRequest webRequest)
    {
		// TODO: 【事前課題】ユーザ作成用Dtoクラスを作成し、トークンを取得してください。
		//       レギュレーション①: HTTPメソッドは "/user/create"
		//       レギュレーション②: リクエストデータは "name" の１つのみ
		//       レギュレーション③: レスポンスデータは "token" の１つのみ

		// ユーザー作成時の情報
		var userCreateRequestDto = new UserCreateRequestDto()
		{
			name = "テストユーザー"
		};

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
			},                                             
            Debug.LogError,                                                                 //通信失敗時の処理
            false                                                                            //トークンを使用するか
            );


		// トークンの設定
		webRequest.SetToken(token);
		

	}

	/// <summary>
	/// ユーザ情報更新
	/// </summary>
	/// <param name="webRequest"></param>
	/// <returns></returns>
	private IEnumerator UpdateUser(WebRequest webRequest)
    {
		// ユーザー更新時の情報
		var userUpdateRequestDto = new UserUpdateRequestDto()
        {
            name = "名前変更後済み"
        };

        // ユーザー更新リクエストを投げる
        // 成功時: 「ユーザー更新完了」と出力する
        // 失敗時: エラーの内容をDebug.LogErrorで出力する
        yield return webRequest.Post<UserUpdateRequestDto, NoneResponseDto>(GetMethod(MethodType.PostUserUpdate), userUpdateRequestDto,
            x => Debug.Log("ユーザー更新完了"), Debug.LogError, true);
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
            Debug.Log("id:" + userGetResponseDto.id);
            Debug.Log("coin:" + userGetResponseDto.coin);
            Debug.Log("highScore:" + userGetResponseDto.highScore);
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

            // ユーザ情報更新用HTTPメソッド
            case MethodType.PostUserUpdate:
                return "/user/update";
            
            // ユーザ情報取得用HTTPメソッド
            case MethodType.GetUser:
                return "/user/get";
        }

        return null;
    }

}



