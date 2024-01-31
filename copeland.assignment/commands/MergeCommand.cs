using Autofac;
using copeland.assignment.core;
using copeland.assignment.models;
using Oakton;

namespace copeland.assignment.commands;

[Description("Merge two files and produce a third file")]
public class MergeCommand : OaktonCommand<MergeCommandInput>, IResolvedCommand
{
    
    private ILifetimeScope _lifetimeScope;
    
    public MergeCommand()
    {
        Usage("Merge two files and produce a third file")
            .Arguments(x => x.FilePath1, x => x.FilePath2, x => x.OutputFile);
    }
    
    public override bool Execute(MergeCommandInput input)
    {
        Console.WriteLine("Getting file merge service");
        var fileMergeService = _lifetimeScope.Resolve<IFileMergeService<DeviceDataTableModel>>();
        
        Console.WriteLine("Merging files");
        fileMergeService.Merge<DeviceDataTableModel>(input.FilePath1, input.FilePath2, input.OutputFile);
        
        Console.WriteLine($"Merge completed successfully to {input.OutputFile}");
        return true;
    }

    public void SetLifetimeScope(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }
}