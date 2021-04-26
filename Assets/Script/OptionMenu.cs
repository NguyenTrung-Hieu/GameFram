using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Dropdown ResDropdown;
    Resolution[] resolutions;
    private void Start()
    {
        var options = new List<string>();
        var resolutions = Screen.resolutions;
        ResDropdown.ClearOptions();
        var list = new List<Vector2>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            var resVec = new Vector2(resolutions[i].width, resolutions[i].height);

            // If the resolution is unique in dimension, add it to our list.
            if (!list.Contains(resVec))
                list.Add(resVec);
        }

        resolutions = new Resolution[list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            resolutions[i].width = (int)list[i].x;
            resolutions[i].height = (int)list[i].y;

            // Add the resolution to the list we'll apply to the dropdown later.
            options.Add(resolutions[i].width + "x" + resolutions[i].height);
        }
        ResDropdown.AddOptions(options);
    }
    public void SetResolution(int ResIndex)
    {
        Resolution resolution = resolutions[ResIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public void setVolume(float Volume)
    {
        audioMixer.SetFloat("VolumeMixer", Volume);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
