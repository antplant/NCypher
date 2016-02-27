using System.Collections.Generic;
using System.Text;

namespace NCypher
{
    public class NodeExpression : IExpression
    {
        private string _alias;
        private readonly List<string> _labels = new List<string>();

        public NodeExpression WithAlias(string alias)
        {
            _alias = alias;
            return this;
        }

        public NodeExpression WithLabel(string label)
        {
            _labels.Add(label);
            return this;
        }
        
        public NodeExpression WithLabels(params string[] labels)
        {
            _labels.AddRange(labels);
            return this;
        }

        public void Write(StringBuilder builder)
        {
            builder.Append("(");

            if (!string.IsNullOrEmpty(_alias))
            {
                builder.Append(_alias);
            }

            if (_labels.Count > 0)
            {
                builder.Append($":{string.Join(", ", _labels)}");
            }

            builder.Append(")");
        }
    }
}