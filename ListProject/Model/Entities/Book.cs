using System;

namespace ListProject.Model
{
    public class Book:Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
    }
}