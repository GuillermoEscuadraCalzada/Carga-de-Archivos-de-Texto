using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  static class KeyWords
{
    public static Dictionary<string, Token> dict = new Dictionary<string, Token>()
    {
        {"PROGRAM", new Token(TokenType.PROGRAM, "PROGRAM") },
        {"VAR", new Token(TokenType.VAR, "VAR") },
        {"DIV", new Token(TokenType.INTEGER_DIV, "DIV") },
        {"INTEGER", new Token(TokenType.INTEGER, "INTEGER") },
        {"REAL", new Token(TokenType.REAL, "REAL") },
        {"BEGIN", new Token(TokenType.BEGIN, "BEGIN") },
        {"END", new Token(TokenType.END, "END") }
    };
}
