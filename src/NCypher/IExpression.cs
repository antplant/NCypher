using System.Text;

namespace NCypher
{
    public interface IExpression
    {
        void Write(StringBuilder builder);
    }
}