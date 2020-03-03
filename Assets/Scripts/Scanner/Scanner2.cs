
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner2 : MonoBehaviour
{
    #region region Variables
    public List<Token> tokenList = new List<Token>();
    
    string textSource;
    private int start = 0;
    private int current = 0;
    private int line = 1;
    Token currentToken;
    int tokenIndex;
    #endregion

    /*Se determina el archivo de texto que se quiere escanear*/
    public void SetTextToScan(string text)
    {
        textSource = text;
    }

    //Se resetea el scanner para que el siguiente texto o las siguientes ves que se use, no empiece en el final y todas sus variables estén libres de información
    public void ResetScan()
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
    
    //Escanea la lista de tokens por completo
    List<Token> scanTokens()
    {
        while (!isAtEnd())
        {
            start = current;
            scanToken();
        }
        tokenList.Add(new Token(TokenType.EOF, null));
        return tokenList;
    }

    //Se agrega un token de símbolos, no necesitan strings
    void addToken(TokenType token)
    {
        tokenList.Add(new Token(token, null));
    }

    //Se agrega un token con un valor de string
    void addToken(TokenType token, string lexeme)
    {
        tokenList.Add(new Token(token, lexeme));
    }

    //Se escanean todos los tokens dentro de los strings existentes
    public char scanToken()
    {
        string str = null;
        char c = advance();
        //c es un caracter
        if (char.IsDigit(c))
        {
            str += c.ToString();
            addToken(TokenType.INTEGER, c.ToString());
        }
        //C es un signo de más
        else if (c.Equals('+'))
        {
            str += c.ToString();
            addToken(TokenType.PLUS, "+");
        }
        //C es un símbolo menos
        else if (c.Equals('-'))
        {
            str += c.ToString();
            addToken(TokenType.MINUS);
        }
        else if (c.Equals(' '))
            c = ' ';
        //Es un símbolo inválido
        else
            Debug.Log("Unavailable character");
        if (!isAtEnd())
            scanToken();
        
        return c;
    }

    public int InterpretFunction()
    {
        currentToken = getNextToken();
        Token left = currentToken;
        if (left.token.Equals(TokenType.INTEGER))
            currentToken = getNextToken();
        else
        {
            Debug.Log("This is an incorrect token.\n");
            return 0;
        }
        Token middle = currentToken;
        if (middle.token.Equals(TokenType.PLUS))
            currentToken = getNextToken();
        else if (middle.token.Equals(TokenType.MINUS))
            currentToken = getNextToken();
        else
        {
            Debug.Log("This is an incorrect token.\n");
            return 0;
        }

        Token right = currentToken;

        if (right.token.Equals(TokenType.INTEGER))
            currentToken = tokenList[0];
        else
        {
            Debug.Log("This is an incorrect token.\n");
            return 0;
        }
        int result = 0;
        if (middle.token.Equals(TokenType.PLUS))
        {
            result = int.Parse(left.lexeme) + int.Parse(right.lexeme);
        }
        else if (middle.token.Equals(TokenType.MINUS))
        {
            result = int.Parse(left.lexeme) - int.Parse(right.lexeme);
        }

        return result;
    }

    Token getNextToken()
    {
        Token newToken = tokenList[tokenIndex];
        tokenIndex++;
        return newToken;
    }
}
