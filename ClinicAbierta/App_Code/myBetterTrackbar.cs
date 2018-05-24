
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
public class myBetterTrackbar : System.Windows.Forms.TrackBar
{

    public bool SuspendChangeEvent { get; set; }

    public myBetterTrackbar()
        : base()
    {
        InitializeComponent();
    }

    protected override void OnCreateControl()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        if ((this.Parent != null))
            this.BackColor = Parent.BackColor;
        base.OnCreateControl();
    }

    protected override void OnValueChanged(EventArgs e)
    {
        if (SuspendChangeEvent == false)
            base.OnValueChanged(e);
    }

    private void InitializeComponent()
    {
        //  
        // DemoControl 
        //  
        this.Name = "myBetterTrackbar";

    }

}
