using control_inventario_function.SoporteDto;
using control_inventario_function.SoporteUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace control_inventario_function.Soporte
{
    public class ExecutorFunctions: IExecutorFunctions
    {
        public async Task<ActionResult> ExecuteFunctions(Func<Task<ActionResult>> action, ILogger logger)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                string mensaje = "";
                string trace = "";
                var isCusTomException = false;
                ManagedException(e, ref mensaje, ref trace, ref isCusTomException);
                logger.LogError(e.Message);
                logger.LogError(e.StackTrace);
                var respone = new Response
                {
                    Message = isCusTomException ? mensaje : Mensajes.algoSalioMal,
                    Trace = trace,
                    Status = false,
                    Data = ""
                };

                return new BadRequestObjectResult(respone);
            }
        }

        private static void ManagedException(Exception e, ref string mensaje, ref string trace, ref bool isCusTomException)
        {
            if (e is AggregateException)
            {
                var aggregateException = e as AggregateException;
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    if (ex is CustomException)
                    {
                        isCusTomException = ex is CustomException;
                    }
                    mensaje = mensaje + " " + ex?.Message + " " + ex?.InnerException + " ";
                    trace = trace + e?.StackTrace;
                }
            }
            else
            {
                if (e is CustomException)
                {
                    isCusTomException = e is CustomException;
                }
                mensaje = e?.Message + " " + e?.InnerException + " ";
                trace = trace + e?.StackTrace;
            }
        }

    }
}

