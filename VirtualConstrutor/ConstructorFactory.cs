// ----------------------------------------------------------------
// Define a factory to make super-fast and generic builders.
// ----------------------------------------------------------------

using System.Linq.Expressions;
using System.Reflection;

namespace VirtualConstructor;

public static class ConstructorFactory<T>
{
    // ------------------------------------------------
    // Internal class to make builder lambda functions
    // ------------------------------------------------
    private static class BuilderFactoryImpl<BUILD_FUNCTION>
    {
        public static BUILD_FUNCTION
            buildBuilder(Type? derivedType)
        {
            // If no derived type specified, use type of T.
            // --------------------------------------------
            derivedType = derivedType ?? typeof(T);
            
            // Check to make sure derivedType is T.
            // ------------------------------------
            if (!derivedType.IsSubclassOf(typeof(T))
                && derivedType != typeof(T))
                throw new ArgumentException("Type error.");

            // Get the generic arguments from the functor.
            // -------------------------------------------
            var genericArguments =
                typeof(BUILD_FUNCTION).GetGenericArguments();

            // The last argument is the return value.
            // We are interested in the parameters types.
            var numArgs = genericArguments.Count();

            // Get the parameters and forget about the return type.
            var buildFunctionParams = genericArguments.Take(numArgs-1);

            // Find the constructor with the same parameters.
            var ctor = derivedType
                .GetConstructor(buildFunctionParams.ToArray());

            // Using LINQ, create Expression parameters with the names 
            // and types matching the constructor.
            var parameters = ctor?
                .GetParameters()?
                .Select(p => Expression
                    .Parameter(p.ParameterType, p.Name))?
                .ToArray() ?? new ParameterExpression[] { };


            if (ctor == null) throw new TargetException("No constructor found.");

            // A lambda expression that creates a new instance 
            // of the type of derivedType using the constructor
            // requested.  It's compiled for super-fast performance.
            var e = Expression.New(ctor, parameters);

            // Compile a lambda function that performs new.
            return Expression
                .Lambda<BUILD_FUNCTION>(e, parameters)
                .Compile();
        }
    }

    // ------------------------------------------------------------------------------
    // Helper functions to create the lambdas.
    // You can add as many additional parameters to support here as you like.
    // ------------------------------------------------------------------------------

    public static Func<T>
        make(Type derivedType)
    {
        return BuilderFactoryImpl<Func<T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, T>
        CreateBuilder<P1>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, P2, T>
        CreateBuilder<P1, P2>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, P2, T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, P2, P3, T>
        CreateBuilder<P1, P2, P3>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, P2, P3, T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, P2, P3, P4, T>
        CreateBuilder<P1, P2, P3, P4>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, P2, P3, P4, T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, P2, P3, P4, P5, T>
        CreateBuilder<P1, P2, P3, P4, P5>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, P2, P3, P4, P5, T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, P2, P3, P4, P5, P6, T>
        CreateBuilder<P1, P2, P3, P4, P5, P6>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, P2, P3, P4, P5, P6, T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, P2, P3, P4, P5, P6, P7, T>
        CreateBuilder<P1, P2, P3, P4, P5, P6, P7>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, P2, P3, P4, P5, P6, P7, T>>
            .buildBuilder(derivedType);
    }

    public static Func<P1, P2, P3, P4, P5, P6, P7, P8, T>
        CreateBuilder<P1, P2, P3, P4, P5, P6, P7, P8>(Type derivedType)
    {
        return BuilderFactoryImpl<Func<P1, P2, P3, P4, P5, P6, P7, P8, T>>
            .buildBuilder(derivedType);
    }
}