using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.DAL;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public UserController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  IActionResult Get() {

            try
            {
                var users = _context.Users.ToList();
                if (users.Count == 0)
                {
                    return NotFound("Users not available");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User details not found with id {id}");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(User model) 
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(User model)
        {
            if(model == null || model.Id == 0)
            {
                return BadRequest("User data is invalid");
            }
            else if(model.Id == 0)
            {
                return BadRequest($"User Id {model.Id} is invalid");
            }

            try
            {
                var user = _context.Users.Find(model.Id);
                if (user == null)
                {
                    return NotFound($"User not found with Id {model.Id}");
                }
                user.Username = model.Username;
                user.Mail = model.Mail;
                user.PhoneNumber = model.PhoneNumber;
                user.Skillsets = model.Skillsets;
                user.Hobby = model.Hobby;
                _context.SaveChanges();
                return Ok("User details updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User not found with id {id}");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok("User deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
