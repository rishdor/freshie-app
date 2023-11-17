using freshie_webAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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

        //DELETE THIS LATER:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        // GET api/<UserController>/5
        // api/user/10000001
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] User value)
        //{
        //    Models.User user = new Models.User() { UserId = id, Name = value.Name, Email = value.Email };

        //    _context.Users.Update(user);
        //    _context.SaveChanges();
        //}


        //FIX THIS SO THERE IS NO SENSITIVE DATA IN THE URL
        // PUT api/<UserController>/5
        //[HttpPut]
        //[Route("change-password/{id}/{old_password}/{new_password}")]
        //public ActionResult ChangePassword(int id, string old_password, string new_password)
        //{
        //    Models.User user = _context.Users.FirstOrDefault(u => u.UserId == id);
        //    if (user != null)
        //    {
        //        if (old_password == user.Password)
        //        {
        //            user.Password = new_password;
        //            _context.Users.Update(user);
        //            _context.SaveChanges();

        //            return NoContent();
        //        }
        //        else
        //            return BadRequest();
        //    }

        //    return NotFound();
        //}

        // DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    Models.User user = _context.Users.FirstOrDefault(u => u.UserId == id);

        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //        _context.SaveChanges();
        //    }
        //    else
        //        throw new Exception("Given user doesn't exist");

        //}

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            //maybe input validation (think of it later)

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return Conflict("A user with this email already exists.");
            }

            if (_context.Users == null)
            {
                return Problem("Entity set 'FridgeHubContext.Users'  is null.");
            }

            string password = user.Password;
            user.Password = HashPassword(user.Password, out var salt);
            user.Salt = salt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        //[HttpGet("login")]
        //public async Task<ActionResult<User>> LoginUser(string email, string password)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        //    if (user == null || !VerifyPassword(password, user.Password, user.Salt))
        //    {
        //        return NotFound("Wrong email or password.");
        //    }
        //    return user;
        //}

        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody] Login loginModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (user == null || !VerifyPassword(loginModel.Password, user.Password, user.Salt))
            {
                return NotFound("Wrong email or password.");
            }
            return user;
        }

        const int keySize = 64;
        const int iterations = 350000;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public static string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(keySize);
            salt = Convert.ToBase64String(saltBytes);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }
        public static bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

    }
}
