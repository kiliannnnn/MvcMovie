 
using Microsoft.EntityFrameworkCore;
using MvcMovie.Core.Interfaces;

namespace MvcMovie.Core.Helpers
{

    public abstract class EFServiceBase<T, U> : EFServiceBase<T> where T : DbContext where U : class
    {
        private DbSet<U> _objectSet; 
        public DbSet<U> ObjectSet
        {
            get
            {
                return _objectSet;
            }
        }

        public EFServiceBase(IContextProvider contextProvider) : base(contextProvider)
        {
            this._objectSet = Context.Set<U>();
        }
    }

    /// <summary>
    /// Classe de base pour les services basé sur un DbContext sous jacent
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EFServiceBase<T> : IService
       where T : DbContext
    {
        private T _context; 
        public T Context
        {
            get
            {
                return _context;
            }
        }


        public EFServiceBase(IContextProvider contextProvider)
        {
            this._context = contextProvider.GetContext<T>();
        }
    }
}
