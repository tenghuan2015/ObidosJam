using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // Allows the array to be edited in the Unity inspector
public class Dialogue
{
    [TextArea(2, 5)]
    public string sentence;
}

public class PredatorsDialouges : MonoBehaviour
{
    public Dialogue[] PredatorManP1;
    public Dialogue[] GirlVsManP1;
    public Dialogue[] PredatorManP2;
    public Dialogue[] GirlVsManP2;
    public Dialogue[] PredatorGrandmaP3;
    public Dialogue[] GirlVsGrandmaP3;
    public Text dialogueText;  
    public Text SupportingText;
    public Button acceptButton; 
    public Button refuseButton;
    public GameObject Buttons;
    public static int phase = 1;
    private int currentDialogueIndex = 0; 
    private int currentPhase = 1;  
    private bool playerOpenedDoor = false;

    void Start()
    {
        Buttons.SetActive(false);
        StartDialoguePhase1();
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
    void StartDialoguePhase1()
    {
        StartCoroutine(DisplayDialogueSequence(PredatorManP1, GirlVsManP1));
    }
    void StartDialoguePhase2()
    {
        StartCoroutine(DisplayDialogueSequence(PredatorManP2, GirlVsManP2));
    }
    void StartDialoguePhase3()
    {
        StartCoroutine(DisplayDialogueSequence(PredatorGrandmaP3, GirlVsGrandmaP3));
    }

    IEnumerator DisplayDialogueSequence(Dialogue[] predatorDialogues, Dialogue[] girlDialogues)
    {
        if(phase>1) AttendCall.instance.StartFromKnocking();
        currentDialogueIndex = 0;
        if(phase == 1)
        {
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
            ShowDecisionOptions();
        }
        else if(phase == 2)
        {
            //dialogueText.text = predatorDialogues[0].sentence;
            WritePredatorDialogueText(predatorDialogues[0].sentence);
            yield return new WaitForSeconds(0.5f);

            //here i will enable the player detect the movement
            ShowDecisionOptions();

            yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(1f);
            ShowDecisionOptions();
            //WritePredatorDialogueText(predatorDialogues[1].sentence);
        }
        else if (phase == 3)
        {
            //dialogueText.text = predatorDialogues[0].sentence;
            WritePredatorDialogueText(predatorDialogues[0].sentence);
            yield return new WaitForSeconds(0.5f);

            //here i will enable the player detect the movement
            ShowDecisionOptions();

            yield return new WaitForSeconds(1f);
            
            yield return new WaitForSeconds(1f);
            ShowDecisionOptions();
            //WritePredatorDialogueText(predatorDialogues[1].sentence);
        }
    }
    void ShowDecisionOptions()
    {
        Buttons.SetActive(true);
        SupportingText.gameObject.SetActive(false);
        acceptButton.onClick.AddListener(PlayerAccepts);
        refuseButton.onClick.AddListener(PlayerRefuses);
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
    }
    public void PlayerRefuses()
    {
        Debug.Log("Player refused to open the door.");
        Buttons.SetActive(false);
        phase++;
        dialogueText.gameObject.transform.parent.gameObject.SetActive(false);
        if (currentPhase == 1)
        {
            Invoke("StartDialoguePhase2", 5f); 
        }
        else if (currentPhase == 2)
        {
            Invoke("StartDialoguePhase3", 5f); 
        }
    }
    void EndGame()
    {
        acceptButton.gameObject.SetActive(false);
        refuseButton.gameObject.SetActive(false);
        Debug.Log("End of Game");
    }
}
