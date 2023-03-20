const string englishAlphabet = "abcdefghijklmnopqrstuvwxyz";

const string @false = "![]";

const string @true = "!![]";

const string zero = "+![]";

const string one = "+!![]";

const string infinity = $"(({one}/{zero})+[])";

const string objectToString = "({}+[])";

const string emptyString = "([]+[])";

const string helloWorld = """console.log("Hello World!");""";

Dictionary<char, string> alphabetMap = new()
{
    ['t'] = $"({@true}+[])[{zero}]",
    ['r'] = $"({@true}+[])[{Number(1)}]",
    ['u'] = $"({@true}+[])[{Number(2)}]",
    ['e'] = $"({@true}+[])[{Number(3)}]",
    ['f'] = $"({@false}+[])[{Number(0)}]",
    ['a'] = $"({@false}+[])[{Number(1)}]",
    ['l'] = $"({@false}+[])[{Number(2)}]",
    ['s'] = $"({@false}+[])[{Number(3)}]",
    ['I'] = $"{infinity}[{Number(0)}]",
    ['n'] = $"{infinity}[{Number(1)}]",
    ['i'] = $"{infinity}[{Number(3)}]",
    ['y'] = $"{infinity}[{Number(7)}]",
    ['o'] = $"{objectToString}[{one}]",
    ['b'] = $"{objectToString}[{Number(2)}]",
    ['j'] = $"{objectToString}[{Number(3)}]",
    ['c'] = $"{objectToString}[{Number(5)}]",
    [' '] = $"{objectToString}[{Number(7)}]",
    ['O'] = $"{objectToString}[{Number(8)}]",
    ['\\'] = $"((/\\\\/)+[])[{one}]",
};

alphabetMap.Add('S', $"({emptyString}[{Constructor()}]+[])[{Number(9)}]");
alphabetMap.Add('g', $"({emptyString}[{Constructor()}]+[])[{Number(14)}]");
alphabetMap.Add('p', $"((/-/)[{Constructor()}]+[])[{Number(14)}]");
alphabetMap.Add('C', $"(()=>{{}})[{Constructor()}]({Obfuscate("return escape")}+[])()(/\\\\/)[{Number(3)}]");

Console.WriteLine($"(()=>{{}})[{Constructor()}]({Obfuscate(helloWorld)}+[])()");

Environment.Exit(0);

static string Number(int i) =>
    i switch
    {
        0 => zero,
        1 => one,
        _ => string.Join('+', Enumerable.Repeat(one[1..], i)),
    };

string Constructor() => $"{Obfuscate("constructor")}+[]";

string ToStringMethod() => $"{Obfuscate("toString")}+[]";

string Obfuscate(string code) =>
    string.Join(
        '+',
        code.Select(
            c => alphabetMap.TryGetValue(c, out var mappedValue)
                ? mappedValue
                : GetUnmappedChar(c)));

string GetUnmappedChar(char c)
{
    return (char.IsDigit(c), char.IsLower(c)) switch
    {
        (true, _) => $"{Number(int.Parse(new[] { c, }))}+[]",
        (_, true) => GetLowercaseLetter(),
        (_, false) => $"{FromCharCode()}({Number(c)})",
    };

    string GetLowercaseLetter() =>
        $"({Number(englishAlphabet.IndexOf(c) + 10)})[{ToStringMethod()}]({Number(englishAlphabet.IndexOf(c) + 11)})";

    string FromCharCode() => $"{emptyString}[{Constructor()}][{Obfuscate("fromCharCode")}+[]]";
}