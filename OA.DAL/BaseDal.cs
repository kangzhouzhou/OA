using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using System.Data.Entity;
using OA.Model;

namespace OA.DAL
{
    public class BaseDal<T> where T : class,new()
    {
        public BaseDal()
        {
            _oaDb = OAContextFactory.CreateContext();
        }

        /// <summary>
        /// 只允许其子类进行访问的EF数据上下文
        /// </summary>
        protected DbContext _oaDb;

        /// <summary>
        /// 加载指定数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return OAContextFactory.CreateContext().Set<T>().Where<T>(whereLambda);
        }

        /// <summary>
        /// 分页提取信息
        /// </summary>
        /// <typeparam name="TSort"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="sortLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<TSort>(int pageIndex, int pageSize, out int pageCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TSort>> sortLambda, bool isAsc = true)
        {
            pageCount = _oaDb.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                return _oaDb.Set<T>().Where<T>(whereLambda).OrderBy<T, TSort>(sortLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return _oaDb.Set<T>().Where<T>(whereLambda).OrderByDescending<T, TSort>(sortLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        /// <summary>
        /// 设置为删除状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DeleteEntity(T entity)
        {
            _oaDb.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
        }

        /// <summary>
        /// 设置为更新状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void EditEntity(T entity)
        {
            _oaDb.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 将数据进行添加状态的标记
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            _oaDb.Entry(entity).State = EntityState.Added;
            return entity;
        }
    }
}
