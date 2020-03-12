using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IVisitable {
    void Accept (IVisitor visitor);
}

/*Se crea una función de visita para cada uno de los nodos que se crearon para almacenar cada uno de los tokens*/
public interface IVisitor {
    /// <summary>
    /// Visita el nodo número
    /// </summary>
    /// <param name="num"></param>
    void Visit(Num num);

    /// <summary>
    /// Visita el nodo Bloque
    /// </summary>
    /// <param name="block"></param>
    void Visit(BinOp block);

    /// <summary>
    /// Visita el nodo Operación Unaria
    /// </summary>
    /// <param name="unary"></param>
    void Visit (UnaryOp unary);
    
    /// <summary>
    /// Visita el nodo Compund
    /// </summary>
    /// <param name="unary"></param>
    void Visit (Compund compund);
   
    /// <summary>
    /// Visita el nodo Type
    /// </summary>
    /// <param name="unary"></param>
    void Visit (Type type);
    
    /// <summary>
    /// Visita el nodo VarDecl
    /// </summary>
    /// <param name="unary"></param>
    void Visit (VarDecl varDecl);
    
    /// <summary>
    /// Visita el nodo Var
    /// </summary>
    /// <param name="unary"></param>
    void Visit (Var var);
   
    /// <summary>
    /// Visita el nodo Block
    /// </summary>
    /// <param name="unary"></param>
    void Visit (Block block);
    
    /// <summary>
    /// Visita el nodo NoOp
    /// </summary>
    /// <param name="unary"></param>
    void Visit (NoOP var);
    
    /// <summary>
    /// Visita el nodo Program
    /// </summary>
    /// <param name="unary"></param>
    void Visit (Program program);
  
    /// <summary>
    /// Visita el nodo Assign
    /// </summary>
    /// <param name="unary"></param>
    void Visit (Assign program);
}

/// <summary>
/// Obtienes el valor de la función que se desee
/// </summary>
class ValueGetter {
    System.Object value; 
    
    public static System.Object GetValue(Node n) {
        NodeVisitor visitor = new NodeVisitor();
        n.Accept(visitor);
        return visitor.value;
    }
   

    public void Return(System.Object val) {
        value = val;
    }
}

class NodeVisitor : ValueGetter, IVisitor
{
    Dictionary<string, System.Object> G_SCOPRE = new Dictionary<string, System.Object>();
    public NodeVisitor () {

    }


    private readonly Node node;

    protected NodeVisitor(Node node)
    {
        this.node = node;
    }
    

#if false
    public static NodeVisitor CreateFromNode (Node node) {

        switch (node.type) {
            case NodeType.BINOP:
                return new BinaryVisitor((BinOp)(node));
            case NodeType.NUM:
                return new NumVisitor((Num)node);
            default:
                Debug.LogError("Error al leer nodo.");
                return default(NodeVisitor);
        }

    } 
#endif

    public void Visit (Num num) {
        Return(num);
    }

    public void Visit (BinOp block) {
        Return(block);
    }

    public void Visit (UnaryOp unary) {
        Return(unary);
    }

    public void Visit (Compund compund) {
        Return(compund);
    }

    public void Visit (Type type) {
        Return(type);
    }

    public void Visit (VarDecl varDecl) {
        Return(varDecl);
    }

    public void Visit (Var var) {
        Return(var);
    }

    public void Visit (Block block) {
        Return(block);
    }

    public void Visit (NoOP var) {
        Return(var);
    }

    public void Visit (Program program) {
        Return(program);
    }

    public void Visit (Assign assign) {
        Return(assign);
    }
}

//public class NumVisitor : NodeVisitor
//{
//    private readonly Num node;

//    public NumVisitor(Num node) : base(node)
//    { //Constructor del visitador de números
//        this.node = node;
//    }
//    public override int Visit()
//    { //Regresa el valor del nodo
//        return node.value;
//    }

//}

//public class UnOpVisitor : NodeVisitor
//{
//    private readonly UnaryOp node;
//    public UnOpVisitor(UnaryOp node) : base(node)
//    {
//        this.node = node;
//    }
//    public override int Visit ()
//    {
//        return node
//    }
//}
//public class BinaryVisitor : NodeVisitor
//{
//    private readonly BinOp node;
//    public BinaryVisitor(BinOp node) : base(node)
//    { //Constructor del visitador de operadores binarios
//        this.node = node;
//    }
//    public override int Visit()
//    { //Visita cada operador posible

//        var left = NodeVisitor.CreateFromNode(node.left);
//        var right = NodeVisitor.CreateFromNode(node.right);

//        if (node.op.token.Equals(TokenType.PLUS))
//        {
//            return left.Visit() + right.Visit();
//        }
//        else if (node.op.token.Equals(TokenType.MINUS))
//        {
//            return left.Visit() - right.Visit();
//        }
//        else if (node.op.token.Equals(TokenType.MULT))
//        {
//            return left.Visit() * right.Visit();
//        }
//        else if (node.op.token.Equals(TokenType.INTEGER_DIV))
//        {
//            return left.Visit() / right.Visit();
//        }


//        return -1;
//    }

//}

public class Interpreter : MonoBehaviour
{

    public Parser parser;

    public Interpreter(Parser parser)
    {
        this.parser = parser;
    }


    public int interpret () {
        Node tree = parser.parse();
        //var visitor = NodeVisitor.CreateFromNode(tree);
        //return visitor.Visit();
        return 0;
    }
   
}
