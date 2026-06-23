namespace ReactRealTimeTasks.Data;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddUser(User user, string password)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

        using var context = new TaskItemsDataContext(_connectionString);
        context.Users.Add(user);
        context.SaveChanges();
    }

    public User GetByEmail(string email)
    {
        using var context = new TaskItemsDataContext(_connectionString);
        return context.Users.FirstOrDefault(u => u.Email == email);
    }

    public User Login(string email, string password)
    {
        var user = GetByEmail(email);
        if (user == null)
        {
            return null;
        }

        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (isCorrectPassword)
        {
            return user;
        }

        return null;
    }

    public bool EmailExists(string email)
    {
        using var ctx = new TaskItemsDataContext(_connectionString);
        return ctx.Users.Any(u => u.Email == email);
    }
}
