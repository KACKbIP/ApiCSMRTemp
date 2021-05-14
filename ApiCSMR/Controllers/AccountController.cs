using ApiCSMR.Interfaces;
using ApiCSMR.Models;
using ApiCSMR.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiCSMR.Models.AccountModel;

namespace ApiCSMR.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountInterface _repository;
        private readonly IGuardInterface _repositoryGuard;
        public AccountController(IAccountInterface repository, IGuardInterface guardRepository)
        {
            this._repository = repository;
            this._repositoryGuard = guardRepository;
        }
        [HttpGet("GetAccount")]
        public ResponseClass<AccountLoginModel> GetAccount(string login, string password)
        {
            ResponseClass<AccountLoginModel> model = new ResponseClass<AccountLoginModel>();
            try
            {
                model.Code = 0;
                model.Message = "Success";
                model.Data = _repository.GetAccount(login, password); 
            }
            catch(Exception e)
            {
                model.Code = -1;
                model.Message = e.Message;                
            }
            return model;
        }
        [HttpPost("ChangePassword")]
        public ResponseClass<bool> ChangePassword(RequestClass<ChangeData> request)
        {
            ResponseClass<bool> model = new ResponseClass<bool>();
            try
            {
                if (!_repositoryGuard.IsValid(request.Login, request.Password, request.Token))
                    throw new Exception("Неверные данные авторизации");
                model.Code = 0;
                model.Message = "Success";
                model.Data = _repository.ChangePassword(request.Login, request.Data.NewPassword);
            }
            catch (Exception e)
            {
                model.Code = -1;
                model.Message = e.Message;
            }
            return model;
        }
    }
}
