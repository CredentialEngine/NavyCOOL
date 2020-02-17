using System;
using System.Collections.Generic;
using System.Text;

using COOLTool.Models;
using SM=Solid.Models;
namespace COOL.Factories
{
	public class AssessmentManager
	{
		/// <summary>
		/// Add method to retrieve the required dataset
		/// </summary>
		/// <param name="pFilter"></param>
		/// <param name="pOrderBy"></param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="pTotalRows"></param>
		/// <returns></returns>
		public static List<Assessment> Search(string pFilter, string pOrderBy, int pageNumber, int pageSize, ref int pTotalRows)
		{

			var results = new List<Assessment>();

			var record = Get_AircraftDispatcherWrittenExam();
			results.Add( record );
			record = Get_AircraftDispatcherPracticalExam();
			results.Add( record );

			return results;
		}

		public static Assessment Get_AircraftDispatcherWrittenExam()
		{
			//if publishing this, use the following and then set thisCTID to actual value to prevent duplicates
			//string thisCTID = "ce-" + Guid.NewGuid().ToString().ToLower();
			string thisCTID = "ce-88758f57-01f0-4a3c-a7f1-d009dd8f3f48";
			var record = new Assessment()
			{
				CE_ID = 100, //TBD
				CA_AgencyID = 1,    //TBD owning agency
				CE_Title = "Aircraft Dispatcher Written Exam",
				CE_URL = "https://www.cool.navy.mil/usn/search/CERT_AIRDISP1628.htm",
				CTID = thisCTID,
				Addresses = new List<Address>()
				 {
					 new Address()
					 {
						 CA_StreetAddress1="123 Main str",
						 CA_City="Springfield",
						 CA_State="MO",
						 CA_Zip="93444",
						 CA_Country="USA"
					 }
				 }
			};
			record.Agency = OrganizationManager.Get_FAA();
			record.CE_Description = @"Regulations
- Subpart C of CFR
- Parts 1, 25, 61, 71, 91, 121, 139, and 175 of CFR
- 49 CFR part 830
Meteorology
- Basic Weather Studies
- Weather, Analysis, and Forecasts
- Weather Observations, Analysis, and Forecasts
Navigation
- Study of the Earth
- Chart Reading, Application, and Use
- National Airspace Plan
- Navigation Systems
- Airborne Navigation Instruments
- Instrument Approach Procedures
Aircraft
- Aircraft Flight Manual
- Systems Overview
- Minimum Equipment List/Configuration Deviation List (MEL/CDL) and Applications.
- Performance
Communications
- Regulatory requirements
- Communication Protocol
- Voice and Data Communications
- Notice to Airmen (NOTAMS)
- Aeronautical Publications
- Abnormal Procedures
Air Traffic Control
- Responsibilities
- Facilities and Equipment
- Airspace classification and route structure
- Flight Plans
- Separation Minimums
- Priority Handling
- Holding Procedures
- Traffic Management
Emergency and Abnormal Procedures
- Security measures on the ground
- Security measures in the air
- FAA responsibility and services
- Collection and dissemination of information on overdue or missing aircraft.
- Means of declaring an emergency
- Responsibility for declaring an emergency
- Required reporting of an emergency
- NTSB reporting requirements
Practical Dispatch Applications
- Human Factors
- Applied Dispatching";

			return record;
		}

