using System;
using System.Threading.Tasks;
using UsersAPI.Entities;

namespace UsersAPI.Repository
{
    public class UnitOfWork: IDisposable
    {
        private readonly UsersDbContext context;
        private GenericRepository<User> usersRepository;

        public UnitOfWork(UsersDbContext context)
        {
            this.context = context;
        }

        public GenericRepository<User> UsersRepository
        {
            get
            {
                if(usersRepository == null)
                {
                    usersRepository = new GenericRepository<User>(context);
                }

                return usersRepository;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if (disposing)
                    context.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
