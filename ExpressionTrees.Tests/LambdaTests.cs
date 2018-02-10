using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Xunit;

namespace ExpressionTrees.Tests
{
    public delegate string IntToStr(int arg);

    public class Dependency
    {
        public virtual string Id(string str)
        {
            return str;
        }
    }
    
    public class LambdaTests
    {
        [Fact]
        public void Lambdas()
        {
            Expression<Func<int, string>> expressionLambda = x => x.ToString();
            Func<int, string> delegateLambda = x => x.ToString();
            IntToStr delegateLambda2 = x => x.ToString();
            var delegateLambda3 = expressionLambda.Compile();
           
            //var expressionLambda2 = x => x.ToString(); [CS0815]
            IEnumerable<int> enumerable = new int[] { };
            IQueryable<int>queryable = enumerable.AsQueryable();
            
            queryable.Select(expressionLambda);
            queryable.Select(delegateLambda);
            
            enumerable.Select(delegateLambda);
            //enumerable.Select(expressionLambda); [CS1929]
        }

        [Fact]
        public void Moq()
        {
            var mock = Mock.Of<Dependency>(x => x.Id(It.IsAny<string>()) == "Mock it!");
            var value = mock.Id("Value");
            
            Assert.Equal("Mock it!", value);
        }        
    }
}