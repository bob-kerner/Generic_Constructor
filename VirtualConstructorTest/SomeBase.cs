namespace GenericConstructorTest;

public abstract class SomeBase
{
    public abstract void Print();
}

public class SomeDerived : SomeBase
{
    private readonly int i;
    private readonly float f;
    private readonly string s;

    public SomeDerived(int i, float f, string s)
    {
        this.i = i;
        this.f = f;
        this.s = s;
    }

    public override void Print()
    {
        Console.WriteLine($"i: {i}, f: {f}, s: \"{s}\"");
    }
}
