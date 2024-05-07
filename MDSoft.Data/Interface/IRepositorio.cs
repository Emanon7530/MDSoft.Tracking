using MDSoft.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MDSoft.Data.Interface
{
	public interface IRepositorio<T>
	{
		Task<int> Agregar(T entidad);
		Task<int> Eliminar(int id);
		Task<int> Actualizar(T entidad);
		Task<int> Contar(Expression<Func<T, bool>> where);
		Task<T> ObtenerPorId(int id, string IncluirPropiedades = null);
		Task<IEnumerable<T>> EncontrarPor(ParametrosDeQuery<T> parametrosDeQuery, string IncluirPropiedades = null);
	}
}
