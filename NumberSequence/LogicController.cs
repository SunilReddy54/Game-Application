using System;

public class LogicController
{
    public static string lastSequence = "";
    public static string currentSequence = "";
    private static string numberSequence = "";
    public static string NumberSeq { get { return numberSequence; } }
    private static int counter = 0;

    public static int CheckSequence(char s)
    {
        currentSequence += s;

        if (counter < numberSequence.Length && s == numberSequence[counter++])
            return numberSequence.Length - counter;
        return -1;
    }

    private static string Reverse(string s)
    {
        char[] array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }

    private static void ResetCounter()
    {
        lastSequence = currentSequence;
        currentSequence = "";
        counter = 0;
        numberSequence = Reverse(numberSequence);
    }

    public static int GenerateNewNumber()
    {
        int n = UnityEngine.Random.Range(0, 10);
        numberSequence += n.ToString();
        ResetCounter();
        return n;
    }

    public static void ResetGame()
    {
        lastSequence = "";
        currentSequence = "";
        numberSequence = "";
        counter = 0;
    }
}
