using Product.Domain.Extensions;
using System;

namespace Product.Domain.Utils
{
    /// <summary>
    /// Contrato de resposta de erro
    /// </summary>
    public class Error
    {
        protected Error()
        {

        }
        public Error(int code, string message)
        {
            Code = code.ToErrorCode();
            Message = message;
        }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error(Enum error)
        {
            Code = error.ToErrorCode();
            Message = error.Description();
        }

        /// <summary>
        /// Código do erro
        /// </summary>
        /// <example>054</example>
        public string Code { get; set; }

        /// <summary>
        /// Mensagem de explicação do erro ocorrido
        /// </summary>
        /// <example>Informe todos os campos obrigatórios</example>
        public string Message { get; set; }
    }
}