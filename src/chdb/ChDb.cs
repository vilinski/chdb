using System.Runtime.InteropServices;

namespace ChDb;

internal static class NativeMethods
{
    const string __DllName = "libchdb.so";

    [DllImport(__DllName, EntryPoint = "query_stable", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern IntPtr query_stable(int argc, string[] argv);

    [DllImport(__DllName, EntryPoint = "free_result", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void free_result(IntPtr result);

    [DllImport(__DllName, EntryPoint = "query_stable_v2", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern IntPtr query_stable_v2(int argc, string[] argv);

    [DllImport(__DllName, EntryPoint = "free_result_v2", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void free_result_v2(IntPtr result);
}

public static class ChDb
{
    public static LocalResult Query(string query, string? format = null)
    {
        ArgumentNullException.ThrowIfNull(query);
        var argv = new[] {
            "clickhouse",
            "--multiquery",
            $"--query={query}",
            $"--output-format={format ?? "TabSeparated"}",
        };

        var ptr = NativeMethods.query_stable_v2(argv.Length, argv);
        var h = Marshal.PtrToStructure<LocalResult.Handle>(ptr);
        if (h == null)
            throw new ArgumentException($"Invalid query or format\nQUERY\n'{query}'\nFORMAT\n'{format}'");
        var res = new LocalResult(h);
        Marshal.FreeHGlobal(ptr);
        return res;
    }
}