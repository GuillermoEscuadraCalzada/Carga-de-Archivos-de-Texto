using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType { BINOP, NUM, UNOP}

public abstract class Node : IVisitable
{ //Nodo que se encuentra dentro del árbol
    public Node left; //Nodo izquierdo
    public Node right; //Nodo derecho
    public Token token; //Token que almacena el nodo 
    public NodeType type;
    public abstract void Accept(IVisitor visitor);
}

public class Num : Node
{ //Número dentro del nodo
    public int value;
    /*Constructor de la clase Num, regresa el valor del token y lo convierte a entero
     *@param[Token _token] Token a parsear*/
    public Num(Token _token)
    {  
        token = _token;
        value = int.Parse(token.lexeme);
        type = NodeType.NUM;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }

}

public class BinOp : Node { //Operaciones binarias
    public Token op; //Operador de números
    /*Constructor de la clase BinOp, obtiene nodos y realiza una operación con esto
     *@param[Node _left] número del lado izquierdo
     *@param[Token _op] operador
     *@param[Node _right] nodo a la derecha*/
    public BinOp(Node _left, Token _op, Node _right) 
    {
        left = _left;
        right = _right;
        token = op = _op;
        type = NodeType.BINOP;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}

public class UnaryOp : Node
{ //Operadores unarios
    public Token Op;
    public Node expr;
    public UnaryOp(Token op, Node expre)
    {
        token = Op = op;
        expr = expre;
        type = NodeType.UNOP;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}


public class Compund : Node
{
    public List<Node> statementList;
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}


public class Assign : Node
{ //Assing servirá para el operador := , asignará si es entero  o real a una variable
    public Token op;
    public Assign(Node left, Token tok, Node right) { //Constructor de la clase assign
        this.left = left;
        this.right = right;
        token = op = tok;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}
public class Var : Node { //Una variable que se almacenará dentro del nodo
    string val;
    public Var (Token t){ //Constructor que pide un token como parámetro
        token = t; //El token del nodo guarda los valores de este token
        val = t.lexeme; //Val toma el string del token
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}

public class NoOP : Node { //No hay operación, por lo tanto no se guardan variables.
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}

public class Program : Node {
    public Block block;
    public string name;
    public Program(string name, Block block)
    {
        this.name = name;
        this.block = block;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}

public class Block : Node { //El bloque que almacenará las declaraciones a futuro, si son variables u otro tipo
    List<Node> declarations;
    public Node compound_Statements;
    public Block (List<Node> declarationList, Node compound_statement)
    {
        declarations = declarationList;
        compound_Statements = compound_statement;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}

public class VarDecl : Node
{ //Declaración de variables dentro del árbol de este programa
    Var variable;
    Type nodeType;
    public VarDecl(Var var, Type type)
    {
        variable = var;
        nodeType = type;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}

public class Type: Node
{
    string value;
    public Type (Token tok) {
        token = tok;
        value = tok.lexeme;
    }
    public override void Accept (IVisitor visitor) {
        visitor.Visit(this);
    }
}