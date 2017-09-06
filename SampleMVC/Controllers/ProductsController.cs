using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleMVC.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        [HttpGet] // Matches '/Products/List'
        public IActionResult List()
        {
            return null;
        }

        [HttpGet("{id}")] // Matches '/Products/Editor/{id}'
        [ActionName("Editor")]
        public IActionResult Edit(int id)
        {
            return null;
        }
    }
}
