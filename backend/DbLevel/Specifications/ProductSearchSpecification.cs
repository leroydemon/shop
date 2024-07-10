using DbLevel.Interfaces;
using DbLevel.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class ProductSearchSpecification : BaseSpecification<Product>
    {
        public ProductSearchSpecification(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                
            }
        }
    }
}
