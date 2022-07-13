using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    /// <summary>
    /// Represents a user in our app
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// ID field of a user. EF will automatically assign SQL id properties to Id properties
        /// </summary>
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}