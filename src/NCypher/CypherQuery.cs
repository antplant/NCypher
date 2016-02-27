using System;
using System.Text;

namespace NCypher
{
    public class CypherQuery
    {
        private readonly MatchExpression _expression;

        public CypherQuery(Func<MatchExpression, MatchExpression> func)
        {
            _expression = func(new MatchExpression());
        }

        public static CypherQuery Match(Func<MatchExpression, MatchExpression> func)
        {
            return new CypherQuery(func);
        }

        public void WriteTo(IQueryWriter writer)
        {
            var builder = new StringBuilder();

            _expression.Write(builder);
            writer.Write(builder.ToString());
        }
    }
}
