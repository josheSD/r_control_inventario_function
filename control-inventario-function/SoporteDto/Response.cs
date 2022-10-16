using control_inventario_function.SoporteUtil;
using Microsoft.AspNetCore.Mvc;

namespace control_inventario_function.SoporteDto
{
    public  class Response<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ActionResult Ok(T dato, string mensaje)
        {
            this.Status = true;
            this.Message = mensaje;
            this.Data = dato;
            return new OkObjectResult(this);
        }

        public ActionResult Accepted(T dato, string mensaje)
        {
            this.Status = true;
            this.Message = mensaje;
            this.Data = dato;
            return new AcceptedResult("Default", this);
        }

        public ActionResult Created(T dato, string mensaje)
        {
            this.Status = true;
            this.Message = mensaje;
            this.Data = dato;
            return new CreatedResult("Default", this);
        }
    }

    public class Response
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
        public dynamic Data { get; set; }

        public ActionResult Ok()
        {
            this.Message = Mensajes.correcto;
            return new OkObjectResult(this);
        }
        public ActionResult Accepted()
        {
            this.Message = Mensajes.correcto;
            return new AcceptedResult("Default", this);
        }

        public ActionResult Created()
        {
            this.Message = Mensajes.correcto;
            return new CreatedResult("Default", this);
        }
    }
}
