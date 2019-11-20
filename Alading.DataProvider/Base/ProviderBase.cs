using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using System.Linq.Expressions;
using System.Data.EntityClient;

namespace Alading.DataProvider
{
    public partial class DataProviderClass : IAlading
    {
        #region 数据访问对象实例
        private static DataProviderClass dataProvider = null;
        public static DataProviderClass Instance()
        {
            if (dataProvider == null)
            {
                dataProvider = new DataProviderClass();
            }

            return dataProvider;
        }
        #endregion

        #region WhereIn条件表达式树
        private static Expression<Func<TElement, bool>> BuildWhereInExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            if (!values.Any())
                return e => false;

            var equals = values.Select(value => (Expression)Expression.Equal(propertySelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
        #endregion

        /// <summary>
        /// 获得SqlClient数据库连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static EntityConnection GetSQLEntityConnection(string connectionString, string modelName)
        {
            EntityConnectionStringBuilder ecsb = new EntityConnectionStringBuilder();
            ecsb.Metadata = string.Format(@"res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", modelName);
            ecsb.Provider = "System.Data.SqlClient";              
            ecsb.ProviderConnectionString = connectionString;
            EntityConnection ec = new EntityConnection(ecsb.ToString());
            return ec;
        }
    }
}
