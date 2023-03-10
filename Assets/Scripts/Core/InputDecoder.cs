using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class InputDecoder
{
    public static List<Character> CharacterList = new List<Character> ();

    public static AudioSource bgmManager = GameObject.Find("BGMControlManager").GetComponent<AudioSource>();
    public static GameObject InterfaceElements = GameObject.Find("UI_Elements");
    private static GameObject Background = GameObject.Find("Background");
    private static Image BackgroundImage = Background.GetComponent<Image>();
    public static GameObject Portrait = GameObject.Find("Portrait");
    private static Image PortraitImage = Portrait.GetComponent<Image>();
    public static GameObject DialogBoxTextObject = GameObject.Find("DialogBoxText");
    public static GameObject NamePlateTextObject = GameObject.Find("NamePlateText");
    public static bool dialogMode = false;

    public static void ParseInputLine(string stringToParse)
    {
        string withOutTabs = stringToParse.Replace("\t", "");
        stringToParse = withOutTabs;

        if (stringToParse == "")
        {
            return;
        }
        if (stringToParse.StartsWith("\""))
        {
            Say(stringToParse);
        }
        
        string[] seperateString = { " ", "'", "\"", "(", ")"};
        string[] args = stringToParse.Split(seperateString, StringSplitOptions.RemoveEmptyEntries);
        foreach (Character character in CharacterList)
        {
            if (args[0] == character.shortName)
            {
                SplitToSay(stringToParse, character);
            }
        }

        if (args[0] == "show")
        {
            showImage(stringToParse);
        }

        if (args[0] == "Clear")
        {
            ClearScene();
        }

        if (args[0] == "Character")
        {
            CreateNewCharacter(stringToParse);
        }
        if (args[0] == "ChangeEmotion")
        {
            foreach (Character character in CharacterList)
            {
                if (args[1] == character.shortName)
                {
                    character.emotion = args[2];
                }
            }
        }

        if (args[0] == "ChangeColor")
        {
            foreach (Character character in CharacterList)
            {
                if (args[1] == character.shortName)
                {
                    Color MyColour = Color.clear; ColorUtility.TryParseHtmlString (args[2], out MyColour);
                    character.color = MyColour;
                }
            }
        }

        if (args[0] == "ChangeSpeed"){
            foreach (Character character in CharacterList)
            {
                if (args[1] == character.shortName)
                {
                    character.textSpeed = float.Parse(args[2]);
                }
            }
        }

        if (args[0] == "PlayMusic"){
            bgmManager.clip = Resources.Load("Sound/Music/" + args[1]) as AudioClip;
            bgmManager.Play();
        }

        if (args[0] == "StopMusic"){
            bgmManager.Stop();
        }
    }

    #region Say Stuff

    public static void SplitToSay (string stringToParse, Character character)
    {
        int toQuote = stringToParse.IndexOf("\"") + 1;
        int endQuote = stringToParse.Length - 1;
        string stringToOutput = stringToParse.Substring(toQuote, endQuote - toQuote);
        Say(character, stringToOutput, character.emotion);
    }

    public static void Say(string what)
    {
        if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        //DialogBoxTextObject.GetComponent<TextMeshProUGUI>().text = what;
        dialogMode = true;
        DialogBoxTextObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().textSpeed = 0.06f;
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().PutDialog(what);
        NamePlateTextObject.GetComponent<TextMeshProUGUI>().text = "";
        PortraitImage.sprite = null;
    }

    public static void Say(Character character, string what, string sideImage)
    {
        if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        //DialogBoxTextObject.GetComponent<TextMeshProUGUI>().text = what;
        dialogMode = true;
        DialogBoxTextObject.GetComponent<TextMeshProUGUI>().color = character.color;
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().textSpeed = character.textSpeed;
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().PutDialog(what, character.voice);
        NamePlateTextObject.GetComponent<TextMeshProUGUI>().text = character.fullName;
        PortraitImage.sprite = Resources.Load<Sprite> ("Images/" + character.shortName + '/' + sideImage);
    }

    

    #endregion

    #region Image Stuff

    public static void showImage(string stringToParse)
    {
        var imageToUse = new Regex(@"show (?<ImageFileName>[^.]+)");
        var matches = imageToUse.Match(stringToParse);
        string imageToShow = matches.Groups["ImageFileName"].ToString();

        BackgroundImage.sprite = Resources.Load<Sprite>("Images/background/" + imageToShow);
    }

    public static void ClearScene()
    {
        BackgroundImage.sprite = null;
    }
    #endregion

    #region New Character

    public static void CreateNewCharacter(string stringToParse)
    {
        string newCharShortName = null;
        string newCharFullName = null;
        Color newCharColor = Color.white;
        string newCharEmotion = null;
        float newCharTextSpeed = 0.06f;
        string newCharVoice = null;

        var characterExpression = new Regex(@"Character\((?<shortName>[a-zA-Z???-??????-???0-9_]+), (?<fullName>[a-zA-Z???-??????-???0-9_]+), color=(?<characterColor>[a-zA-Z???-??????-???0-9_]+), emotion=(?<emotion>[a-zA-Z???-??????-???0-9_]+), speed=(?<textSpeed>[a-zA-Z???-??????-???0-9_.]+), voice=(?<voice>[a-zA-Z???-??????-???0-9_.]+)\)");
        if (characterExpression.IsMatch(stringToParse))
        {
            var matches = characterExpression.Match(stringToParse);
            newCharShortName = matches.Groups["shortName"].ToString();
            newCharFullName = matches.Groups["fullName"].ToString();
            newCharColor = Color.clear; ColorUtility.TryParseHtmlString(matches.Groups["characterColor"].ToString(), out newCharColor);
            newCharEmotion = matches.Groups["emotion"].ToString();
            newCharTextSpeed = float.Parse(matches.Groups["textSpeed"].ToString());
            newCharVoice = matches.Groups["voice"].ToString();
        }


        CharacterList.Add(new Character(newCharShortName, newCharFullName, newCharColor, newCharEmotion, newCharTextSpeed, newCharVoice));
    }
    #endregion
}
