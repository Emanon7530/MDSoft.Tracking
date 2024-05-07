using AutoMapper;
using MDSoft.Data.Interface;
using MDSoft.Data.Repository;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSoft.Tracking.Services
{
    public class RepresentanteServices 
    {
        IRepositorio<Representante> _Repolote;
        IMapper _mapper;

        public RepresentanteServices(IMapper mapper )
        {
            _Repolote = new Repositorio<Representante>();
            _mapper = mapper;
        }
    }
}
