using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IStudioRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<int> GenerateStudioId();

        Task<bool> SaveChangesAsync();

        Task<Studio[]> GetAllStudiosAsync();

        Task<Studio> GetStudioAsync(int studioId);

        Task<Studio[]> GetStudiosByMovieIdAsync(int movieId);
    }
}