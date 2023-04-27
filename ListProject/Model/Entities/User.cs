using System.ComponentModel;

namespace ListProject.Model
{
    public class User
    {
        private string name;
        private string password;

        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }
        public string Password
        {
            get => password;
            set => password = value;
        }
    }
}