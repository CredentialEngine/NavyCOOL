using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavyCOOLPublish.Models
{
	public class RequestParameters
	{
		public RequestParameters(int entityTypeId)
		{
			EntityTypeId = entityTypeId;
			SetType();
			PageNumber = 1;
			PageSize = 50;
			Filter = "";
			OrderBy = "newest";
			PayloadPrefix = "";
			Community = "";
			//??we are already setting Type, what is the difference?
			switch( entityTypeId )
			{
				case 1:
					EntityType = "Credential"; break;
				case 2:
					EntityType = "Organization"; break;
				case 3:
					EntityType = "Assessment"; break;
				case 7:
					EntityType = "LearningOpp"; break;
				case 11:
					EntityType = "conceptScheme";
					break;
				case 17:
					EntityType = "competencyFramework";
					break;
				case 19:
					EntityType = "ConditionManifest"; break;
				case 20:
					EntityType = "CostManifest"; break;
				default:
					EntityType = string.Format( "Unknown EntityTypeId: {0}", entityTypeId );
					break;
			}
		}
		public string EntityType { get; set; }
		public int EntityTypeId { get; set; }
		public string Type { get; set; }
		public string Filter { get; set; }
		public string OrderBy { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

		public string PayloadPrefix { get; set; }
		public string Community { get; set; }
		public bool DoingGenerate { get; set; }
		public bool DoingPublish { get; set; }
		public bool DoingPublishSync { get; set; }
		public bool DoingDeleteBeforePublish { get; set; }
		public string SetAppKey(string keyType)
		{
			return Type + keyType;
		}
		public void SetType()
		{
			if( EntityTypeId == 1 )
				Type = "cred";
			else if( EntityTypeId == 2 )
				Type = "org";
			else if( EntityTypeId == 3 )
				Type = "asmt";
			else if( EntityTypeId == 7 )
				Type = "lopp";
			else if( EntityTypeId == 11 )
				Type = "conceptScheme";
			else if( EntityTypeId == 17 )
				Type = "competencyFramework";
			else if( EntityTypeId == 19 )
				Type = "condManifest";
			else if( EntityTypeId == 20 )
				Type = "costManifest";
			else
				Type = "unknown";
		}
	}
}
