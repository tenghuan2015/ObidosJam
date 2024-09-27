using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // Allows the array to be edited in the Unity inspector
public class Dialogue
{
    [TextArea(2, 5)]
    public string sentence;
}
public class SceneGeneric : MonoBehaviour
{
    public enum Scene{
        initial, scene1, scene2, scene3, ending
    }
    public Scene scene;

    public Dialogue[] Predator;
    public Dialogue[] Girl;
    public Text dialogueText;
    public Text SupportingText;
    public Button acceptButton;
    public Button refuseButton;
    public Button suspectButton;
    public GameObject Buttons;
    private bool playerOpenedDoor = false;
    private int currentDialogueIndex = 0;

    public List<string> buttonsTexts= new List<string>();

    void Start()
    {
        StartDialogue();
        Buttons.SetActive(false);

    }
    void StartDialogue()
    {
        StartCoroutine(DisplayDialogueSequence(Predator, Girl, ((int)scene)-1));
    }
    IEnumerator DisplayDialogueSequence(Dialogue[] predatorDialogues, Dialogue[] girlDialogues, int num)
    {
        currentDialogueIndex = 0;
       if(num ==0)
        {
            print("num equals 0");
            for (int i = 0; i < predatorDialogues.Length; i++)
            {
                WritePredatorDialogueText(predatorDialogues[i].sentence);
                //dialogueText.text = predatorDialogues[i].sentence;
                yield return new WaitForSeconds(2f);
                if (i < girlDialogues.Length)
                {
                    WriteSupportingText(girlDialogues[i].sentence);
                    //SupportingText.text = girlDialogues[i].sentence;
                    yield return new WaitForSeconds(2f);
                }
            }
            ShowDecisionOptions(false, buttonsTexts[0], buttonsTexts[1]);
        }
       else if(num ==1)
        {
            print("num equals 1");

            WritePredatorDialogueText(predatorDialogues[0].sentence);
            yield return new WaitForSeconds(0.5f);
            //here i will enable the player detect the movement
            ShowDecisionOptions(true, buttonsTexts[0], buttonsTexts[1]);

        }
        else if (num==2)
        {
            print("in phase 3");

            //dialogueText.text = predatorDialogues[0].sentence;
            WritePredatorDialogueText(predatorDialogues[0].sentence);
            yield return new WaitForSeconds(0.5f);

            //here i will enable the player detect the movement
            ShowDecisionOptions(true, buttonsTexts[0], buttonsTexts[1]);
        }
        else
        {
            WritePredatorDialogueText(predatorDialogues[0].sentence);
            //shakes camera
        }
            
    }
    void WriteSupportingText(string str)
    {
        SupportingText.gameObject.SetActive(true);
        SupportingText.text = str;
        SupportingText.gameObject.transform.parent.gameObject.SetActive(true);
        dialogueText.gameObject.transform.parent.gameObject.SetActive(false);
    }
    void WritePredatorDialogueText(string str)
    {
        dialogueText.gameObject.transform.parent.gameObject.SetActive(true);
        dialogueText.text = str;
        SupportingText.gameObject.transform.parent.gameObject.SetActive(false);
    }
    void ShowDecisionOptions(bool suspects, string str1, string str2)
    {
        Buttons.SetActive(true);
        if (suspects)
        {
            refuseButton.gameObject.SetActive(true);
            refuseButton.transform.GetChild(0).GetComponent<Text>().text = str1;
            suspectButton.gameObject.SetActive(true);
            suspectButton.transform.GetChild(0).GetComponent<Text>().text = str2;
            acceptButton.gameObject.SetActive(false);

        }
        else
        {
            refuseButton.gameObject.SetActive(true);
            refuseButton.transform.GetChild(0).GetComponent<Text>().text = str1;
            suspectButton.gameObject.SetActive(false);
            acceptButton.gameObject.SetActive(true);
            acceptButton.transform.GetChild(0).GetComponent<Text>().text = str2;

        }
        SupportingText.gameObject.transform.parent.gameObject.SetActive(false);
        //acceptButton.onClick.AddListener(PlayerAccepts);
        //refuseButton.onClick.AddListener(PlayerRefuses);
        //refuseButton.onClick.AddListener(PlayerSuspects);
    }
    public void PlayerAccepts()
    {
        playerOpenedDoor = true;
        Debug.Log("Player opened the door! Game Over.");
        dialogueText.text = "You opened the door, and you died!";
        EndGame();
    }
    public void PlayerSuspects()
    {
        Buttons.SetActive(false);
        dialogueText.gameObject.transform.parent.gameObject.SetActive(false);
        TelephoneController.isPanningEnabled=true;
        print("player suspects");
        StartCoroutine(EndSuspect());
    }
    IEnumerator EndSuspect()
    {
        //phase++;
        WritePredatorDialogueText(Predator[1].sentence);
        yield return new WaitForSeconds(2);
        ShowDecisionOptions(false, buttonsTexts[2], buttonsTexts[3]);
    }
    public void PlayerRefuses()
    {
        Debug.Log("Player refused to open the door.");
        Buttons.SetActive(false);
        dialogueText.gameObject.transform.parent.gameObject.SetActive(false);
        AttendCall.instance.StartFromKnocking();
    }
    void EndGame()
    {
        acceptButton.gameObject.SetActive(false);
        refuseButton.gameObject.SetActive(false);
        Debug.Log("End of Game");
    }
}
