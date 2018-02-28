using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public static class Converter
    {
        public static TDto ConvertToDto<TDto, TEntity>(this TEntity entity, Func<TEntity, TDto> map)
        {
            return map.Invoke(entity);
        }

        public static TEntity ConvertToEntity<TEntity, TDto>(this TDto dto, Func<TDto, TEntity> map)
        {
            return map.Invoke(dto);
        }
    }
}
