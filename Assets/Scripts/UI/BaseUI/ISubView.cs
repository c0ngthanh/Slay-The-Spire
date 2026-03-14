public interface ISubView
{
    public void Init(); // Call it on Initialize of main View
    public void Tick(); // Called automatically when ReturnToPool() is called
}
