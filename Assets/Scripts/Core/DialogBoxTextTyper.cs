using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogBoxTextTyper:MonoBehaviour
{
    TextMeshProUGUI dialogText;
    private static string dialog;
    public bool quickDialog = false;
    public bool loading = true;

    private List<string> commands = new List<string>();
    private int commandLine = 0;
    private string lastCommand = "";

    void Start()
    {
        dialogText = GetComponent<TextMeshProUGUI>();
        dialog = "";
    }

    void Update(){
        if (!loading)
        {
            if (commands[commandLine] != lastCommand)
            {
                lastCommand = commands[commandLine];
                InputDecoder.ParseInputLine(commands[commandLine]);
            }

            if (!InputDecoder.dialogMode && commandLine < commands.Count - 1)
            {
                commandLine++;
            }

            if (InputDecoder.dialogMode)
            {
                if ( !quickDialog && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("left ctrl")) ) quickDialog = true;
                else if ( quickDialog && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("left ctrl")) ) 
                {
                    InputDecoder.dialogMode = false;
                    if (commands.Count - 1 == commandLine)  //end of the script
                    {
                        commands.Clear();
                        InputDecoder.InterfaceElements.SetActive(false);
                    }
                }
            }
        }
    }

    public void LoadScript(string filePath)
    {
        TextAsset commandFile = Resources.Load<TextAsset>("Scripts/Opening");
        var commandArray = commandFile.text.Split('\n');
        foreach (var line in commandArray)
        {
            commands.Add(line);
        }
        loading = false;
    }

    public void PutDialog(string say)
    {
        dialogText = GetComponent<TextMeshProUGUI>();
        dialog = say;
        quickDialog = false;
        StartCoroutine(DisplayLine());
    }

    
    
    
    IEnumerator DisplayLine()
    {
        dialogText.text = "";


        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            if (quickDialog)
            { 
                dialogText.text = dialog;
                break;
            }
            else
            {
                if (dialogText.text == dialog)
                {
                    quickDialog = true;
                    break;
                }
                yield return new WaitForSeconds(0.04f);
            }
        }

    }
    
}
