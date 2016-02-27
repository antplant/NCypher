using System;
using System.Collections.Generic;
using System.Text;

namespace NCypher
{
    public class MatchExpression
    {
        private readonly List<IExpression> _expressions = new List<IExpression>();

        public void Write(StringBuilder builder)
        {
            foreach (var expression in _expressions)
            {
                expression.Write(builder);
            }
        }

        public MatchExpression Node(Func<NodeExpression, NodeExpression> func)
        {
            _expressions.Add(func(new NodeExpression()));
            return this;
        }

        public MatchExpression RelatesTo()
        {
            _expressions.Add(new RelationshipExpression());
            return this;
        }

        public MatchExpression RelatesTo(Func<RelationshipExpression, RelationshipExpression> func)
        {
            _expressions.Add(func(new RelationshipExpression()));
            return this;
        }
    }
}