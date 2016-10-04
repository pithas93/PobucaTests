using System;
using System.Linq.Expressions;
using System.Reflection;

namespace JPB_Tests
{

    public class Class1
    {
        static void Main(string[] args)
        {
            var domain = "matrix";
            Check(() => domain);
            Console.ReadLine();
        }

        public static void Check<T>(Expression<Func<T>> expr)
        {
            var body = ((MemberExpression)expr.Body);
            Console.WriteLine("Name is: {0}", body.Member.Name);
            Console.WriteLine("Value is: {0}", ((FieldInfo)body.Member)
           .GetValue(((ConstantExpression)body.Expression).Value));
        }
    }
}

//Ab Bb -1
//Bb Ab 1
//Ab Ab 0