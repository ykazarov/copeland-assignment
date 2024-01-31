using Autofac;
using Oakton;

namespace copeland.assignment.commands;

public class AutofacCommandCreator : ICommandCreator
{
    private readonly ILifetimeScope _scope;

    public AutofacCommandCreator(ILifetimeScope scope)
    {
        _scope = scope ?? throw new ArgumentNullException(nameof(scope));
    }
    
    public IOaktonCommand CreateCommand(Type commandType)
    {
        return (IOaktonCommand) _scope.Resolve(commandType);
    }

    public object CreateModel(Type modelType)
    {
        return _scope.Resolve(modelType);
    }
}