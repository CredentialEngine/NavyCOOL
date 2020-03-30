﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOLTool.Services.Models.Input
{
	public class DurationProfile
	{
		public DurationProfile()
		{
			MinimumDuration = new DurationItem();
			MaximumDuration = new DurationItem();
			ExactDuration = new DurationItem();
		}

		/// <summary>
		/// 1 - Exact Estimated Duration
		/// 2 - Range Estimated Duration
		/// 3 - Renewal frequency
		/// </summary>
		public int DurationProfileTypeId { get; set; }
		public DurationItem MinimumDuration { get; set; }
		public DurationItem MaximumDuration { get; set; }
		public DurationItem ExactDuration { get; set; }

		public int MinimumMinutes { get; set; }
		public string MinimumDurationISO8601 { get; set; }
		public int MaximumMinutes { get; set; }
		public string MaximumDurationISO8601 { get; set; }
		public string ExactDurationISO8601 { get; set; }
		public bool IsRange { get; set; }
	}
	//

	public class DurationItem
	{
		//public string DurationISO8601 { get; set; }
		public int Years { get; set; }
		public int Months { get; set; }
		public int Weeks { get; set; }
		public int Days { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public bool HasValue { get { return Years + Months + Weeks + Days + Hours + Minutes > 0; } }
		public bool HasWeeks { get { return Weeks > 0; } }

		public string Print()
		{
			var parts = new List<string>();
			if ( Years > 0 ) { parts.Add( Years + " year" + ( Years == 1 ? "" : "s" ) ); }
			if ( Months > 0 ) { parts.Add( Months + " month" + ( Months == 1 ? "" : "s" ) ); }
			if ( Weeks > 0 ) { parts.Add( Weeks + " week" + ( Weeks == 1 ? "" : "s" ) ); }
			if ( Days > 0 ) { parts.Add( Days + " day" + ( Days == 1 ? "" : "s" ) ); }
			if ( Hours > 0 ) { parts.Add( Hours + " hour" + ( Hours == 1 ? "" : "s" ) ); }

			if ( Minutes > 0 ) { parts.Add( Minutes + " minute" + ( Minutes == 1 ? "" : "s" ) ); }

			return string.Join( ", ", parts );
		}
		public int TotalMinutes()
		{
			var totalMinutes = 0;
			if ( Years > 0 ) totalMinutes += Years * 365 * 24 * 3600;

			if ( Months > 0 ) totalMinutes += Months * 30 * 24 * 3600;

			if ( Weeks > 0 ) totalMinutes += Weeks * 5 * 24 * 3600;

			if ( Days > 0 ) totalMinutes += Days * 24 * 3600;

			if ( Hours > 0 ) totalMinutes += Hours * 3600;

			if ( Minutes > 0 ) totalMinutes += Minutes;

			return totalMinutes;
		}
	}
}
