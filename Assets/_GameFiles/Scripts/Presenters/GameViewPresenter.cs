using TadPoleFramework.Core;
using TadPoleFramework.UI;

namespace TadPoleFramework
{
    public class GameViewPresenter : BasePresenter
    {
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case WarningSenderEventArgs warningSenderEventArgs:
                    (view as GameView)?.ActivateWarningText(warningSenderEventArgs.Message);
                    break;
                case ShuffleCubesEventArgs shuffleCubesEventArgs:
                    (view as GameView)?.ActivateShufflingText();
                    break;
            }
        }
        public void OnShufflingButtonClick()
        {
            BroadcastUpward(new ShuffleButtonClickedEventArgs());
        }
    }
}