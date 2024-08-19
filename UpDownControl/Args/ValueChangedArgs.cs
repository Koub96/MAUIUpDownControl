namespace UpDownControl.Args;

public class ValueChangedArgs : EventArgs
{
    public string Value { get; private set; }

    public ValueChangedArgs(string value)
    {
        this.Value = value;
    }
}