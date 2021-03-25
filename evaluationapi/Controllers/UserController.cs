using Dapper;
using eValuate.WebApi.Interfaces;
using eValuate.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.WebApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UserController(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userRepository.GetAllUsers();
            //return Ok(products);
            return new JsonResult(users);
        }

        [HttpGet(nameof(GelUserById))]
        public async Task<ActionResult<IEnumerable<User>>> GelUserById(int id)
        {
            var users = await _userRepository.GetUserById(id);
            //return Ok(products);
            return new JsonResult(users);
        }


        [HttpPost(nameof(Create))]
        public async Task<int> Create(User user)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Id", user.Id, DbType.Int32);
            dp_params.Add("Name", user.Name, DbType.String);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = await Task.FromResult(_userRepository.execute_sp<int>("[dbo].[sp_AddMember]", dp_params, commandType: CommandType.StoredProcedure));
            return result;
        }


        [HttpGet(nameof(GetUsers))]
        public async Task<List<User>> GetUsers()
        {
            var result = await Task.FromResult(_userRepository.GetAll<User>($"Select * from Test_User", null, commandType: CommandType.Text));
            return result;
        }


        [HttpPost(nameof(Update))]
        public async Task<int> Update(User user)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Id", user.Id, DbType.Int32);
            dp_params.Add("Name", user.Name, DbType.String);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = await Task.FromResult(_userRepository.execute_sp<int>("[dbo].[sp_UpdateUser]"
                , dp_params,
                commandType: CommandType.StoredProcedure));
            return result;
        }


        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Id", Id, DbType.Int32);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = await Task.FromResult(_userRepository.execute_sp<int>("[dbo].[sp_DeleteUser]"
                , dp_params,
                commandType: CommandType.StoredProcedure));
            return result;
        }


    }
}
