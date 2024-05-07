using MDSoft.Data.Interface;
using MDSoft.Tracking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services.Interface
{
    public interface IUsuarioSistemaRepository : IRepositorio<UsuarioSistema>
    {
		Task<UsuarioSistema> Login(string Username, string Password);

		Task<UsuarioSistema> GetById(int UserId);
		Task<UsuarioSistema> Register(UsuarioSistema userInformation);

		Task<UsuarioSistema> ResetPassword(string Username);

		Task<UsuarioSistema> ChangePassword(string Username, string OldPassword, string NewPassword);

    }
}
