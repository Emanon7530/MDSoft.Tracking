using AutoMapper;
using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Exceptions;
using MDSoft.Tracking.Services.Interface;
using MDSoft.Tracking.Services.Repository;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services
{
    public class UsuarioSistemaServices
    {
        IUsuarioSistemaRepository _Repolote;
        IMapper _mapper;

        public UsuarioSistemaServices(IMapper mapper )
        {
            _Repolote = new UsuarioSistemaRepository();
            _mapper = mapper;
        }

        public async Task<UsuarioSistemaDTO> Login(string Username, string Password)
        {

            UsuarioSistemaDTO result = null;

            try
            {
                var Userfound = await _Repolote.Login(Username, Password);

                if (Userfound != null)
                {
                    result = _mapper.Map<UsuarioSistemaDTO>(Userfound);
                }
                else
                {
                    throw new InvalidUsernameOrPasswordException();
                }

                return result;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<UsuarioSistemaDTO> ResetPassword(string emailAddress)
        {

            UsuarioSistemaDTO result = null;

            try
            {
                var _param = new ParametrosDeQuery<UsuarioSistema>(1, 100);

                _param.Where = x => x.UsuCorreoElectronico == emailAddress;

                var Userfound = await _Repolote.EncontrarPor(_param);

                if (Userfound == null)
                {
                    throw new UserNotFoundWithEmailAddressExcepcion(emailAddress);
                }

                result = _mapper.Map<UsuarioSistemaDTO>(Userfound.FirstOrDefault());

                return result;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<UsuarioSistemaDTO> Register(UsuarioSistemaDTO userInformation)
        {

            UsuarioSistema result = null;

            try
            {

                result = _mapper.Map<UsuarioSistema>(userInformation);

                var Userfound = await _Repolote.Register(result);

                userInformation = _mapper.Map<UsuarioSistemaDTO>(Userfound);

                return userInformation;

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
