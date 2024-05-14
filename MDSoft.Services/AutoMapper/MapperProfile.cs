using AutoMapper;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.Dto;
using MDSoft.Tracking.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MDSoft.Tracking.Services.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            /// Map To Entity
            CreateMap<ComprasProducto, ComprasProductoDTO>().ReverseMap();
            CreateMap<ComprasProductosDetalle, ComprasProductosDetalleDTO>().ReverseMap();
            CreateMap<Proveedore, ProveedoreDTO>().ReverseMap();
            CreateMap<Representante, RepresentanteDTO>().ReverseMap();
            CreateMap<RecepcionesCompra, RecepcionesCompraDTO>().ReverseMap();
            CreateMap<RecepcionesComprasDetalle, RecepcionesComprasDetalleDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<ComprasDetalle, ComprasDetalleDTO>().ReverseMap();
            CreateMap<UsuarioSistema, UsuarioSistemaDTO>().ReverseMap();
            CreateMap<LotesFermentacion, LotesFermentacionDTO>().ReverseMap();
            CreateMap<LotesFermentacionDetalle, LotesFermentacionDetalleDTO>().ReverseMap();
            CreateMap<LotesSecadoMaquina, LotesSecadoMaquinaDTO>().ReverseMap();
            CreateMap<LotesSecadoMaquinaDetalle, LotesSecadoMaquinaDetalleDTO>().ReverseMap();
            CreateMap<LotesLimpieza, LotesLimpiezaDTO>().ReverseMap();
            CreateMap<LotesLimpiezaDetalle, LotesLimpiezaDetalleDTO>().ReverseMap();
            CreateMap<LotesSecadoNatural, LotesSecadoNaturalDTO>().ReverseMap();
            CreateMap<LotesSecadoNaturalDetalle, LotesSecadoNaturalDetalleDTO>().ReverseMap();
        }

    }
}
