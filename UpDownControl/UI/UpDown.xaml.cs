using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using UpDownControl.Args;
using UpDownControl.Models;

namespace UpDownControl.UI;

public partial class UpDown : ContentView
{
    #region Bindable Properties
    public static readonly BindableProperty CultureProperty =
        BindableProperty.Create(nameof(Step), typeof(CultureInfo), typeof(UpDown), CultureInfo.CurrentCulture, propertyChanged: OnCultureChanged);
    private static void OnCultureChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;
        if (string.IsNullOrEmpty(control.ValueLabel.Text))
            return;

        CultureInfo oldCulture = oldValue as CultureInfo;
        CultureInfo newCulture = newValue as CultureInfo;

        control.ValueLabel.Text = Convert.ToDecimal(control.ValueLabel.Text, oldCulture).ToString(newCulture);
    }
    public static readonly BindableProperty StepProperty =
            BindableProperty.Create(nameof(Step), typeof(decimal), typeof(UpDown), Convert.ToDecimal(1), propertyChanged: OnStepChanged);
    private static void OnStepChanged (BindableObject bindable, object oldValue, object newValue)
    {
    }
    public static readonly BindableProperty InitialValueProperty =
        BindableProperty.Create(nameof(Step), typeof(decimal), typeof(UpDown), Convert.ToDecimal(0), propertyChanged: OnInitialValueChanged);
    private static void OnInitialValueChanged (BindableObject bindable, object oldValue, object newValue)
    {
        UpDown control = bindable as UpDown;
        if (!string.IsNullOrEmpty(control.ValueLabel.Text))
            return;

        control.ValueLabel.Text = ((decimal)newValue).ToString(CultureInfo.CurrentCulture);
    }
    public static readonly BindableProperty UpperLimitProperty =
        BindableProperty.Create(nameof(Step), typeof(decimal?), typeof(UpDown), null, propertyChanged: OnUpperLimitChanged);
    private static void OnUpperLimitChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;
        decimal currentValue = Convert.ToDecimal(control.ValueLabel.Text, control.Culture);

        if (currentValue > (decimal)newValue)
            throw new Exception("Cannot enforce an upper limit when the current value has already exceeded it.");
    }
    public static readonly BindableProperty LowerLimitProperty =
        BindableProperty.Create(nameof(Step), typeof(decimal?), typeof(UpDown), null, propertyChanged: OnLowerLimitChanged);
    private static void OnLowerLimitChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;
        decimal currentValue = Convert.ToDecimal(control.ValueLabel.Text, control.Culture);

        if (currentValue < (decimal)newValue)
            throw new Exception("Cannot enforce a lower limit when the current value has already exceeded it.");
    }
    public static readonly BindableProperty OrientationProperty =
        BindableProperty.Create(nameof(Step), typeof(ControlOrientation), typeof(UpDown), ControlOrientation.Horizontal, propertyChanged: OnOrientationChanged);
    private static void OnOrientationChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        if ((ControlOrientation)oldValue == (ControlOrientation)newValue)
            return;

        switch ((ControlOrientation)newValue)
        {
           case ControlOrientation.Horizontal : 
               break;
           case ControlOrientation.Vertical : 
               break;
        }
    }
    #endregion
    
    #region Properties
    public EventHandler<ValueChangedArgs> ValueChanged { get; set; }
    public EventHandler UpperLimitReached { get; set; }
    public EventHandler LowerLimitReached { get; set; }
    public CultureInfo Culture
    {
        get => (CultureInfo)GetValue(CultureProperty);
        set => SetValue(CultureProperty, value);
    }
    public decimal Step
    {
        get => (decimal)GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }
    public decimal InitialValue
    {
        get => (decimal)GetValue(InitialValueProperty);
        set => SetValue(InitialValueProperty, value);
    }
    public decimal? UpperLimit
    {
        get => (decimal?)GetValue(UpperLimitProperty);
        set => SetValue(UpperLimitProperty, value);
    }
    public decimal? LowerLimit
    {
        get => (decimal?)GetValue(LowerLimitProperty);
        set => SetValue(LowerLimitProperty, value);
    }
    public ControlOrientation Orientation
    {
        get => (ControlOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
    #endregion
    
    public UpDown()
    {
        InitializeComponent();
        
        this.UpButton.Clicked += UpButtonOnClicked;
        this.DownButton.Clicked += DownButtonOnClicked;
    }

    #region Events
    private void DownButtonOnClicked(object? sender, EventArgs e)
    {
        try
        {
            decimal currentValue = Decimal.Parse(this.ValueLabel.Text, Culture);

            currentValue -= this.Step;

            if (LowerLimit != null && currentValue <= LowerLimit)
            {
                this.LowerLimitReached?.Invoke(this, EventArgs.Empty);
                return;
            }

            this.ValueLabel.Text = currentValue.ToString(Culture);
            
            this.ValueChanged?.Invoke(this, new ValueChangedArgs(currentValue.ToString(Culture)));
        }
        catch (Exception exception)
        {
            throw;
        }
    }
    private void UpButtonOnClicked(object? sender, EventArgs e)
    {
        try
        {
            decimal currentValue = Decimal.Parse(this.ValueLabel.Text, Culture);

            currentValue += this.Step;
            
            if (UpperLimit != null && currentValue >= UpperLimit)
            {
                this.UpperLimitReached?.Invoke(this, EventArgs.Empty);
                return;
            }

            this.ValueLabel.Text = currentValue.ToString(Culture);
            
            this.ValueChanged?.Invoke(this, new ValueChangedArgs(currentValue.ToString(Culture)));
        }
        catch (Exception exception)
        {
            throw;
        }
    }
    #endregion
}