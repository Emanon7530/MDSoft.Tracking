using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Tracking.DTOs;

/// <summary>
/// Maestro de Productos
/// </summary>
public partial class ProductoDTO : ObservableObject
{
    /// <summary>
    /// ID Producto
    /// </summary>
    [ObservableProperty]
    public int proId ;

    /// <summary>
    /// ID Linea fk:Lineas
    /// </summary>
    [ObservableProperty]
    public int? linId ;

    /// <summary>
    /// Descripcion 
    /// </summary>
    [ObservableProperty]
    public string proDescripcion ;

    /// <summary>
    /// Precio
    /// </summary>
    [ObservableProperty]
    public decimal? proPrecio ;

    /// <summary>
    /// Precio
    /// </summary>
    [ObservableProperty]
    public decimal? proPrecio2 ;

    /// <summary>
    /// Precio 3 Cuando no se usa LipCodigo
    /// </summary>
    [ObservableProperty]
    public decimal? proPrecio3 ;

    /// <summary>
    /// Precio mínimo
    /// </summary>
    [ObservableProperty]
    public decimal? proPrecioMin ;

    /// <summary>
    /// Código
    /// </summary>
    [ObservableProperty]
    public string proCodigo ;

    /// <summary>
    /// Codigo de Barra
    /// </summary>
    [ObservableProperty]
    public string proReferencia ;

    /// <summary>
    /// Unidades por Producto
    /// </summary>
    [ObservableProperty]
    public decimal? proUnidades ;

    /// <summary>
    /// Cantidad
    /// </summary>
    [ObservableProperty]
    public decimal? proCantidad ;

    /// <summary>
    /// ID Categoria 1
    /// </summary>
    [ObservableProperty]
    public int? cat1Id ;

    /// <summary>
    /// ID Categoria 2  fk:Categoria2
    /// </summary>
    [ObservableProperty]
    public int? cat2Id ;

    /// <summary>
    /// ID Categoria 3  fk:Categoria3
    /// </summary>
    [ObservableProperty]
    public int? cat3Id ;

    /// <summary>
    /// Descripcion 1
    /// </summary>
    [ObservableProperty]
    public string proDescripcion1 ;

    /// <summary>
    /// Descripción 2
    /// </summary>
    [ObservableProperty]
    public string? proDescripcion2 ;

    /// <summary>
    /// Descripción
    /// </summary>
    [ObservableProperty]
    public string? proDescripcion3 ;

    /// <summary>
    /// Datos 1
    /// </summary>
    [ObservableProperty]
    public string? proDatos1 ;

    /// <summary>
    /// Datos 2
    /// </summary>
    [ObservableProperty]
    public string? proDatos2 ;

    /// <summary>
    /// Datos 3
    /// </summary>
    [ObservableProperty]
    public string? proDatos3 ;

    /// <summary>
    /// Impuesto
    /// </summary>
    [ObservableProperty]
    public decimal? proItbis ;

    /// <summary>
    /// Selectivo
    /// </summary>
    [ObservableProperty]
    public decimal? proSelectivo ;

    /// <summary>
    /// Impuesto AdValorem
    /// </summary>
    [ObservableProperty]
    public decimal? proAdValorem ;

    /// <summary>
    /// Indicador detalle
    /// </summary>
    [ObservableProperty]
    public bool? proIndicadorDetalle ;

    /// <summary>
    /// Código descuento
    /// </summary>
    [ObservableProperty]
    public string? proCodigoDescuento ;

    /// <summary>
    /// Cargo por Distribucion
    /// </summary>
    [ObservableProperty]
    public decimal? proCargoDistribucion ;

    /// <summary>
    /// Codigo Sector  fk:Sectores
    /// </summary>
    [ObservableProperty]
    public string? secCodigo ;

    /// <summary>
    /// Usuario que creo o modifico el registro
    /// </summary>
    [ObservableProperty]
    public string? usuInicioSesion ;

    /// <summary>
    /// Fecha Creacion/Actualizacion 
    /// </summary>
    [ObservableProperty]
    public DateTime? proFechaActualizacion ;

    /// <summary>
    /// Codigo Unidad de Medida fk:UsosMultiples Grupo:UnmCodigo
    /// </summary>
    [ObservableProperty]
    public string? unmCodigo ;

    /// <summary>
    /// Imagen producto
    /// </summary>
    [ObservableProperty]
    public string? proImg ;

    /// <summary>
    /// Identificador Unico
    /// </summary>
    [ObservableProperty]
    public Guid rowguid ;

    /// <summary>
    /// Peso
    /// </summary>
    [ObservableProperty]
    public decimal? proPeso ;

    /// <summary>
    /// Volumen
    /// </summary>
    [ObservableProperty]
    public decimal? proVolumen ;

    [ObservableProperty]
    public string? proGrupoProductos ;

    [ObservableProperty]
    public string? proLotes ;

    [ObservableProperty]
    public decimal? proHolgura ;

    [ObservableProperty]
    public string? proInventarios ;

    [ObservableProperty]
    public string? proListaPrecios ;

    [ObservableProperty]
    public decimal? proDescuentoMaximo ;

    [ObservableProperty]
    public decimal? proCantidadMinVenta ;

    [ObservableProperty]
    public decimal? proCantidadMaxVenta ;

    [ObservableProperty]
    public decimal? proCantidadMultiploVenta ;

    [ObservableProperty]
    public string? proColor ;

    [ObservableProperty]
    public string? proPaisOrigen ;

    [ObservableProperty]
    public string? proAnio ;

    [ObservableProperty]
    public string? proMedida ;

    [ObservableProperty]
    public string? proEstado ;

    [ObservableProperty]
    public Guid? proAtributo1 ;

    [ObservableProperty]
    public Guid? proAtributo2 ;
}
