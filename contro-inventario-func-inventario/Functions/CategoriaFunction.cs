using control_inventario_function.Soporte;
using control_inventario_function.SoporteDto;
using control_inventario_function.SoporteUtil;
using control_inventario_service_inventario.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using control_inventario_service_inventario.ServiceDto;

namespace contro_inventario_func_inventario.Functions
{
    public class CategoriaFunction
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IExecutorFunctions _executorFunctions;

        public CategoriaFunction(ICategoriaService categoriaService, IExecutorFunctions executorFunctions)
        {
            this._categoriaService = categoriaService;
            this._executorFunctions = executorFunctions;
        }

        [FunctionName("CategoriaListar")]
        [OpenApiOperation(operationId: "categoriaListar", tags: new[] { "Categoria" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> categoriaListar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categoria/listar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var lista = await _categoriaService.Lista();
                var response = new Response<List<CategoriaDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(lista, Mensajes.correcto);
            }, log);
        }

    }
}
