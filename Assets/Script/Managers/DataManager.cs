using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [SerializeField] public AudioMixer SFXMixer;
    [SerializeField] public AudioMixer MusicMixer;
    [SerializeField] public Volume GlobalVolume;
    [NonSerialized] public ColorAdjustments exposure;

    public float SFXVolume;
    public float MusicVolume;
    public float BrightnessValue;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GlobalVolume.gameObject);
        }
        LoadData();

        GlobalVolume.profile.TryGet(out exposure);
        SFXMixer.SetFloat("SFXVolume", SFXVolume);//Mathf.Log10(SFXVolume + 0.01f) * 80);
        MusicMixer.SetFloat("MusicVolume", MusicVolume);// Mathf.Log10(MusicVolume + 0.01f) * 80);
        exposure.postExposure.value = BrightnessValue;

    }
    private void OnApplicationQuit()
    {
        WriteData();
    }
    class SaveData
    {
        public float SFXVolume;
        public float MusicVolume;
        public float BrightnessValue;
    }

    public void WriteData()
    {
        SaveData data = new SaveData();
        data.SFXVolume = SFXVolume;
        data.MusicVolume = MusicVolume;
        data.BrightnessValue = BrightnessValue;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            SFXVolume = data.SFXVolume;
            MusicVolume = data.MusicVolume;
            BrightnessValue = data.BrightnessValue;
        }
    }
}
