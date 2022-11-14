namespace Brainfuck.Js;

public static class Program
{
    private const string EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

    private const string False = "![]";

    private const string True = "!![]";

    private const string Zero = "+![]";

    private const string One = "+!![]";

    private const string Infinity = $"(({One}/{Zero})+[])";

    private const string ObjectToString = "({}+[])";

    private const string EmptyString = "([]+[])";

    private static readonly Dictionary<char, string> AlphabetMap = new()
    {
        ['t'] = $"({True}+[])[{Number(0)}]",
        ['r'] = $"({True}+[])[{Number(1)}]",
        ['u'] = $"({True}+[])[{Number(2)}]",
        ['e'] = $"({True}+[])[{Number(3)}]",
        ['f'] = $"({False}+[])[{Number(0)}]",
        ['a'] = $"({False}+[])[{Number(1)}]",
        ['l'] = $"({False}+[])[{Number(2)}]",
        ['s'] = $"({False}+[])[{Number(3)}]",
        ['I'] = $"{Infinity}[{Number(0)}]",
        ['n'] = $"{Infinity}[{Number(1)}]",
        ['i'] = $"{Infinity}[{Number(3)}]",
        ['y'] = $"{Infinity}[{Number(7)}]",
        ['o'] = $"{ObjectToString}[{One}]",
        ['b'] = $"{ObjectToString}[{Number(2)}]",
        ['j'] = $"{ObjectToString}[{Number(3)}]",
        ['c'] = $"{ObjectToString}[{Number(5)}]",
        [' '] = $"{ObjectToString}[{Number(7)}]",
        ['O'] = $"{ObjectToString}[{Number(8)}]",
        ['\\'] = $"((/\\\\/)+[])[{One}]",
    };

    public static void Main(string[] args)
    {
        AlphabetMap.Add('S', $"({EmptyString}[{Constructor()}]+[])[{Number(9)}]");
        AlphabetMap.Add('g', $"({EmptyString}[{Constructor()}]+[])[{Number(14)}]");
        AlphabetMap.Add('p', $"((/-/)[{Constructor()}]+[])[{Number(14)}]");
        AlphabetMap.Add('C', $"(()=>{{}})[{Constructor()}]({Obfuscate("return escape")}+[])()(/\\\\/)[{Number(3)}]");

        Console.WriteLine($"(()=>{{}})[{Constructor()}]({Obfuscate("console.log(\"Hello World!\");")}+[])()");

        Environment.Exit(0);
    }

    private static string Number(int i) =>
        i switch
        {
            0 => Zero,
            1 => One,
            _ => string.Join('+', Enumerable.Repeat(One[1..], i)),
        };

    private static string Constructor() => $"{Obfuscate("constructor")}+[]";

    private static string ToStringMethod() => $"{Obfuscate("toString")}+[]";

    private static string Obfuscate(string code) =>
        string.Join(
            '+',
            code.Select(
                static c => AlphabetMap.TryGetValue(c, out var mappedValue)
                    ? mappedValue
                    : GetUnmappedChar(c)));

    private static string GetUnmappedChar(char c)
    {
        return char.IsLower(c) ? GetLowercaseLetter(c) : $"{FromCharCode()}({Number(c)})";

        static string GetLowercaseLetter(char c) =>
            $"({Number(EnglishAlphabet.IndexOf(c) + 10)})[{ToStringMethod()}]({Number(EnglishAlphabet.IndexOf(c) + 11)})";

        static string FromCharCode() => $"{EmptyString}[{Constructor()}][{Obfuscate("fromCharCode")}+[]]";
    }
}