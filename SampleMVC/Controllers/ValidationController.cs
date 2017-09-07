using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;
using SampleMVC.Models;

namespace SampleMVC.Controllers
{
    [Route("api/validation")]
    public class ValidationController : Controller
    {
        //Return type is a model
        [HttpGet("{id?}")]
        public async Task<ValidationMaxModel> ModelStateTest(int? id)
        {
            if (!id.HasValue)
                return null;

            var model = await ValidationMaxModel.GetModell(id.Value);

            this.HttpContext.Response.StatusCode = 201;
            return model;
        }

        //Return type is a wrapper
        [HttpPost("V1/{id}")]
        public async Task<ObjectResult> ModelStateTestPost([Required] int id, [FromBody] ValidationMaxModel inputModel)
        {
            this.ModelState.Clear();

            var model = await ValidationMaxModel.GetModell(id);
            inputModel.LastPurchaseDate = model.LastPurchaseDate;

            if (this.TryValidateModel(inputModel))
            {
                return Ok(model);
            }

            if (await this.TryUpdateModelAsync(model))
            {
                return Ok(model);
            }


            return BadRequest(ModelState);
        }

        [HttpPost("V2/{id}")]
        public ObjectResult ModelStateTestPost2([Required] int id, [FromBody] ValidationMaxRelativeModel inputModel)
        {
            if (this.TryValidateModel(inputModel))
            {
                return Ok(inputModel);
            }

            return BadRequest(ModelState);
        }
    }
}
