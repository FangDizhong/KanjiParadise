using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

using UnityEngine.SceneManagement;

public class CardBagWin : Window
{

    private GComponent _cardBag;
    private GList _cardList;

    public string[] _char =new string[] {"一","二","三","四"};


public CardBagWin()
    {
    }

    protected override void OnInit()
    {
        // UIObjectFactory.SetPackageItemExtension("ui://Main/Card", typeof(Card));

        this.contentPane = UIPackage.CreateObject("Main", "CardBagWin").asCom;
        this.Center();
        this.modal = true;

        _cardList = this.contentPane.GetChild("card_list").asList;

        


        // _cardList.itemRenderer = RenderListItem;
        // _list.numItems = 45;
    }

    // void RenderListItem(int index, GObject obj)
    // {
    //     GButton button = (GButton)obj;
    //     button.icon = "i" + UnityEngine.Random.Range(0, 10);
    //     button.title = "" + UnityEngine.Random.Range(0, 100);
    // }

    override protected void DoShowAnimation()
    {
        ShowItem();
        
        // this.SetScale(0.1f, 0.1f);
        // this.SetPivot(0.5f, 0.5f);
        // this.TweenScale(new Vector2(1, 1), 0.3f).OnComplete(ShowItem);

    }

    // override protected void DoHideAnimation()
    // {
    //     this.TweenScale(new Vector2(0.1f, 0.1f), 0.3f).OnComplete(this.HideImmediately);
    // }
    public void ShowItem()
    {
        _cardList.RemoveChildrenToPool();

        int cnChar = _char.Length;
        for (int i = 0; i < cnChar; i++)
        {
            GButton _item = (GButton)_cardList.AddItemFromPool();
            // item.setTitle(_char[i]);
            _item.title = _char[i];
            // _item.FlyIn(0f);
        }

        _cardList.onClickItem.Add(__clickItem);


        _cardList.EnsureBoundsCorrect();

        float delay = 0f;
        int cnCard = _cardList.numChildren;
        for (int i = 0; i < cnCard; i++)
        {
            GButton _item = (GButton)_cardList.GetChildAt(i);
            if (_cardList.IsChildInView(_item))
            {
                _item.visible = false;
                _item.GetTransition("FlyIn").Play(1, delay, null);
                delay += 0.2f;
            }
            else
                break;
        }
    }

    // public void ShowItem()
    // {
    //     _cardList.EnsureBoundsCorrect();

    //     float delay = 0f;
    //     int cnChar = _char.Length;

    //     for (int i = 0; i < cnChar; i++)
    //     {
    //         GButton _item = (GButton)_cardList.GetChildAt(i);
    //         if (_cardList.IsChildInView(_item))
    //         {
    //             _item.FlyIn(delay);
    //             delay += 0.2f;
    //         }
    //         else
    //         {
    //             Debug.Log("else");
    //         }
    //             // break;
    //     }
    // }

    void __clickItem(EventContext context)
    {
        // GButton item = (GButton)context.data;
        // this.contentPane.GetChild("n11").asLoader.url = item.icon;
        // this.contentPane.GetChild("n13").text = item.icon;
    
        GButton _item = (GButton)context.data;
        _item.GetTransition("FlyOut").Play();
        // Card _item = (Card)context.data;
        // _item.FlyOut(0f);
    }
}
//     // Start is called before the first frame update
//     void Start()
//     {
//     _cardBag = this.GetComponent<UIPanel>().ui;
//     GList _cardList = _cardBag.GetChild("card_list").asList;
    // _cardList.RemoveChildrenToPool();
    //     string[] _char =new string[] {"一","二","三","四"};
    //     int cnChar = _char.Length;
    //     for (int i = 0; i < cnChar; i++)
    //     {
    //         GButton _item = _cardList.AddItemFromPool().asButton;
    //         // item.GetChild("t0").text = "" + (i + 1);
    //         _item.GetChild("title").text = _char[i];
    //                     StartCoroutine(Commons.DelayToInvoke.DelayToInvokeDo( 4f, () =>{_item.GetTransition("FlyIn").Play();} ) );

            
    //         // _item.GetController("button").onChanged.Add(PlayStoneBtnSound);
    //         // item.GetChild("t2").asTextField.color = testColor[UnityEngine.Random.Range(0, 4)];
    //         // item.GetChild("star").asProgress.value = (int)((float)UnityEngine.Random.Range(1, 4) / 3f * 100);
    //         _cardList.onClickItem.Add(onClickItem);
//         }
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     void onClickItem(EventContext context)
//     {

//     }
// }
