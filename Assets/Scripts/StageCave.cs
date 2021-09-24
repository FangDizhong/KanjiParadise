using UnityEngine;
using System.Collections.Generic;
using FairyGUI;

using UnityEngine.SceneManagement;

// using Commons;

public class StageCave : MonoBehaviour
{
    private GComponent _stageCave;
    private Transition _OpenDoor;

    private GComponent _numMask;
    private Controller _showNum;
    private GList _charBtnList;

    private Dictionary<int, string> _shijiCharDict = new Dictionary<int,string> ();
    
    private int _currentNumIndex = 0;
    private string[] _question;
    private string _currentChar;

    private Dictionary<string, GComponent> _stageCaveObjects;

    //Sound
    private AudioSource _Sound_StoneScrape;
    private AudioSource _Sound_StoneButton;
    private AudioSource _Sound_GetCard;
    [SerializeField] AudioClip[] _SoundList_StoneScrape;
    [SerializeField] AudioClip[] _SoundList_StoneButton;
    // private GComponent _stageContainer;
    // private Controller _viewController;
    // private GButton _btnStart;
    // private Transition _startGame;

    // private Dictionary<string, GComponent> _stageButtons;

    // public Gradient lineGradient;

    CardBagWin _cardBagWin;

    void Awake()
    {
        // #if (UNITY_5 || UNITY_5_3_OR_NEWER)
        //         //Use the font names directly
        //         UIConfig.defaultFont = "PottaOne-Regular";
        // #else
        //         //Need to put a ttf file into Resources folder. Here is the file name of the ttf file.
        //         UIConfig.defaultFont = "PottaOne-Regular";
        // #endif
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
        _OpenDoor = _stageCave.GetTransition("OpenDoor");

        // 指事字数据库
        _shijiCharDict.Add(0,"零");
        _shijiCharDict.Add(1,"一");
        _shijiCharDict.Add(2,"二");
        _shijiCharDict.Add(3,"三");
        _shijiCharDict.Add(4,"四");

        // 题目
        _question = new string[] {"零","三","一","二","四"};

        // 石头转盘
        _numMask = _stageCave.GetChild("num_mask").asCom;
        _numMask.touchable = true;
        _showNum = _numMask.GetController("ShowNum");
        _numMask.onClick.Add(StartNumGame);

        // 汉字按钮组件
        _charBtnList = _stageCave.GetChild("character_list").asList;
        _charBtnList.RemoveChildrenToPool();
        string[] _char =new string[] {"一","二","三","四"};
        int cnChar = _char.Length;
        for (int i = 0; i < cnChar; i++)
        {
            GButton _item = _charBtnList.AddItemFromPool().asButton;
            // item.GetChild("t0").text = "" + (i + 1);
            _item.GetChild("title").text = _char[i];
            _item.GetController("button").selectedPage = "down";
            _item.GetController("button").onChanged.Add(PlayStoneBtnSound);
            // item.GetChild("t2").asTextField.color = testColor[UnityEngine.Random.Range(0, 4)];
            // item.GetChild("star").asProgress.value = (int)((float)UnityEngine.Random.Range(1, 4) / 3f * 100);
            _charBtnList.onClickItem.Add(onClickItem);
        }
        _charBtnList.touchable = false;


        // Fade in
        GComponent _fade = UIPackage.CreateObject("Main", "Fade").asCom;
        _stageCave.AddChild(_fade).Center();
        Transition _fadeIn = _fade.GetTransition("FadeIn");
        _fadeIn.timeScale = 0.5f; //把速度设置为0.5倍
        _fadeIn.Play( ()=>{_stageCave.RemoveChild(_fade).Dispose();} );

        //Sound
        _Sound_StoneScrape = GameObject.Find("/Sound_StoneScrape").GetComponent<AudioSource>();
        _Sound_StoneButton = GameObject.Find("/Sound_StoneButton").GetComponent<AudioSource>();
        _Sound_GetCard = GameObject.Find("/Sound_GetCard").GetComponent<AudioSource>();
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

    void SetNextNumber()
    {   
        _currentNumIndex　+= 1;

        if (_currentNumIndex < (_question.Length))
        {  
            _currentChar = _question[_currentNumIndex];

            foreach (int key in _shijiCharDict.Keys)
            {
                if (_shijiCharDict[key].Equals(_currentChar))
                {
                    _showNum.selectedIndex = key;

                    // play Sound
                    _Sound_StoneScrape.clip = _SoundList_StoneScrape[Random.Range(0, _SoundList_StoneScrape.Length)];
                    _Sound_StoneScrape.PlayOneShot(_Sound_StoneScrape.clip);
                }
            }
        }
    }
    
    void StartNumGame()
    {
        if(_numMask.touchable == true)
        {
            SetNextNumber();
            _numMask.touchable = false;
            _charBtnList.touchable = true;

            //让按钮可点
            for (int i = 0; i < _charBtnList.numChildren; i++)
            {
                GButton _item = _charBtnList.GetChildAt(i).asButton;
                _item.GetController("button").selectedPage = "up";
            }
        }
    }

    void onClickItem(EventContext context)
    {

        // 获取点击的按钮
        // int _index = _charBtnList.GetChildIndex((GObject)context.data);
        GButton _item = ((GObject)context.data).asButton;
        
        if(_item.title == _currentChar) 
        {
            _item.touchable = false;
            SetNextNumber();
        }
        else
        {
                                                                // 1秒后，执行返回按钮
            StartCoroutine(Commons.DelayToInvoke.DelayToInvokeDo( 1f, () =>{_item.selected =false;} ) );
        }   

        isStageClear();
    }

    void PlayStoneBtnSound()
    {
        // play Sound
        _Sound_StoneButton.clip = _SoundList_StoneButton[Random.Range(0, _SoundList_StoneButton.Length)];
        _Sound_StoneButton.PlayOneShot(_Sound_StoneButton.clip);
        // Debug.Log("Changed");
    }

    void isStageClear()
    {
        if(_currentNumIndex == _question.Length)
        {
            
            _OpenDoor.Play(0,0.5f,ShowCard);

            // 此方法创建出来的窗口总在下面
            // SceneManager.LoadSceneAsync("CardBag",LoadSceneMode.Additive);
            // var _cardBagScene = SceneManager.GetSceneByName("CardBag");
            // SceneManager.sceneLoaded += (Scene sc, LoadSceneMode loadSceneMode) =>
            // {SceneManager.SetActiveScene(_cardBagScene);};


        }
    }

    void ShowCard()
    {
        _cardBagWin = new CardBagWin();
        _cardBagWin.Show();
        
        _Sound_GetCard.Play();
        // _cardBagWin.ShowItem();
    }
}