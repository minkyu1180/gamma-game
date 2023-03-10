using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogBoxTextTyper:MonoBehaviour
{
    TextMeshProUGUI dialogText;
    AudioSource audioSource;
    private static string dialog;
    public bool quickDialog = false;
    public bool loading = true;
    public float textSpeed = 0.04f;
    public AudioClip speechVoice;

    private AudioClip clickSound;
    private List<string> commands = new List<string>();
    private int commandLine = 0;
    private string lastCommand = "";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogText = GetComponent<TextMeshProUGUI>();
        clickSound = Resources.Load("Sound/Voice/clickSound") as AudioClip;
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

            if (!InputDecoder.dialogMode && commandLine < commands.Count)
            {
                commandLine++;
            }

            if (InputDecoder.dialogMode)
            {
                if ( !quickDialog && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("left ctrl")) ) quickDialog = true;
                else if ( quickDialog && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("left ctrl")) ) 
                {
                    InputDecoder.dialogMode = false;
                    audioSource.PlayOneShot(clickSound);
                    if (commands.Count - 1 == commandLine)  //end of the script
                    {
                        commands.Clear();
                        InputDecoder.InterfaceElements.SetActive(false);
                    }
                }
            }
            else
            {
                if (commands.Count == commandLine)  //end of the script
                {
                    commands.Clear();
                    InputDecoder.InterfaceElements.SetActive(false);
                }
            }
        }
    }

    public void LoadScript(string filePath)
    {
        TextAsset commandFile = Resources.Load<TextAsset>(filePath);
        var commandArray = commandFile.text.Split('\n');
        foreach (var line in commandArray)
        {
            commands.Add(line);
        }
        loading = false;
    }

    public void PutDialog(string say)
    {
        audioSource.mute = true;
        dialog = say;
        quickDialog = false;
        StartCoroutine(DisplayLine());
    }

    public void PutDialog(string say, AudioClip voice)
    {
        audioSource.mute = false;
        speechVoice = voice;
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
            if (letter != ' '){
                audioSource.PlayOneShot(speechVoice);
            }
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
                yield return new WaitForSeconds(textSpeed);
            }
        }

    }
    
}
