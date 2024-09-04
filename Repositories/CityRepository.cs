using Recycler.API.RecyclerModels;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public class CityRepository:ICityRepository
    {
        #region CREATE
        public async Task<City> CreateCity(City city)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.City.Add(city);
                db.SaveChanges();
                return city;
            }
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteCity(int id)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                City city = db.City.Where(p => p.CityId == id).FirstOrDefault();
                db.City.Remove(city);
                await db.SaveChangesAsync();
                return true;
            }
        }
        #endregion

        #region GETALL
        public async Task<List<City>> GetAllCities()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.City.ToList();
            }
        }
        #endregion

        #region UPDATE
        public async Task<bool> UpdateCity(City city)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.City.Update(city);
                db.SaveChanges();
                return true;
            }
        }
        #endregion
    }
}
