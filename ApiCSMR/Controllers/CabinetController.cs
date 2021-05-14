using ApiCSMR.Interfaces;
using ApiCSMR.Models;
using ApiCSMR.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Controllers
{
    [ApiController]
    public class CabinetController : ControllerBase
    {
        private readonly ICabinetInterface _repositoryCabinet;
        private readonly IGuardInterface _repositoryGuard;
        public CabinetController(ICabinetInterface cabinetRepository, IGuardInterface guardRepository)
        {
            this._repositoryCabinet = cabinetRepository;
            this._repositoryGuard = guardRepository;
        }
        [HttpPost("GetCabinet")]
        public ResponseClass<CabinetModel> GetCabinet(RequestClass<dynamic> request)
        {
            try
            {
                if (!_repositoryGuard.IsValid(request.Login, request.Password, request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<CabinetModel> response = new ResponseClass<CabinetModel>();
                response.Data = _repositoryCabinet.GetCabinet(request.Login);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch(Exception e)
            {
                ResponseClass<CabinetModel> response = new ResponseClass<CabinetModel>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("GetQueryForAgent")]
        public ResponseClass<List<QueryModel>> GetQuery(RequestClass<QueryListData> request)
        {
            try
            {
                if (!_repositoryGuard.IsValid(request.Login, request.Password, request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<List<QueryModel>> response = new ResponseClass<List<QueryModel>>();
                response.Data = _repositoryCabinet.GetQuery(request.Data,request.Login);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<List<QueryModel>> response = new ResponseClass<List<QueryModel>>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }

        [HttpPost("GetTarifByAgent")]
        public ResponseClass<List<TarifModel>> GetQuery(RequestClass<dynamic> request)
        {
            try
            {
                if (!_repositoryGuard.IsValid(request.Login, request.Password, request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<List<TarifModel>> response = new ResponseClass<List<TarifModel>>();
                response.Data = _repositoryCabinet.GetTarifByAgent(request.Login);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<List<TarifModel>> response = new ResponseClass<List<TarifModel>>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
    }
}
