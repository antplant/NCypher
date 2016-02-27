using System;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCypher.Tests
{
    public class MatchTests
    {
        [Test]
        public void MatchNode_WithAlias()
        {
            var query = CypherQuery.Match(m => m.
                Node(n => n
                    .WithAlias("n")));

            var writer = MockRepository.GenerateMock<IQueryWriter>();

            writer.Expect(o => o.Write("(n)"));
            query.WriteTo(writer);

            writer.VerifyAllExpectations();
        }

        [Test]
        public void MatchNode_WithLabel()
        {
            var query = CypherQuery.Match(m => m.Node(n => n.WithLabel("Foo")));
            var writer = MockRepository.GenerateMock<IQueryWriter>();

            writer.Expect(o => o.Write("(:Foo)"));
            query.WriteTo(writer);

            writer.VerifyAllExpectations();
        }

        [Test]
        public void MatchNode_WithAliasAndLabel()
        {
            var query = CypherQuery.Match(m => m
                .Node(n => n
                    .WithAlias("n")
                    .WithLabel("Foo")));

            var writer = MockRepository.GenerateMock<IQueryWriter>();

            writer.Expect(o => o.Write("(n:Foo)"));
            query.WriteTo(writer);

            writer.VerifyAllExpectations();
        }
    }

    public class CypherQuery
    {
        private readonly MatchExpression _expression;

        public CypherQuery(Func<MatchExpression, MatchExpression> func)
        {
            _expression = func(new MatchExpression());
        }

        public static CypherQuery Match(Func<MatchExpression, MatchExpression> func)
        {
            return new CypherQuery(func);
        }

        public void WriteTo(IQueryWriter writer)
        {
            var builder = new StringBuilder();

            _expression.Write(builder);
            writer.Write(builder.ToString());
        }
    }

    public class MatchExpression
    {
        private NodeExpression _node;

        public void Write(StringBuilder builder)
        {
            _node.Write(builder);
        }

        public MatchExpression Node(Func<NodeExpression, NodeExpression> func)
        {
            _node = func(new NodeExpression());
            return this;
        }
    }

    public class NodeExpression
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

    public interface IQueryWriter
    {
        void Write(string query);
    }
}
