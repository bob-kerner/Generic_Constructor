namespace VirtualConstructor;

public class SomeGeneric<T> 
where T : new() {
    static T build_me()
    {
        return new T();
    }
}