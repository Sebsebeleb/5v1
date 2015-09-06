using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public static class TextUtilities
{
    // Mapping between keyword -> color. Basically what color the different keywords should be
    private static readonly Dictionary<String, String> keywordMapping = new Dictionary<String, String>()
    {
        {"stunned", Colors.Stunned},
        {"stun", Colors.Stunned},
        {"damage", Colors.DamageWord},
        {"applied", Colors.EffectApplied},
        {"apply", Colors.EffectApplied},
        {"heal", Colors.HealValue},
        {"you", Colors.You},

    };

    // The compiled regex for searching for keywords to prettify
    private static readonly Regex keywordReplacer = new Regex(string.Join("|", keywordMapping.Keys.ToArray()));

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

    /// <summary>
    /// Boldify a string
    /// </summary>
    /// <param name="text">The string to boldify</param>
    /// <returns>The same text surrounded with <b></b></returns>
    public static string Bold(string text){
        return String.Format("<b>{0}</b>", text);
    }

    // Colours common words. For example, turns "Stunned" into "<color=Colors.StunnedColor>Stunned</color>"
    /// <summary>
    /// Automatically colours common keywords used in the game with an appropriate colour.
    /// </summary>
    /// <param name="text">The text to prettify with colours</param>
    /// <returns>Prettified text withg colours</returns>
    public static string ImproveText(string text){
        return keywordReplacer.Replace(text, ColorfyKeywords);
    }

    /// <summary>
    /// Called on each keyword in each string during regex, surrounds the word with the correct colour tags
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    private static string ColorfyKeywords(Match m){
        string color = keywordMapping[m.Value.ToLower()];

        return string.Format("<color={0}>{1}</color>", color, m.Value);
    }
}
