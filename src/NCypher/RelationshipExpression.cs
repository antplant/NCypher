using System.Text;

namespace NCypher
{
    public class RelationshipExpression : IExpression
    {
        private string _alias;

        public void Write(StringBuilder builder)
        {
            builder.Append($"-[{_alias}]->");
        }

        public RelationshipExpression WithAlias(string alias)
        {
            _alias = alias;
            return this;
        }
    }
}