
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Puhoi.Business.Interfaces;
using Puhoi.Models.Models;
using Puhoi.Data;
using Puhoi.DataModels;
using Puhoi.Models;
using AutoMapper;

namespace Puhoi.Business.Managers
{
	public class StoreManager : IStoreManager
	{
		private readonly IDataStore _dataStore;
		private IMapper _mapper;

		public StoreManager(IDataStore dataStore, IMapper mapper)
		{
			_dataStore = dataStore;
			_mapper = mapper;
		}

		public HttpModelResult Add(BaseModel model)
		{
			HttpModelResult result = new HttpModelResult();
			if( _dataStore.Get(model.Name) == null)
			{
				result = AddModel(model);
			}
			else
			{
				result.HttpStatus = HttpStatusCode.Conflict;
				return result;
			}
			return result;
		}

		private HttpModelResult AddModel(BaseModel model)
		{
			HttpModelResult result = new HttpModelResult();
			StoreDto dto = _mapper.Map<StoreDto>(model);
			try
			{
				dto = (StoreDto)_dataStore.AddorUpdate(dto);
				if (dto != null)
				{
					StoreModel createdModel = _mapper.Map<StoreModel>(dto);
					result.Model = createdModel;
					result.HttpStatus = HttpStatusCode.Created;
				}
				else
				{
					result.HttpStatus = HttpStatusCode.InternalServerError;
				}
			}
			catch
			{
				result.HttpStatus = HttpStatusCode.InternalServerError;
			}
			return result;
		}

		public HttpModelResult Delete(Guid id)
		{
			HttpModelResult result = new HttpModelResult();
			BaseDto dto = _dataStore.Delete(id);
			result.HttpStatus = dto == null ? 
				HttpStatusCode.NotFound : HttpStatusCode.OK;
			return result;
		}


		public HttpModelResult Get(Guid id)
		{
			HttpModelResult result = new HttpModelResult();
			BaseDto dto = _dataStore.Get(id);
			if( dto == null )
			{
				result.HttpStatus = HttpStatusCode.NotFound;
			}
			else
			{
				result.HttpStatus = HttpStatusCode.OK;
				result.Model = _mapper.Map<StoreModel>(dto);
			}
			return result;
		}

		public HttpModelResult Update(BaseModel model, Guid id)
		{
			if( _dataStore.Get(id) == null )
			{
				return Add(model);
			}
			
			model.Id = id;
			StoreDto dto = _mapper.Map<StoreDto>(model);
			dto = (StoreDto)_dataStore.AddorUpdate(dto);
			return new HttpModelResult
			{
				HttpStatus = HttpStatusCode.OK,
				Model = _mapper.Map<StoreModel>(dto)
			};
		}

		public HttpModelResult GetAll()
		{
			HttpModelResult result = new HttpModelResult();
			IEnumerable<BaseDto> dtos = _dataStore.GetAll();
			List<StoreModel> models = dtos.Select(baseDto =>
                            _mapper.Map<StoreModel>(baseDto)).ToList();
		    result.Models = models.AsEnumerable();
		    result.HttpStatus = HttpStatusCode.OK;
            return result;
		}
	}
}
