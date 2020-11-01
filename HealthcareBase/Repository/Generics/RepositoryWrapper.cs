using System;
using HealthcareBase.Model.Database;

namespace Repository.Generics
{
    /// <summary>
    /// Wrapper class around a Repository object.
    /// Storing direct references to the Repository object inside of this wrapper is highly discouraged due to
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
    }
}