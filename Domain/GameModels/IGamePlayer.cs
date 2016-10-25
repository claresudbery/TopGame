namespace Domain.GameModels
{
    public interface IGamePlayer
    {
        void DisplayAllDeckContents();
        bool bStop { get; set; }
        int iStoredPauseSize { get; set; }
        int iPauseSize { get; set; }
    }
}