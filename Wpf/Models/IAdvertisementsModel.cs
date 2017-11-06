using ICMServer.Models;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public interface IAdvertisementsModel : ICollectionModel<advertisement>
    {
        Task MoveUpAsync(advertisement obj);
        Task MoveDownAsync(advertisement obj);
    }
}
