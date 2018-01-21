using UnityEngine;

public class Number : MonoBehaviour
{
    public TextMesh _textMesh;
    public bool justReset, justLaunched, resetCount;
    public float resetTimer, dotLimit, timeLimit;
    public int instructionFontSize, defaultFontSize, fontSizeModifier;

    protected string display(string instructions, string reset, string defaultString)
    {
        if (justLaunched)
            return instructions;

        if (resetTimer > dotLimit)
            return reset;

        return defaultString;
    }

    protected int fontSizer(int startSize, int normalSize)
    {
        if (justLaunched)
            return startSize;
        return normalSize;
    }

}
