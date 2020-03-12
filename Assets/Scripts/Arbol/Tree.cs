using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Clase padre de las variables enteras y flotantes
/// </summary>
public abstract class Variables
{
    /// <summary>
    /// El tipo de token que se le asignará a cada variable
    /// </summary>
    public TokenType tokenType;
    
    /// <summary>
    /// El identificador de cada variable (nombre)
    /// </summary>
    public string ID;
    
    /// <summary>
    /// Todas las variables tendrán un flotante pero se casteará a entero o se dejará así
    /// </summary>
    float value;
   
    /// <summary>
    /// Función abstracta que regresa el valor de la variablem
    /// </summary>
    /// <returns></returns>
    public abstract float GetValue();
    
    /// <summary>
    /// Función abstracta que modifica el valor de la variable
    /// </summary>
    /// <param name="val"></param>
    public abstract void SetValue(float val);
}

/// <summary>
/// Clase para Variables de enteros
/// </summary>
public class VariablesInt : Variables
{
    /// <summary>
    /// El valor de esta variable
    /// </summary>
    int value;

    /// <summary>
    /// Constructor de la clase variable de enteros, recibe un identificador y de un TokenType
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="type"></param>
    public VariablesInt(string ID, TokenType type)
    {
        this.tokenType = type;
        this.ID = ID;
    }
    
    /// <summary>
    /// Sobrescribe la función de su clase padre y regresa el valor de esta variable
    /// </summary>
    /// <returns></returns>
    public override float GetValue()
    {
        return value;
    }


    /// <summary>
    /// Se sobrescribe la función de la clase padre y se modifica el valor de esta clase
    /// </summary>
    /// <param name="val"></param>
    public override void SetValue(float val)
    {
        value = (int)val;
    }

}

public class VariablesFloat : Variables
{
    float value;

    public VariablesFloat(string id, TokenType type)
    {
        this.ID = id;
        tokenType = type;
    }

    /// <summary>
    /// Regresa el valor de este nodo variable
    /// </summary>
    /// <returns></returns>
    public override float GetValue()
    {
        return value;
    }

    /// <summary>
    /// Se modifica el valor de este nodo
    /// </summary>
    /// <param name="val"></param>
    public override void SetValue(float val)
    {
        value = val;
    }
}

/// <summary>
/// Clase de nodos que guardarán un token
/// </summary>
public class NodeLR
{
    NodeLR left;
    NodeLR right;
    Variables variable;

    /// <summary>
    /// Valor de este nodo
    /// </summary>
    public float value;
    /// <summary>
    /// Token del nodo que lo distinguirá de otros
    /// </summary>
    TokenType token; 

    /// <summary>
    /// Constructor para la clase NodeLR que solo guardará una variable
    /// </summary>
    /// <param name="v"></param>
    public NodeLR(Variables v)
    {
        variable = v; ///La variable es igual a la del argumento
        value = variable.GetValue(); ///El valor es igual a su variable
    }

    /// <summary>
    /// Constructor de la clase que solo resivirá tokens
    /// </summary>
    /// <param name="type"></param>
    public NodeLR(TokenType type)
    {
        token = type;
    }
  
    public NodeLR(NodeLR left, TokenType type, NodeLR right)
    {
        this.left = left;
        this.token = type;
        this.right = right;
    }

}


public class Node2
{
    /// <summary>
    /// Un token que identificará al nodo
    /// </summary>
    Token token;

    public string name;    
    
    /// <summary>
    /// Una lista de nodos hijo de este nodo
    /// </summary>
    List<Node2> hijos;
   
    /// <summary>
    /// Constructor de la clase nodo que recibe un token como parámetro
    /// </summary>
    /// <param name="t"></param>
    public Node2(Token t)
    {
        token = t; ///El token del nodo es igual al token del parámetro
        hijos = new List<Node2>();
    }

   

    /// <summary>
    /// Función que añade hijos al nodo actual
    /// </summary>
    /// <param name="node"></param>
    public void AddNode(Node2 node)
    {
        hijos.Add(node); ///Se añade el nodo a la lista de hijos
    }

    public void PrintSons()
    {
        foreach(Node2 node in hijos)
        {
            Debug.Log("Section: " + node.name);
        }
    }
}

/// <summary>
/// Clase árbol que servirá para leer todo un texto y resolver operaciones con el
/// </summary>
public class Tree : MonoBehaviour
{
    /// <summary>
    /// Lista de tokens que nos permitirá analizar todo lo que se encuentra dentro de un texto
    /// </summary>
    List<Token> tokenList;

    /// <summary>
    /// Lista de variables que serán constantes a lo largo del programa
    /// </summary>
    public List<Variables> VARIABLES;

    /// <summary>
    /// String que se modificará dependiendo de los errores que vayan surgiendo a lo largo del programa
    /// </summary>
    string error = "";

    /// <summary>
    /// Primer nodo del árbol, la raíz
    /// </summary>
    Node2 first;

    /// <summary>
    /// Índice que nos permitirá avanzar dentro de la lista de tokens
    /// </summary>
    int index;

    /// <summary>
    /// Constructor por defecto del árbol
    /// </summary>
    public Tree()
    {
        index = 0;
        tokenList = new List<Token>(); ///Crea una nueva lista de tokens
        VARIABLES = new List<Variables>(); ///Crea una nueva lista de variables
    }

