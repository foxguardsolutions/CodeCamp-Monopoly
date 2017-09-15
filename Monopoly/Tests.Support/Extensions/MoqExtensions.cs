using System;
using System.Linq.Expressions;
using Moq;
using Moq.Language;
using Moq.Language.Flow;

namespace Tests.Support.Extensions
{
    // The setup methods contained here can be called on a Mock to set up mocking of a method that takes out or ref arguments.
    // When set up using one of these methods, any argument passed to the mocked method will match the setup.
    // https://gist.github.com/7Pass/1c6b329e85ca29071f42
    public static class MoqExtensions
    {
        public static ISetup<T, TResult> SetupIgnoreArgs<T, TResult>(
            this Mock<T> mock,
            Expression<Func<T, TResult>> expression)
            where T : class
        {
            expression = new MakeAnyVisitor().VisitAndConvert(
                expression, "SetupIgnoreArgs");

            return mock.Setup(expression);
        }

        public static ISetupSequentialResult<TResult> SetupSequenceIgnoreArgs<T, TResult>(
            this Mock<T> mock,
            Expression<Func<T, TResult>> expression)
            where T : class
        {
            expression = new MakeAnyVisitor().VisitAndConvert(
                expression, "SetupIgnoreArgs");

            return mock.SetupSequence(expression);
        }

        private class MakeAnyVisitor : ExpressionVisitor
        {
            protected override Expression VisitConstant(ConstantExpression node)
            {
                if (node.Value != null)
                    return base.VisitConstant(node);

                var method = typeof(It)
                    .GetMethod("IsAny")
                    .MakeGenericMethod(node.Type);

                return Expression.Call(method);
            }
        }
    }
}
