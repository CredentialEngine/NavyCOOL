using System;
using System.Collections.Generic;
using System.Web.Http;

using COOLTool.Services;
using COOLTool.Services.Models.Input;

using Utilities;

using EntityRequest = COOLTool.Services.Models.Input.RatingRequest;

namespace NavyCOOL.PublishingAPI.Controllers
{
    public class RatingController : ApiController
    {
        string thisClassName = "RatingController";
        string controller = "rating";
        RequestHelper helper = new RequestHelper();
        RatingServices mgr = new RatingServices();

        /// <summary>
        /// Publish an Rating to the Rating Engine Registry
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route( "rating/publish" )]
        public ApiPublishResponse Publish(EntityRequest request)
        {
            List<string> messages = new List<string>();
            var response = new ApiPublishResponse();
            string statusMessage = "";

            try
            {
                if ( request == null )
                {
                    response.Messages.Add( "Error - please provide a valid Rating request." );
                    return response;
                }

                LoggingHelper.DoTrace( 2, string.Format( "NavyCOOLPublishingAPI.{0}.Publish request. ctid: {1}", thisClassName, request.Rating.CTID ) );

                bool isTokenRequired = true;
                string apiToken = "";
                if ( HelperServices.IsAuthTokenValid( ref apiToken, ref statusMessage, isTokenRequired ) == false )
                {
                    response.Messages.Add( "Error - please provide a valid API Key: " + statusMessage );
                    return response;
                }
                helper.ApiKey = apiToken;
                helper.SerializedInput = HelperServices.LogInputFile( request, request.Rating.CTID, "Rating", "Publish", 5 );

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
        /// Delete request of an Rating by CTID and owning rating
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete, Route( "rating/delete" )]
        public ApiDeleteResponse Delete(DeleteRequest request)
        {
            bool isValid = true;
            List<string> messages = new List<string>();
            var response = new ApiDeleteResponse();

            try
            {
                if ( request == null || request.CTID == null )
                {
                    response.Messages.Add( "Error - please provide a valid Rating delete request." );
                    return response;
                }
                //RegistryServices cer = new RegistryServices( controller, "", request.CTID );
                ////TODO - should a delete be allowed if ratings exist
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

    }
}