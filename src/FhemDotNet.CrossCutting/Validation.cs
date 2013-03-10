using System;
using System.Linq.Expressions;

namespace FhemDotNet.CrossCutting
{
    public class Validation
    {
        public static void NotNullOrEmpty(Expression<Func<string>> expr)
        {
            if (!string.IsNullOrEmpty(expr.Compile()()))
                return;

            var param = (MemberExpression)expr.Body;
            throw new ArgumentNullException("The parameter '" + param.Member.Name + "' cannot be null or empty.");
        }
    }
}