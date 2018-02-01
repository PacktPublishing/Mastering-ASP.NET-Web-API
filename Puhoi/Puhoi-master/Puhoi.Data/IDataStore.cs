using Puhoi.DataModels;
using System;
using System.Collections.Generic;

namespace Puhoi.Data
{
    public interface IDataStore
    {
        BaseDto AddorUpdate(BaseDto dto);
        BaseDto Delete(Guid id);
        BaseDto Get(Guid id);
        BaseDto Get(string name);
        IEnumerable<BaseDto> GetAll();
    }
}
