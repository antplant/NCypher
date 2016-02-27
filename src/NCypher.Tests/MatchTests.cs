using System.Diagnostics;
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

        [Test]
        public void MatchRelationship()
        {
            var query = CypherQuery.Match(m => m
                .Node(n => n.WithAlias("n"))
                .RelatesTo()
                .Node(n => n.WithAlias("m")));

            var writer = MockRepository.GenerateMock<IQueryWriter>();

            writer.Expect(o => o.Write("(n)-[]->(m)"));
            query.WriteTo(writer);

            writer.VerifyAllExpectations();
        }

        [Test]
        public void MatchRelationhip_WithAlias()
        {
            var query =
                CypherQuery.Match(
                    m => m.Node(n => n.WithAlias("n")).RelatesTo(r => r.WithAlias("r")).Node(n => n.WithAlias("m")));

            var writer = MockRepository.GenerateMock<IQueryWriter>();

            writer.Expect(o => o.Write("(n)-[r]->(m)"));
            query.WriteTo(writer);

            writer.VerifyAllExpectations();
        }
    }
}