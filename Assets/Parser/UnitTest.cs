using UnityEngine;
using System.IO;
using System.Collections;
using TMPro;
public class UnitTest : MonoBehaviour
{
    //string test = "PROGRAM Part10; VAR number : INTEGER; BEGIN END.";

    Scanner5 scanner;
    Tree tree;
    public TextMeshProUGUI text, document;
    //Parser parser;
    // Use this for initialization
    void Start ()
    {
        scanner = new Scanner5();
        tree = new Tree();
    }

    public void PrintToScreen()
    {
      
        tree.ResetLists();
        scanner.setTextToScan(LoadFile());
        scanner.printScanner();
        tree.SetTokenList(scanner.GetTokenList());
        tree.CreateTree();
        tree.PrintVars();
        TreeVariables();
    }

    void TreeVariables()
    {
        text.text = "";
        foreach(Variables vars in tree.VARIABLES)
        {
            
            text.text += "Variable: " + vars.ID + " type: " + vars.tokenType + " value: " + vars.GetValue() + "\n";
        }
    }

   string LoadFile()
   {
        string assetPath = Application.streamingAssetsPath; 
        string fileName = "Interprete.txt";
        document.text = fileName;
        string path = assetPath + "/" + fileName;
        print(path);
        StreamReader streamR = new StreamReader(path);
        string rawText = streamR.ReadToEnd();
        streamR.Close();

        print(rawText);
        return rawText;
    }
}
