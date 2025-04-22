﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface ISpecification<TEntity , Tkey> where TEntity : BaseEntity<Tkey>        
    {
        //Property Signture For Each Dynamic Part In Query
        public Expression<Func<TEntity , bool>> Criteria { get; } 

        List<Expression<Func<TEntity , object>>> IncludeExpressions {  get; }
    }
}
