using System;
using System.Collections.Generic;
using System.Linq;
using Puhoi.DataModels;

namespace Puhoi.Data
{
    public class DataStore : IDataStore
    {
        List<BaseDto> _dtoRepository;
        public DataStore()
        {
            _dtoRepository = new List<BaseDto>();
        }

        public BaseDto AddorUpdate(BaseDto dto)
        {
            if (dto.UId == Guid.Empty)
            {
                dto.UId = Guid.NewGuid();
                _dtoRepository.Add(dto);
            }
            else
            {
                BaseDto currentDto = _dtoRepository.Find(d => d.UId == dto.UId);
                _dtoRepository.Remove(currentDto);
                _dtoRepository.Add(dto);
            }
            return dto;
        }

        public BaseDto Delete(Guid id)
        {
            BaseDto dto  = _dtoRepository.Find(d => d.UId == id);
            if (dto != null)
            {
                _dtoRepository.Remove(dto);
                return dto;
            }
            else
            {
                return null;
            }
        }

        public BaseDto Get(string name)
        {
            return _dtoRepository.FirstOrDefault(d => d.Name == name);
        }

        public BaseDto Get(Guid id)
        {
            return _dtoRepository.FirstOrDefault(d => d.UId == id);
        }

        public IEnumerable<BaseDto> GetAll()
        {
            return _dtoRepository;
        }
    }
}
