using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Tokenizer
{
    static void Main()
    {
        Console.Write("Type in file: ");
        string fileName = "Textfiles\\" + Console.ReadLine() + ".txt";
        string currentDirectory = Directory.GetCurrentDirectory();
        string projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.FullName;
        string filePath = Path.Combine(projectDirectory, fileName);

        try
        {
            List<Token> tokens = tokenizeFile(filePath);
            foreach (Token token in tokens)
            {
                Console.WriteLine($"Token: {token.Text} ({token.Type}) at row {token.Row} index {token.Index}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    static List<Token> tokenizeFile(string filePath)
    {
        List<Token> tokens = new List<Token>();
        int index = 0, row = 1;
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] hyphenTokens = line.Split('-');

                foreach (string token in hyphenTokens)
                {
                    Token newToken = new Token(token, ClassifyToken(token), row, index);
                    tokens.Add(newToken);
                    index += token.Length + 1;
                }

                tokens.Add(new Token("\\n", "LINE BREAK", row, index));
                row++;
                index = 0;
            }
        }

        tokens.RemoveAt(tokens.Count - 1);

        tokens.Add(new Token("EOF", "EOF", row, index));
        return tokens;
    }
    static String ClassifyToken(String token)
    {
        String punctuationMarks = ".?!,:;()[]{}<>\"'/*&#~\\@^|`";
        if (string.IsNullOrWhiteSpace(token))
        {
            return "SPACE";
        }
        if (Regex.IsMatch(token, @"^[a-zA-Z]+$"))
        {
            return "WORD";
        }
        if (token.Split(' ').Length > 1)
        {
            return "PHRASE";
        }
        if (punctuationMarks.Contains(token.Trim()))
        {
            return "PUNCTUATION";
        }
        else
        {
            return "SPECIAL CHARACTER";
        }
    }


}
class Token
{
    public string Text { get; }
    public string Type { get; }
    public int Row { get; }
    public int Index { get; }
    public Token(string Text, string Type, int Row, int Index)
    {
        this.Text = Text;
        this.Type = Type;
        this.Row = Row;
        this.Index = Index;
    }
}
