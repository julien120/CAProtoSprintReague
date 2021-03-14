using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectConnect.Network.ResponseDto
{
	[Serializable]
	public class RankingResponseDto : DtoBase
	{
		public List<RankInfo> ranks;
	}

	[Serializable]
	public class RankInfo
	{
		public string userId;
		public string userName;
		public string rank;
		public string score;
    
    }
}
