using UnityEngine;
using System.Collections.Generic;
using FairyGUI;

using UnityEngine.SceneManagement;

public class StageCave : MonoBehaviour
{
    private GComponent _stageCave;
    private GComponent _numMask;
    private Controller _showNum;
    private GList _charList;
    private Dictionary<string, GComponent> _stageCaveObjects;
    // private GComponent _stageContainer;
    // private Controller _viewController;
    // private GButton _btnStart;
    // private Transition _startGame;

    // private Dictionary<string, GComponent> _stageButtons;

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

        _numMask = _stageCave.GetChild("num_mask").asCom;
        _showNum = _numMask.GetController("ShowNum");
        _charList = _stageCave.GetChild("character_list").asList;
        _charList.RemoveChildrenToPool();

        string[] _char =new string[] {"一","二","三","四"};
        int cnChar = _char.Length;
        for (int i = 0; i < cnChar; i++)
        {
            GButton item = _charList.AddItemFromPool().asButton;
            // item.GetChild("t0").text = "" + (i + 1);
            item.GetChild("title").text = _char[i];
            // item.GetChild("t2").asTextField.color = testColor[UnityEngine.Random.Range(0, 4)];
            // item.GetChild("star").asProgress.value = (int)((float)UnityEngine.Random.Range(1, 4) / 3f * 100);
        
            _charList.onClickItem.Add(onClickItem);
        }


        // Fade in
        GComponent _fade = UIPackage.CreateObject("Main", "Fade").asCom;
        _stageCave.AddChild(_fade).Center();
        Transition _fadeIn = _fade.GetTransition("FadeIn");
        _fadeIn.timeScale = 0.5f; //把速度设置为0.5倍
        _fadeIn.Play( ()=>{_stageCave.RemoveChild(_fade).Dispose();} );
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

        // if (context.inputEvent.keyCode == KeyCode.Alpha1)
        // {
        //     _showNum.selectedIndex = 1;
        // }

        // if (context.inputEvent.keyCode == KeyCode.Alpha2)
        // {
        //     _showNum.selectedIndex = 2;
        // }
    }

    void onClickItem(EventContext context)
    {
        Debug.Log(context);
        int index = _charList.GetChildIndex((GObject)context.data);
        _showNum.selectedIndex = index + 1;
    }
}