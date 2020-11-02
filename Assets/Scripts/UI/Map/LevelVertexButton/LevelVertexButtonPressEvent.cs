namespace enjoythevibes.UI.Map.LevelVertexButton
{
    public struct LevelVertexButtoPressEventType : IEventType
    {
    }

    public class LevelVertexButtonPressEventArg : IEventArgument
    {
        public int ButtonIndex { private set; get; }

        public LevelVertexButtonPressEventArg(int buttonIndex)
        {
            ButtonIndex = buttonIndex;
        }
    }
}