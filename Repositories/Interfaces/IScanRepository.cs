using Recycler.API.RecyclerModels;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API
{
    public interface IScanRepository
    {
        Task<Scan> GetScanById(int id);
        Task<Scan> CreateScan(Scan scan);
        Task<bool> DeleteScan(int id);
        Task<bool> UpdateScan(Scan scan);
        Task<List<Scan>> GetAllScans();
        List<Scan> GetScanDataTable(PaginationFilter filter);
        int GetScanCount(decimal priceFrom, decimal priceTo);
    }
}
