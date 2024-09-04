using Recycler.API.RecyclerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public interface ICityRepository
    {
        Task<City> CreateCity(City city);
        Task<bool> UpdateCity(City city);
        Task<List<City>> GetAllCities();
        Task<bool> DeleteCity(int id);

    }
}
