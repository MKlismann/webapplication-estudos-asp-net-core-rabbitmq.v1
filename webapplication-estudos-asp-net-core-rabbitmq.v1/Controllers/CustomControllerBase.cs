using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected HttpResponseMessage CustomResponseOk(dynamic content = null)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            AddContent(content, ref response);

            return response;
        }

        protected HttpResponseMessage CustomResponseCreated()
        {
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        protected HttpResponseMessage CustomResponseNotFound(dynamic content = null)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            AddContent(content, ref response);

            return response;
        }

        protected HttpResponseMessage CustomResponseBadRequest(dynamic content = null)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            AddContent(content, ref response);

            return response;
        }

        protected HttpResponseMessage CustomResponseInternalServerError(dynamic content = null)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            AddContent(content, ref response);

            return response;
        }

        private void AddContent(string content, ref HttpResponseMessage response)
        {
            if (content != null)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(content));
            }
        }
    }
}
