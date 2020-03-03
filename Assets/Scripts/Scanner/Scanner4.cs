
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false
public class Scanner4 : MonoBehaviour
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
        

        symbols(c); //Detectar qué simbolo es.

        if (!isAtEnd())
            scanToken();

        return c;
    }

    char symbols(char ch)
    {
        //Pregunta qué caracter puede ser el char 'ch', si es más, menos, multiplicación, división
        if (ch.Equals('+'))
            addToken(TokenType.PLUS); //El símbolo es de más
        else if (ch.Equals('-'))
            addToken(TokenType.MINUS); //El símbolo es de menos
        else if (ch.Equals('*'))
            addToken(TokenType.MULT);//El símbolo es de multiplicación
        else if (ch.Equals('/'))
            addToken(TokenType.DIV); //El símbolo es de división
        else if (ch.Equals(' '))
            advance();
        else
            Debug.Log("Unavailable character");

        return ch;
    }

    public int InterpretFunction()
    {
        scanToken();
        currentToken = getNextToken();
        int result = term();
        scanTokens();
        while (currentToken.token <= TokenType.PLUS && currentToken.token >= TokenType.MINUS)
        {
            Token t = currentToken;
            if (t.token.Equals(TokenType.PLUS))
            {
                currentToken = getNextToken();
                result += term();
            }
            if (t.token.Equals(TokenType.MINUS))
            {
                currentToken = getNextToken();
                result -= term();
            }
        }
#region comentario
        //scanToken();
        //int result = tokenIsDigit();
        //scanTokens();
        //currentToken = getNextToken();
        //Token token = currentToken; //El primer token que se encuentra en el arreglo

        //while (token.token != TokenType.EOF)
        //{
        //    while (token.token.Equals(TokenType.INTEGER))
        //    { //Mientras el token tenga el símbolo de entero
        //        string str = result.ToString() + tokenIsDigit().ToString(); 
        //        int result2 = int.Parse(str);
        //        result = result2;
        //        token = currentToken;
        //    }
        //    if (token.token.Equals(TokenType.PLUS))
        //    { //El token tiene el símbolo de suma
        //        result += tokenIsDigit();
        //    }
        //    else if (token.token.Equals(TokenType.MINUS))
        //    {//El token tiene el símbolo de resta
        //        result -= tokenIsDigit();
        //    }
        //    else if (token.token.Equals(TokenType.MULT))
        //    { //El token tiene el símbolo de multiplicación
        //        result *= tokenIsDigit();
        //    }
        //    else if (token.token.Equals(TokenType.DIV))
        //    { //El token tiene el símbolo de división
        //        result /= tokenIsDigit();
        //    }
        //    token = getNextToken();
        //}
#endregion

        return result;
    }

    int tokenIsDigit()
    {
        currentToken = getNextToken(); //El current Token irá avanzando entre la oración ingresada por el usuario
        Token t = currentToken;
        if (t.token.Equals(TokenType.INTEGER))
            return int.Parse(t.lexeme);
        else
            return 0;
    }

    /*Avanza en la lista de tokens y lo guarda en una variable*/
    Token getNextToken()
    {
            Token newToken ;
        if (tokenIndex < tokenList.Count)
        {
            newToken = tokenList[tokenIndex];
            tokenIndex++;
        }
        else
        {
            newToken = null;
        }
        return newToken;
    }

    int term()
    { //Preguntar si el token siguiente es multiplicación o división
        int result = factor(); //Recupera el valor si es que factor es un entero

        while (currentToken.token >= TokenType.MULT && currentToken.token <= TokenType.INTEGER_DIV)
        {//Mientras token sea MINUS o PLUS, haz lo siguiente
            Token token = currentToken;
            if (token.token.Equals(TokenType.MULT))
            { //Si el token actual es PLUS, haz una suma
                currentToken = getNextToken();
                result *= factor();
            }else if (token.token.Equals(TokenType.INTEGER_DIV))
            {//Si el token actual es MINUS, haz una resta
                currentToken = getNextToken();
                result /= factor();
            }
        }
        

        return result;
    }

    /*Recupera el primer elemento de lo que se ingresó en la caja de texto y determinar si es un entero o no
     *@return regrea un entero*/
    int factor()
    {

        string str = "";
        //while(currentToken.token.Equals(TokenType.INTEGER) )
        {
            if (currentToken.token.Equals(TokenType.INTEGER))
            { //Pregunta si el token actual es un entero
                str += currentToken.lexeme;
                currentToken = getNextToken();
            }
            else
                return 0;

        //    if (currentToken == null)
        //    {
        //        return int.Parse(str); //Convierte el string a entero y regresa ese valor

        //    }
        
        }
        return int.Parse(str); //Convierte el string a entero y regresa ese valor
    }

}
#endif