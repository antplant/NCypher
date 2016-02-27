using System.Text;

namespace NCypher
{
    public class RelationshipExpression : IExpression
    {
        public void Write(StringBuilder builder)
        {
            builder.Append("-[]->");
        }
    }
}