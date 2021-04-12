using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace PathFollowing.Test.Main
{
    public class MainInputValueDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "-50", 0 };
            yield return new object[] { "-7", 0 };
            yield return new object[] { "-1", 0 };
            yield return new object[] { "0", 0 };
            yield return new object[] { "1", 1 };
            yield return new object[] { "2", 2 };
            yield return new object[] { "3", 3 };
            yield return new object[] { "4", 4 };
            yield return new object[] { "5", 5 };
            yield return new object[] { "6", 6 };
            yield return new object[] { "7", 7 };
            yield return new object[] { "8", 8 };
            yield return new object[] { "9", 9 };
            yield return new object[] { "10", 10 };
            yield return new object[] { "11", 11 };
            yield return new object[] { "12", 0 };
            yield return new object[] { "13", 0 };
            yield return new object[] { "101", 0 };
            yield return new object[] { "", 0 };
            yield return new object[] { " ", 0 };
            yield return new object[] { "falkjfaslkjfek", 0 };
            yield return new object[] { "\"\"\"\"", 0 };
            yield return new object[] { "!!\"#$$\"", 0 };
        }
    }
}
