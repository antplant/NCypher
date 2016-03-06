using System.Text;

namespace NCypher.Expressions
{
    public interface IExpression
    {
        void Write(StringBuilder builder);
    }
}