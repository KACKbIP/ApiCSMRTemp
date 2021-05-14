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
    public class AdminController : Controller
    {
        private readonly IAdminInterface _repositoryAdmin;
        private readonly IGuardInterface _repositoryGuard;
        public AdminController(IAdminInterface adminRepository, IGuardInterface guardRepository)
        {
            this._repositoryAdmin = adminRepository;
            this._repositoryGuard = guardRepository;
        }

        [HttpPost("AddAgent")]
        public ResponseClass<AgentResponse> AddAgent(RequestClass<AgentData> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");
                request.Data.Password = HelperRepository.EncrypteText(request.Data.Account + "!#").Substring(0, 6);

                ResponseClass<AgentResponse> response = new ResponseClass<AgentResponse>();
                response.Data = _repositoryAdmin.AddAgent(request.Data.Account,request.Data.Password,request.Data.Name,request.Data.IsTest);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<AgentResponse> response = new ResponseClass<AgentResponse>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("GetAgents")]
        public ResponseClass<List<AgentModel>> GetAgents(RequestClass<dynamic> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<List<AgentModel>> response = new ResponseClass<List<AgentModel>>();
                response.Data = _repositoryAdmin.GetAgents();
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<List<AgentModel>> response = new ResponseClass<List<AgentModel>>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("AddQuery")]
        public ResponseClass<bool> AddQuery(RequestClass<QueryData> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Data = _repositoryAdmin.AddQuery(request.Data);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }

        [HttpPost("GetQuery")]
        public ResponseClass<List<QueryModel>> GetQuery(RequestClass<QueryListData> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<List<QueryModel>> response = new ResponseClass<List<QueryModel>>();
                response.Data = _repositoryAdmin.GetQuery(request.Data);
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
        [HttpPost("GetTarifs")]
        public ResponseClass<List<TarifModel>> GetTarifs(RequestClass<dynamic> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<List<TarifModel>> response = new ResponseClass<List<TarifModel>>();
                response.Data = _repositoryAdmin.GetTarifs();
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
        [HttpPost("GetAgentTarifs")]
        public ResponseClass<List<TarifAgentModel>> GetAgentTarifs(RequestClass<dynamic> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<List<TarifAgentModel>> response = new ResponseClass<List<TarifAgentModel>>();
                response.Data = _repositoryAdmin.GetAgentTarifs();
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<List<TarifAgentModel>> response = new ResponseClass<List<TarifAgentModel>>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("AddTarif")]
        public ResponseClass<bool> AddTarif(RequestClass<TarifModel> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Data = _repositoryAdmin.AddTarif(request.Data);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("AddTarifAgent")]
        public ResponseClass<bool> AddTarifAgent(RequestClass<TarifAgentModel> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Data = _repositoryAdmin.AddTarifAgent(request.Data);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("DeleteTarif")]
        public ResponseClass<bool> DeleteTarif(RequestClass<TarifModel> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Data = _repositoryAdmin.DeleteTarif(request.Data);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<bool> response = new ResponseClass<bool>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("UpdateAgent")]
        public ResponseClass<dynamic> UpdateAgent(RequestClass<UpdateAgent> request)
        {
            try
            {
                if (!_repositoryGuard.IsValidAdmin(request.Token))
                    throw new Exception("Неверные данные авторизации");

                ResponseClass<dynamic> response = new ResponseClass<dynamic>();
                _repositoryAdmin.UpdateAgent(request.Data);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<dynamic> response = new ResponseClass<dynamic>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
    }
}
