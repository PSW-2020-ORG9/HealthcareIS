// File:    RenovationFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class RenovationFileRepository

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public class RenovationFileRepository : GenericFileRepository<Renovation, int>, RenovationRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly RoomRepository roomRepository;

        public RenovationFileRepository(RoomRepository roomRepository, string filePath) : base(filePath)
        {
            this.roomRepository = roomRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<Renovation> getByRoomAndTime(Room room, TimeInterval time)
        {
            return GetMatching(renovation => renovation.Room.Equals(room) && renovation.TimeInterval.Overlaps(time));
        }

        protected override int GenerateKey(Renovation entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Renovation ParseEntity(Renovation entity)
        {
            try
            {
                if (entity.Room != null)
                    entity.Room = roomRepository.GetByID(entity.Room.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}