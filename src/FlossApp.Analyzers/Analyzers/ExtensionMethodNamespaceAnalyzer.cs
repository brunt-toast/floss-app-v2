using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FlossApp.Analyzers.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ExtensionMethodNamespaceAnalyzer : DiagnosticAnalyzer
{
    private const string DiagnosticId = "FAX0001";
    private static readonly LocalizableString Title = "Extension method must be declared in standardised namespace";
    private static readonly LocalizableString MessageFormat = "Change namespace to *.Extensions.Fully.Qualified.Name";
    private static readonly LocalizableString Description = "Extension methods must be declared in namespace *.Extensions.Fully.Qualified.Name.";
    private const string Category = "Naming";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId, Title, MessageFormat, Category,
        DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(PerformAnalysis, SyntaxKind.MethodDeclaration);
    }

    private static void PerformAnalysis(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not MethodDeclarationSyntax methodDeclaration)
        {
            return;
        }

        bool isExtensionMethod = methodDeclaration.ParameterList.Parameters.Count > 0 &&
                                 methodDeclaration.ParameterList.Parameters[0].Modifiers.Any(SyntaxKind.ThisKeyword);
        if (!isExtensionMethod)
        {
            return;
        }

        // declaring class name
        var classDeclaration = methodDeclaration.Parent as ClassDeclarationSyntax;
        string className = classDeclaration?.Identifier.Text;

        // declaring class namespace
        string namespaceName = null;
        SyntaxNode current = classDeclaration;
        while (current != null && current is not NamespaceDeclarationSyntax)
        {
            current = current.Parent;
            if (current is NamespaceDeclarationSyntax namespaceDeclaration)
            {
                namespaceName = namespaceDeclaration.Name.ToString();
            }
        }

        if (namespaceName is null)
        {
            return;
        }

        // fully qualified param type 
        string firstParamQualifiedType = null;
        if (methodDeclaration.ParameterList.Parameters.Count > 0)
        {
            var firstParam = methodDeclaration.ParameterList.Parameters[0];
            var firstParamType = firstParam.Type;
            if (firstParamType is null)
            {
                return;
            }

            var semanticModel = context.SemanticModel;
            var typeInfo = semanticModel.GetTypeInfo(firstParamType, context.CancellationToken);
            firstParamQualifiedType = typeInfo.Type?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }


        bool success = namespaceName.EndsWith($".Extensions.{firstParamQualifiedType}Extensions");
        if (!success)
        {
            var diagnostic = Diagnostic.Create(Rule, methodDeclaration.GetLocation());
            context.ReportDiagnostic(diagnostic);
        }
    }

}
