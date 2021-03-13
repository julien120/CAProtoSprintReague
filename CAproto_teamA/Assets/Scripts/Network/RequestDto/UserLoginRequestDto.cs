using System;

namespace ProjectConnect.Network.RequestDto
{
	[Serializable]
	public class UserLoginRequestDto : DtoBase
	{
		public string userId;
		public string password;
	}
}
