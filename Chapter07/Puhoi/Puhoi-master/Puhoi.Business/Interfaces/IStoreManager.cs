using Puhoi.Models;
using Puhoi.Models.Models;
using System;

namespace Puhoi.Business.Interfaces
{
    public interface IStoreManager
    {
        HttpModelResult Add(BaseModel model);
        HttpModelResult Update(BaseModel model, Guid id);
        HttpModelResult Get(Guid id);
        HttpModelResult Delete(Guid id);
        HttpModelResult GetAll();

    }
}