		public static SM.AssessmentDTO Get_AircraftDispatcherWrittenExamasDTO()
		{
			Assessment asmt = Get_AircraftDispatcherWrittenExam();
			//
			var record = Get_AssessmentasDTO( asmt );

			return record;
		}
		public static Assessment Get_AircraftDispatcherPracticalExam()
		{
			//if publishing this, use the following and then set thisCTID to actual value to prevent duplicates
			string thisCTID = "ce-" + Guid.NewGuid().ToString().ToLower();
			//
			var record = new Assessment()
			{
				CE_ID = 101, //TBD
				CA_AgencyID = 1,    //TBD owning agency
				CE_Title = "Aircraft Dispatcher Practical Exam",
				CE_URL = "https://www.cool.navy.mil/usn/search/CERT_AIRDISP1628.htm",
				CTID = thisCTID,
				Addresses = new List<Address>()
				 {
					 new Address()
					 {
						 CA_StreetAddress1="123 Main str",
						 CA_City="Springfield",
						 CA_State="MO",
						 CA_Zip="93444",
						 CA_Country="USA"
					 }
				 }
			};
			record.Agency = OrganizationManager.Get_FAA();
			record.CE_Description = @"Flight Planning/Dispatch Release:
- Regulatory Requirements
- Meteorology
- Weather Observations, Analysis, and Forecasts
- Weather Related Hazards
- Aircraft Systems, Performance, and Limitations
- Navigation and Aircraft Navigation Systems
- Practical Dispatch Applications
- Manuals, Handbooks, and Other Written Guidance
Preflight, takeoff, and Departure:
- Air Traffic Control Procedures
- Airports, Crew, and Company Procedures
Inflight Procedures:
- Routing, Re-routing, and Flight Plan Filing
- En Route Communication Procedures and Requirements
Arrival, Approach, and Landing Procedures:
- ATC and Air Navigation Procedures
Post Flight Procedures:
- Communication Procedures and Requirements
- Trip Records
Abnormal and Emergency Procedures:
- Abnormal and Emergency Procedures";

			return record;
		}
		public static SM.AssessmentDTO Get_AircraftDispatcherPracticalExamasDTO()
		{
			Assessment asmt = Get_AircraftDispatcherPracticalExam();
			//
			var record = Get_AssessmentasDTO( asmt );

			return record;
		}
		public static Assessment Get_Sample1()
		{
			string thisCTID = "ce-0c07ce76-e285-4dad-0001-d5962e14ea88";

			//
			var record = new Assessment()
			{
				CE_ID = 1,
				CA_AgencyID = 1,    //owning agency
				CE_Title = "Some assessment 1",
				CE_URL = "http://example.com/assessment1",
				CE_Description = "A description for an assessment.",
				CTID = thisCTID,
				Addresses = new List<Address>()
				 {
					 new Address()
					 {
						 CA_StreetAddress1="123 Main str",
						 CA_City="Springfield",
						 CA_State="MO",
						 CA_Zip="93444",
						 CA_Country="USA"
					 }
				 }
			};
			

			return record;
		}

		public static SM.AssessmentDTO Get_Sample1asDTO()
		{
			Assessment asmt = Get_Sample1();
			//
			var record = Get_AssessmentasDTO( asmt );

			return record;
		}


		public static SM.AssessmentDTO Get_AssessmentasDTO(Assessment asmt)
		{
			//
			var record = new SM.AssessmentDTO()
			{
				CTID = asmt.CTID,
				Name = asmt.CE_Title,
				Description = asmt.CE_Description,
				SubjectWebpage = asmt.CE_URL,
				AvailableOnlineAt = asmt.CE_URL, //???

			};
			if ( asmt.Agency != null && asmt.Agency.CA_AgencyID > 0 )
			{
				//actually must have a CTID, since will need the OwnedByOrganizationCTID populated

				if ( !string.IsNullOrWhiteSpace( asmt.Agency.CTID ) )
				{
					record.OwnedBy = new List<SM.OrganizationReference>()
					{
						new SM.OrganizationReference()
						{
							CTID = asmt.Agency.CTID
						}
					};

					record.OwnedByOrganizationCTID = asmt.Agency.CTID;
				}
				else
				{
					record.OwnedBy = new List<SM.OrganizationReference>()
						{
							new SM.OrganizationReference()
							{
								Name = asmt.Agency.CA_AgencyName,
								SubjectWebpage = asmt.Agency.CA_AgencyHomePageURL,
								Description = asmt.Agency.Description,
								Type = "CredentialOrganization"
							}
						};
				}
			}

			return record;
		}
	}
}

