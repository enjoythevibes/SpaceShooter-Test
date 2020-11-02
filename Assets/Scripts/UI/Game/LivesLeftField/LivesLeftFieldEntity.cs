using UnityEngine;
using TMPro;

namespace enjoythevibes.UI.Game.LivesLeftField
{
    public class LivesLeftFieldEntity : MonoBehaviour
    {
        private string fieldFormat;
        private TextMeshProUGUI field;

        private void Awake()
        {
            field = GetComponent<TextMeshProUGUI>();
            fieldFormat = field.text;
            EventsManager.AddListener<LivesLeftChangeTextEventType>(OnLivesLeftChangeText);
        }

        private void OnDestroy()
        {
            EventsManager.RemoveListener<LivesLeftChangeTextEventType>(OnLivesLeftChangeText);
        }

        private void OnLivesLeftChangeText(IEventArgument arg)
        {
            var argument = arg as LivesLeftChangeTextEventArg;
            field.SetText(fieldFormat, argument.LivesLeft);        
        }
    }
}