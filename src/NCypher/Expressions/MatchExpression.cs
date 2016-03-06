using System;
using System.Collections.Generic;
using System.Text;

namespace NCypher.Expressions
{
    public class MatchExpression : IExpression
    {
        private readonly IList<IExpression> _expressions;

        public MatchExpression(IList<IExpression> expressions)
        {
            _expressions = expressions;
        }

        public void Write(StringBuilder builder)
        {
            builder.Append("MATCH ");
        }

        public NodeExpression Node(Func<NodeExpression, NodeExpression> func)
        {
            _expressions.Add(this);
            return func(new NodeExpression(_expressions));
        }

        public IExpression Path(Func<PathExpression, IExpression> func)
        {
            _expressions.Add(this);
            return func(new PathExpression(_expressions));
        }
    }
}