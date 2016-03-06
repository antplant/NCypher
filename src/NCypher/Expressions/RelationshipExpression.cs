using System;
using System.Collections.Generic;
using System.Text;

namespace NCypher.Expressions
{
    public class RelationshipExpression : IExpression
    {
        private string _alias;
        private string _label;
        private readonly IList<IExpression> _expressions;

        public RelationshipExpression(IList<IExpression> expressions)
        {
            _expressions = expressions;
        }

        public void Write(StringBuilder builder)
        {
            builder.Append($"-[{_alias}");

            if (!string.IsNullOrEmpty(_label))
            {
                builder.Append($":{_label}");
            }

            builder.Append("]->");
        }

        public RelationshipExpression WithAlias(string alias)
        {
            _alias = alias;
            return this;
        }

        public RelationshipExpression WithLabel(string label)
        {
            _label = label;
            return this;
        }

        public NodeExpression Node(Func<NodeExpression, NodeExpression> func)
        {
            _expressions.Add(this);
            return (func(new NodeExpression(_expressions)));
        }
    }
}