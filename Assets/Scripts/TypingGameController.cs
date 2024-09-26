using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Text;

public class TypingGameController : MonoBehaviour
{

    public TMP_Text displayText; // 显示数字串的文本
    public TMP_InputField inputField; // 玩家输入框
    public TMP_Text timerText; // 计时器文本
    public bool success;
    
    public Animator animator;


   public Image circularTimerImage; // 公共变量，用于在 Inspector 中赋值

    private string[] numberStrings = new string[3]; // 存储三组数字串
    private int currentStringIndex = 0; // 当前显示的数字串索引
    private bool gameStarted = false;
    private float remainingTime = 10f; // 游戏时长
    private float totalDuration = 10f;

    private int attemptsLeft = 3; // 剩余尝试次数

    private string playerInputString = "";





    
    // public Text feedbackText;
    void Start()
    {
        InitializeNumberStrings();
        UpdateDisplayText();
        StartCoroutine(StartTimer());
        //inputField.onValueChanged.AddListener(OnInputChanged);
        // OnMessageArrived();
        // flashEffect = GetComponent<FlashEffect>();
       
    }
    void OnEnable()
    {
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
            Debug.Log("Update开始执行");
            // 您的更新逻辑
            // ...
            Debug.Log("准备检查输入");
            // 检查输入的代码
            Debug.Log("输入检查完成");
        }
        catch (Exception e)
        {
            Debug.LogError($"发生异常: {e.Message}\n{e.StackTrace}");
        }

        
    }

    // 初始化三组预定义的数字串
void InitializeNumberStrings()
{
    numberStrings[0] = "9202265545";
    numberStrings[1] = "8135377601";
    numberStrings[2] = "1372649268";
}

// 更新显示的数字串
void UpdateDisplayText()
{   Debug.Log("Updating display text");
    // 生成一个 0 到 numberStrings.Length - 1 之间的随机数
    currentStringIndex = UnityEngine.Random.Range(0, numberStrings.Length);
    displayText.text = "+" + numberStrings[currentStringIndex];
}

    // 启动计时器
    IEnumerator StartTimer()
    {
        Debug.Log("Starting timer");
        while (remainingTime >= 0)
        {
            circularTimerImage.fillAmount = remainingTime / totalDuration;
            timerText.text = remainingTime.ToString("F0");
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
    }

    
    void EndGame(bool success)
    {
        gameStarted = false;
        if (success)
        {
            success = true;
            //ResetGame();
            Debug.Log("Congratulations! You won!");

        }
        else
        {
            ResetGame();
            Debug.Log("Game Over!");
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
        if(msg.Length == 1)
        {
            Debug.Log(msg);
            if (playerInputString.Length < numberStrings[currentStringIndex].Length)
            {
        // 获取每个输入的字符并添加到字符串中
            playerInputString += msg;
            inputField.text = playerInputString;
        }
        }
        if(playerInputString.Length == numberStrings[currentStringIndex].Length){
        // 判断输入的字符串是否与目标字符串相等
        bool isEqual = playerInputString.Equals(numberStrings[currentStringIndex]);

        if (isEqual)
        {
            Debug.Log("输入正确！");
            // 在这里添加正确输入的处理逻辑
            inputField.text = "";
            EndGame(true);
        }
        else
        {
            //Debug.Log("输入错误。正确的字符串是：" + targetString);
            // 在这里添加错误输入的处理逻辑
            if (attemptsLeft > 1)
            {
                attemptsLeft-=1;
                Debug.Log($"尝试次数剩余：{attemptsLeft}");
                FailEffect();
                ResetGame();
                // 清空输入框并更新显示的数字串
                // inputField.text = "";
                // UpdateDisplayText();
            }
            else
            {
                // 尝试次数用尽，结束游戏
                FailEffect();
                EndGame(false);
                Debug.Log("尝试次数用尽，游戏结束。");
            
            }
        }
        }
        
    

        
    
    }
    
     void OnConnectionEvent(bool success)
    {
        
    }
    
    

    }
