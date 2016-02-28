using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NCypher
{
    public class CypherQuery
    {
        private readonly IExpression _expression;

        private readonly IList<IExpression> _expressions = new List<IExpression>(); 

        public CypherQuery(Func<MatchExpression, IExpression> func)
        {
            _expressions.Add(func(new MatchExpression(_expressions)));
        }

        public string Text
        {
            get
            {
                var builder = new StringBuilder();
                WriteTo(builder);
                return builder.ToString();
            }
        }

        public static CypherQuery Match(Func<MatchExpression, IExpression> func)
        {
            return new CypherQuery(func);
        }

        public void WriteTo(IQueryWriter writer)
        {
            var builder = new StringBuilder();

            _expression.Write(builder);
            writer.Write(builder.ToString());
        }

        public void WriteTo(StringBuilder builder)
        {
            foreach (var expression in _expressions)
            {
                expression.Write(builder);
            }
        }
    }
}
