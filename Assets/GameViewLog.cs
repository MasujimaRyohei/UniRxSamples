using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class GameViewLog : MonoBehaviour
{
    [SerializeField]
    private Text    textUI;

    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private Button clearButton;

    private void Awake()
    {
        clearButton.OnClickAsObservable().Subscribe(_ => ClearScrollRect());

        Application.logMessageReceived  += OnLogMessage;
    }

    private void ClearScrollRect()
    {
        textUI.text = string.Empty;
            scrollRect.horizontalScrollbar.gameObject.SetActive(false);
        scrollRect.verticalScrollbar.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived  += OnLogMessage;
    }

    private void OnLogMessage(string i_logText, string i_stackTrace, LogType i_type)
    {
        if (string.IsNullOrEmpty(i_logText))
        {
            return;
        }

        switch (i_type)
        {
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                i_logText = string.Format("<color=red>{0}</color>", i_logText);
                break;
            case LogType.Warning:
                i_logText = string.Format("<color=yellow>{0}</color>", i_logText);
                break;
            default:
                break;
        }

        textUI.text += i_logText + System.Environment.NewLine;
    }
} 