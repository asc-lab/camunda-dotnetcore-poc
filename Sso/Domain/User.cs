namespace Sso.Domain
{
    public class User
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public UserType UserType { get; private set; }
        public string CustomerCode { get; private set; }

        public static User Salesman(string login, string pwd) => new User(login,pwd,UserType.Sales,null);
        
        public static User Accountant(string login, string pwd) => new User(login,pwd,UserType.Accounting,null);
        
        public static User Customer(string login, string pwd, string customerCode) => new User(login,pwd,UserType.Customer,customerCode);
        
        private User(string login, string password, UserType userType, string customerCode)
        {
            Login = login;
            Password = password;
            UserType = userType;
            CustomerCode = customerCode;
        }

        public bool PasswordMatches(string pwd) => Password == pwd;
    }
}