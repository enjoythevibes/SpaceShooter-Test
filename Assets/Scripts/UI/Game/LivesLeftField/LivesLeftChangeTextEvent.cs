namespace enjoythevibes.UI.Game.LivesLeftField
{
    public struct LivesLeftChangeTextEventType : IEventType
    {
    }

    public class LivesLeftChangeTextEventArg : IEventArgument
    {
        public int LivesLeft { set; get; } = -1;
    }
}