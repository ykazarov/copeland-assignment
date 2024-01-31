using Autofac;

namespace copeland.assignment.core;

public interface IResolvedCommand
{
    void SetLifetimeScope(ILifetimeScope lifetimeScope);
}