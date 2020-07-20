using MediaCatalog.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IActorRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Actor[]> GetAllActorsAsync();

        Task<Actor> GetActorAync(int actorId);

        Task<Actor[]> GetActorsByMovieIdAsync(int movieId);

        Task<Actor> GetActorByNameAsync(string lastName, string firstName);

        Task<int> GenerateActorId();

        Task<Actor[]> GetActorsByNameSearchValue(string value);
    }
}