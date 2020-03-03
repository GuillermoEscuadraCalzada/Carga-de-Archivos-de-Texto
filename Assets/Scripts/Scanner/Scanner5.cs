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

    /*Indicamos el  texto a escanear
     *@param[String text] texto fuente que se analizará*/
    public void setTextToScan(string text)
    {
        
        resetScan();
        textSource = text;
        scanTokens();
    }

    /*Se resetea el escaner, todos las variables vuelven a su estado principal*/
    private void resetScan()
    {
        tokenList.Clear();
        start = 0;
        current = 0;
        line = 1;
        currentToken = null;
        //textSource = null;
        tokenIndex = 0;
    }

    /*Escanea el caracter para detectar qué tipo de símbolo es 
     *@param[char ch] caracter a analizar
     *return regresa el caracter después de determinar token o lanzar error*/
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
            addToken(TokenType.REAL_DIV,currentChar.ToString()); //El símbolo es de división
        else if (ch.Equals('('))
            addToken(TokenType.LPAR); //El símbolo es de división
        else if (ch.Equals(')'))
            addToken(TokenType.RPAR); //El símbolo es de división
        else if (ch.Equals('{'))
        { //Añadir comentarios
            skip_comment();
        } else if (char.IsLetter(ch))
        { //Preguntas si el caracter es una letra
            identifier();
        } else if (ch.Equals(':') && peek().Equals('='))
        { //Preguntar si el char actual y el próximo es el siguiente string :=
            advance();
            advance();
            addToken(TokenType.ASSIGN, ":=");
        }
        else if (ch.Equals(';'))
        { //Preguntar si es punto y coma
            addToken(TokenType.SEMI, ch.ToString());
            advance();
        }else if (ch.Equals(':'))
        {
            addToken(TokenType.COLON, ch.ToString());
            advance();
        }
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

    void skip_comment()
    {
        while(!currentChar.Equals('}') && !currentChar.Equals('\0'))  { //Avanza hasta encontrar un final de línea o una llave de cierre
            advance();
        }
        advance();
    }
    void identifier()
    {
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
    }
    //Se escanean todos los tokens dentro de los strings existentes
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
        else if (currentChar.Equals(' '))
            currentChar = ' ';
        else
        {
            symbols(currentChar); //Detectar qué simbolo es.
        }

        if (!isAtEnd())
            scanToken();

        return currentChar;
    }

    char peek() {
        peek_pos = current + 1;
        if(peek_pos > textSource.Length)
        {
            return '\0';
        }
        
        return textSource[peek_pos];
    }

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

    /*Pregunt si el índice current está al final de la lista*/
    bool isAtEnd()
    {
        return current > textSource.Length;
    }

    /*Avanza dentro del texto fuente para regresarons un caracter
     *@return el caracter a analizar*/
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

    void getNumber() {
        string num = "";
        while(char.IsDigit(currentChar)&& !currentChar.Equals('\0'))
        {
            num += currentChar;
            advance();
        }
        if(currentChar == '.')
        {
            num += currentChar;
            advance();
            while(char.IsDigit(currentChar) && !currentChar.Equals('\0'))
            {
                num += currentChar;
            }
            addToken(TokenType.REAL, num);
        }
        else
        {
            num += currentChar;
            addToken(TokenType.INTEGER, num);
        }
    }

    /*Avanza en la lista de tokens y lo guarda en una variable*/
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

    int tokenIsDigit()
    {
        currentToken = getNextToken(); //El current Token irá avanzando entre la oración ingresada por el usuario
        Token t = currentToken;
        if (t.token.Equals(TokenType.INTEGER))
            return int.Parse(t.lexeme);
        else
            return -1;
    }

    public void printScanner ()
    {
        foreach(Token t in tokenList)
        {
            Debug.Log(t.to_String());
        }
    }
}
