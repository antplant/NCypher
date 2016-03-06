using System;
using System.Collections.Generic;
using System.Text;

namespace NCypher.Expressions
{
    public class PathExpression : IExpression
    {
        private string _alias;
        private readonly IList<IExpression> _expressions;

        public PathExpression(IList<IExpression> expressions)
        {
            _expressions = expressions;
        }

        public void Write(StringBuilder builder)
        {
            builder.Append($"{_alias} = ");
        }

        public PathExpression WithAlias(string alias)
        {
            _alias = alias;
            return this;
        }

        public NodeExpression Node(Func<NodeExpression, NodeExpression> func)
        {
            _expressions.Add(this);
            return func(new NodeExpression(_expressions));
        }
    }
}