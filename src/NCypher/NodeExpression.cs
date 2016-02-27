using System.Text;

namespace NCypher
{
    public class NodeExpression : IExpression
    {
        private string _alias;
        private string _label;

        public NodeExpression WithAlias(string alias)
        {
            _alias = alias;
            return this;
        }

        public NodeExpression WithLabel(string label)
        {
            _label = label;
            return this;
        }

        public void Write(StringBuilder builder)
        {
            builder.Append("(");

            if (!string.IsNullOrEmpty(_alias))
            {
                builder.Append(_alias);
            }

            if (!string.IsNullOrEmpty(_label))
            {
                builder.Append($":{_label}");
            }

            builder.Append(")");
        }
    }
}