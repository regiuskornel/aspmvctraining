using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleMVC.Controllers
{
    [Route("api/[controller]")]
    public class FormatterController : Controller
    {
        [HttpGet]
        [Consumes("application/xml")]
        [Produces("application/json")]
        public object Get([FromBody] FormatterInputModel model)
        {
            return new { id = 10, @class = "Business" };
        }
    }

    public class FormatterInputModel
    {
        public int CurrentClassId { get; set; }

        public Guid? PartnerId { get; set; }
    }


}
