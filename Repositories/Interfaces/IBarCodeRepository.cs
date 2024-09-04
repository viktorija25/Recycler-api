using Recycler.API.RecyclerModels;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API
{
    public interface IBarCodeRepository
    {
        Task<List<BarCode>> GetAllBarCodes();
        Task<BarCode> GetBarCodeById(int id);
        Task<bool> DeleteBarCode(int id);
        Task<BarCode> CreateBarCode(BarCode barCode);
        Task<bool> UpdateBarCode(BarCode barCode);
        List<BarCode> GetBarCodeDataTable(PaginationFilter filter);
        int GetBarCodesCount(decimal priceFrom, decimal priceTo);


    }
}
