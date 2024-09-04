using Recycler.API.RecyclerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API
{
    public interface ILookUpRepository
    {
        Task<List<PlatformConfigutration>> GetAll();
        Task<List<PlatformConfigutration>> GetUserType();
        Task<List<PlatformConfigutration>> GetUserStatus();
        Task<List<PlatformConfigutration>> GetBarCodeType();
    }
}
