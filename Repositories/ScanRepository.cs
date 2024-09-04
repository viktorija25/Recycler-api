using Microsoft.EntityFrameworkCore;
using Recycler.API.RecyclerModels;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public class ScanRepository:IScanRepository
    {

        #region GET BY ID
        public async Task<Scan> GetScanById(int id)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                var scan = db.Scan.Where(s=>s.ScanId==id).FirstOrDefault();
                return scan;
            }
        }
        #endregion

        #region CREATE
        public async Task<Scan> CreateScan(Scan scan)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.Scan.Add(scan);
                db.SaveChanges();
                return scan;
            }
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteScan(int id)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                Scan scan = db.Scan.Where(p => p.ScanId == id).FirstOrDefault();
                db.Scan.Remove(scan);
                await db.SaveChangesAsync();
                return true;
            }
        }
        #endregion

        #region UPDATE
        public async Task<bool> UpdateScan(Scan scan)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.Scan.Update(scan);
                db.SaveChanges();
                return true;
            }
        }
        #endregion

        #region GET ALL 
        public async Task<List<Scan>> GetAllScans()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.Scan.ToList();
            }
        }
        #endregion

        #region DATATABLE

        public List<Scan> GetScanDataTable(PaginationFilter filter)
        {
            using (RecyclerContext db = new RecyclerContext())
            {
                if (filter.SortDirection.ToLower() == "asc")
                {
                    switch (filter.SortColumn)
                    {
                        case "user":
                            {
                                return db.Scan.Where(p => (filter.SizeFrom == 0 || p.AccountId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.AccountId <= filter.SizeTo))
                                                  .OrderBy(x => x.AccountId)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b=>b.BottleSizeNavigation)
                                                  .ToList();
                            }
                        case "barcode":
                            {
                                return db.Scan.Where(p => (filter.SizeFrom == 0 || p.BarCodeId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.BarCodeId <= filter.SizeTo))
                                                  .OrderBy(x => x.BarCodeId)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b => b.BottleSizeNavigation)
                                                  .ToList();
                            }
                        case "date":
                            {
                                return db.Scan.Where(p => (p. DateCreated >= filter.dateFrom) && ( p.DateCreated <= filter.dateTo))
                                                  .OrderBy(x => x.DateCreated)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b => b.BottleSizeNavigation)
                                                  .ToList();
                            }
                        default:
                            {
                                return db.Scan.Where(p => (filter.SizeFrom == 0 || p.AccountId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.AccountId <= filter.SizeTo))
                                                  .OrderBy(x => x.DateCreated)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b => b.BottleSizeNavigation)
                                                  .ToList();
                            }
                    }
                }
                else
                {
                    switch (filter.SortColumn)
                    {
                        case "user":
                            {
                                return db.Scan.Where(p => (filter.SizeFrom == 0 || p.AccountId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.AccountId <= filter.SizeTo))
                                                  .OrderByDescending(x => x.AccountId)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b => b.BottleSizeNavigation)
                                                  .ToList();
                            }
                        case "barcode":
                            {
                                return db.Scan.Where(p => (filter.SizeFrom == 0 || p.BarCodeId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.BarCodeId <= filter.SizeTo))
                                                  .OrderByDescending(x => x.BarCodeId)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b => b.BottleSizeNavigation)
                                                  .ToList();
                            }
                        case "date":
                            {
                                return db.Scan.Where(p => (p.DateCreated >= filter.dateFrom) && (p.DateCreated <= filter.dateTo))
                                                  .OrderByDescending(x => x.DateCreated)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b => b.BottleSizeNavigation)
                                                  .ToList();
                            }
                        default:
                            {
                                return db.Scan.Where(p => (filter.SizeFrom == 0 || p.AccountId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.AccountId <= filter.SizeTo))
                                                  .OrderByDescending(x => x.BarCodeId)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.Account).Include(b => b.BarCode).ThenInclude(b => b.BottleSizeNavigation)
                                                  .ToList();
                            }
                    }
                }
            }
        }

        public int GetScanCount(decimal sizeFrom, decimal sizeTo)
        {
            using (RecyclerContext db = new RecyclerContext())
            {
                return db.Scan.Where(p => (sizeFrom == 0 || p.AccountId >= sizeFrom) && (sizeTo == 0 || p.AccountId <= sizeTo)).Count();
            }
        }
        #endregion
    }
}
