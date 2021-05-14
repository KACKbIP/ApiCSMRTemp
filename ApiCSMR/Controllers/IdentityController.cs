using ApiCSMR.Interfaces;
using ApiCSMR.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IIdentityInterface _repositoryIdentity;
        private readonly IGuardInterface _repositoryGuard;
        public IdentityController(IIdentityInterface identityRepository, IGuardInterface guardRepository)
        {
            this._repositoryIdentity = identityRepository;
            this._repositoryGuard = guardRepository;
        }
        [HttpPost("GetReport")]
        public ResponseClass<List<ReportModel>> GetReport(RequestClass<ReportData> request)
        {
            try
            {
                if (!_repositoryGuard.IsValid(request.Login, request.Password, request.Token))
                    throw new Exception("Неверные данные авторизации");
                ResponseClass<List<ReportModel>> response = new ResponseClass<List<ReportModel>>();
                response.Data = _repositoryIdentity.GetReport(request.Login,request.Data.DateFrom, request.Data.DateTo);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<List<ReportModel>> response = new ResponseClass<List<ReportModel>>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("GetReportByAgent")]
        public ResponseClass<List<ReportModel>> GetReportByAgent(RequestClass<ReportData> request)
        {
            try
            {
                if (!_repositoryGuard.IsValid(request.Login, request.Password, request.Token))
                    throw new Exception("Неверные данные авторизации");
                ResponseClass<List<ReportModel>> response = new ResponseClass<List<ReportModel>>();
                response.Data = _repositoryIdentity.GetReportByAgent(request.Data);
                response.Code = 0;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<List<ReportModel>> response = new ResponseClass<List<ReportModel>>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
        [HttpPost("Identification")]
        public ResponseClass<decimal> Identification(RequestClass<IdentificationModel> request)
        {
            try
            {
                if (!_repositoryGuard.IsValid(request.Login, request.Password, request.Token))
                    throw new Exception("Неверные данные авторизации");
                ResponseClass<decimal> response = new ResponseClass<decimal>();
                int key = _repositoryIdentity.GetIdentificationId(request.Data.IIN, request.Login);
                response.Data = _repositoryIdentity.GetIdentification(request.Data,key)*100;
                response.Code = 0;
                response.Message = "Success";
                _repositoryIdentity.UpdateIdentification(key, response.Data);
                return response;
            }
            catch (Exception e)
            {
                ResponseClass<decimal> response = new ResponseClass<decimal>();
                response.Code = -1;
                response.Message = e.Message;

                return response;
            }
        }
    }
}