    /// <summary>
    /// Adquiere la lista de tokens de otro lado, preferiblemente de un scanner
    /// </summary>
    /// <param name="tokenList"></param>
    public void SetTokenList(List<Token> tokenList)
    {
        this.tokenList = tokenList;
    }

    /// <summary>
    /// Crea todo el árbol que nos servirá para el programa
    /// </summary>
    public void CreateTree()
    {
        try
        {
            ///La lista de tokens tiene un mayor número a cero
            if (tokenList.Count != 0)
            {
                GetProgram();
                PrintVars();
                GetBegin();
                first.PrintSons();
            }
            else
            { ///Lanza este error, la lista de tokens está vacía
                throw new System.InvalidOperationException("This List is emty.");
            }
        } catch (Exception e)
        {
            Debug.LogError("[" + e.GetType() + "] " + e.Message);
        }
    }

    /// <summary>
    /// Obtenemos el primer bloque dentro del texto, la parte que nos especifíca que es un programa
    /// </summary>
    void GetProgram()
    {
        ///El elemento actual dentro de la lista de tokens tiene un tipo llamado Program
        if (tokenList[index].tokenType.Equals(TokenType.PROGRAM))
        {
            first = new Node2(tokenList[index]); ///Crea el nodo First como este elemento
            first.name = tokenList[Advance()].lexeme; ///Avanza y obten el nombre del programa
            Advance();
            ///El siguiente elemento de la lista de tokens es la sección VAR
            if (tokenList[Advance()].tokenType.Equals(TokenType.VAR))
            {
                Node2 Vars = new Node2(tokenList[index]);
                Vars.name = tokenList[index].lexeme;
                first.AddNode(Vars);
                ///Avanza hasta que el elemento actual dentro de la lista de tokens sea diferente a ID
                while (tokenList[Advance()].tokenType.Equals(TokenType.ID))
                {
                    GetVars();
                }
            }
            else
            {
                error += "There is no Variables section\n";
            }
        }
        else
        {
            error += "Missing Program at line 0!\n";
        }
    }

    /// <summary>
    /// Sección del texto donde se resuelven operaciones matemáticas
    /// </summary>
    void GetBegin()
    {
        if (tokenList[index].tokenType.Equals(TokenType.BEGIN))
        {
            Node2 Begin = new Node2(tokenList[index]);
            Begin.name = tokenList[index].lexeme;
            first.AddNode(Begin);
            Debug.Log("You've arrived to the operations zone");
            while (tokenList[Advance()].tokenType.Equals(TokenType.ID))
            {
                Assign();
            }
        }
    }

    void Assign()
    {
        if (tokenList[Advance()].tokenType.Equals(TokenType.ASSIGN))
        {
             ///Crear nodos donde se pregunta si el elemento actual es un número o un signo de operación binaria / unaria
        }
    }

    /// <summary>
    /// Imprime todas las variables globales que se encuentran dentro del árbol
    /// </summary>
    public void PrintVars()
    {
        for(int i = 0; i < VARIABLES.Count; i++)
        {
            Debug.Log("Variable: " + VARIABLES[i].ID + " type: " + VARIABLES[i].tokenType);
        }
    }

    /// <summary>
    /// Obtenemos todas las variables que se encuentran dentro de la sección de VARS
    /// </summary>
    void GetVars()
    {
        ///Se crea una lista de ids para que así se puedan crear nodos dependiendo del nombre y tipo
        List<string> ids = new List<string>();

        ///Avanzamos hasta que dejen de haber variables en esta línea del texto
        while (tokenList[index].tokenType == TokenType.ID)
        {
            ids.Add(tokenList[index].lexeme); ///Se añaden a la lista de strings
            Advance(); //Avanza uno el index
        }

        ///Si el elemento actual dentro de la lista de tokens es igual a un entero, entra aquí
        if (tokenList[index].tokenType.Equals(TokenType.INTEGER))
        {
            ///Por cada uno de los strings dentro de la lista de strings, avanza
            foreach(string id in ids)
            {
                ///Crea una variable de entero con el nombre de este string y con tipo entero
                VariablesInt vI = new VariablesInt(id, TokenType.INTEGER);

                ///Cre un nuevo nodo con esta variable
                NodeLR nodo = new NodeLR(vI);

                ///Añade este nodo a las variables globales
                VARIABLES.Add(vI);
            }
        } else if (tokenList[index].tokenType.Equals(TokenType.REAL))
        { ///El elemento actual es un número real

            ///Por cada uno de los strings, avanza
            foreach (string id in ids)
            {
                ///Crea una variable flotante con este string
                VariablesFloat vI = new VariablesFloat(id, TokenType.REAL);

                ///Crea un nodo con esta variable
                NodeLR nodo = new NodeLR(vI);

                VARIABLES.Add(vI); ///Añade esta variable a las variables globales
            }
        }
        else
        {
            
        }
        ids.Clear(); ///Libera la lista de strings

        Advance();
    }

    /// <summary>
    /// Aumenta en uno el índice y regresa este nuevo valor
    /// </summary>
    /// <returns></returns>
    public int Advance()
    {
        if (index + 1 < tokenList.Count)
        {
            index++;
        }
        else
        {
            //throw new System.
        }
        return index;
    }



#if false
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
#endif
}
