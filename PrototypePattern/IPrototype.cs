namespace PrototypePattern
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
}
