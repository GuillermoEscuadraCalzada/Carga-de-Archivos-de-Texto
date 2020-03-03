using UnityEngine;
using System.Collections;
using TMPro;

#if false
public class REPL : MonoBehaviour
{
    Scanner5 scanner5;
    Parser parser;
    Interpreter interpreter;
    public TMP_InputField textBox;
    public TextMeshProUGUI console;
    string consoleText;

    private void Start()
    {
        //scanner = new Scanner();
        ////scanner2 = new Scanner2();
        ////scanner3 = new Scanner3();
        ////scanner4 = new Scanner4();
        scanner5 = new Scanner5();
        parser = new Parser(scanner5);
        interpreter = new Interpreter(parser);
        consoleText = "";
        console.text = consoleText;
        textBox.ActivateInputField();
    }

    public void OnTextEntered()
    {
        consoleText += textBox.text + '\n';
        scanner5.setTextToScan(textBox.text);
        consoleText += interpreter.interpret();
        consoleText += '\n';
        console.text = consoleText;
        textBox.text = "";
        textBox.ActivateInputField();
        textBox.text = "";
    }
}
#endif