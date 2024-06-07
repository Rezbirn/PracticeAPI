using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Data;
using PracticeAPI.Models;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public AddressController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var address = _dbContext.Address.FirstOrDefault(x => x.Id == id);
            if (address is not null)
            {
                return Ok(address);
            }
            return NotFound();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var addressAll = _dbContext.Address.ToList();
            if (addressAll != null && addressAll.Any())
            {
                return Ok(addressAll);
            }
            return NotFound();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public IActionResult Post(AddressCreateModel address)
        {
            if (address.IsValid())
            {
                _dbContext.Address.Add(address.ToAddress());
                _dbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Put(int id, AddressCreateModel address)
        {
            if (address.IsValid())
            {
                var oldAddress = _dbContext.Address.FirstOrDefault(y => y.Id == id);
                if (oldAddress is not null)
                {
                    _dbContext.Entry(oldAddress).CurrentValues.SetValues(address);
                    _dbContext.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var address = _dbContext.Address.FirstOrDefault(x => x.Id == id);
            if (address is not null)
            {
                _dbContext.Address.Remove(address);
                _dbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
