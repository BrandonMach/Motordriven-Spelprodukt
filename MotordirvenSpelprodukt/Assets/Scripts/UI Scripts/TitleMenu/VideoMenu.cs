using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoMenu : MenuAbstract, IMenu
{
    public TMPro.TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        SettingResolutionDropDownContent();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    void SettingResolutionDropDownContent()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResoultionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResoultionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResoultionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResoultion(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public override void ClickBack()
    {
        base.ClickBack();
    }

    public void ClickESC()
    {
        base.ClickESC();
    }

    public override void ClickMenuOption1()
    {
        base.ClickMenuOption1();
    }

    public override void ClickMenuOption2()
    {
        base.ClickMenuOption2();
    }

    public override void ClickMenuOption3()
    {
        _prevMenu.SetActive(true);
        gameObject.SetActive(false);

        Debug.Log(_prevMenu.name + _prevMenu.activeSelf);
    }
}
