using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
namespace MDSoft.Tracking.Services
{
    public class LoteLimpiezaServices 
    {
        IRepositorio<LotesLimpieza> _Repolote;
        IMapper _mapper;

        public LoteLimpiezaServices(IMapper mapper )
        {
            _Repolote = new Repositorio<LotesLimpieza>();
            _mapper=mapper;
        }

    }
}
