namespace Product.Domain.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Converte número para formato padrão de erro
        /// </summary>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ToErrorCode(this int errorValue) => errorValue.ToString().PadLeft(3, '0');

    }
}