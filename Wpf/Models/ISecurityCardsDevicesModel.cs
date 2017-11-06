using ICMServer.Models;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public interface ISecurityCardsDevicesModel : ICollectionModel<icmap>
    {
        void SetSecurityCard(iccard obj);
        Task DeleteSecurityCardAsync(iccard obj);
    }
}
