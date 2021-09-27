using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class TestParticle : MonoBehaviour
{
    GComponent _mainView;

    void Awake()
    {
        UIPackage.AddPackage("FGUI/Main");
        UIObjectFactory.SetPackageItemExtension("ui://Main/Card", typeof(Card));
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        Stage.inst.onKeyDown.Add(OnKeyDown);

        _mainView = this.GetComponent<UIPanel>().ui;

        Card _card= new Card();
        _card.setTitle("田");
        _card.onClick.Add(__onClick);

        _mainView.AddChild(_card).Center();

    }

    void __onClick(EventContext context)
    {    
        Card _item = (Card)context.data;
        _item.Rotate(2f,5f);
    }

    void OnKeyDown(EventContext context)
    {
        if (context.inputEvent.keyCode == KeyCode.Escape)
        {
            Application.Quit();
        }
    }
}
