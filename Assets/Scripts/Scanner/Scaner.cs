using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner 
{
    public List<Token> tokenList = new List<Token>();
    string textSource;
    private int start = 0;
    private int current = 0;
    private int line = 1;
    Token currentToken;
    int tokenIndex;

    /*Se determina el archivo de texto que se quiere escanear*/ 
    public void SetTextToScan(string text) 
    {
        textSource = text; 
    }

    //Se resetea el scanner para que el siguiente texto o las siguientes ves que se use, no empiece en el final y todas sus variables estén libres de información
    void ResetScan()
    {
        tokenList.Clear();
        start = 0;
        current = 0;
        line = 1;
        currentToken = null;
        tokenIndex = 0;
        textSource = null;
    }

    /*Avanza en el string actual letra por letra
     *return el caracter actual del string*/
    char advance()
    {
        current++;
        return textSource[current - 1];
    }

    /*Pregunta si el caracter actual ya llegó al final o si se pasó del string.
     *return verdadero o falso*/
    bool isAtEnd()
    {
        return current >= textSource.Length;
    }


    List<Token> scanTokens()
    {
        start = current;
        while (!isAtEnd())
        { 
            scanToken();
        }
        tokenList.Add(new Token(TokenType.EOF, null));
        return tokenList;
    }

    void addToken(TokenType token)
    {
        tokenList.Add(new Token(token, null));
    }

    void addToken(TokenType token, string lexeme)
    {
        tokenList.Add(new Token(token, lexeme));
    }

    public void scanToken()
    {
        char c = advance();
        if (char.IsDigit(c)) 
        {
            addToken(TokenType.INTEGER, c.ToString());
        }else if(c == '+')
        {
            addToken(TokenType.PLUS);
        }
        else
        {
            Debug.Log("Unavailable character");
        }

    }

    

}
