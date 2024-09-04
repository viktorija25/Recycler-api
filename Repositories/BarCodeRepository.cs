using Recycler.API.RecyclerModels;
using Microsoft.EntityFrameworkCore;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public class BarCodeRepository : IBarCodeRepository
    {
        #region CREATE
        public async Task<BarCode> CreateBarCode(BarCode barCode)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.BarCode.Add(barCode);
                db.SaveChanges();
                return barCode;
            }
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteBarCode(int id)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                BarCode barCode = db.BarCode.Where(p => p.BarCodeId == id).FirstOrDefault();
                db.BarCode.Remove(barCode);
                await db.SaveChangesAsync();
                return true;
            }
        }
        #endregion

        #region GET ALL 
        public async Task<List<BarCode>> GetAllBarCodes()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.BarCode.ToList();
            }
        }
        #endregion

        #region GET BY ID
        public async Task<BarCode> GetBarCodeById(int id)
        {
            await using(RecyclerContext db = new RecyclerContext())
            {
                var barCode = db.BarCode.Where(b=>b.BarCodeId==id).FirstOrDefault();
                return barCode;
            }         
        }
        #endregion

        #region UPDATE
        public async Task<bool> UpdateBarCode(BarCode barCode)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.BarCode.Update(barCode);
                db.SaveChanges();
                return true;
            }
        }
        #endregion

        #region DATATABLE

        public List<BarCode> GetBarCodeDataTable(PaginationFilter filter)
        {
            using (RecyclerContext db = new RecyclerContext())
            {

                if (filter.SortDirection.ToLower() == "asc")
                {
                    switch (filter.SortColumn)
                    {
                        case "size":
                            {
                                return db.BarCode.Where(p => (filter.SizeFrom == 0 || p.BottleSize >= filter.SizeFrom) && (filter.SizeTo == 0 || p.BottleSize <= filter.SizeTo))
                                                  .OrderBy(x => x.BottleSize)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.BottleSizeNavigation).Include(b => b.CreatedByNavigation)
                                                  .ToList();
                            }
                        default:
                            {
                                return db.BarCode.Where(p => (filter.SizeFrom == 0 || p.BottleSize >= filter.SizeFrom) && (filter.SizeTo == 0 || p.BottleSize <= filter.SizeTo))
                                                  .OrderBy(x => x.CreatedBy)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.BottleSizeNavigation).Include(b => b.CreatedByNavigation)
                                                  .ToList();
                            }
                    }
                }
                else
                {
                    switch (filter.SortColumn)
                    {
                        case "size":
                            {
                                return db.BarCode.Where(p => (filter.SizeFrom == 0 || p.BottleSize >= filter.SizeFrom) && (filter.SizeTo == 0 || p.BottleSize <= filter.SizeTo))
                                                  .OrderByDescending(x => x.BottleSize)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.BottleSizeNavigation).Include(b => b.CreatedByNavigation)
                                                  .ToList();
                            }
                        default:
                            {
                                return db.BarCode.Where(p => (filter.SizeFrom == 0 || p.BottleSize >= filter.SizeFrom) && (filter.SizeTo == 0 || p.BottleSize <= filter.SizeTo))
                                                  .OrderByDescending(x => x.CreatedBy)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.BottleSizeNavigation).Include(b => b.CreatedByNavigation)
                                                  .ToList();
                            }
                    }
                }
            }
        }

        public int GetBarCodesCount(decimal sizeFrom, decimal sizeTo)
        {
            using (RecyclerContext db = new RecyclerContext())
            {
                return db.BarCode.Where(p => (sizeFrom == 0 || p.BottleSize >= sizeFrom) && (sizeTo == 0 || p.BottleSize <= sizeTo)).Count();
            }
        }
        #endregion
    }
}
