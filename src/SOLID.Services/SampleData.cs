using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solid.Models;
using OrgRef = RA.Models.Input.OrganizationReference;


namespace COOLTool.Services
{
    public class SampleData
    {
		public static string FAA_CTID = "ce-0c07ce76-e285-4dad-9b99-d5962e14ea88";
		// "EnvelopeUrl": "https://sandbox.credentialengineregistry.org/navy/envelopes/0d3f7be3-dd35-4e86-a732-857af4e48053",
		//"GraphUrl": "https://sandbox.credentialengineregistry.org/navy/graph/ce-0c07ce76-e285-4dad-9b99-d5962e14ea88",

		public static string GI_BILL = @"Reimbursement for exam fees has been approved for payment through the GI Bill. Click for external link to GI Bill licensing and certification information. Note: GI Bill approval data is updated quarterly. For the latest information, visit the WEAMS Licenses/Certifications Search page. Make sure to select 'Both' in the LAC Category Type drop-down before searching.";

		public static Agency Get_FAA()
		{
			Agency agency = new Agency();
			//
			agency.CTID = FAA_CTID;
			agency.CA_AgencyName = "Federal Aviation Administration (FAA)";
			agency.CA_AgencyStreetAddress1 = "800 Independence Avenue SW";
			agency.CA_AgencyCity = "Washington";
			agency.CA_AgencyState = "DC";
			agency.CA_AgencyZip = "20591";
			agency.CA_AgencyCountry = "USA";
			agency.CA_PhoneNumber = "(866) 835-5322";
			//note cool site has a link to a help page
			/// http://faa.custhelp.com/app/ask
			agency.CA_AgencyHomePageURL = "https://www.faa.gov/";
			//apparantly a description is available
			agency.Description = "Our continuing mission is to provide the safest, most efficient aerospace system in the world.";

			agency.AgentType.Add( "orgType:Government" );
			agency.AgentSectorType = "agentSector:Public";

			return agency;
		}

		public static CredentialDTO Get_AircraftDispatcher() 
		{
			var faa = Get_FAA();

			CredentialDTO c = new CredentialDTO();
			//same as on staging server
			c.CTID = "ce-c976a9e2-2910-4052-a8e9-93fc3eec37db";
			c.CE_CertTitle = "	Aircraft Dispatcher";
			c.CE_CertDescription = "Aircraft Dispatcher is an entry level certification and is required for all Aircraft Dispatchers to demonstrate and pass a written and oral exam to receive their license. This certificate also introduces the applicant to an array of crucial components within the FAA and airport operations. Most applicants will attend an Aircraft Dispatchers five (5) week course and take multiple field trips to local area airline operational control centers. Upon completing the course, the student will be an expert in reading and interpreting Meteorological Terminal Aviation Routine Weather Report (METAR), Terminal Area Forecast (TAF) forecasts, and many important aviation weather maps, which are used in daily airline Flight Dispatch offices. Both Domestic and Global Navigation Systems are covered thoroughly, as well as the North Atlantic Track System (NATS). NOTAMs and Pilot Reports are also covered during the course.";

			c.Agency = faa;
			c.CredentialType = "Certificate";
			c.HasGIBillReimbursement = true;
			c.IsInDemand = true;

			//temp use of orgRef - COOL uses resource_type - need to determing the expected format.
			c.AccreditedBy.Add( new OrgRef()
			{
				CTID = faa.CTID
			} );

			//Requirements - would convert to a condition profile
			c.Requirements = new Cred_Requirements()
			{
				Description = @"Option 1:
A total of at least 2 years’ experience in the 3 years before the date of application, in any one or in any combination of the following areas:

In military aircraft operations as a—
- Pilot;
- Flight navigator; or
- Meteorologist.
In aircraft operations conducted under part 121 of this chapter as—
An assistant in dispatching air carrier aircraft, under the direct supervision of a dispatcher certificated under this subpart;
			A pilot;
			A flight engineer; or
			A meteorologist.
In aircraft operations as—
An Air Traffic Controller; or
A Flight Service Specialist.
In aircraft operations, performing other duties that the Administrator finds provide equivalent experience.
Option 2: Applicant must ra statement of graduation issued or revalidated, showing that the person has successfully completed an approved aircraft dispatcher course in accordance with Federal Aviation Regulation 65.70( b ).",
				EligibilityRequirements = new List<string>() { @"Applicants must meet requirements listed under Title 14 - Chapter I - Subchapter D - Part 65 - Subpart C—Aircraft Dispatchers." }

		};

			ConditionProfile requires = new ConditionProfile();
			//
			requires.Name = "Training and/Experience Requirements";
			requires.Description = c.Requirements.Description;

			//what to do with elibibility
			//string eligibilty = @"Applicants must meet requirements listed under Title 14 - Chapter I - Subchapter D - Part 65 - Subpart C—Aircraft Dispatchers.";
			//the latter has a URL. This could be the condition subject webpage
			requires.SubjectWebpage = "https://www.ecfr.gov/cgi-bin/text-idx?c=ecfr;sid=4128757e254de87854acaaa4090010b9;rgn=div5;view=text;node=14%3A2.0.1.1.4;idno=14;cc=ecfr#se14.2.65_151";
			//what to do with other? Just append to description
			string otherDescription = @"The Aircraft Dispatcher credential has the following other requirements:
To be eligible for an aircraft dispatcher certificate, a person must—

Be at least 23 years of age;
Be able to read, speak, write, and understand the English language;
Pass the required knowledge test;
Pass the required practical test;
Meet the experience or training requirements.
Note: To be eligible to take the aircraft dispatcher knowledge test, a person must be at least 21 years of age.";


			requires.YearsOfExperience = 2;
			requires.Condition = new List<string>() { "Education: High School Diploma/GED", "Fee", "Other", "Written Exam", "Practical Exam" };
			requires.Condition.Add( c.Requirements.EligibilityRequirements[0] );


			//what to do for assessment info?
			//written exam and practical exam have lists that could be competencies. However these would be better published separately.
			requires.TargetAssessment.Add( new RA.Models.Input.EntityReference()
			{
				Name = "Written Exam",
				Description = @"Regulations
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
- Applied Dispatching",
				SubjectWebpage = "https://www.cool.navy.mil/usn/search/CERT_AIRDISP1628.htm"//this required, so=???
			} );
			requires.TargetAssessment.Add( new RA.Models.Input.EntityReference()
			{
				Name = "Practical Exam",
				Description = @"Flight Planning/Dispatch Release:
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
- Abnormal and Emergency Procedures",
				SubjectWebpage = "https://www.cool.navy.mil/usn/search/CERT_AIRDISP1628.htm"//this required, so=???
			} );
			//what to do with exam prep and testing info. The entity reference doesn't have extended properties, but could more be added?

			c.Requires.Add( requires );

			return c;
		} 
    }
}
