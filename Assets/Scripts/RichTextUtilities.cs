
using System;

public static class RichTextUtilities
{
    /// <summary>
    /// Returns a new string that colors the passed string with the given color
    /// </summary>
    /// <param name="color"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string FontColor(string color, string text)
    {
        return String.Format("<color={0}>{1}</color>", color, text);
    }
}
