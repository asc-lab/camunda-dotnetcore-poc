namespace Sso.Domain
{
    public interface IUsersList
    {
        void Add(User user);
        User FindByLogin(string login);
    }
}