using NUnit.Framework;

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

            AssertQueryOutputs(query, "MATCH (n)");
        }

        [Test]
        public void MatchNode_WithLabel()
        {
            var query = CypherQuery.Match(m => m.Node(n => n.WithLabel("Foo")));
            AssertQueryOutputs(query, "MATCH (:Foo)");
        }

        [Test]
        public void MatchNode_WithMultipleLabels()
        {
            var query = CypherQuery.Match(m => m.Node(n => n.WithLabels("Foo", "Bar")));
            AssertQueryOutputs(query, "MATCH (:Foo, Bar)");
        }

        [Test]
        public void MatchNode_WithAliasAndLabel()
        {
            var query = CypherQuery.Match(m => m
                .Node(n => n
                    .WithAlias("n")
                    .WithLabel("Foo")));

            AssertQueryOutputs(query, "MATCH (n:Foo)");
        }

        [Test]
        [TestCase("SomeValue", "MATCH (n:Foo { SomeProp:\"SomeValue\" })")]
        [TestCase(1234, "MATCH (n:Foo { SomeProp:1234 })")]
        public void MatchNode_WithProperty(object value, string output)
        {
            var query = CypherQuery.Match(m => m
                .Node(n => n.WithAlias("n")
                    .WithLabel("Foo")
                    .WithProperty("SomeProp", value)));

            AssertQueryOutputs(query, output);
        }

        [Test]
        public void MatchRelationship()
        {
            var query = CypherQuery.Match(m => m
                .Node(n => n.WithAlias("n"))
                .RelatesTo()
                .Node(n => n.WithAlias("m")));

            AssertQueryOutputs(query, "MATCH (n)-[]->(m)");
        }

        [Test]
        public void MatchRelationhip_WithAlias()
        {
            var query = CypherQuery.Match(m => m
                .Node(n => n.WithAlias("n"))
                .RelatesTo(r => r.WithAlias("r"))
                .Node(n => n.WithAlias("m")));

            AssertQueryOutputs(query, "MATCH (n)-[r]->(m)");
        }

        [Test]
        public void MatchRelationship_WithLabel()
        {
            var query = CypherQuery.Match(m => m.Node(n => n)
                .RelatesTo(r => r.WithLabel("Foo"))
                .Node(n => n));

            AssertQueryOutputs(query, "MATCH ()-[:Foo]->()");
        }

        private static void AssertQueryOutputs(CypherQuery query, string output)
        {
            Assert.AreEqual(output, query.Text);
        }
    }
}