using ICMServer.Models;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public interface IRoomsModel : ICollectionModel<Room>
    {
        Task DeleteRoomsWhichHaveNoDevicesAsync();
    }
}
