using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false
public class Parser : MonoBehaviour
{
    public Scanner5 scanner;
    Token currentToken;

    public Parser (Scanner5 scanner)
    {
        this.scanner = scanner;
    }
    /// <summary>
    /// El bloque principal del programa, a partir de aquí, inicia todo
    /// </summary>
    /// <returns></returns>
    Node program () { 
        validateToken(TokenType.PROGRAM);
        Node varNode = variable();
        validateToken(TokenType.SEMI);
        Block _block = block();
        validateToken(TokenType.DOT);
        return null;
    }

    /// <summary>
    /// Se crea un nodo bloque donde las declaraciones
    /// </summary>
    /// <returns></returns>
    Block block (){
        Block block= new Block(declarations(), compundStatement());
        return block;
    }
    
    /// <summary>
    /// Se crea un nodo variable donde se almacenan los valores al inicio del programa
    /// </summary>
    /// <returns></returns>
    Node variable () {
        //variable : ID
        Node variable = new Var(currentToken);
        validateToken(TokenType.ID);
        return variable;
    }

    Node assignmentStatement () {
        /*Se asignan los valores a las variables. del lado izquierdo va la variable y del derecho el valor de la misma*/
        return null;
    }

    List<Node> declarations (){
        validateToken(TokenType.VAR);
        //while(curr)
        return null;
    }

    List<Node> varDecl () {
        return null;
    }

    Node compundStatement () {
        /*Esta clase puede regresar un nodo vacío, un statement o tipo asignación*/
        return null;
    }

    List<Node> statementList() {
        return null;
    }

    Node statement () {
        return null;
    }

    /// <summary>
    /// Valida el token actual preguntando si su tipo se encuentra dentro de los adecuados
    /// </summary>
    /// <param name="tokenType"></param>
    void validateToken(TokenType tokenType) {
        if(currentToken.tokenType.Equals(tokenType))
        {
            currentToken = scanner.getNextToken();
        } else
        {
            Debug.Log("Invalid Token! " + currentToken.typeName);
        }
    }

    /// <summary>
    /// Pregunta si el primer elemento es un número o si es un paréntesis, en caso de ser un paréntesis se reinicia la búsqueda con expretion pero desde el token siguiente
    /// </summary>
    /// <returns></returns>
    Node factor() {

        Node node = null;
        if (currentToken.tokenType.Equals(TokenType.INTEGER)) { //Pregunta si el token actual es un entero
            node =  new Num(currentToken);
            validateToken(TokenType.INTEGER);
            return node;
        }
        else if (currentToken.tokenType.Equals(TokenType.LPAR))
        { //Pregunta si el token actual es un entero
            validateToken(TokenType.LPAR);
            //currentToken = scanner.getNextToken();
            node = expretion();
            validateToken(TokenType.RPAR);
            //currentToken = scanner.getNextToken();
            return node;
        }
        return node;
    }

    /// <summary>
    ///Preguntar si el token siguiente es multiplicación o división
    /// </summary>
    /// <returns></returns>
    Node term() { 
        Node node = factor();

        /**/
        while (currentToken.tokenType >= TokenType.MULT && currentToken.tokenType <= TokenType.INTEGER_DIV)
        {
            Token token = currentToken;
            if (token.tokenType.Equals(TokenType.MULT))
            { //Si el token actual es PLUS, haz una suma
                validateToken(TokenType.MULT);
            }
            else if (token.tokenType.Equals(TokenType.INTEGER_DIV))
            {//Si el token actual es MINUS, haz una resta
                validateToken(TokenType.INTEGER_DIV);
            }
            node = new BinOp(node, token, factor()); //Regresa un operador binario
        }
        return node; //regresa el nodo
    }
    
    /// <summary>
    /// Es lo que se encuentra dentro del archivo o dentro de un string
    /// </summary>
    /// <returns></returns>
    Node expretion() {
        Node node = term();
        while (currentToken.tokenType <= TokenType.PLUS && currentToken.tokenType >= TokenType.MINUS)
        {
            Token t = currentToken;
            if (t.tokenType.Equals(TokenType.PLUS))
            {
                validateToken(TokenType.PLUS);
            }
            if (t.tokenType.Equals(TokenType.MINUS))
            {
                validateToken(TokenType.MINUS);
            }
            node = new BinOp(node, t, term());
        }
        return node;
    }
    public Node parse()
    {
        currentToken = scanner.getNextToken();
        
        return expretion();
    }
}

#endif