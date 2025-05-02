using System.Runtime.InteropServices;

namespace CoD2_Coloured_Name_Generator;

internal static partial class ConsoleColorManager
{
    #region True color import and setup

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetConsoleMode(IntPtr handle, out int mode);

    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial IntPtr GetStdHandle(int handle);

    private const int STD_OUTPUT_HANDLE = -11;
    private const int ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    internal static void EnableTrueColor()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var handle = GetStdHandle(STD_OUTPUT_HANDLE);

            if (!GetConsoleMode(handle, out int mode))
            {
                Console.WriteLine("Warning: Failed to get console mode. True color support may not work.");
                return;
            }

            if (!SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING))
            {
                Console.WriteLine("Warning: Failed to enable true color support. Output may not appear as expected.");
            }
        }
        else
        {
            // On Unix-like systems, ANSI escape codes typically work without setup,
            Console.WriteLine("Note: Running on a non-Windows platform. True color support depends on your terminal.");
        }
    }

    #endregion

    internal static ColorCode[] ColorCodes { get; } =
    [
        new(new(0, 0, 0), "0", "Black"),
        new(new(255, 0, 0), "1", "Red"),
        new(new(0, 255, 0), "2", "Green"),
        new(new(255, 255, 0), "3", "Yellow"),
        new(new(0, 0, 255), "4", "Blue"),
        new(new(0, 255, 255), "5", "Lightblue"),
        new(new(255, 0, 255), "6", "Purple"),
        new(new(255, 255, 255), "7", "White"),
        new(new(153, 153, 153), "9", "Grey")
    ];

    internal static string BlackColorIndex = "0"; // Extra checks needed for black, because it works different in COD 2

    internal static void SetForegroundColor(RGB rgb) =>
        Console.Write($"\x1b[38;2;{rgb.Red};{rgb.Green};{rgb.Blue}m");

    internal static void SetForegroundBaseColor() =>
        SetForegroundColor(new(255, 255, 255));

    internal static void SetForegroundHighlightColor() =>
        SetForegroundColor(new(0, 125, 255));

    internal static void SetForegroundWarningColor() =>
        SetForegroundColor(new(255, 125, 0));

    internal static void SetBackgroundColor(RGB rgb) =>
        Console.Write($"\x1b[48;2;{rgb.Red};{rgb.Green};{rgb.Blue}m");
}