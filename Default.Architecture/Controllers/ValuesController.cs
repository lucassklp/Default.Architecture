using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Repository.Interfaces;
using Domain.Entities;

namespace Default.Architecture.Controllers
{
    [AllowAnonymous]
    [Route("api/values")]
    public class ValuesController : Controller
    {

        IUserRepository repository;

        public ValuesController(IUserRepository repository)
        {
            this.repository = repository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return repository.SelectAll();
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
