using FluentValidation.Resources;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Validators.CustomValidators
{
    public class CPFValidator : PropertyValidator
    {
        public CPFValidator() : base(new LanguageStringSource(nameof(CPFValidator)))
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var cpf = context.PropertyValue as string;
            string tempCpf, digito;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma, resto;

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            try
            {
                long.Parse(cpf);
            }
            catch
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
                
            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
                
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);

        }
    }
}
