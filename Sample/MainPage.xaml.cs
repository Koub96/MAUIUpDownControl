using System.Globalization;
using UpDownControl.Args;
using UpDownControl.Models;

namespace Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        
        this.UpDownControl.ValueChanged += ValueChanged;
    }

    private void ValueChanged(object? sender, ValueChangedArgs e)
    {
        e.ToString();
        
        this.UpDownControl.Culture = new CultureInfo("en-US");
    }
}