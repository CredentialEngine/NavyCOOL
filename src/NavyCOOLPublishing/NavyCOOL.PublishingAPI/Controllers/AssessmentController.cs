using System;
using System.Collections.Generic;
using System.Web.Http;

using COOLTool.Services;
using COOLTool.Services.Models.Input;

using Utilities;

using EntityRequest = COOLTool.Services.Models.Input.AssessmentRequest;

namespace NavyCOOL.PublishingAPI.Controllers
{
    public class AssessmentController : ApiController
    {
        string thisClassName = "AssessmentController";
        string controller = "assessment";
        RequestHelper helper = new RequestHelper();
        AssessmentServices mgr = new AssessmentServices();

        /// <summary>
        /// Publish an Assessment to the Assessment Engine Registry
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route( "assessment/publish" )]
        public ApiPublishResponse Publish(EntityRequest request)
        {
            List<string> messages = new List<string>();
            var response = new ApiPublishResponse();
            string statusMessage = "";

            try
            {
                if ( request == null )
                {
                    response.Messages.Add( "Error - please provide a valid Assessment request." );
                    return response;
                }

                LoggingHelper.DoTrace( 2, string.Format( "NavyCOOLPublishingAPI.{0}.Publish request. ctid: {1}", thisClassName, request.Assessment.Ctid ) );

                bool isTokenRequired = true;
                string apiToken = "";
                if ( HelperServices.IsAuthTokenValid( ref apiToken, ref statusMessage, isTokenRequired ) == false )
                {
                    response.Messages.Add( "Error - please provide a valid API Key: " + statusMessage );
                    return response;
                }
                helper.ApiKey = apiToken;
                helper.SerializedInput = HelperServices.LogInputFile( request, request.Assessment.Ctid, "Assessment", "Publish", 5 );

                mgr.Publish( request, helper );

                response.Payload = helper.Payload;
                response.Successful = helper.IsValidRequest;

                if ( helper.IsValidRequest )
                {
                    if ( helper.Messages.Count > 0 )
                        response.Messages = helper.GetAllMessages();
                    response.GraphUrl = helper.GraphUrl;
                    response.EnvelopeUrl = helper.EnvelopeUrl;
                }
                else
                {
                    response.Messages = helper.GetAllMessages();
                    response.Successful = false;
                }

            }
            catch ( Exception ex )
            {
                response.Messages.Add( ex.Message );
                response.Successful = false;
            }
            return response;
        } //
        /// <summary>
        /// Delete request of an Assessment by CTID and owning assessment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete, Route( "assessment/delete" )]
        public ApiDeleteResponse Delete(DeleteRequest request)
        {
            List<string> messages = new List<string>();
            var response = new ApiDeleteResponse();

            try
            {
                if ( request == null || request.CTID == null )
                {
                    response.Messages.Add( "Error - please provide a valid Assessment delete request." );
                    return response;
                }
                //RegistryServices cer = new RegistryServices( controller, "", request.CTID );
                ////TODO - should a delete be allowed if assessments exist
                //var isValid = cer.DeleteRequest( request, controller, ref messages );
                //if ( isValid )
                //{
                //    response.Successful = true;
                //}
                //else
                //{
                //    response.Messages.AddRange( messages );
                //    response.Successful = false;
                //}
            }
            catch ( Exception ex )
            {
                response.Messages.Add( ex.Message );
                response.Successful = false;
            }
            return response;


        } //

    }
}
