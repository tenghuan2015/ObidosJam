using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Text;
using UnityEditor;
public class TypingGameController : MonoBehaviour
{

    public TMP_Text displayText; // 显示数字串的文本
    public TMP_InputField inputField; // 玩家输入框
    public TMP_Text timerText; // 计时器文本
    public bool success;
    
    public Animator animator;
    public Animator animator01;
      public Animator animator02;
      public Animator animator03;
    public Animator animator04;
     public Animator animator05;
                       
      public Animator animator06;
       public Animator animator07;
       public Animator animator08;
       public Animator animator09;
       public Animator animator00;


   public Image circularTimerImage; // 公共变量，用于在 Inspector 中赋值

    //private string[] numberStrings = new string[3]; // 存储三组数字串
    private int currentStringIndex = 0; // 当前显示的数字串索引
    private bool gameStarted = false;
    private float remainingTime = 10f; // 游戏时长
    private float totalDuration = 10f;

    private int attemptsLeft = 6; // 剩余尝试次数

    private string playerInputString = "";

    [SerializeField] private AudioSource audioSource;
    public NumGenerator numGenerator;
    private string currentNum;
    void Start()
    {
        //InitializeNumberStrings();
        UpdateDisplayText();
        StartCoroutine(StartTimer());
        //inputField.onValueChanged.AddListener(OnInputChanged);
        // OnMessageArrived();
        // flashEffect = GetComponent<FlashEffect>();
         if (animator == null)
            animator = GetComponent<Animator>();

    }
    
    public void FailEffect()
    {
         animator.SetTrigger("Flash");
    }
    
    void Update()
    {
        try
        {
            // Debug.Log("Update开始执行");
            // // 您的更新逻辑
            // // ...
            // Debug.Log("准备检查输入");
            // // 检查输入的代码
            // Debug.Log("输入检查完成");

            TimeOut();
        }
        catch (Exception e)
        {
            Debug.LogError($"发生异常: {e.Message}\n{e.StackTrace}");
        }

        
    }

    // 初始化三组预定义的数字串
    /*
void InitializeNumberStrings()
{
    numberStrings[0] = "9202265545";
    numberStrings[1] = "8135377601";
    numberStrings[2] = "1372649268";
}
    */

// 更新显示的数字串
void UpdateDisplayText()
{
         Debug.Log("Updating display text");
        // 生成一个 0 到 numberStrings.Length - 1 之间的随机数
        //currentStringIndex = UnityEngine.Random.Range(0, numberStrings.Length);
        currentNum = numGenerator.GetCurrentRanNum();
        displayText.text = "+" + currentNum;
}

    // 启动计时器
    IEnumerator StartTimer()
    {
        UpdateDisplayText();
        Debug.Log("Starting timer");
        while (remainingTime >= 0)
        {
            
            circularTimerImage.fillAmount = remainingTime / totalDuration;
            timerText.text = remainingTime.ToString("F0");
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
    }
    void TimeOut()
    {
//        Debug.Log(remainingTime);
        if(remainingTime < 0.01f && !success)
    { 
        checkFailStatus();
    }
    }

    
    void EndGame(bool success)
    {
        gameStarted = false;
        if (success)
        {
            //ResetGame();
            Debug.Log("Congratulations! You won!");
            playSound("Assets/Sounds/Dialog/F1.mp3");
            //PhoneInputController.Instance.PlayAudio("Assets/Sounds/Dialog/F1.mp3");

        }
        else
        {
            //ResetGame();
            StopAllCoroutines();
            inputField.text = ""; // 清空输入框
            Debug.Log("Game Over!");
            playSound("Assets/Sounds/Effect/NoAnswer.mp3");
        }
    }

    // 开始游戏
    public void ResetGame()
    {
        StopAllCoroutines();
        gameStarted = true;
        inputField.text = ""; // 清空输入框
        // currentStringIndex = 0;
        remainingTime = 10f;
        UpdateDisplayText();
        playerInputString = "";
        StartCoroutine(StartTimer());
    }
    
 
    void OnMessageArrived(string msg)
    {
        //Debug.Log(msg);
        switch (msg)
        {
            case "1":
                animator01.SetTrigger("1press");
                break;
            case "2":
                animator02.SetTrigger("2press");
                break;
            case "3":
                animator03.SetTrigger("3press");
                break;
            case "4":
                animator04.SetTrigger("4press");
                break;
            case "5":
                animator05.SetTrigger("5press");
                break;
            case "6":
                animator06.SetTrigger("6press");
                break;
            case "7":
                animator07.SetTrigger("7press");
                break;
            case "8":
                animator08.SetTrigger("8press");
                break;
            case "9":
                animator09.SetTrigger("9press");
                break;
            case "0":
                animator00.SetTrigger("0press");
                break;
        }
        

        if(msg.Length == 1)
        {
            Debug.Log(msg);
            
            if (playerInputString.Length < currentNum.Length)
            {playSound("Assets/Sounds/Effect/ding.mp3");
        // 获取每个输入的字符并添加到字符串中
            playerInputString += msg;
            inputField.text = playerInputString;
        }
        }
        if(playerInputString.Length == currentNum.Length){
        // 判断输入的字符串是否与目标字符串相等
        bool isEqual = playerInputString.Equals(currentNum);

        if (isEqual)
        {
            Debug.Log("输入正确！");
            // 在这里添加正确输入的处理逻辑
            inputField.text = "";
            success = true;
            EndGame(success);
        }
        else
        {
            checkFailStatus();
        }
        }
        
    

        
    
    }
    private void checkFailStatus(){
            // 在这里添加错误输入的处理逻辑
            if (attemptsLeft > 1)
            {
                attemptsLeft-=1;
                Debug.Log($"尝试次数剩余：{attemptsLeft}");
                FailEffect();
                playSound("Assets/Sounds/Effect/NoAnswer.mp3");
                ResetGame();
                // 清空输入框并更新显示的数字串
                // inputField.text = "";
                // UpdateDisplayText();
            }
            else
            {
                // 尝试次数用尽，结束游戏
                FailEffect();
                success = false;
                remainingTime = 10f;
                UpdateDisplayText();
                EndGame(success);
                Debug.Log("尝试次数用尽，游戏结束。");
            
            }

    }
     void OnConnectionEvent(bool success)
    {
        
    }
    
    private void playSound(string path){
        // the audio path should be like this: "Assets/Sounds/Dialog/F1.mp3"
        AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("can't find audio file: " + path);
        }
    }


    }
