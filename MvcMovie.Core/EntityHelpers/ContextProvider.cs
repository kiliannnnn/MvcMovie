using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcMovie.Core.Helpers
{
    public class ContextProviderOptions<T> where T : DbContext
    { 

        private DbContextOptionsBuilder<T> _dbContextOptions;

        /// <summary>
        /// Ajoute les options d'instantiation pour le type de DbContext T dans les options de configuration du ContextProvider
        /// </summary>
        /// <param name="optionsAction"></param>
        public void SetContextOptions(Action<DbContextOptionsBuilder<T>> optionsAction)
        {
            //_dbContextOptions = _dbContextOptions;
            _dbContextOptions = new DbContextOptionsBuilder<T>();
            //_defaultOptionsBuilder[typeof(T)] = new DbContextOptionsBuilder<T>();
            optionsAction(_dbContextOptions);
        }

        /// <summary>
        /// Récupère les options d'instanciation du DbContext T en fonction de son type
        /// </summary>
        /// <typeparam name="T">Le Type de DbContext à instancier</typeparam>
        /// <returns></returns>
        public DbContextOptions GetContextOptions()
        {
            return _dbContextOptions.Options;
        }
    }


    public interface IContextProvider
    {
        T GetContext<T>() where T : DbContext;
    }

    /// <summary>
    /// Service qui servira à fournir un DbContext avec la méthode GetContext(TypeDuDbContext).
    /// Le DbContext retourné est soit celui registré en Scope par la méthode services.AddDbContext, soit si celui-ci est non récupérable (par exemple scope singleton ou transient, ...) un nouveau sera instancié par le ContextProvider avec inversion de dépendances en utilisant ses options ContextProviderOptions
    /// </summary>
    public class ContextProvider : IContextProvider, IDisposable //where T : DbContext
    {

        private readonly IServiceProvider _serviceprovider;
        private List<DbContext> _disposableDbContexts;

        public ContextProvider(IServiceProvider serviceprovider )
        {
            _serviceprovider = serviceprovider;
            _disposableDbContexts = new List<DbContext>();
        }

        public T GetContext<T>() where T : DbContext
        {

            T context = null;
            try
            {
                //On cherche le contexte dans les services registrés
                context = (T)_serviceprovider.GetService(typeof(T));
            }
            catch//(Exception ex)
            {
                
            }
            finally
            {
                if (context == null)
                {
                    //Une erreur est survenue (possibilité que l'on cherche a utilier le dbcontext en scope à partir d'un service en singleton) ou le contexte n'a pas été trouvé:
                    //on va instancier le dbcontext avec inversion de dépendances
                    var settings = ((IOptions<ContextProviderOptions<T>>)_serviceprovider.GetService(typeof(IOptions<ContextProviderOptions<T>>))).Value;

                    context = (T)ActivatorUtilities.CreateInstance(_serviceprovider, typeof(T), settings.GetContextOptions());
                    //Ce contexte aura le même lifespan que le ContextProvider: on le registre pour le disposer quand le contextProvider sera disposé
                    _disposableDbContexts.Add(context);
                }
            }
            return context;

        }

        public void Dispose()
        {
            _disposableDbContexts.ForEach(z => z.Dispose());
        }
    }

}