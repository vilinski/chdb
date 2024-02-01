using System.Runtime.InteropServices;

namespace ChDb;

public record LocalResult(string? Buf, string? ErrorMessage, ulong RowsRead, ulong BytesRead, TimeSpan Elapsed)
{
    public LocalResult(Handle h) : this(
        Marshal.PtrToStringUTF8(h.buf, h.len),
        Marshal.PtrToStringUTF8(h.error_message),
        h.rows_read,
        h.bytes_read,
        TimeSpan.FromSeconds(h.elapsed))
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Handle
    {
        public nint buf;
        public int len;
        public nint _vec;
        public double elapsed;
        public ulong rows_read;
        public ulong bytes_read;
        public nint error_message;
    }
}