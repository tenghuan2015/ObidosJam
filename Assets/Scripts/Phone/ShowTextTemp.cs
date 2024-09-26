using UnityEngine;
using UnityEngine.UI;
public class ShowTextTemp : MonoBehaviour
{
    [SerializeField] private Text LastMsg;
    [SerializeField] private Button BtLastMsg;
    [SerializeField] private Text ChoiceMsg;
    [SerializeField] private Button BtChoice;
    [SerializeField] private Button BtPlayMusic1;
    [SerializeField] private Button BtPlayMusic2;
    [SerializeField] private Button BtPlayRing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BtChoice.onClick.AddListener(ShowChoiceText);
        BtLastMsg.onClick.AddListener(ShowLastMsg);
        BtPlayMusic1.onClick.AddListener(PlayMusic1);
        BtPlayMusic2.onClick.AddListener(PlayMusic2);
        BtPlayRing.onClick.AddListener(PlayRing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ShowLastMsg(){
        LastMsg.text = PhoneInputController.Instance.GetLastMsg();
    }
    private void ShowChoiceText()
    {
        int confirm = PhoneInputController.Instance.GetPlayerOption();
        if(confirm == 1){
            ChoiceMsg.text = "Choice A";
        }
        else if(confirm == 0){
            ChoiceMsg.text = "Choice B";
        }
        else
        {
            ChoiceMsg.text = "No Choice";
        }
    }
    private void PlayMusic1(){

        PhoneInputController.Instance.PlayAudio("Assets/Sounds/Dialog/F1.mp3");
    }
    private void PlayMusic2(){
        PhoneInputController.Instance.PlayAudio("Assets/Sounds/Dialog/P1.mp3");
    }
}
