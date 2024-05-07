using System;
using System.Linq.Expressions;

namespace MDSoft.Data.Common
{
	public class ParametrosDeQuery<T>
	{
		public ParametrosDeQuery(int pagina, int top)
		{
			Pagina = pagina;
			Top = top;
			Where = null;
			OrderBy = null;
			OrderByDescending = null;
		}

		public int Pagina { get; set; }
		public int Top { get; set; }
		public Expression<Func<T, bool>> Where { get; set; }
		public Func<T, object> OrderBy { get; set; }
		public Func<T, object> OrderByDescending { get; set; }
	}
}