using TadPoleFramework.Core;

namespace TadPoleFramework
{
    public class SceneStartedEventArgs : BaseEventArgs
    {
        
    }

    public class CubeControllerIsCreated : BaseEventArgs
    {
        public CubeController CubeController { get; set; }

        public CubeControllerIsCreated(CubeController cubeController)
        {
            CubeController = cubeController;
        }
    }
    public class BoardIsCreatedEventArgs : BaseEventArgs
    {
        
    }

    public class CubeIsExplodeEventArgs : BaseEventArgs
    {
        public CubeController CubeController { get; set; }
        public float Column { get; set; }
        public float Row { get; set; }

        public CubeIsExplodeEventArgs(float column, float row, CubeController cubeController)
        {
            Column = column;
            Row = row;
            CubeController = cubeController;
        }
    }
}