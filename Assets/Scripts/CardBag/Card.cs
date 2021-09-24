using UnityEngine;
using FairyGUI;

public class Card : GButton
{
    GTextField _title;

    GObject _back;
    GComponent _front;
    GGraph _mask_radius;
    GGraph _particle;

    Transition _FlyIn;
    Transition _FlyOut;
    Transition _Turn;
    Transition _TurnBack;
    Transition _TurnFront;

    public Card()
    {}

    public override void ConstructFromXML(FairyGUI.Utils.XML cxml)
    {
        base.ConstructFromXML(cxml);

        _back = GetChild("back");
        _front = GetChild("front").asCom;
        _mask_radius = GetChild("mask_radius").asGraph;
        _title = _front.GetChild("title").asTextField;
        _back.visible = false;

        // Particle
        _particle = this.GetChild("particle").asGraph;

        Object prefab = Resources.Load("Star");
        GameObject go = (GameObject)Object.Instantiate(prefab);
        _particle.SetNativeObject(new GoWrapper(go));
        
        // Transition
        _FlyIn = this.GetTransition("FlyIn");
        _FlyOut = this.GetTransition("FlyOut");
        _Turn = this.GetTransition("Turn");
        _TurnBack = this.GetTransition("TurnBack");
        _TurnFront = this.GetTransition("TurnFront");
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
    
    /// <summary>
    /// 调用FairyGUI的动效，能设置重复次数，但无法整体设置EaseType，不推荐
    /// </summary>
    /// <param name="times"></param>
    /// <param name="roundPerSecond"></param>
    /// <param name="delay"></param>
    public void Turn(int times, float roundPerSecond, float delay)
    {
        _Turn.timeScale = roundPerSecond * 2; // 2秒/圈
        // this.visible = false;
        _Turn.Play(times, delay, null);
    }
    
    public void TurnBack(float delay)
    {
        // this.visible = false;
        _TurnBack.Play(1, delay, null);
    }

        public void TurnFront(float delay)
    {
        // this.visible = false;
        _TurnFront.Play(1, delay, null);
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
        _mask_radius.displayObject.perspective = true;
        _particle.displayObject.perspective = true;
    
        // this.displayObject.perspective = true;
    }

    public void Rotate(float round, float duration)
    {
        this.SetPerspective();

        if (GTween.IsTweening(this))
            return;

        bool toOpen = !_front.visible;
        GTween.To(0, round * 360, duration).SetTarget(this).SetEase(EaseType.QuadOut).OnUpdate(TurnInTween).SetUserData(toOpen);
    }

    void TurnInTween(GTweener tweener)
    {
        bool toOpen = (bool)tweener.userData;
        float v = tweener.value.x;
        _mask_radius.rotationY = v;
        _particle.rotationY = -180 + v;

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
