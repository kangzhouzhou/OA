using OA.DALFactory;
using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public abstract class BaseService<T> where T : class,new()
    {
        public BaseService()
        {
            CurrentDal = SetCurrentDal();
        }

        public IDBSession CurrentDBSession
        {
            get
            {
                return new DBSession();
            }
        }

        public IBaseDal<T> CurrentDal { get; set; }

        public abstract IBaseDal<T> SetCurrentDal();

        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
        }

        public virtual IQueryable<T> LoadPageEntities<TSort>(int pageIndex, int pageSize, out int pageCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, TSort>> sortLambda, bool isAsc = true)
        {
            return CurrentDal.LoadPageEntities<TSort>(pageIndex, pageSize, out pageCount, whereLambda, sortLambda, isAsc);
        }

        public void DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
        }

        public void EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
        }

        public void AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
        }

        public int SaveChange()
        {
            return CurrentDBSession.SaveChange();
        }
    }
}
