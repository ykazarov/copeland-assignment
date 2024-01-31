using Oakton;

namespace copeland.assignment.commands;

public class MergeCommandInput
{
    [Description("First file to merge")]
    public string FilePath1 { get; set; }
    
    [Description("Second file to merge")]
    public string FilePath2 { get; set; }
    
    [Description("Path to the output file")]
    public string OutputFile { get; set; }
}