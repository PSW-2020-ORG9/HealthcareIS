using System;

namespace User.API.Infrastructure.Repositories
{
    /// <summary>
    /// Wrapper class around an IPreparable object.
    /// Storing direct references to the object inside of this wrapper is highly discouraged due to
    /// possible data collisions.
    /// Instead, use the Repository property when needed.
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public class RepositoryWrapper<TRepository>
        where TRepository : class, IPreparable
    {
        private readonly TRepository _repository;

        /// <summary>
        /// Repository object inside of this wrapper. Allows direct access to database operations.
        /// </summary>
        public TRepository Repository
        {
            get
            {
                _repository.Prepare();
                return _repository;
            }
        }

        /// <summary>
        /// Constructor which takes in an arbitrary number of arguments for the given Repository, in the form
        /// of an array. A Repository constructor to best match the given parameters will be invoked.
        /// If the constructor doesn't take any parameters, null or an empty array can be passed in.
        /// </summary>
        /// <param name="varargs"></param>
        public RepositoryWrapper(object[] varargs)
        {
            _repository = Activator.CreateInstance(typeof(TRepository), varargs) as TRepository;
        }

        /// <summary>
        /// Constructor which takes any object as parameter, and instantiates the given Repository with it.
        /// </summary>
        /// <exception cref="ArgumentException"> If no Repository constructor exists for the given parameter type </exception>
        /// <param name="param"></param>
        public RepositoryWrapper(object param)
        {
            _repository = Activator.CreateInstance(typeof(TRepository), param) as TRepository;
        }

        /// <summary>
        /// Instantiates a wrapper with the given Repository object inside.
        /// </summary>
        /// <param name="repository"></param>
        public RepositoryWrapper(TRepository repository)
        {
            _repository = repository;
        }
    }
}