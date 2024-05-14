using MDSoft.Data.Common;
using MDSoft.Data.Interface;
using MDSoft.Tracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MDSoft.Data.Repository
{
    public class Repositorio<T> : IRepositorio<T> where T : class, new()
    {
        readonly DbContext _context;

        public DbContext Context => _context;

        public Repositorio()
        {
            _context = new MovilBusiness5StdContext();
        }

        public async Task<int> Actualizar(T entidad)
        {
            var db = Context;
            db.Entry(entidad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> ExecuteProcedureNonQuery(T entidad, string storeProcedureName, string[] parameterNames, string[] values)
        {
            var db = Context;
            var result = await db.Set<T>().FromSqlRaw<T>(storeProcedureName, parameterNames, values).ToListAsync();

            return result;
        }

        public async Task<int> Agregar(T entidad)
        {
            var db = Context;
            //(Entidad)entidad.CreatedDate = DateTime.Now;
            db.Entry(entidad).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            return await db.SaveChangesAsync();
        }

        public async Task<int> Eliminar(int id)
        {
            var db = Context;
            var entidad = new T();// { Id = id };

            db.Entry(entidad).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return await db.SaveChangesAsync();
        }

        public IEnumerable<T> BuscarPorQuery(Expression<Func<T, bool>> whereTrue)
        {
            var db = Context;

            IQueryable<T> query = db.Set<T>();

            // Not Tracking Entities
            query.Where(whereTrue);

            var result = query.ToList();

            return result;
        }

        public async Task<IEnumerable<T>> EncontrarPor(ParametrosDeQuery<T> parametrosDeQuery,
                                                       string? IncluirPropiedades = null)
        {
            var db = Context;

            IQueryable<T> query = db.Set<T>();


            if (IncluirPropiedades != null)
            {
                foreach (var includeProperty in IncluirPropiedades.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Not Tracking Entities
            if (parametrosDeQuery.Where != null)
            {
                query = query.Where(parametrosDeQuery.Where).AsNoTracking();
            }

            query = query.Take(parametrosDeQuery.Top).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> EncontrarPor(Expression<Func<T, bool>> where,
                                                       Func<T, Object>? orderBy = null,
                                                       string? IncluirPropiedades = null)
        {
            //var orderByClass = ObtenerOrderBy(parametrosDeQuery);
            //Expression<Func<T, bool>> whereTrue = x => true;
            //var where = (parametrosDeQuery.Where == null) ? whereTrue : parametrosDeQuery.Where;

            var db = Context;

            IQueryable<T> query = db.Set<T>();


            if (IncluirPropiedades != null)
            {
                foreach (var includeProperty in IncluirPropiedades.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Not Tracking Entities
            query.Where(where).AsNoTracking();

            if (orderBy != null)
            {
                query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }

        private OrderByClass ObtenerOrderBy(ParametrosDeQuery<T>? parametrosDeQuery)
        {
            if (parametrosDeQuery.OrderBy == null && parametrosDeQuery.OrderByDescending == null)
            {
                //return new OrderByClass(x => x.Id, true);
            }

            return (parametrosDeQuery.OrderBy != null)
                ? new OrderByClass(parametrosDeQuery.OrderBy, true) :
                new OrderByClass(parametrosDeQuery.OrderByDescending, false);
        }

        public async Task<T> ObtenerPorId(int id, string? IncluirPropiedades = null)
        {
            var db = Context;
            IQueryable<T> query = db.Set<T>();

            if (IncluirPropiedades != null)
            {
                foreach (var includeProperty in IncluirPropiedades.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(); // (x => x.Id == id);
        }

        public async Task<int> Contar(Expression<Func<T, bool>> where)
        {
            var db = Context;
            return await db.Set<T>().Where(where).CountAsync();
        }

        public async Task<IEnumerable<T>> TraerTodos()
        {
            using var db = Context;
            return await db.Set<T>().AsNoTracking().ToListAsync();
        }

        private class OrderByClass
        {

            public OrderByClass()
            {

            }

            public OrderByClass(Func<T, object> orderBy, bool isAscending)
            {
                OrderBy = orderBy;
                IsAscending = isAscending;
            }


            public Func<T, object> OrderBy { get; set; }
            public bool IsAscending { get; set; }
        }
    }
}