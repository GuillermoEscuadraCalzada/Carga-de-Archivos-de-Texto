using UnityEngine;
using System.Collections;

public class UnitTest : MonoBehaviour
{
    string test = "PROGRAM Part10; VAR number : INTEGER; BEGIN END.";
    Scanner5 scanner;
    Parser parser;
    // Use this for initialization
    void Start ()
    {
        scanner = new Scanner5();
        scanner.setTextToScan(test);
        scanner.printScanner();
        parser = new Parser(scanner);
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
