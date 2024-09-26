using UnityEngine;
using UnityEngine.UI;
public class ShowTextTemp : MonoBehaviour
{
    [SerializeField] private Text LastMsg;
    [SerializeField] private Button BtLastMsg;
    [SerializeField] private Text ChoiceMsg;
    [SerializeField] private Button BtChoice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BtChoice.onClick.AddListener(ShowChoiceText);
        BtLastMsg.onClick.AddListener(ShowLastMsg);
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
        bool confirm = PhoneInputController.Instance.GetPlayerOption();
        if(confirm){
            ChoiceMsg.text = "Confirmed";
        }
        else{
            ChoiceMsg.text = "Rejected";
        }
    }
}
