using Microsoft.AspNetCore.Mvc;
using MDSoft.Tracking.Services;
using AutoMapper;
using MDSoft.Tracking.Services.DTO;
using System.Runtime.Serialization;
using MDSoft.Tracking.API.Utilities;

namespace MDSoft.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class SeguridadController : ControllerBase
{
    private readonly ILogger<SeguridadController> _logger;
    private readonly UsuarioSistemaServices _seguridad;
    IMapper _mapper;
    private readonly IMailService _mailService;

    public SeguridadController(ILogger<SeguridadController> logger, IMapper mapper, IMailService _MailService)
    {
        _logger = logger;
        _mapper = mapper;
        _seguridad = new UsuarioSistemaServices(mapper);
        _mailService = _MailService;
    }

    [HttpPost]
    [Route("SendMail")]
    public bool SendMail(MailData mailData)
    {
        return _mailService.SendMail(mailData);
    }

    [HttpGet()]
    [Route("Login")]
    public async Task<UsuarioSistemaDTO> Login(string Username, string Password)
    {
        try
        {

            var result = await _seguridad.Login(Username, Password);

            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
    [Route("ResetPassword")]
    [HttpGet()]
    public async Task<UsuarioSistemaDTO> ResetPassword(string emailAddress)
    {
        try
        {

            var result = await _seguridad.ResetPassword(emailAddress);

            if (result!=null)
            {
                _mailService.SendMail(new MailData()
                {
                    EmailBody = "Clic al link para resetear su clave",
                    EmailSubject = "Resetear Clave",
                    EmailToId = "enriquillo_manon@hotmail.com", //result.UsuCorreoElectronico,
                    EmailToName = result.UsuNombres
                });
            }

            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
    [Route("Register")]
    [HttpPost()]
    public async Task<UsuarioSistemaDTO> Login(UsuarioSistemaDTO userInformation)
    {
        try
        {

            var result = await _seguridad.Register(userInformation);
 
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

}
