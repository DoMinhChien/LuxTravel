using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Core.Extensions
{
    public static class MapperExtension
    {
        public static TTarget MapTo<TTarget>(this object source) where TTarget : class
        {
            return Mapper.Map<TTarget>(source);

        }
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }

    }
}
