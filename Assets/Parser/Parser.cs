using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public Scanner5 scanner;
    Token currentToken;

    public Parser (Scanner5 scanner)
    {
        this.scanner = scanner;
    }

    Node program () { //El bloque principal del programa, a partir de aquí, inicia todo
        validateToken(TokenType.PROGRAM);
        Node varNode = variable();
        validateToken(TokenType.SEMI);
        Block _block = block();
        validateToken(TokenType.DOT);
        return null;
    }

    /*Se crea un nodo bloque donde las declaraciones*/
    Block block (){
        Block block= new Block(declarations(), compundStatement());
        return block;
    }
    //Se crea un nodo variable donde se almacenan los valores al inicio del programa
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
    void validateToken(TokenType tokenType) {
        if(currentToken.token == tokenType)
        {
            currentToken = scanner.getNextToken();
        }else
        {
            Debug.Log("Invalid Token! " + currentToken.typeName);
        }
    }

    Node factor() {

        Node node = null;
        if (currentToken.token.Equals(TokenType.INTEGER)) { //Pregunta si el token actual es un entero
            node =  new Num(currentToken);
            validateToken(TokenType.INTEGER);
            return node;
        }
        else if (currentToken.token.Equals(TokenType.LPAR))
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

    Node term() { //Preguntar si el token siguiente es multiplicación o división
        Node node = factor();

        while (currentToken.token >= TokenType.MULT && currentToken.token <= TokenType.INTEGER_DIV)
        {
            Token token = currentToken;
            if (token.token.Equals(TokenType.MULT))
            { //Si el token actual es PLUS, haz una suma
                validateToken(TokenType.MULT);
                //currentToken = scanner.getNextToken();
            }
            else if (token.token.Equals(TokenType.INTEGER_DIV))
            {//Si el token actual es MINUS, haz una resta
                validateToken(TokenType.INTEGER_DIV);
                //currentToken = scanner.getNextToken();
            }
            node = new BinOp(node, token, factor()); //Regresa un operador binario
        }
        return node; //regresa el nodo
    }

    Node expretion() {
        Node node = term();
        while (currentToken.token <= TokenType.PLUS && currentToken.token >= TokenType.MINUS)
        {
            Token t = currentToken;
            if (t.token.Equals(TokenType.PLUS))
            {
                validateToken(TokenType.PLUS);
                //currentToken = scanner.getNextToken();
            }
            if (t.token.Equals(TokenType.MINUS))
            {
                validateToken(TokenType.MINUS);
                //currentToken = scanner.getNextToken();
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
