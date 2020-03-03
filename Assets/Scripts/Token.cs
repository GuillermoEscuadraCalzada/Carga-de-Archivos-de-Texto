﻿using System.Collections;
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
    public TokenType token;
    public string typeName;
    public string lexeme;
    
    //Constructor de la clase token, la cual recibe un enum y el string que la determinará
    //Ejemplo: Int ---- 1
    //         Plus --- +
    public Token(TokenType _token, string _lexeme)
    {
        token = _token;
        lexeme = _lexeme;
    }

    /*Se convierte el tipo de string más el string que se introdujo
     *  Ejemplo: integer:1
     *  return un string*/
    public string to_String()
    {
        //TypeString();
        return lexeme + ":" + typeName;
    }

    public string TypeString()
    {
        if (token.Equals(TokenType.INTEGER)) {
            typeName = "integer";
        }else if (token.Equals(TokenType.MINUS))
        {
            typeName = "minus";
        }
        else if (token.Equals(TokenType.PLUS))
        {
            typeName = "plus";
        }
        return typeName;
    }
}
