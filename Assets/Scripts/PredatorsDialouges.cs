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

    private int currentDialogueIndex = 0; 
    private int currentPhase = 1;  
    private bool playerOpenedDoor = false;



    void Start()
    {
        acceptButton.gameObject.SetActive(false);
        refuseButton.gameObject.SetActive(false);
        StartDialoguePhase1();
    }

    void StartDialoguePhase1()
    {
        StartCoroutine(DisplayDialogueSequence(PredatorManP1, GirlVsManP1, 1));
    }

    void StartDialoguePhase2()
    {
        StartCoroutine(DisplayDialogueSequence(PredatorManP2, GirlVsManP2, 2));
    }

    void StartDialoguePhase3()
    {
        StartCoroutine(DisplayDialogueSequence(PredatorGrandmaP3, GirlVsGrandmaP3, 3));
    }

    IEnumerator DisplayDialogueSequence(Dialogue[] predatorDialogues, Dialogue[] girlDialogues, int phase)
    {
        currentDialogueIndex = 0;
        if(phase == 1)
        {
            for (int i = 0; i < predatorDialogues.Length; i++)
            {
                dialogueText.text = predatorDialogues[i].sentence;
                yield return new WaitForSeconds(2f);
                if (i < girlDialogues.Length)
                {
                    dialogueText.text = girlDialogues[i].sentence;
                    yield return new WaitForSeconds(2f);
                }
            }
            ShowDecisionOptions();
        }
        else if(phase == 2)
        {
           
                dialogueText.text = predatorDialogues[0].sentence;

                //here you enable the player detection movement

                yield return new WaitForSeconds(1f);
                //change supporting text here --- telling the player to pan through the predator
                TelephoneController.isPanningEnabled=true;
            yield return new WaitForSeconds(1f);
            dialogueText.text = predatorDialogues[1].sentence;


            //if (i < girlDialogues.Length)
            //{
            //    dialogueText.text = girlDialogues[i].sentence;
            //    yield return new WaitForSeconds(2f);
            //}

        }

    }

    void ShowDecisionOptions()
    {
        acceptButton.gameObject.SetActive(true);
        refuseButton.gameObject.SetActive(true);

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

    public void PlayerRefuses()
    {
        Debug.Log("Player refused to open the door.");
        acceptButton.gameObject.SetActive(false);
        refuseButton.gameObject.SetActive(false);

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
