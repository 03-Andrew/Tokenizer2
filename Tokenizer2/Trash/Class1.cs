using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer2.TextFiles
{
    internal class Class1
    {
        /*
         * int start = 0;
                int curr = 0;
                while (curr < line.Length)
                {
                    if (line[curr] == '-')
                    {
                        string token = line.Substring(start, curr - start);
                        Token newToken = new Token(token, TokenType(token), row, start);
                        tokens.Add(newToken);
                        // Update start to the character after the hyphen
                        start = curr + 1;
                    }
                    curr++;
                }

                // Create a token for the remaining part of the line after the last hyphen (if any)
                if (start < line.Length)
                {
                    string token = line.Substring(start);
                    Token newToken = new Token(token, TokenType(token), row, start);
                    tokens.Add(newToken);
                }

                tokens.Add(new Token("\\n", "LINE BREAK", row, index - 1));
                row++; // Increment row number outside the loop
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
                    Token newToken = new Token(token, TokenType(token), row, index);
                    tokens.Add(newToken);
                    index += token.Length + 1;
                }
                tokens.Add(new Token("\\n", "LINE BREAK", row, index - 1));
                row++;
                index = 0;
            }
        }

        tokens.RemoveAt(tokens.Count - 1);

        tokens.Add(new Token("EOF", "EOF", row, index));
        return tokens;
    }
    static List<Token> tokenizeFile3(string filePath)
    {
        List<Token> tokens = new List<Token>();
        int row = 1;
        string hold = "";
        bool isHold = false;

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                int start = 0;
                bool hasDelimiter = false; // Flag to check if the line has a delimiter
                for (int index = 0; index < line.Length; index++)
                {
                    if (line[index] == '-')
                    {
                        string token = line.Substring(start, index - start);
                        if (isHold)
                        {
                            token = hold + token; // Combine with the previously held token
                            isHold = false;
                        }
                        Token newToken = new Token(token, TokenType(token), row, start);
                        tokens.Add(newToken);

                        // Add delimiter token
                        Token delimiter = new Token("-", "DELIMITER    ", row, index);
                        tokens.Add(delimiter);

                        start = index + 1;
                        hasDelimiter = true; // Update the flag since a delimiter is found
                    }
                }

                // Create a token for the remaining part of the line after the last hyphen (if any)
                string lastToken = line.Substring(start);
                if (!string.IsNullOrEmpty(lastToken))
                {
                    if (!hasDelimiter && isHold)
                    {
                        hold += lastToken; // Concatenate with the previous held token
                    }
                    else
                    {
                        Token newToken = new Token(lastToken, TokenType(lastToken), row, start);
                        tokens.Add(newToken);
                    }
                }
                else if (isHold && !hasDelimiter)
                {
                    // Handle the case when the line ends with a hyphen and no subsequent text
                    Token newToken = new Token(hold, TokenType(hold), row, start - hold.Length);
                    tokens.Add(newToken);
                    isHold = false;
                }

                // Check for holding the token for the next line
                if (line.Length > 0 && line[line.Length - 1] != '-')
                {
                    hold = lastToken;
                    isHold = true;
                }

                // Add line break token
                tokens.Add(new Token("\\n", "LINE BREAK   ", row, line.Length));
                row++;
            }
        }

        tokens.RemoveAt(tokens.Count - 1); // Remove the extra line break token
        tokens.Add(new Token("EOF", "EOF          ", row - 1, 0)); // Correct the row for EOF
        return tokens;
    }
    static List<Token> tokenizeFile4(string filePath)
    {
        List<Token> tokens = new List<Token>();
        int index, row = 1;
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            bool continuesFromPreviousLine = false;
            string partialToken = "";
            while ((line = sr.ReadLine()) != null)
            {
                int start = 0;
                if (continuesFromPreviousLine)
                {
                    partialToken += line;
                }
                
                for (index = 0; index < line.Length; index++)
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

                if (!line.EndsWith("-"))
                {
                    partialToken = "";
                    continuesFromPreviousLine = false;
                    tokens.Add(new Token("\\n", "LINE BREAK", row, index));
                    row++;
                }
                else
                {
                    continuesFromPreviousLine = true;
                }
            }
        }

        tokens.RemoveAt(tokens.Count - 1);

        tokens.Add(new Token("EOF", "EOF", row, 0));
        return tokens;
    }

         **/
    }
}
