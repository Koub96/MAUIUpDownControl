namespace UpDownControl.Args;

public class ValueChangedArgs : EventArgs
{
    public decimal Value { get; private set; }

    public ValueChangedArgs(decimal value)
    {
        this.Value = value;
    }
}