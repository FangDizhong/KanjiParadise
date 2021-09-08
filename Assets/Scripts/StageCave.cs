using UnityEngine;
using System.Collections.Generic;
using FairyGUI;

using UnityEngine.SceneManagement;

public class StageCave : MonoBehaviour
{
    private GComponent _stageCave;
    private GComponent _stageContainer;
    private Controller _viewController;
    private GButton _btnStart;
    private Transition _startGame;

    private Dictionary<string, GComponent> _stageButtons;

    // public Gradient lineGradient;

    void Awake()
    {
#if (UNITY_5 || UNITY_5_3_OR_NEWER)
        //Use the font names directly
        UIConfig.defaultFont = "PottaOne-Regular";
#else
        //Need to put a ttf file into Resources folder. Here is the file name of the ttf file.
        UIConfig.defaultFont = "PottaOne-Regular";
#endif
        // UIPackage.AddPackage("FGUI/BasicEl");
        UIPackage.AddPackage("FGUI/Main");

        // UIConfig.verticalScrollBar = "ui://Basics/ScrollBar_VT";
        // UIConfig.horizontalScrollBar = "ui://Basics/ScrollBar_HZ";
        // UIConfig.popupMenu = "ui://Basics/PopupMenu";
        // UIConfig.buttonSound = (NAudioClip)UIPackage.GetItemAsset("Basics", "click");
    }

    void Start()
    {
        Stage.inst.onKeyDown.Add(OnKeyDown);

        _stageCave = this.GetComponent<UIPanel>().ui;

        // Fade in
        GComponent _fade = UIPackage.CreateObject("Main", "Fade").asCom;
        _stageCave.AddChild(_fade).Center();
        Transition _fadeIn = _fade.GetTransition("FadeIn");
        _fadeIn.timeScale = 0.5f; //把速度设置为0.5倍
        _fadeIn.Play();
    }

    

    void Update()
    {
        // _startGame.Play ();
    }

    void OnKeyDown(EventContext context)
    {
        if (context.inputEvent.keyCode == KeyCode.Escape)
        {
            Application.Quit();
        }
    }

    
}