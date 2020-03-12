using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TokenType 
{ 
    INTEGER, 
    REAL,
    INTEGER_CONST,
    REAL_CONST,
    INTEGER_DIV,
    REAL_DIV,
    MINUS,
    PLUS,
    MULT,
    LPAR,
    RPAR,
    ID,
    ASSIGN,
    BEGIN,
    END,
    SEMI,
    DOT,
    PROGRAM,
    VAR,
    COLON,
    COMMA,
    EOF
}

public class Token
{
    /// <summary>
    /// El tipo de token de este token
    /// </summary>
    public TokenType tokenType;
    
    /// <summary>
    /// El nombre del token
    /// </summary>
    public string lexeme;
    

    /// <summary>
    /// Constructor de la clase token, recibe un tipo de Token y un nombre
    /// </summary>
    /// <param name="_token"></param>
    /// <param name="_lexeme"></param>
    public Token(TokenType _token, string _lexeme)
    {
        tokenType = _token;
        lexeme = _lexeme;
    }


     /// <summary>
     /// Esto manda el token en form ad estring
     /// </summary>
     /// <returns></returns>
    public string to_String()
    {

        return lexeme;
    }

}
