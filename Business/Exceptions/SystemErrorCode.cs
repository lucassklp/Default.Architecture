using System;

namespace Business.Exceptions
{
    public static class SystemErrors
    {
        public static (int, string) UserAlreadyExists => (1, "<1> - Erro: O usuário já existe no sistema.");
    }
}
