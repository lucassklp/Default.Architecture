using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Default.Architecture.Validators.CustomValidators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TProperty> CPF<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CPFValidator());
        }

    }
}
