using System;
using System.Text;
using Veldrid.Sdl2;

namespace Kyvos.VeldridIntegration.Sdl2;
public static class DisplayInformation
{
    /// <summary>
    /// gets sdl display mode
    /// </summary>
    /// <returns>true if successfully retrived display mode, false if error</returns>
    public static unsafe bool GetDisplayMode(out SDL_DisplayMode mode)
    {
        SDL_DisplayMode l_mode;
        var res = Sdl2Native.SDL_GetDesktopDisplayMode(0, &l_mode);
        mode = l_mode;
        return res == 0;
    }


    public static int GetDisplayRefreshRate(Func<int> @default)
    {
        if (GetDisplayMode(out SDL_DisplayMode mode))
        {
            return mode.refresh_rate;
        }
        return @default();

    }

    public static int GetDisplayRefreshRate() => GetDisplayRefreshRate(() => 60);

    public static (int width, int height) GetDisplayResolution(Func<(int, int)> @default)
    {
        if (GetDisplayMode(out SDL_DisplayMode mode))
        {
            return (mode.w, mode.h);
        }

        return @default();
    }

    public static (int width, int height) GetDisplayResolution() => GetDisplayResolution(() => (960, 960));

    public static int GetDisplayWidth() => GetDisplayResolution().width;

    public static int GetDisplayHeight() => GetDisplayResolution().height;

    public static int GetNumberOfDisplays()
    {
        return Sdl2Native.SDL_GetNumVideoDisplays();
    }

    public static unsafe string GetSDLError()
    {
        return GetString(Sdl2Native.SDL_GetError());
    }

    private static unsafe string GetString(byte* start)
    {
        if (start != null)
        {
            int charCount = 0;
            while (start[charCount] != 0)//0 byte indicates end of string as always
            {
                charCount++;
            }

            return Encoding.UTF8.GetString(start, charCount);
        }
        return "";
    }

}

