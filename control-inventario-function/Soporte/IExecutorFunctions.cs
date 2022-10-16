using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace control_inventario_function.Soporte
{
    public interface IExecutorFunctions
    {
        Task<ActionResult> ExecuteFunctions(Func<Task<ActionResult>> action, ILogger logger);
    }
}
