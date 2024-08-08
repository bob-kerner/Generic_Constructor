using GenericConstructorTest;

// Create and cache the builder of base type:
var some_base_constructor 
    = VirtualConstructor.ConstructorFactory<SomeBase>
        .CreateBuilder<int,float,string>(typeof(SomeDerived));

// Create an instance of SomeDerived using your "virtual" constructor.
var base_reference = some_base_constructor(4, 3.2f, "foo foo");

// Call the virtual Print method on the reference to the object you just created:
base_reference.Print();
