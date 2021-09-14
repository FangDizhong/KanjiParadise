using UnityEngine;
using FairyGUI;

public class Card : GButton
{
    GTextField _title;
    // Controller _readController;
    // Controller _fetchController;
    Transition _FlyIn;
    Transition _FlyOut;

    public override void ConstructFromXML(FairyGUI.Utils.XML cxml)
    {
        base.ConstructFromXML(cxml);

        _title = this.GetChild("title").asTextField;
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
}
