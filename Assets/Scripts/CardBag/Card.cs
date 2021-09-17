using UnityEngine;
using FairyGUI;

public class Card : GButton
{
    GTextField _title;
    // Controller _readController;
    // Controller _fetchController;
    Transition _FlyIn;
    Transition _FlyOut;

    GObject _back;
    GObject _front;

    public override void ConstructFromXML(FairyGUI.Utils.XML cxml)
    {
        base.ConstructFromXML(cxml);

        _title = this.GetChild("title").asTextField;
        _back = GetChild("back");
        _front = GetChild("front");
        _back.visible = false;
        // _readController = this.GetController("IsRead");
        // _fetchController = this.GetController("c1");
        _FlyIn = this.GetTransition("FlyIn");
        _FlyOut = this.GetTransition("FlyOut");
    }

    public void setTitle(string value)
    {
        _title.text = value;
    }

    // public void setRead(bool value)
    // {
    //     _readController.selectedIndex = value ? 1 : 0;
    // }

    // public void setFetched(bool value)
    // {
    //     _fetchController.selectedIndex = value ? 1 : 0;
    // }

    public void FlyIn(float delay)
    {
        this.visible = false;
        _FlyIn.Play(1, delay, null);
    }
    public void FlyOut(float delay)
    {
        this.visible = false;
        _FlyOut.Play(1, delay, null);
    }

    public bool opened
    {
        get
        {
            return _front.visible;
        }

        set
        {
            GTween.Kill(this);

            _front.visible = value;
            _back.visible = !value;
        }
    }

    public void SetPerspective()
    {
        _front.displayObject.perspective = true;
        _back.displayObject.perspective = true;
    }

    public void Turn()
    {
        if (GTween.IsTweening(this))
            return;

        bool toOpen = !_front.visible;
        GTween.To(0, 180, 0.8f).SetTarget(this).SetEase(EaseType.QuadOut).OnUpdate(TurnInTween).SetUserData(toOpen);
    }

    void TurnInTween(GTweener tweener)
    {
        bool toOpen = (bool)tweener.userData;
        float v = tweener.value.x;
        if (toOpen)
        {
            _back.rotationY = v;
            _front.rotationY = -180 + v;
            if (v > 90)
            {
                _front.visible = true;
                _back.visible = false;
            }
        }
        else
        {
            _back.rotationY = -180 + v;
            _front.rotationY = v;
            if (v > 90)
            {
                _front.visible = false;
                _back.visible = true;
            }
        }
    }
}
