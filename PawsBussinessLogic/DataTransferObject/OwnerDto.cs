using PawsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsBussinessLogic.DataTransferObject
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string DNI { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public District District { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
