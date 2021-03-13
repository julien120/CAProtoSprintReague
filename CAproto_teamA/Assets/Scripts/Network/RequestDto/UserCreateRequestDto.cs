using System;

namespace ProjectConnect.Network.RequestDto
{
	[Serializable]
	public class UserCreateRequestDto : DtoBase
	{
		public string name;
		public string password;
	}
}
