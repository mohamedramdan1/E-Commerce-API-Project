using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence
{
    // Helper Class tha that have Query Creation
    static class SpecificationEvaluator
    {
        // Create Query
        //_dbContect.Products.Where(P=<=>P.id == id).Include(P=>P.ProductBrand).Include(P=>P.ProductType)
        public static IQueryable<TEntity> CreateQuery<TEntity , Tkey>(IQueryable<TEntity> InputQuery , ISpecification<TEntity, Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
            var Query = InputQuery; //_dbContect.Products

            if (specification.Criteria is not null)
            {
                Query = Query.Where(specification.Criteria);//_dbContect.Products.Where(P=>P.id == id)
            }

            if (specification.OrderBy is not null)
            {
                Query = Query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IncludeExpressions is not null && specification.IncludeExpressions.Count > 0 )
            {
                Query = specification.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp)); //        //_dbContect.Products.Where(P=<P.id == id).Include(P=>P.ProductBrand).Include(P=>P.ProductType)

            }

            if (specification.IsPaginated)//true
            {
                Query = Query.Skip(specification.Skip).Take(specification.Take);
            }

            return Query;
        }
    }
}
