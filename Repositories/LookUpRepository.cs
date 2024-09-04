using Recycler.API.RecyclerModels;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public class LookUpRepository: ILookUpRepository
    {
        #region GET ALL 
        public async Task<List<PlatformConfigutration>> GetAll()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.PlatformConfigutration.ToList();
            }
        }
        #endregion

        #region GET USER TYPE 
        public async Task<List<PlatformConfigutration>> GetUserType()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.PlatformConfigutration.Where(p=>p.ProgramCode=="2") .ToList();
            }
        }
        #endregion

        #region GET USER STATUS 
        public async Task<List<PlatformConfigutration>> GetUserStatus()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.PlatformConfigutration.Where(p => p.ProgramCode == "1").ToList();
            }
        }
        #endregion

        #region GET BARCODE TYPE 
        public async Task<List<PlatformConfigutration>> GetBarCodeType()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.PlatformConfigutration.Where(p => p.ProgramCode == "3").ToList();
            }
        }
        #endregion


    }
}
