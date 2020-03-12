using UnityEngine;
using System.IO;
using System.Collections;

public class UnitTest : MonoBehaviour
{
    //string test = "PROGRAM Part10; VAR number : INTEGER; BEGIN END.";

    Scanner5 scanner;
    Tree tree;
    //Parser parser;
    // Use this for initialization
    void Start ()
    {
        scanner = new Scanner5();
        scanner.setTextToScan(LoadFile());
        scanner.printScanner();
        tree = new Tree();
        tree.SetTokenList(scanner.GetTokenList());
        tree.CreateTree();

    }

   string LoadFile()
   {
        string assetPath = Application.streamingAssetsPath;
        string fileName = "Interprete.txt";
        string path = assetPath + "/" + fileName;
        print(path);
        StreamReader streamR = new StreamReader(path);
        string rawText = streamR.ReadToEnd();
        streamR.Close();

        print(rawText);
        return rawText;
    }
}
