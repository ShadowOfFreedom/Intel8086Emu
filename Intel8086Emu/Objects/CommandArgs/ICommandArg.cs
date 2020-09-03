namespace Intel8086Emu.Objects.CommandArgs
{
    public interface ICommandArg
    {
        string StringValue { get; set; }
        ushort Value { get; }
        ArgSize Size { get; }
    }
}