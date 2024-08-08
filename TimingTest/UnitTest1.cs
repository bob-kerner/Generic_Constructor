using GenericConstructorTest;

namespace TimingTest;

 
 
public class Tests
{
    private Func<int,float,string,SomeBase> _some_base_constructor;
    [SetUp]
    public void Setup()
    {
        // Create and cache the builder of base type:
        this._some_base_constructor 
            = VirtualConstructor.ConstructorFactory<SomeBase>
                .CreateBuilder<int,float,string>(typeof(SomeDerived));
        Assert.Pass();
    }

    [Test]
    public void TestVirtualConstructorPerformance()
    {
        // 1 ms.
        // Create an instance of SomeDerived using your "virtual" constructor.
        
        if (_some_base_constructor is not null){
            for (int i = 0; i < 1e10; i++){
                var base_reference = _some_base_constructor(4, 3.2f, "foo foo");
            }
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void TestActivatePerformance()
    {
        // 42ms
        for (int i = 0; i < 1e10; i++)
        {
            var base_reference = Activator.CreateInstance(typeof(SomeDerived), new object[] { 4, 3.2f, "foo foo" });
        }

        Assert.Pass();
    }
    //
    [Test]
    public void TestRawBaselinePerformance()
    {
        for (int i = 0; i < 1e10; i++)
        {
            var x = new SomeDerived(4, 3.2f, "foo foo");
        }

        Assert.Pass();
        
    }
}