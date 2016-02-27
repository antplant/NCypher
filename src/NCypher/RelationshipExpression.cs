using System.Text;

namespace NCypher
{
    public class RelationshipExpression : IExpression
    {
        private string _alias;
        private string _label;

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
    }
}