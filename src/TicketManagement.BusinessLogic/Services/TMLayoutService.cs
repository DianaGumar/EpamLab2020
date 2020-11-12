using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic
{
    public interface ITMLayoutService
    {
        int RemoveTMLayout(int id);

        List<TMLayoutDto> GetAllTMLayout();

        TMLayoutDto GetTMLayout(int id);

        TMLayoutDto CreateTMLayout(TMLayoutDto obj);

        int UpdateTMLayout(TMLayoutDto obj);
    }

    internal class TMLayoutService : ITMLayoutService
    {
        private readonly ITMLayoutRepository _tmlayoutRepository;

        public TMLayoutService(ITMLayoutRepository tmlayoutRepository)
        {
            _tmlayoutRepository = tmlayoutRepository;
        }

        private static TMLayoutDto ConvertToDto(TMLayout obj)
        {
            return new TMLayoutDto
            {
                Id = obj.Id,
                Description = obj.Description,
                VenueId = obj.VenueId,
            };
        }

        private static TMLayout ConvertToEntity(TMLayoutDto obj)
        {
            return new TMLayout
            {
                Id = obj.Id,
                Description = obj.Description,
                VenueId = obj.VenueId,
            };
        }

        public int RemoveTMLayout(int id)
        {
            _tmlayoutRepository.Remove(id);

            TMLayout obj = _tmlayoutRepository.GetById(id);

            return obj == null ? 1 : 0;
        }

        public List<TMLayoutDto> GetAllTMLayout()
        {
            List<TMLayout> objs = _tmlayoutRepository.GetAll().ToList();
            var objsDto = new List<TMLayoutDto>();

            foreach (var item in objs)
            {
                objsDto.Add(ConvertToDto(item));
            }

            return objsDto;
        }

        public TMLayoutDto CreateTMLayout(TMLayoutDto obj)
        {
            List<TMLayout> objs = _tmlayoutRepository.GetAll()
               .Where(a => a.VenueId == obj.VenueId
               && a.Description == obj.Description).ToList();

            if (objs.Count == 0)
            {
                TMLayout e = _tmlayoutRepository.Create(ConvertToEntity(obj));
                obj.Id = e.Id;

                return obj;
            }
            else
            {
                return ConvertToDto(objs.ElementAt(0));
            }
        }

        public TMLayoutDto GetTMLayout(int id)
        {
            TMLayout obj = _tmlayoutRepository.GetById(id);

            return ConvertToDto(obj);
        }

        public int UpdateTMLayout(TMLayoutDto obj)
        {
            _tmlayoutRepository.Update(ConvertToEntity(obj));

            return 1;
        }
    }
}
