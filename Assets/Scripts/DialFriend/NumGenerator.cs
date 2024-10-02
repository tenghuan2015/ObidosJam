using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NumGenerator : MonoBehaviour
{
    public GameObject phoneNumberPrefab; // 电话号码的预制体

    [System.Serializable]
    public struct SpawnArea
    {
        public Vector2 min;
        public Vector2 max;
    }

    public SpawnArea[] spawnAreas = new SpawnArea[2]; // 定义两个生成区域
    
    public float spawnInterval = 10f; // 生成新号码的间隔时间
    public float displayDuration = 10f; // 号码显示的持续时间
    private GameObject currentPhoneNumber;
    private float nextSpawnTime;
    private string currentNum;
    private void Start()
    {
        GeneratePhoneNumber();
    }
    void Update()
    {
        // 检查是否到了生成新号码的时间
        if (Time.time >= nextSpawnTime && currentPhoneNumber == null)
        {
            GeneratePhoneNumber();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void GeneratePhoneNumber()
    {
        // 随机选择一个生成区域
        SpawnArea selectedArea = spawnAreas[Random.Range(0, spawnAreas.Length)];

        // 在选定的区域内随机生成位置
        Vector3 randomPosition = new Vector2(
            Random.Range(selectedArea.min.x, selectedArea.max.x),
            Random.Range(selectedArea.min.y, selectedArea.max.y)
        );

        // 实例化预制体
        currentPhoneNumber = Instantiate(phoneNumberPrefab, transform);
        RectTransform rectTransform = currentPhoneNumber.GetComponentInChildren<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = randomPosition;
        }

        // 设置随机电话号码
        SetRandomPhoneNumber(currentPhoneNumber);

        // 启动协程以在指定时间后销毁号码
        StartCoroutine(DestroyAfterDelay(currentPhoneNumber, displayDuration));

        //change display layer
        // 设置排序顺序为最上层
        MeshRenderer spriteRenderer = currentPhoneNumber.GetComponentInChildren<MeshRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 32767; // 最大排序顺序值
        }
        else
        {
            Renderer renderer = currentPhoneNumber.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sortingOrder = 32767;
            }
        }
    }

    void SetRandomPhoneNumber(GameObject phoneNumberObject)
    {
        TMP_Text textMesh = phoneNumberObject.GetComponentInChildren<TMP_Text>();
        if (textMesh != null)
        {
            textMesh.text = GenerateRandomPhoneNumber();
        }
    }

    string GenerateRandomPhoneNumber()
    {
        // 生成一个不含特殊字符的10位数字
        currentNum = Random.Range(1000000000, 1000000000 + 1000000000).ToString("D10");
        return currentNum;
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
        currentPhoneNumber = null;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (SpawnArea area in spawnAreas)
        {
            Vector3 center = this.transform.TransformPoint(new Vector3((area.min.x + area.max.x) / 2, (area.min.y + area.max.y) / 2, 0));
            Vector3 size = new Vector3(area.max.x - area.min.x, area.max.y - area.min.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
    public string GetCurrentRanNum(){
        if(currentNum!=null){
            Debug.Log("current num is :" + currentNum);
            return currentNum;
        }
        return null;
    }


}
