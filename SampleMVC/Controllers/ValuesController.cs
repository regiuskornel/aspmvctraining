using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleMVC.Infrastucture;
using SampleMVC.Services;

namespace SampleMVC.Controllers
{
    [HttpHeader("Author","Logmein")] //Custom filter
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMyCustomService _custom;

        public ValuesController(IMyCustomService custom)
        {
            _custom = custom;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _custom.DoSomething(); //Call injected service

            return new string[] { this.Request.QueryString.ToString(), "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
