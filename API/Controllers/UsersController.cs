using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//QUESTIONS
// HOW does context get the users? 

namespace API.Controllers 
{
    // Boilerplate for API controllers
    [ApiController]
    //Route that goes to this controller I.E https://localhost:5001/api/users. users comes from the Name of the route
    [Route("api/[controller]")]
    // We make a class called UsersController that inherits ControllerBase which uses the AspNetCore. 
    public class UsersController : ControllerBase
    {
        // A DataContext variable set by the constuctor. This Variable holds the information required to make databse connections
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        // Uses the attribute HttpGet and this will forward the get request to our route to this method
        [HttpGet]
        // We use type IEnumerable to get a list of Users. We use IEnumerable since it is less heavy than the list type.
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            // We create a variable that holds the users found from the database. 
            var users =  _context.Users;
            // Here we change users into a list Asynchronously 
            await users.ToListAsync();
            // Here we return the users to the endpoint or to the app requesting the data
            return users;
        }
        // api/users/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}