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
        BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(UpDown), CultureInfo.CurrentCulture, propertyChanged: OnCultureChanged);
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
        BindableProperty.Create(nameof(InitialValue), typeof(decimal), typeof(UpDown), Convert.ToDecimal(0), propertyChanged: OnInitialValueChanged);
    private static void OnInitialValueChanged (BindableObject bindable, object oldValue, object newValue)
    {
        UpDown control = bindable as UpDown;

        control.ValueLabel.Text = ((decimal)newValue).ToString(control.Culture);
    }
    public static readonly BindableProperty UpperLimitProperty =
        BindableProperty.Create(nameof(UpperLimit), typeof(decimal?), typeof(UpDown), null, propertyChanged: OnUpperLimitChanged);
    private static void OnUpperLimitChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;
        decimal currentValue = Convert.ToDecimal(control.ValueLabel.Text, control.Culture);

        if (currentValue > (decimal)newValue)
            throw new Exception("Cannot enforce an upper limit when the current value has already exceeded it.");
    }
    public static readonly BindableProperty LowerLimitProperty =
        BindableProperty.Create(nameof(LowerLimit), typeof(decimal?), typeof(UpDown), null, propertyChanged: OnLowerLimitChanged);
    private static void OnLowerLimitChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;
        decimal currentValue = Convert.ToDecimal(control.ValueLabel.Text, control.Culture);

        if (currentValue < (decimal)newValue)
            throw new Exception("Cannot enforce a lower limit when the current value has already exceeded it.");
    }
    // public static readonly BindableProperty OrientationProperty =
    //     BindableProperty.Create(nameof(Orientation), typeof(ControlOrientation), typeof(UpDown), ControlOrientation.Horizontal, propertyChanged: OnOrientationChanged);
    // private static void OnOrientationChanged (BindableObject bindable, object oldValue, object newValue)
    // {
    //     var control = bindable as UpDown;
    //
    //     if ((ControlOrientation)oldValue == (ControlOrientation)newValue)
    //         return;
    //
    //     switch ((ControlOrientation)newValue)
    //     {
    //        case ControlOrientation.Horizontal :
    //            // control.GridLayout.RowDefinitions.Clear();
    //            // control.GridLayout.ColumnDefinitions.Clear();
    //            //
    //            // control.GridLayout.ColumnDefinitions.Add(new ColumnDefinition(50));
    //            // control.GridLayout.ColumnDefinitions.Add(new ColumnDefinition(100));
    //            // control.GridLayout.ColumnDefinitions.Add(new ColumnDefinition(50));
    //            // control.GridLayout.RowDefinitions.Add(new RowDefinition(100));
    //            //
    //            // Grid.SetColumn(control.DownButton, 0);
    //            // Grid.SetColumn(control.ValueLabel, 1);
    //            // Grid.SetColumn(control.UpButton, 2);
    //            break;
    //        case ControlOrientation.Vertical : 
    //            // control.GridLayout.RowDefinitions.Clear();
    //            // control.GridLayout.ColumnDefinitions.Clear();
    //            //
    //            // control.GridLayout.ColumnDefinitions.Add(new ColumnDefinition(100));
    //            // control.GridLayout.RowDefinitions.Add(new RowDefinition(50));
    //            // control.GridLayout.RowDefinitions.Add(new RowDefinition(100));
    //            // control.GridLayout.RowDefinitions.Add(new RowDefinition(50));
    //            //
    //            // control.GridLayout.ColumnDefinitions[0].Width = control.HeightRequest;
    //            // control.GridLayout.RowDefinitions[0].Height = control.WidthRequest * 0.24;
    //            // control.GridLayout.RowDefinitions[1].Height = control.WidthRequest * 0.52;
    //            // control.GridLayout.RowDefinitions[2].Height = control.WidthRequest * 0.24;
    //            //
    //            // Grid.SetRow(control.UpButton, 0);
    //            // Grid.SetRow(control.ValueLabel, 1);
    //            // Grid.SetRow(control.DownButton, 2);
    //            //
    //            // control.FrameLayout.WidthRequest = control.HeightRequest;
    //            // control.FrameLayout.HeightRequest = control.WidthRequest;
    //            control.RotateTo(90);
    //            break;
    //     }
    // }
    public static readonly BindableProperty DownButtonImageProperty =
        BindableProperty.Create(nameof(DownButtonImage), typeof(ImageSource), typeof(UpDown), null, propertyChanged: DownButtonImageChanged);
    private static void DownButtonImageChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        control.DownButton.Source = (ImageSource)newValue;
    }
    public static readonly BindableProperty UpButtonImageProperty =
        BindableProperty.Create(nameof(UpButtonImage), typeof(ImageSource), typeof(UpDown), null, propertyChanged: UpButtonImageChanged);
    private static void UpButtonImageChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        control.UpButton.Source = (ImageSource)newValue;
    } 
    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(UpDown), Colors.Black, propertyChanged: BorderColorChanged);
    private static void BorderColorChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        control.FrameLayout.BorderColor = newValue as Color;
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(UpDown), 0, propertyChanged: CornerRadiusChanged);
    private static void CornerRadiusChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        control.FrameLayout.CornerRadius = Convert.ToInt64(newValue);
    }
    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(UpDown), Colors.White, propertyChanged: BackgroundColorChanged);
    private static void BackgroundColorChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        control.FrameLayout.BackgroundColor = newValue as Color;
        control.GridLayout.BackgroundColor = newValue as Color;
        control.UpButton.BackgroundColor = newValue as Color;
        control.DownButton.BackgroundColor = newValue as Color;
    }
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(UpDown), Colors.Black, propertyChanged: TextColorChanged);
    private static void TextColorChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        control.ValueLabel.TextColor = newValue as Color;
    }
    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(UpDown), (double)8, propertyChanged: FontSizeChanged);
    private static void FontSizeChanged (BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as UpDown;

        control.ValueLabel.FontSize = (double)newValue;
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
    // public ControlOrientation Orientation
    // {
    //     get => (ControlOrientation)GetValue(OrientationProperty);
    //     set => SetValue(OrientationProperty, value);
    // }
    public ImageSource DownButtonImage
    {
        get => (ImageSource)GetValue(DownButtonImageProperty);
        set => SetValue(DownButtonImageProperty, value);
    }
    public ImageSource UpButtonImage
    {
        get => (ImageSource)GetValue(UpButtonImageProperty);
        set => SetValue(UpButtonImageProperty, value);
    }
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }
    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    //Intentional hiding of the Visual Element property.
    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    #endregion

    public UpDown()
    {
        InitializeComponent();

        this.UpButton.Clicked += UpButtonOnClicked;
        this.DownButton.Clicked += DownButtonOnClicked;

        this.BackgroundColor = Colors.Transparent;
        
        if(string.IsNullOrEmpty(ValueLabel.Text))
            ValueLabel.Text = ((decimal)0).ToString(Culture);
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
            
            this.ValueChanged?.Invoke(this, new ValueChangedArgs(currentValue));
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
            
            this.ValueChanged?.Invoke(this, new ValueChangedArgs(currentValue));
        }
        catch (Exception exception)
        {
            throw;
        }
    }
    #endregion

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width < 0 || height < 0)
            return;

        if (FrameLayout == null)
            return;
        
        FrameLayout.WidthRequest = width;
        FrameLayout.HeightRequest = height;

        //if (this.Orientation == ControlOrientation.Horizontal)
        //{
            this.GridLayout.RowDefinitions[0].Height = height;;
            this.GridLayout.ColumnDefinitions[0].Width = width * 0.24;
            this.GridLayout.ColumnDefinitions[1].Width = width * 0.52;
            this.GridLayout.ColumnDefinitions[2].Width = width * 0.24;
        //}
        // else
        // {
        //     this.GridLayout.ColumnDefinitions[0].Width = width;
        //     this.GridLayout.RowDefinitions[0].Height = height * 0.24;
        //     this.GridLayout.RowDefinitions[1].Height = height * 0.52;
        //     this.GridLayout.RowDefinitions[2].Height = height * 0.24;
        // }
    }
}