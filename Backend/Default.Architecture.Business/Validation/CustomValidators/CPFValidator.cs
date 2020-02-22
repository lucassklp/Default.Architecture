using FluentValidation.Resources;
using FluentValidation.Validators;

namespace Default.Architecture.Business.Validators.CustomValidators
{
    public class CPFValidator : PropertyValidator
    {
        public CPFValidator() : base(new LanguageStringSource(nameof(CPFValidator)))
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var cpf = context.PropertyValue as string;
            string tempCpf, digit;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum, rest;

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
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            rest = sum % 11;
            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = digit + rest.ToString();
            return cpf.EndsWith(digit);

        }
    }
}
