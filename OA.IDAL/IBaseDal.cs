using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
    /// <summary>
    /// 数据公共接口,增删改查分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDal<T> where T:class,new()
    {
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        IQueryable<T> LoadPageEntities<TSort>(int pageIndex, int pageSize, out int pageCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TSort>> sortLambda, bool isAsc = true);

        void DeleteEntity(T entity);

        void EditEntity(T entity);

        T AddEntity(T entity);
    }
}
