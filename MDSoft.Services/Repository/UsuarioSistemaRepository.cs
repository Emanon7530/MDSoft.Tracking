using Azure.Identity;
using MDSoft.Data.Common;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Exceptions;
using MDSoft.Tracking.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Repository
{
    public class UsuarioSistemaRepository : Repositorio<UsuarioSistema>, IUsuarioSistemaRepository
    {
        public UsuarioSistemaRepository() : base()
        {

        }
        public Task<UsuarioSistema> ChangePassword(string Username, string OldPassword, string NewPassword)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioSistema> GetById(int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioSistema> Login(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new InvalidUsernameOrPasswordException();
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new InvalidUsernameOrPasswordException();
            }

            UsuarioSistema userfound = null;

            var _param = new ParametrosDeQuery<UsuarioSistema>(1, 100);
            _param.Where = x => x.UsuCorreoElectronico == Username;
            var users = await this.EncontrarPor(_param);

            if (users.Count() <= 0)
            {
                throw new InvalidUsernameOrPasswordException();
            }

            userfound = users?.FirstOrDefault();

            return userfound;
        }

        public Task<UsuarioSistema> Register(UsuarioSistema userInformation)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioSistema> ResetPassword(string Username)
        {
            throw new NotImplementedException();
        }

        private string GetTokenByCredentials(string Username, string Password)
        {

            string HashString = $"{Username}#{Password}#{DateTime.Now.ToString("yyyyMMddhhmmss")}";

            return HashString;
        }
    }
}
