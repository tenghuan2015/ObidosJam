using UnityEngine;
using System.Collections;

public class SoundGenerator : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips; // 音频剪辑数组
    private AudioSource audioSource;
    private static SoundGenerator instance; // 单例实例
    public static SoundGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundGenerator>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SoundGenerator");
                    instance = obj.AddComponent<SoundGenerator>();
                }
            }
            return instance;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //StartCoroutine(PlayRandomSounds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            if (audioClips.Length > 0)
            {
                // 随机选择一个音频剪辑
                AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];
                audioSource.clip = randomClip;
                // 播放选中的音频
                audioSource.Play();

                // 等待音频播放完毕
                yield return new WaitForSeconds(randomClip.length);

                // 等待3-5秒
                yield return new WaitForSeconds(Random.Range(3f, 5f));
            }
            else
            {
                yield return null;
            }
        }
    }
    public void PlayRanSound()
    {
        if (audioClips.Length > 0)
        {
            // 随机选择一个音频剪辑
            AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.clip = randomClip;
            // 播放选中的音频
            audioSource.Play();
        }
    }
}
