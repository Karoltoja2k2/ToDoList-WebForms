using System;
using System.Linq;
using ToDoList.DataLayer.Model;

namespace ToDoList.DataLayer.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetByName(string name);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly ToDoListDbContext _context;
        public UserRepository(ToDoListDbContext context)
            : base(context)
        {
            _context = context;
        }

        public User GetByName(string name)
        {
            return _context.User.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}