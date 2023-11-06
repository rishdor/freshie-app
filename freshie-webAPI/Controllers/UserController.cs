using freshie_DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace freshie_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FreshieDbContext _context; 
        public UserController(FreshieDbContext context)
        {
            _context = context;
        }


        // GET api/<UserController>/5
        // api/user/10000001
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(u=>u.user_id==id);
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("register/{new_password}")]
        public void Post([FromBody] User value, string new_password)
        {
            //ustawiam user id=0, bo kolumna jest identity i sama nada identyfikator
            Model.User user =new Model.User() {user_id=0,Name=value.Name,Email=value.Email , Password=new_password};
 
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            Model.User user = new Model.User() { user_id = id, Name = value.Name, Email = value.Email };

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Route("change-password/{id}/{old_password}/{new_password}")]
        public ActionResult ChangePassword(int id,string old_password, string new_password)
        {
            Model.User user = _context.Users.FirstOrDefault(u => u.user_id == id);
            if (user != null)
            {
                if (old_password == user.Password)
                {
                    user.Password = new_password;
                    _context.Users.Update(user);
                    _context.SaveChanges();

                    return NoContent();
                }
                else 
                    return BadRequest();
            }

            return NotFound();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Model.User user =_context.Users.FirstOrDefault(u=>u.user_id==id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
                throw new Exception("Given user doesn't exist");
            
        }
    }
}
