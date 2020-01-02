using OA.DALFactory;
using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IBLL
{
    public interface IBaseService<T> where T : class,new()
    {
        IDBSession CurrentDBSession { get; }

        IBaseDal<T> CurrentDal { get; set; }

        IBaseDal<T> SetCurrentDal();

        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        IQueryable<T> LoadPageEntities<TSort>(int pageIndex, int pageSize, out int pageCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TSort>> sortLambda, bool isAsc = true);

        void DeleteEntity(T entity);

        void EditEntity(T entity);

        void AddEntity(T entity);

        int SaveChange();
    }
}
