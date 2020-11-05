using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Services.Commands;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Services.Queries;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Events;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Controllers
{
    [ApiController]
    [Route("api/region-northwind")]
    [Produces("application/json")]
    public class RegionNorthwindEventController : CustomControllerBase
    {
        private IIncludeRegionNorthwindEventCommand _includeRegionNorthwindEventCommand;
        private IGetRegionNorthwindEventQuery _getRegionNorthwindEventQuery;



        public RegionNorthwindEventController(
            IIncludeRegionNorthwindEventCommand includeRegionNorthwindEventCommand,
            IGetRegionNorthwindEventQuery getRegionNorthwindEventQuery)
        {
            _includeRegionNorthwindEventCommand = includeRegionNorthwindEventCommand;
            _getRegionNorthwindEventQuery = getRegionNorthwindEventQuery;
        }



        /// <summary>
        /// Inclui uma mensagem na fila de Regions do Northwind.
        /// </summary>
        /// <param name="request">Objeto com os dados da Region que será enviada para a fila.</param>
        /// <remarks>
        /// Exemplo:
        /// {
        ///     "TransactionId": "5",
        ///     "RegionDescription": "Minas Gerais"
        /// }
        /// </remarks>
        /// <returns>HTTP Status Code correspondente à conclusão da execução.</returns>
        /// <response code="201">Item enviado com sucesso para a fila.</response>
        /// <response code="400">Erro na regra de negócio.</response>
        /// <response code="500">Erro não tratado na regra de negócio.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<HttpResponseMessage> IncludeNewRegionAsync(IncludeRegionNorthwindEvent request)
        {
            try
            {
                _includeRegionNorthwindEventCommand.Handle(request);
                return CustomResponseCreated();
            }
            catch (ApplicationException customException)
            {
                var exceptionDetails = customException.InnerException == null
                  ? string.Empty
                  : $" - {customException.InnerException.Message}";

                var exceptionMessage = string.Concat(customException.Message, exceptionDetails);

                return CustomResponseBadRequest(exceptionMessage);
            }
            catch (Exception genericException)
            {
                var exceptionDetails = genericException.InnerException == null
                  ? string.Empty
                  : $" - {genericException.InnerException.Message}";

                var exceptionMessage = string.Concat(genericException.Message, exceptionDetails);

                return CustomResponseInternalServerError(exceptionMessage);
            }
        }

        /// <summary>
        /// Recupera uma mensagem da Fila de Regions do Northwind.
        /// </summary>
        /// <returns>Mensagem mais "antiga" da fila.</returns>
        /// <response code="200">Item recuperado com sucesso.</response>
        /// <response code="400">Erro na regra de negócio.</response>
        /// <response code="404">Item não encontrado.</response>
        /// <response code="500">Erro não tratado na regra de negócio.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<HttpResponseMessage> GetOldestRegionAsync()
        {
            try
            {
                var retrievedMessage = _getRegionNorthwindEventQuery.Handle();
                if (!string.IsNullOrWhiteSpace(retrievedMessage))
                {
                    return CustomResponseOk(retrievedMessage);
                }
                else
                {
                    return CustomResponseNotFound();
                }
            }
            catch (ApplicationException customException)
            {
                var exceptionDetails = customException.InnerException == null
                  ? string.Empty
                  : $" - {customException.InnerException.Message}";

                var exceptionMessage = string.Concat(customException.Message, exceptionDetails);

                return CustomResponseBadRequest(exceptionMessage);
            }
            catch (Exception genericException)
            {
                var exceptionDetails = genericException.InnerException == null
                  ? string.Empty
                  : $" - {genericException.InnerException.Message}";

                var exceptionMessage = string.Concat(genericException.Message, exceptionDetails);

                return CustomResponseInternalServerError(exceptionMessage);
            }
        }
    }
}
