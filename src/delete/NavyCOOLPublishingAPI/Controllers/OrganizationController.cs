using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;

using COOLTool.Services.Models;
using COOLTool.Services.Models.Input;
using EntityRequest = COOLTool.Services.Models.Input.OrganizationRequest;
using COOLTool.Services;
using Utilities;

namespace NavyCOOLPublishingAPI.Controllers
{
    public class OrganizationController : ApiController
    {
        string thisClassName = "OrganizationController";
        string controller = "organization";
        RequestHelper helper = new RequestHelper();
        OrganizationServices mgr = new OrganizationServices();

        /// <summary>
        /// Publish an Organization to the Credential Engine Registry
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route( "organization/publish" )]
        public ApiPublishResponse Publish(EntityRequest request)
        {
            List<string> messages = new List<string>();
            var response = new ApiPublishResponse();
            string statusMessage = "";

            try
            {
                if ( request == null )
                {
                    response.Messages.Add( "Error - please provide a valid Organization request." );
                    return response;
                }

                LoggingHelper.DoTrace( 2, string.Format( "NavyCOOLPublishingAPI.{0}.Publish request. ctid: {1}", thisClassName, request.Agency.CTID ) );

                bool isTokenRequired = true;
                string apiToken = "";
                if ( HelperServices.IsAuthTokenValid( ref apiToken, ref statusMessage, isTokenRequired ) == false )
                {
                    response.Messages.Add( "Error - please provide a valid API Key: " + statusMessage );
                    return response;
                }
                helper.ApiKey = apiToken;
                helper.SerializedInput = HelperServices.LogInputFile( request, request.Agency.CTID, "Organization", "Publish", 5 );

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
        /// Delete request of an Organization by CTID and owning organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete, Route( "organization/delete" )]
        public ApiDeleteResponse Delete(DeleteRequest request)
        {
            bool isValid = true;
            List<string> messages = new List<string>();
            var response = new ApiDeleteResponse();

            try
            {
                if ( request == null || request.CTID == null )
                {
                    response.Messages.Add( "Error - please provide a valid Organization delete request." );
                    return response;
                }
                //RegistryServices cer = new RegistryServices( controller, "", request.CTID );
                ////TODO - should a delete be allowed if credentials exist
                //isValid = cer.DeleteRequest( request, controller, ref messages );
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


        [HttpPost, Route( "organization/register" )]
        public ApiPublishResponse Register(EntityRequest request)
        {
            var response = new ApiPublishResponse();

            return response;
        }
    }
}
