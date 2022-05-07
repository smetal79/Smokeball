using System.Threading.Tasks;

namespace Smokeball.Seo.Ui.Services
{
    public interface IFetchSeoStatus
    {
        Task<string> GetRankings(GetStatusRequest request);
    }
}
