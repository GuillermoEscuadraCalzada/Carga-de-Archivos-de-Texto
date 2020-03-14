using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scanner5 : MonoBehaviour
{
    List<Token> tokenList = new List<Token>();


    string textSource; //El texto que vamos a analizar
    private int start = 0; //Empieza en 0 dentro del analizar
    private int current = 0; //El índice del token actual
    private int line = 1; //Línea 1 de texto
    public Token currentToken; //Token actual de la lista de Tokens
    int tokenIndex; //índice del token
    char currentChar;
    int peek_pos=0;

    private void Start()
    {
        resetScan();
        scanTokens();
        //for(int i = 0; i < tokenList.Count; i++)
        //{
        //    Debug.Log(tokenList[i].lexeme);
        //}
    }


    /// <summary>
    /// Indicamos el  texto a escanear
    /// </summary>
    /// <param name="text"> texto fuente que se analizará</param>
    public void setTextToScan(string text)
    {
        resetScan();
        textSource = text;
        scanTokens();
    }

    /// <summary>
    /// Se resetea el escaner, todos las variables vuelven a su estado principal
    /// </summary>
    public void resetScan()
    {
        tokenList.Clear();
        start = 0;
        current = 0;
        line = 1;
        currentToken = null;
        //textSource = null;
        tokenIndex = 0;
    }


    /// <summary>
    /// Escanea el caracter para detectar qué tipo de símbolo es 
    /// </summary>
    /// <returns> char </returns>
    /// <param name="ch"></param>
    char Symbols(char ch)
    {
        //Pregunta qué caracter puede ser el char 'ch', si es más, menos, multiplicación, división
        if (ch.Equals('+'))
            addToken(TokenType.PLUS); //El símbolo es de más
        else if (ch.Equals('-'))
            addToken(TokenType.MINUS); //El símbolo es de menos
        else if (ch.Equals('*'))
            addToken(TokenType.MULT);//El símbolo es de multiplicación
        else if (ch.Equals('/'))
            addToken(TokenType.REAL_DIV,currentChar.ToString()); //El símbolo es de división
        else if (ch.Equals('('))
            addToken(TokenType.LPAR); //El símbolo es de división
        else if (ch.Equals(')'))
            addToken(TokenType.RPAR); //El símbolo es de división
        else if (ch.Equals('{'))
        { //Añadir comentarios
            Skip_Comment();
        } else if (char.IsLetter(ch))
        { //Preguntas si el caracter es una letra
            Identifier();
        } else if (ch.Equals(':') && peek() == '=')
        { //Preguntar si el char actual y el próximo es el siguiente string :=
            //char equals = peek();
            //if(equals == '=')
            //{
                advance();
                advance();
                addToken(TokenType.ASSIGN, ":=");
            //}
            //else
            //{
            //    addToken(TokenType.COLON, ch.ToString());
            //    advance();
            //}
        }
        else if (ch.Equals(';'))
        { //Preguntar si es punto y coma
            addToken(TokenType.SEMI, ch.ToString());
            advance();
        }
        //else if (ch.Equals(':'))
        //{
        //    addToken(TokenType.COLON, ch.ToString());
        //    advance();
        //}
        else if (ch.Equals('.'))
        {
            addToken(TokenType.DOT, ch.ToString());
            advance();
        }
        //C es un signo de más
        //else if (ch.Equals(' '))
        //    advance();
        else
            Debug.Log("Unavailable character");

        return ch;
    }

    /// <summary>
    /// Se salta todo lo que se encuentre dentro de las llaves '{' y '}'
    /// </summary>
    void Skip_Comment()
    {
        while(!currentChar.Equals('}') && !currentChar.Equals('\0'))  { //Avanza hasta encontrar un final de línea o una llave de cierre
            currentChar = advance();
        }
        advance();
    }

    /// <summary>
    /// Identifica si el string es una palabra reservada dentro del diccionario o es una palabra diferente, para lo cual lo meterá dentro de un ID
    /// </summary>
    void Identifier() { 
        string id = "";
        while(char.IsLetterOrDigit(currentChar) && !currentChar.Equals('\0'))
        {
            id += currentChar;
            currentChar = advance();
        }
        if (KeyWords.dict.ContainsKey(id))
        {
            tokenList.Add(KeyWords.dict[id]);
        }
        else
        {
            addToken(TokenType.ID, id);
        }
        Symbols(currentChar);
    }

    /// <summary>
    /// Se escanean todos los tokens dentro de los strings existentes
    /// </summary>
    /// <returns></returns>
    public char scanToken()
    {
        string str = null; //Guarda un string que sea nulo
        currentChar = advance();
        //c es un caracter
        if (char.IsDigit(currentChar))
        {
            //str += currentChar.ToString();
            //addToken(TokenType.INTEGER, currentChar.ToString());
            getNumber();
        }
        //C es un signo de más
        else if (currentChar.Equals(' ') || currentChar.Equals('\r') || currentChar.Equals('\n')|| currentChar.Equals('\t'))
            currentChar = ' ';
        else
        {
            Symbols(currentChar); //Detectar qué simbolo es.
        }

        if (!isAtEnd())
            scanToken();

        return currentChar;
    }

    char peek() {
        peek_pos = current;
        if(peek_pos > textSource.Length)
        {
            return '\0';
        }
        
        return textSource[peek_pos];
    }

    /// <summary>
    /// Se escanean todos los tokens que se encuentren dentro de la lista de tokens y se regresa esta misma lista
    /// </summary>
    /// <returns></returns>
    List<Token> scanTokens()
    {
        while (!isAtEnd())
        {
            //start = current;
            scanToken();
        }
        tokenList.Add(new Token(TokenType.EOF, null)); //Se añade un token que indica el final del texto fuente
        return tokenList; //Regresa la lista de tokens
    }

    /// <summary>
    /// Regresa la lista de tokens
    /// </summary>
    /// <returns></returns>
    public List<Token> GetTokenList()
    {
        return tokenList;
    }

    /// <summary>
    /// Se agrega un token de símbolos, no necesitan strings
    /// </summary>
    /// <param name="token"></param>
    void addToken(TokenType token)
    {
        tokenList.Add(new Token(token, null));
    }

    /// <summary>
    /// Se agrega un token con un valor de string
    /// </summary>
    /// <param name="token"></param>
    /// <param name="lexeme"></param>
    void addToken(TokenType token, string lexeme)
    {
        tokenList.Add(new Token(token, lexeme));
    }

    /// <summary>
    /// Pregunt si el índice current está al final de la lista
    /// </summary>
    /// <returns></returns>
    bool isAtEnd()
    {
        return current > textSource.Length;
    }

     /// <summary>
    /// Avanza dentro del texto fuente para regresarons un caracter
    ///
    /// </summary>
    /// <returns></returns>
    char advance() {

        /*Pregunta en qué posición se encuentra actualmente current dentro del texto a analizar  y se avanza uno constantemente*/
        if (current < textSource.Length)
        {
            current++;
            return textSource[current - 1];
        }
        else if(current == textSource.Length)
        {
            char c = textSource[current - 1];
            current++;
            return c;
        }
        else
            return ' ';
    }

    /// <summary>
    /// Busca obtener qué tipo de número es, si es entero o real (flotante)
    /// </summary>
    void getNumber() {
        string num = "";
        while(char.IsDigit(currentChar)&& !currentChar.Equals('\0'))
        { //Es un número, lo añade al strint y avanza
            num += currentChar;
            currentChar = advance();
        }
        if(currentChar.Equals('.'))
        { //Si el siguiente token es un punto repite el while hasta terminar en todos los decimales
            num += currentChar;
            currentChar = advance();
            while(char.IsDigit(currentChar) && !currentChar.Equals('\0'))
            { //Vuelve a avanzar el string dentro del char actual
                num += currentChar;
                currentChar = advance();
            }
            addToken(TokenType.REAL, num);
        }
        else
        { 
            addToken(TokenType.INTEGER, num);
        }
        Symbols(currentChar);
    }

    /// <summary>
    /// Avanza en la lista de tokens y lo guarda en una variable
    /// </summary>
    /// <returns></returns>
    public Token getNextToken()
    {
        Token newToken;
        if (tokenIndex < tokenList.Count){ //El token indx todavía o llega a la cantidad máxima de tokens
            newToken = tokenList[tokenIndex]; //Guarda la variable en esta posicón del tokenList
            tokenIndex++; //Aumenta el índice
        }
        else
        {
            newToken = null; //Regresa Nulo
        }
        return newToken; //Regresa el token
    }

    /// <summary>
    /// El token es un número
    /// </summary>
    /// <returns></returns>
    int tokenIsDigit()
    {
        currentToken = getNextToken(); //El current Token irá avanzando entre la oración ingresada por el usuario
        Token t = currentToken;
        if (t.tokenType.Equals(TokenType.INTEGER))
            return int.Parse(t.lexeme);
        else
            return -1;
    }

    /// <summary>
    /// Se imprime todo el escaner
    /// </summary>
    public void printScanner ()
    {
        foreach(Token t in tokenList)
        {
            Debug.Log("Token: " + t.to_String() + " Type: " + t.tokenType);
        }
    }
}
