using System.Runtime.InteropServices;

namespace ChDb;

public record Session
{
    public string? Format { get; init; }
    public string? DataPath { get; init; }
    public string? UdfPath { get; init; }
    public string? LogLevel { get; init; }

    public LocalResult Execute(string query, string? format = null)
    {
        var argv = new[] {
            "clickhouse",
            "--multiquery",
            $"--output-format={format ?? Format ?? "TabSeparated"}",
            $"--query={query}",
            $"--path={DataPath}",
            $"--udf_path={UdfPath}",
            $"--log_level={LogLevel}",
        }
            .ToArray();
        var argc = argv.Length;
        var ptr = NativeMethods.query_stable(argc, argv);
        var h = Marshal.PtrToStructure<LocalResult.Handle>(ptr) ?? throw new InvalidOperationException("null handle");
        var result = new LocalResult(h);
        Marshal.FreeHGlobal(ptr);
        return result;
    }
}