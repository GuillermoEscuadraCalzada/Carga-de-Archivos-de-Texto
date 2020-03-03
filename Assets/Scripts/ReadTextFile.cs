using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ReadTextFile : MonoBehaviour
{
    // Start is called before the first frame update
    string assetPath;
    string fileName;
    string path;

    public GameObject txtMesh;

    public GameObject cube; //Prefabs u objetos en escena

    public GameObject sphere;
    GameObject obj;

    //Lista para la carga de objetos en escena
    List<GameObject> primitives;

    TextMeshProUGUI txtM;
    void Start()
    {
        primitives = new List<GameObject>(); //Crear una nueva Lista
        txtM = txtMesh.GetComponent<TextMeshProUGUI>(); //Obtener un objeto TextMesh desde el inspector
        LoadFile();
    }

    void parseText(string textToParse)
    {
        string[] lines = textToParse.Split('\n'); //Al momento de encontrar un salto de línea, se termina el token y salta al siguiente
        int i = 0; //Contador de líneas
        foreach (string line in lines)
        {
            print(i + ":" + line);
            parseLine(line, i);
            i++;
        }

        //string number = "5";
        //int n = int.Parse(number);
    }

    public void LoadFile()
    {
        foreach (GameObject obj in primitives)
        {
            Destroy(obj);
        }
        primitives.Clear();

        assetPath = Application.streamingAssetsPath;
        fileName = "Primitives.txt";
        path = assetPath + "/" + fileName;
        print(path);
        StreamReader streamR = new StreamReader(path);
        string rawText = streamR.ReadToEnd();
        streamR.Close();

        print(rawText);
        parseText(rawText);
        txtM.text = rawText;
    }
    void parseLine(string line, int lineNumber)
    {
        string[] tokens = line.Split(','); //Separar las líneas por comas
        if (tokens.Length >= 4) { //Si el tokens es igual a 4, se procesa la siguiente información
            string firstToken = tokens[0];
            char firstChar = firstToken[0];

            //Omitir los comentarios representados por #
            if (!firstChar.Equals('#'))
            {
                if (firstToken.Equals("cube"))
                {
                    string secondToken = tokens[1]; //Toma el caracter en la posición 1 de los tokens
                    string thirdToken = tokens[2]; //Toma el caracter en la posición 2 de los tokens
                    string fourthToken = tokens[3]; //Toma el caracter en la posición 3 de los tokens
                    //Parsear el valor de los string y convertirlos a enteros
                    parseLocation(cube, secondToken, thirdToken, fourthToken);
                    if(tokens.Length > 4)
                    {
                        string fifthToken = tokens[4];
                        string sixthToken = tokens[5];
                        string seventhToken = tokens[6];
                        parseScale(cube, fifthToken, sixthToken, seventhToken);

                        string rotXToken = tokens[7];
                        string rotYToken = tokens[8];
                        string rotZToken = tokens[9];
                        parseRotation(cube, rotXToken, rotYToken, rotZToken);

                        string colorRToken = tokens[10];
                        string colorGToken = tokens[11];
                        string colorBToken = tokens[12];
                        parseColor(cube, colorRToken, colorGToken, colorBToken);
                    }

                    primitives.Add(obj);
                }
                else if (firstToken.Equals("sphere"))
                {
                    string secondToken = tokens[1]; //Toma el caracter en la posición 1 de los tokens
                    string thirdToken = tokens[2]; //Toma el caracter en la posición 2 de los tokens
                    string fourthToken = tokens[3]; //Toma el caracter en la posición 3 de los tokens
                    parseLocation(sphere, secondToken, thirdToken, fourthToken); //Se determina la posición del cubo dentro del mundo 

                    if (tokens.Length > 4)
                    { //Si el tamaño de los tokens es mayor a cuatro, se modifica la escala, rotación y color
                        string fifthToken = tokens[4];
                        string sixthToken = tokens[5];
                        string seventhToken = tokens[6];
                        parseScale(sphere, fifthToken, sixthToken, seventhToken); //Se transforma la escala del esfera

                        string rotXToken = tokens[7];
                        string rotYToken = tokens[8];
                        string rotZToken = tokens[9];
                        parseRotation(sphere, rotXToken, rotYToken, rotZToken); //Se transforma la rotación de la esfera

                        string colorRToken = tokens[10];
                        string colorGToken = tokens[11];
                        string colorBToken = tokens[12];
                        parseColor(sphere, colorRToken, colorGToken, colorBToken); //Se transforma el color de la esfera
                    }

                    primitives.Add(obj);
                    
                }
            }
        }
        else
        { //No hay una cantidad necesaria de Tokens o hay algo mal escrito
            Debug.Log("Tokens are not enough: " + tokens.Length);
        }
    }

    void parseLocation(GameObject gameObject, string sToken, string tToken, string fToken)
    {
       float x = float.Parse(sToken);
       float y = float.Parse(tToken);
       float z = float.Parse(fToken);

        obj = Instantiate(gameObject, new Vector3(x, y, z), Quaternion.identity);
    }

    void parseScale(GameObject gameObject, string sToken, string tToken, string fToken)
    {
        float x = float.Parse(sToken);
        float y = float.Parse(tToken);
        float z = float.Parse(fToken);

        gameObject.transform.localScale += new Vector3(x, y, z);
    }

    void parseRotation(GameObject gameObject, string sToken, string tToken, string fToken)
    {
        float x = float.Parse(sToken);
        float y = float.Parse(tToken);
        float z = float.Parse(fToken);

        gameObject.transform.Rotate(x, y, z);
    }

    void parseColor(GameObject gameObject, string sToken, string tToken, string fToken)
    {
        Color color;
        float x = float.Parse(sToken);
        float y = float.Parse(tToken);
        float z = float.Parse(fToken);
       
        color.r = x/255;
        color.g = y/255;
        color.b = z/255;

        gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b);
    }
}
