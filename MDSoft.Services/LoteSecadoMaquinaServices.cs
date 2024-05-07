using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
namespace MDSoft.Tracking.Services
{
    public class LoteSecadoMaquinaServices 
    {
        IRepositorio<LotesSecadoMaquina> _Repolote;
        IMapper _mapper;

        public LoteSecadoMaquinaServices(IMapper mapper )
        {
            _Repolote = new Repositorio<LotesSecadoMaquina>();
            _mapper=mapper;
        }

    }
}
