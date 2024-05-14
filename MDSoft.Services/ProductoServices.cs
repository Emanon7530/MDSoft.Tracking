using AutoMapper;
using MDSoft.Data.Common;
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
    public class ProductoServices
    {
        IRepositorio<Producto> _Repolote;
        IMapper _mapper;

        public ProductoServices(IMapper mapper )
        {
            _Repolote = new Repositorio<Producto>();
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductoDTO>> GetAll()
        {
            IEnumerable<ProductoDTO> result = null;

            IEnumerable<Producto> compra = await _Repolote.TraerTodos();

            if (compra.Count() > 0)
            {
                result = _mapper.Map<IEnumerable<ProductoDTO>>(compra);
            }

            return result;

        }

        public async Task<ProductoDTO> GetByCode(string productCode)
        {
            ProductoDTO result = null;

            var _param = new ParametrosDeQuery<Producto>(1, 100);

            _param.Where = x => x.ProCodigo.Equals(productCode);

            var products = await _Repolote.EncontrarPor(_param);

            if (products != null)
            {
                result = _mapper.Map<ProductoDTO>(products.FirstOrDefault());
            }

            return result;

        }

        public async Task<ProductoDTO> GetByBarCode(string barCode)
        {
            try
            {
                ProductoDTO result = null;

                var _param = new ParametrosDeQuery<Producto>(1, 100);

                _param.Where = x => x.ProReferencia.Equals(barCode);

                var products = await _Repolote.EncontrarPor(_param);

                if (products != null)
                {
                    result = _mapper.Map<ProductoDTO>(products.FirstOrDefault());
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
