using System.ComponentModel;

namespace Product.Domain.Error
{

    /// <summary>
    /// Enum que mapeia erros da aplicação
    /// </summary>
    public enum EErrors
    {

        /// <summary>
        /// Ocorreu um erro não conhecido
        /// </summary>
        [Description("Ocorreu um erro não conhecido")]
        ProductUndefinedError,

        /// <summary>
        /// O campo descrição precisa ter de 3 a 100 caracteres
        /// </summary>
        [Description("O campo descrição precisa ter de 3 a 100 caracteres")]
        ProductCreateAsyncDescriptionInvalid,

        /// <summary>
        /// A data de fabricação precisa ser antes da data de validade do produto
        /// </summary>
        [Description("A data de fabricação precisa ser antes da data de validade do produto")]
        ProductCreateAsyncManufacturingDateInvalid,

        /// <summary>
        /// Não foi encontrado um fornecedor com o identificador informado
        /// </summary>
        [Description("Não foi encontrado um fornecedor com o identificador informado")]
        ProductCreateAsyncSupplierIdInvalid,

        /// <summary>
        /// Produto já existe
        /// </summary>
        [Description("Produto já existe")]
        ProductCreateAsyncProductAlreadyExists,

        /// <summary>
        /// Produto não encontrado ou excluído
        /// </summary>
        [Description("Produto não encontrado ou excluído")]
        ProductGetAsyncProductNotFound,

        /// <summary>
        /// Fornecedor não encontrado ou excluído
        /// </summary>
        [Description("Fornecedor não encontrado ou excluído")]
        SupplierGetAsyncSupplierNotFound,

        /// <summary>
        /// Produto não encontrado ou já excluído
        /// </summary>
        [Description("Produto não encontrado ou já excluído")]
        ProductDeleteAsyncProductNotFound,

        /// <summary>
        /// Fornecedor não encontrado ou já excluído
        /// </summary>
        [Description("Fornecedor não encontrado ou já excluído")]
        SupplierDeleteAsyncSupplierNotFound,

        /// <summary>
        /// Descrição precisa ter de 3 a 100 caracteres
        /// </summary>
        [Description("Descrição precisa ter de 3 a 100 caracteres")]
        SupplierCreateAsyncDescriptionInvalid,

        /// <summary>
        /// Fornecedor já existe
        /// </summary>
        [Description("Fornecedor já existe")]
        SupplierCreateAsyncSupplierAlreadyExists,

        /// <summary>
        /// Produto não encontrado ou já excluído
        /// </summary>
        [Description("Produto não encontrado ou já excluído")]
        ProductUpdateAsyncProductNotFound,

        /// <summary>
        /// Fornecedor não encontrado ou já excluído
        /// </summary>
        [Description("Fornecedor não encontrado ou já excluído")]
        SupplierUpdateAsyncSupplierNotFound,

        /// <summary>
        /// Insira um CNPJ válido
        /// </summary>
        [Description("Insira um CNPJ válido")]
        SupplierCreateAsyncNationalRegistrationInvalid
    }
}