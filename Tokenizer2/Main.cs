using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;


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
    public string GetInfo()
    {
        return $"Token: {Text,-25} {Type,-20} at row {Row} index {Index}";
    }

}

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
            List<Token> tokens = tokenizeFile2(filePath);
            foreach (Token token in tokens)
            {
                if(token.Text != "EOF")
                {
                    Console.WriteLine(token.GetInfo());
                    continue;
                }
                Console.WriteLine($"{token.Text,-32} {token.Type,-20}" +
                        $" at row {token.Row} index {token.Index} ");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static List<Token> tokenizeFile2(string filePath)
    {
        List<Token> tokens = new List<Token>();
        int index=0, row = 1;
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                int start = 0;
                for(index = 0; index < line.Length; index++)
                {
                    if (line[index] == '-')
                    {
                        string token = line.Substring(start, index - start);
                        Token newToken = new Token(token, TokenType(token), row, start);
                        tokens.Add(newToken);
                        start = index + 1;

                        Token delimeter = new Token("-", "DELIMITER", row, index);
                        tokens.Add(delimeter);
                    }
                }
                if (start < line.Length)
                {
                    string token = line.Substring(start);
                    Token newToken = new Token(token, TokenType(token), row, start);
                    tokens.Add(newToken);
                }
                tokens.Add(new Token("\\n", "LINE BREAK", row, index));
                row++; 

            }
        }

        tokens.RemoveAt(tokens.Count - 1);
        tokens.Add(new Token("EOF", "End Of File", row-1, index));
        return tokens;
    }

    static String TokenType(String token)
    {
        String punctuationMarks = ".?!,:;()[]{}<>\"'/*&#~\\@^|`";
        //String operations = "+*-/%";
        if (string.IsNullOrWhiteSpace(token))
        {
            return "SPACE";
        }
        else if (double.TryParse(token.Replace(" ", "").Replace(",", ""), out _))
        {
            return "NUMBER";
        }
        else if (Regex.IsMatch(token.Trim(), @"^[a-zA-Z]{2,}$") || token.Trim().ToLower().Equals("a") 
            || token.Trim().ToLower().Equals("i"))
        {
            return "WORD";
        }
        else if (token.Trim().Length == 1 && Char.IsLetter(token[0]))
        {
            return "LETTER";
        }
        else if (Regex.IsMatch(token, @"\b[A-Za-z]+\b") && token.Trim().Split(' ').Count() > 1)
        {
            return "PHRASE";
        }
        else if (punctuationMarks.Contains(token))
        {
            return "PUNCTUATION";
        }
        else
        {
            return "SPECIAL TOKEN";
        }
    }
    static string[] getTokens(List<Token> tokens)
    {
        List<string> tokenList = new List<string>();
        for(int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].Text != "-")
            {
                tokenList.Add(tokens[i].Text);
            }
        }
        return tokenList.ToArray();
    }
}

