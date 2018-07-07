namespace Controllers
{
    public interface IGameController
    {
        void Pause();
        void Resume();
        void SlideLeft();
        void SlideRight();
        void Reset();
    }
}