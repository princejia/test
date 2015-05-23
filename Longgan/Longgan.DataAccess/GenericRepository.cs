using Longgan.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Longgan.DataAccess
{
    public class GenericRepository<TModel> : Repository where TModel : Model
    {
        private DbSet<TModel> _dbSet = null;
        internal DbSet<TModel> DbSet
        {
            get
            {
                if (_dbSet == null) _dbSet = Context.Set<TModel>();
                return _dbSet;
            }
        }

        internal DbQuery<TModel> DbQuery
        {
            get
            {
                return DbSet.AsNoTracking();
            }
        }

        public virtual List<TModel> Get()
        {
            return DbQuery.ToList();
        }

        protected virtual List<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            return DbQuery.Where(filter).ToList();
        }

        protected virtual List<TModel> Get(Expression<Func<TModel, bool>> filter, Expression<Func<TModel, object>> include)
        {
            return DbQuery.Where(filter).Include(include).ToList();
        }

        protected virtual List<TModel> Get(Expression<Func<TModel, bool>> filter, Expression<Func<TModel, object>> include1, Expression<Func<TModel, object>> include2)
        {
            return DbQuery.Where(filter).Include(include1).Include(include2).ToList();
        }

        protected virtual List<TModel> Get(Expression<Func<TModel, bool>> filter, Expression<Func<TModel, object>> include1, Expression<Func<TModel, object>> include2, Expression<Func<TModel, object>> include3)
        {
            return DbQuery.Where(filter).Include(include1).Include(include2).Include(include3).ToList();
        }

        public virtual void Insert(params TModel[] entities)
        {
            foreach (TModel entity in entities)
            {
                DbSet.Add(entity);
            }

            if (entities.Length > 0) Context.SaveChanges();
        }

        public virtual void Update(params TModel[] entities)
        {
            foreach (TModel entity in entities)
            {
                RemoveHoldingEntityInContext(entity);

                if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }

            if (entities.Length > 0) Context.SaveChanges();
        }

        private Boolean RemoveHoldingEntityInContext(TModel entity)
        {
            var objContext = ((IObjectContextAdapter)Context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TModel>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        public virtual void UpdateColumns(TModel entity, params Expression<Func<TModel, object>>[] propertiesToUpdate)
        {
            RemoveHoldingEntityInContext(entity);

            if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
            foreach (Expression<Func<TModel, object>> propertyToUpdate in propertiesToUpdate)
            {
                Context.Entry(entity).Property(propertyToUpdate).IsModified = true;
            }

            Context.SaveChanges();
        }

        //public virtual void Upsert(Expression<Func<TModel, object>> identifierExpression, params TModel[] entities)
        //{
        //    foreach (TModel entity in entities)
        //    {
        //        DbSet.AddOrUpdate(identifierExpression, entities);
        //    }

        //    if (entities.Length > 0) Context.SaveChanges();
        //}

        public virtual void Delete(params TModel[] entities)
        {
            foreach (TModel entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached) DbSet.Attach(entity);
                DbSet.Remove(entity);
            }

            if (entities.Length > 0) Context.SaveChanges();
        }

        public virtual void Delete(params object[] id)
        {
            TModel entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        protected virtual void Delete(Expression<Func<TModel, bool>> filter)
        {
            Delete(DbSet.Where(filter).ToArray());
        }

        #region Use for store procedure

        /// <summary>
        /// Exec sp
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parameters">new SqlParameter() </param>
        /// <returns></returns>
        public static int ProcedureQuery(string sp, params SqlParameter[] parameters)
        {
            using (var context = new DatabaseContext())
            {
                using (context.Database.Connection)
                {
                    context.Database.Connection.Open();
                    var cmd = context.Database.Connection.CreateCommand();
                    cmd.CommandText = sp;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Retrieve Data
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<TElement> ProcedureQuery<TElement>(string sp, params object[] parameters)
        {
            string sql = "";
            List<TElement> items = new List<TElement>();
            using (var context = new DatabaseContext())
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    sql += "" + FormatSqlString(parameters[i]) + ",";
                }
                sql = "EXEC " + sp + " " + sql.TrimEnd(',');
                context.Database.CommandTimeout = 10000;
                var tempResult = context.Database.SqlQuery<TElement>(sql, parameters).ToList();
                if (tempResult != null && tempResult.Count() > 0)
                {
                    items = tempResult;
                }
            }
            return items;
        }

        public static IEnumerable<TElement> ProcedureQuery<TElement>(string sp)
        {
            string sql = "";
            List<TElement> items = new List<TElement>();
            using (var context = new DatabaseContext())
            {

                sql = "EXEC " + sp + " ";
                var tempResult = context.Database.SqlQuery<TElement>(sql).ToList();
                if (tempResult != null && tempResult.Count() > 0)
                {
                    items = tempResult;
                }
            }
            return items;
        }

        public int ProcedureExec(string sp, params object[] parameters)
        {
            string sql = "";
            int tempResult = 0;

            using (var context = new DatabaseContext())
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    sql += "" + FormatSqlString(parameters[i]) + ",";
                }
                sql = "EXEC " + sp + " " + sql.TrimEnd(',');

                tempResult = context.Database.ExecuteSqlCommand(sql, parameters);
            }
            return tempResult;
        }

        public int ProcedureExec(string sp)
        {
            string sql = "";
            int tempResult = 0;

            using (var context = new DatabaseContext())
            {
                sql = "EXEC " + sp + " ";
                tempResult = context.Database.ExecuteSqlCommand(sql);
            }
            return tempResult;
        }

        private static string FormatSqlString(object obj)
        {
            if (obj.GetType() == typeof(System.Boolean))
            {
                return ((bool)obj ? "1" : "0");
            }
            if (obj.GetType() == typeof(System.Guid))
            {
                return "'" + obj.ToString() + "'";
            }
            if (obj.GetType() == typeof(System.Int16) || obj.GetType() == typeof(System.Int32) || obj.GetType() == typeof(System.Int64) || obj.GetType() == typeof(System.Decimal) || obj.GetType() == typeof(System.Double))
            {
                return obj.ToString();
            }
            return obj.ToString();
        }

        #endregion

    }

    /// <summary>
    /// How to use:
    /// var d = Context.Database.SqlQueryForDynamic("select * from Profile");
    ///foreach (dynamic item in d)
    ///{
    ///    string s = item.Name;
    ///}
    /// </summary>
    public static class DatabaseExtensions
    {
        public static IEnumerable SqlQueryForDynamic(this Database db,
                string sql,
                params object[] parameters)
        {
            IDbConnection defaultConn = new System.Data.SqlClient.SqlConnection();

            return SqlQueryForDynamicOtherDB(db, sql, defaultConn, parameters);
        }

        public static IEnumerable SqlQueryForDynamicOtherDB(this Database db,
                      string sql,
                      IDbConnection conn,
                      params object[] parameters)
        {
            conn.ConnectionString = db.Connection.ConnectionString;

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            IDataReader dataReader = cmd.ExecuteReader();

            if (!dataReader.Read())
            {
                return null; //If no result, return Null
            }

            #region Build dynamic fields

            TypeBuilder builder = DatabaseExtensions.CreateTypeBuilder(
                          "EF_DynamicModelAssembly",
                          "DynamicModule",
                          "DynamicType");

            int fieldCount = dataReader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                DatabaseExtensions.CreateAutoImplementedProperty(
                  builder,
                  dataReader.GetName(i),
                  dataReader.GetFieldType(i));
            }

            #endregion

            dataReader.Close();
            dataReader.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            Type returnType = builder.CreateType();

            if (parameters != null)
            {
                return db.SqlQuery(returnType, sql, parameters);
            }
            else
            {
                return db.SqlQuery(returnType, sql);
            }
        }

        public static TypeBuilder CreateTypeBuilder(string assemblyName,
                              string moduleName,
                              string typeName)
        {
            TypeBuilder typeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
              new AssemblyName(assemblyName),
              AssemblyBuilderAccess.Run).DefineDynamicModule(moduleName).DefineType(typeName,
              TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            return typeBuilder;
        }

        public static void CreateAutoImplementedProperty(
                            TypeBuilder builder,
                            string propertyName,
                            Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = builder.DefineField(
              string.Concat(
                PrivateFieldPrefix, propertyName),
              propertyType,
              FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = builder.DefineProperty(
              propertyName,
              System.Reflection.PropertyAttributes.HasDefault,
              propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes = MethodAttributes.Public
              | MethodAttributes.SpecialName
              | MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = builder.DefineMethod(
                string.Concat(
                  GetterPrefix, propertyName),
                propertyMethodAttributes,
                propertyType,
                Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = builder.DefineMethod(
              string.Concat(SetterPrefix, propertyName),
              propertyMethodAttributes,
              null,
              new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }

    }
}
