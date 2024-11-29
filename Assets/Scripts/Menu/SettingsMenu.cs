using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioMixer audioMixer;


    public GameObject SettingsMenu;

    public void OnSettings()
    {
        SettingsMenu.SetActive(true);

    }

    public void MainMenu()
    {
        SettingsMenu.SetActive(false);
    }


    
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);

    }

    public void SetQuality (int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

}
