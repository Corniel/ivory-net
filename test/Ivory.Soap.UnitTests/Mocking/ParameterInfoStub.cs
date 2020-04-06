using System;
using System.Reflection;

namespace Ivory.Soap.UnitTests.Mocking
{
    public class ParameterInfoStub : ParameterInfo
    {
        public ParameterInfoStub(
            string name = null,
            Type parameterType = null)
        {
            Name = name;
            ParameterType = parameterType;
        }

        public override string Name { get; }
        public override Type ParameterType { get; }
    }
}
