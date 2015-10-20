﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Stormpath.SDK.Impl.Linq.Parsing.Expressions;
using Stormpath.SDK.Impl.Linq.Parsing.Expressions.ResultOperators;
using Stormpath.SDK.Impl.Linq.Parsing.Translators;
using Stormpath.SDK.Impl.Linq.QueryModel;

namespace Stormpath.SDK.Impl.Linq.Parsing
{
    internal sealed class CompilingExpressionVisitor : ExpressionVisitor
    {
        private FieldNameTranslator fieldNameTranslator
            = new FieldNameTranslator();

        public CollectionResourceQueryModel Model { get; private set; }
            = new CollectionResourceQueryModel();

        internal Expression VisitParsedExpression(ParsedExpression node)
        {
            throw new NotSupportedException($"{node.GetType()} is an unsupported expression.");
        }

        internal Expression VisitTake(TakeExpression node)
        {
            if (!this.Model.Limit.HasValue)
                this.Model.Limit = node.Value;

            return node;
        }

        internal Expression VisitSkip(SkipExpression node)
        {
            if (!this.Model.Offset.HasValue)
                this.Model.Offset = node.Value;

            return node;
        }

        internal Expression VisitOrderBy(OrderByExpression node)
        {
            string translatedFieldName = null;
            if (!this.fieldNameTranslator.TryGetValue(node.FieldName, out translatedFieldName))
                throw new NotSupportedException($"{node.FieldName} is not a supported field.");

            this.Model.OrderByTerms.Add(new OrderBy()
            {
                FieldName = translatedFieldName,
                Direction = node.Direction
            });

            return node;
        }

        internal Expression VisitFilter(FilterExpression node)
        {
            this.Model.FilterTerm = node.Value;

            return node;
        }

        internal Expression VisitResultOperator(ResultOperatorExpression node)
        {
            this.Model.ResultOperator = node.ResultType;

            if (node.ResultType == ResultOperator.First)
            {
                this.Model.ResultDefaultIfEmpty = (node as FirstResultOperator).DefaultIfEmpty;
            }
            else if (node.ResultType == ResultOperator.Single)
            {
                this.Model.ResultDefaultIfEmpty = (node as SingleResultOperator).DefaultIfEmpty;
            }

            return node;
        }
    }
}
