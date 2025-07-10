using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MomChecklist : MonoBehaviour
{
    public GameObject pollo;
    public GameObject ventilador;
    public GameObject lavadora;
    public GameObject basura;
    public GameObject nevera;
    public GameObject horno;
    public GameObject microondas;
    public GameObject ventana;

    public int ChickenCooked = 0;
    public int ChickenSeasoned = 0;
    public bool FanOn = true;
    public bool WashingMachineOn = true;
    public bool TrashTakenOut = false;
    public bool FridgeClosed = true;
    public bool FurnaceClosed = true;
    public bool MicrowaveClosed = true;
    public bool WindowClosed = true;
    public int errors = 0;
    public Collider2D[] colliders;
    public List<string> MomText;
    public GameObject DialoguePanel;
    public TMP_Text DialogueText;
    public bool didDialogueStart;
    public int lineIndex;

    public void Update()
    {
        //mira si presiona el espacio y si el panel de dialogo esta activo
        if (Input.GetKeyDown(KeyCode.Return) && !didDialogueStart)
        {
        }
        else if (Input.GetKeyDown(KeyCode.Return) && DialogueText.text == MomText[lineIndex])
        {
            NextDialogueLine();
        }
    }
    public void StartMom()
    {
        CameraMovement.instance.canMove = false;
        HoldManager.Instance.canGrab = false;

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        CameraMovement.instance.SwitchCamera(CameraMovement.instance.cameras[3]);
        CameraMovement.instance.blinkScript.Blink(3);
        CheckThings();
    }

    public void CheckThings()
    {
        Debug.Log("Checking things...");
        if (pollo.GetComponent<Chicken>().frozenPercent > 0)
        {
            ChickenCooked = 0; // Chicken is frozen
        }
        else if (pollo.GetComponent<Chicken>().cookingPercent < 50)
        {
            ChickenCooked = 1; // Chicken is raw
        }
        else if (pollo.GetComponent<Chicken>().cookingPercent >= 50 && pollo.GetComponent<Chicken>().cookingPercent < 70)
        {
            ChickenCooked = 2; // Chicken is cooked but not burnt
        }
        else if (pollo.GetComponent<Chicken>().cookingPercent >= 70)
        {
            ChickenCooked = 3; // Chicken is burnt
        }
        ChickenSeasoned = Convert.ToInt32(pollo.GetComponent<Chicken>().seasoning);
        FanOn = ventilador.GetComponent<Fan>().isOn;
        WashingMachineOn = lavadora.GetComponent<Washing>().completada;
        TrashTakenOut = basura.GetComponent<TrashCan>().isOpen;
        FridgeClosed = !nevera.GetComponent<Fridge>().isOn;
        FurnaceClosed = !horno.GetComponent<Oven>().isOn;
        MicrowaveClosed = !microondas.GetComponent<Defroster>().isOn;
        WindowClosed = !ventana.GetComponent<Window>().isOpen;
        StartDialogue();

    }
    public void StartDialogue()
    {
        Debug.Log("Starting dialogue...");
        SendText();
        DialoguePanel.SetActive(true);
        didDialogueStart = true;
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }
    public void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < MomText.Count)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            DialoguePanel.SetActive(false);
            didDialogueStart = false;
            lineIndex = 0;
        }
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;
        foreach (char letter in MomText[lineIndex])
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); 
        }
    }

    public static MomChecklist Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        colliders = FindObjectsOfType<Collider2D>();
    }

    public void SendText()
    {
        Debug.Log("Sending text to mom...");
        MomText[0] = "I'm home darling! Did you do what I told you? How is the chicken?\n";
        errors = 0;
        if (ChickenCooked == 0)
        {
            MomText.Add("What?? How are we supposed to eat a frozen chicken?\n");
            errors++;
        }
        else if (ChickenCooked == 1)
        {
            MomText.Add("The chicken is raw, do you expect us to eat that?\n");
            errors++;
        }
        else if (ChickenCooked == 2)
        {
            MomText.Add("The chicken seems perfectly cooked \n");
            if (ChickenSeasoned == 0)
            {
                MomText.Add("but you forgot to season it, please do so before serving.\n");
                errors++;
            }
            else if (ChickenSeasoned < 15)
            {
                MomText.Add("and it's seasoned, but still kinda tasteless.\n");
                errors++;
            }
            else if (ChickenSeasoned <= 20)
            {
                MomText.Add("and it's seasoned, well done!\n");
            }
            else if (ChickenSeasoned > 20)
            {
                MomText.Add("but you seasoned the chicken too much, it's too salty now!\n");
                errors++;
            }
        }
        else if (ChickenCooked == 3)
        {
            MomText.Add("This chicken is burnt, thank god you didn't burn the house too!\n");
            errors++;
        }
        if (FanOn)
        {
            if (errors == 0)
            {
                MomText.Add("But hey, you left the fan on! I explicitly told you to turn it off before I came home.\n");
            }
            else
            {
                MomText.Add("And you left the fan on! I explicitly told you to turn it off before I came home.\n");
            }
            errors++;
        }
        if (WashingMachineOn)
        {
            if (errors == 0)
            {
                MomText.Add("But you left the washing machine on, I'm pretty sure it has already finished.\n");
            }
            else
            {
                MomText.Add("You also left the washing machine on, didn't you hear the music or what?\n");
            }
            errors++;
        }
        if (!TrashTakenOut)
        {
            if (errors == 0)
            {
                MomText.Add("But you didn't take out the trash, I can smell it from here!\n");
            }
            else
            {
                MomText.Add("And you didn't take out the trash, I can smell it from here!\n");
            }
            errors++;
        }
        if (WindowClosed)
        {
            if (errors == 0)
            {
                MomText.Add("But the window is still closed, the entire house will smell like chicken.\n");
            }
            else
            {
                MomText.Add("Didn't I tell you to open the window? Now the entire house will smell like chicken.\n");
            }
            errors++;
        }
        if (!FridgeClosed)
        {
            if (errors == 0)
            {
                MomText.Add("You left the fridge open, the food's gonna go to waste!\n");
            }
            else
            {
                MomText.Add("Ugh! You also left the fridge open, the food's gonna go to waste!\n");
            }
            errors++;
        }
        if (!FurnaceClosed)
        {
            if (errors == 0)
            {
                MomText.Add("You left the furnace open, I can feel the heat from here!\n");
            }
            else
            {
                MomText.Add("And you left the furnace open, I can feel the heat from here!\n");

            }
            errors++;
        }
        if (!MicrowaveClosed)
        {
            if (errors == 0)
            {
                MomText.Add("But you left the microwave open, it will get dusty.\n");
            }
            else
            {
                MomText.Add("And you left the microwave open too, it's gonna get real dusty.\n");
            }
            errors++;
        }
        if (errors == 0)
        {
            MomText.Add("You did everything right, I'm proud of you!\n");
        }
        else if (errors <= 3 && ChickenCooked == 2)
        {
            MomText.Add("You forgot a few things but at least we have something to eat.\n");
        }
        else if (errors <= 5 && ChickenCooked == 2)
        {
            MomText.Add("You forgot a lot of things, but at least we have something to eat.\n");
        }
        else
        {
            MomText.Add("We have no dinner, I don't even know what to say.\n");
        }
    }
}
