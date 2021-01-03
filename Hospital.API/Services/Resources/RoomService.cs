using General.Repository;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Services.Resources
{
    public class RoomService : IRoomService
    {
        private readonly RepositoryWrapper<IRoomRepository> _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
        }

        public IEnumerable<Room> GetRoomsByIds(IEnumerable<int> ids)
            => _roomRepository.Repository.GetMatching(r => ids.Contains(r.Id));

        public Room GetById(int id)
            => _roomRepository.Repository.GetByID(id);
    }
}
