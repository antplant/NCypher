using System.Text;

namespace NCypher
{
    internal interface IExpression
    {
        void Write(StringBuilder builder);
    }
}