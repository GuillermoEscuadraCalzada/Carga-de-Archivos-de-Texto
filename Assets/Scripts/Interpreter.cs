using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IVisitable {
    void Accept (IVisitor visitor);
}

public interface IVisitor {
    public void Visit (Num num);
    //void Visit (Num num);
    void Visit(BinOp block);
    void Visit (UnaryOp unary);
    void Visit (Compund compund);
    void Visit (Type type);
    void Visit (VarDecl varDecl);
    void Visit (Var var);
    void Visit (Block block);
    void Visit (NoOP var);
    void Visit (Program program);
    void Visit (Assign program);
}

class ValueGetter {
    public static System.Object GetValue(Node n) {
        NodeVisitor visitor = new NodeVisitor();
        n.Accept(visitor);
        return visitor.value;
    }
   

    public void Return(System.Object val) {
        value = val;
    }
    System.Object value;
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
        //Return(unary)
    }

    public void Visit (Compund compund) {
    }

    public void Visit (Type type) {
    }

    public void Visit (VarDecl varDecl) {
    }

    public void Visit (Var var) {
        Return(var);
    }

    public void Visit (Block block) {
        throw new System.NotImplementedException();
    }

    public void Visit (NoOP var) {
        throw new System.NotImplementedException();
    }

    public void Visit (Program program) {
        throw new System.NotImplementedException();
    }

    public void Visit (Assign program) {
        throw new System.NotImplementedException();
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
