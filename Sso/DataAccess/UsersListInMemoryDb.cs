using System.Collections.Concurrent;
using System.Collections.Generic;
using Sso.Domain;

using static Sso.Domain.User;

namespace Sso.DataAccess
{
    public class UsersListInMemoryDb : IUsersList
    {
        private IDictionary<string,User> users = new ConcurrentDictionary<string, User>();

        public UsersListInMemoryDb()
        {
            Add(Salesman("Jim.Salesman", "pass"));
            Add(Accountant("Tim.Accountant", "pass"));
            Add(Customer("Mike.Client", "pass", "NYPD"));
            Add(Customer("Tim.Client", "pass", "UN"));
        }

        public void Add(User user)
        {
            users[user.Login] = user;
        }

        public User FindByLogin(string login)
        {
            return users[login];
        }
    }
}