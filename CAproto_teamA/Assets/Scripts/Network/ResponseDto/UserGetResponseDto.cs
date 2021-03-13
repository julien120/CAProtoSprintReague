using System;

namespace ProjectConnect.Network.ResponseDto
{
    [Serializable]
    public class UserGetResponseDto : DtoBase
    {

		/// <summary>
		/// ユーザ名
		/// </summary>
		/// <value>ユーザ名</value>
		public string name;

		/// <summary>
		/// ハイスコア
		/// </summary>
		/// <value>ハイスコア</value>
		public int highScore;


		//以下はNetworkSampleのみで使用
		/// <summary>
		/// id
		/// </summary>
		/// <value>id</value>
		public string id;

		/// <summary>
		/// 所持コイン
		/// </summary>
		/// <value>所持コイン</value>
		public int coin;
	}
}
