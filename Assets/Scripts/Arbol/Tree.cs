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
    public VariablesFloat()
    {
        tokenType = TokenType.REAL;
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
  
    /// <summary>
    /// Obtiene un nodo en la parte izquierda, un token y un nodo a la derecha. Dependiendo del token se puede realizar: suma, resta, multiplicación o división
    /// </summary>
    /// <param name="left"></param>
    /// <param name="type"></param>
    /// <param name="right"></param>
    public NodeLR(NodeLR left, TokenType type, NodeLR right)
    {
        this.left = left;
        this.token = type;
        this.right = right;
    }

    /// <summary>
    /// Se realiza una operacion dependiendo del tokentype de este nodo
    /// </summary>
    public void OperateLeftRight()
    {
        variable = new VariablesFloat();
        if (token.Equals(TokenType.MULT))
        {
            
            variable.SetValue(variable.GetValue() + (float)left.value * (float)right.value);
        }else if (token.Equals(TokenType.REAL_DIV))
        {
            if(right.value != 0)
                variable.SetValue((float)left.value / (float)right.value);
            else
            {
                throw new System.InvalidOperationException("Can't devide by zero");
            }
        }
        else if (token.Equals(TokenType.PLUS))
        {
            variable.SetValue(variable.GetValue() + (float)left.value + (float)right.value);
        }
        else if (token.Equals(TokenType.MINUS))
        {
            variable.SetValue(variable.GetValue() + (float)left.value - (float)right.value);          
        }
        variable.ID = variable.GetValue().ToString();
        value = variable.GetValue();
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
    /// Regresa una variable dentro de la lista global de variables, en caso de no existir, regresa nulo
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    Variables GetVariableFromList(string s)
    {
        ///Itera por todas as variables
        for (int i = 0; i < VARIABLES.Count; i++)
        {
            ///Si la variable en este indice tiene el mismo ID al argumento, regresa esta variable
            if (VARIABLES[i].ID == s)
            {
                return VARIABLES[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Cambia el valor de una variable Dentro de la lista de variables
    /// </summary>
    /// <param name="s"></param>
    void ChangeVariableFromList(Variables s)
    {
        ///Itera sobre la lista de variables
        for (int i = 0; i < VARIABLES.Count; i++)
        {
            ///Si el identificador es el mismo, cambia el nombre de la variable
            if (VARIABLES[i].ID == s.ID)
            {
                VARIABLES[i] = s;
                break;
            }
        }
    }


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
                GetProgram(); ///Entra y busca si existe un programa
                //PrintVars(); ///Imprime las variables
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
            foreach (string id in ids)
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
    /// Sección del texto donde se resuelven operaciones matemáticas
    /// </summary>
    void GetBegin()
    {
        ///Pregunta si el token actual es igual a begin
        if (tokenList[index].tokenType.Equals(TokenType.BEGIN))
        {
            Node2 Begin = new Node2(tokenList[index]); ///Crea un nodo begin 
            Begin.name = tokenList[index].lexeme; //El nodo begin tiene el valor al elemento actual
            first.AddNode(Begin); ///Añade el nodo al primer elemento del árbol
            Assign();
            PrintVars();
            Debug.Log("You've arrived to the operations zone");

        }
        else
        {
            error += "Missing Begin Section!\n";
        }
    }

    /// <summary>
    /// Se asignan valores a las variables que se encuentran dentro de las variables globales
    /// </summary>
    void Assign()
    {
        ///Avanzar hasta que sea diferente de end, por lo que esta sección se terminaría
        while(tokenList[index].tokenType != TokenType.END) 
        {
            if (tokenList[index].tokenType != TokenType.ID)
                Advance();
            if (tokenList[index].tokenType.Equals(TokenType.ID))
            {

                Variables var = GetVariableFromList(tokenList[index].lexeme);
                if(tokenList[index].tokenType != TokenType.ASSIGN)
                    Advance();
                if (tokenList[index].tokenType.Equals(TokenType.ASSIGN))
                {
                    NodeLR nlr = GetExpretion();
                    var.SetValue(nlr.value);
                    ChangeVariableFromList(var);
                }
            }
            else
            {

            }

            ///Crear nodos donde se pregunta si el elemento actual es un número o un signo de operación binaria / unaria
        }
    }

    /// <summary>
    /// Obtener la expresión que se encuentra dentro de una línea de texto, dicha expresión nos permitirá asignarle un valor a una variable global
    /// </summary>
    /// <returns></returns>
    NodeLR GetExpretion()
    {
        NodeLR term = null;
        while (tokenList[index].tokenType != TokenType.SEMI && tokenList[index].tokenType != TokenType.RPAR)
        {

            term = GetTerm();
            //if(tokenList[index].tokenType != TokenType.PLUS || tokenList[index].tokenType != TokenType.MINUS)
            //    Advance();
            if (tokenList[index].tokenType.Equals(TokenType.PLUS))
            {
                NodeLR add = new NodeLR(term, TokenType.PLUS, GetExpretion());
                add.OperateLeftRight();
                return add;
            }
            else if (tokenList[index].tokenType.Equals(TokenType.MINUS))
            {
                NodeLR resta = new NodeLR(term, TokenType.MINUS, GetExpretion());
                resta.OperateLeftRight();
                return resta;
            }
            //Advance();
        }
        return term;
    }

    NodeLR GetTerm()
    {
        NodeLR factor = GetFactor();
        //if(tokenList[index].tokenType != TokenType.MULT || tokenList[index].tokenType != TokenType.REAL_DIV)
        // Advance();
        if (tokenList[index].tokenType.Equals(TokenType.MULT))
        {
            NodeLR mult = new NodeLR(factor, TokenType.MULT, GetTerm());
            mult.OperateLeftRight();
            return mult;
        } else if (tokenList[index].tokenType.Equals(TokenType.REAL_DIV))
        {
            NodeLR div = new NodeLR(factor, TokenType.REAL_DIV, GetTerm());
            div.OperateLeftRight();
            return div;
        }

        return factor;
    }
    
    /// <summary>
    /// Se crea un nodo que regresa un entero o flotante y si hay un paréntesis, crea un while hasta que se cierre el paréntesis
    /// </summary>
    /// <returns></returns>
    NodeLR GetFactor()
    {
        NodeLR toReturn = null;

        Advance();
        if(tokenList[index].tokenType != TokenType.LPAR /*&& tokenList[index].tokenType != TokenType.RPAR*/)
        {
            //toReturn = CheckUnarySigns();
            return CheckUnarySigns();
        }
        else
        {

            toReturn = GetExpretion();
            Advance();
            //return GetExpretion();
        }

        return toReturn;
    }

    /// <summary>
    /// Checa si hay un signo y avanza hasta que sea un entero, se pueden acumular la cantidad que sean necesarias de signos. Si el total es impar, el siguiente valor se multiplica por -1
    /// </summary>
    NodeLR CheckUnarySigns()
    {
        int indx = 0; ///Crea un index que nos indica cuántos signos negativos hay en el archivo

        ///Avanza sí el siguiente token es un símbolo
        while (tokenList[index].tokenType.Equals(TokenType.PLUS) || tokenList[index].tokenType.Equals(TokenType.MINUS))
        {
            ///El token es igual a el símbolo de menos
            if (tokenList[index].tokenType.Equals(TokenType.MINUS))
            {
                indx++; ///Aumenta el índex
            }
            Advance();
        }
        VariablesFloat variables = new VariablesFloat(tokenList[index].lexeme, tokenList[index].tokenType);
        float Num = CheckID(); ///Crea un número que mande a llamar la función de CheckID
        Advance();
        ///El index es diferente de cero  y si el index tiene residuo diferente de cero, multiplica el valor por -1
        if (indx != 0 && indx % 2 != 0)
        {
            Num *= -1;
        }

        variables.SetValue(Num);
        return new NodeLR(variables);

    }

    /// <summary>
    /// Checa si el elemento actual es un entero o un número real
    /// </summary>
    /// <returns></returns>
    float CheckID()
    {
        ///Preguntar si el elemento actual dentro de la lista de tokens es un ID
        if (tokenList[index].tokenType.Equals(TokenType.ID))
        {
            bool found = false;
            ///Preguntar si el elemento actual se encuentra dentro de las variables globales del árbol
            if (GetVariableFromList(tokenList[index].lexeme) != null)
            {
                found = true;
            }
            if (!found)
            {
                error += "Couldn't found this variable in the globla variables\n";
            }
            //Advance();
            return (float)GetVariableFromList(tokenList[index].lexeme).GetValue();
        }
        else if (tokenList[index].tokenType == TokenType.INTEGER)
        {
            return float.Parse(tokenList[index].lexeme);
        }
        else if (tokenList[index].tokenType == TokenType.REAL)
        {
            return float.Parse(tokenList[index].lexeme);
        }
        return 0;
    }



    /// <summary>
    /// Imprime todas las variables globales que se encuentran dentro del árbol
    /// </summary>
    public void PrintVars()
    {
        for(int i = 0; i < VARIABLES.Count; i++)
        {
            Debug.Log("Variable: " + VARIABLES[i].ID + " type: " + VARIABLES[i].tokenType + " value: " + VARIABLES[i].GetValue());
        }
    }


    /// <summary>
    /// Aumenta en uno el índice y regresa este nuevo valor
    /// </summary>
    /// <returns></returns>
    public int Advance()
    {
        int currInd = index;
        if (currInd + 1 < tokenList.Count)
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
    si es signo avanza hasta que sea entero o real
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
