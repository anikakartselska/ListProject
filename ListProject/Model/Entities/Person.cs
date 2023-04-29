using System;

namespace ListProject.Model.Entities
{
    public class Person:Entity
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}