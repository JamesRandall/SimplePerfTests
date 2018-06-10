using System;
using System.Reflection;

namespace simplePerformanceTest.WebApi.Net47.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}