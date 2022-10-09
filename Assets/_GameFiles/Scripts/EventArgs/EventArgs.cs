using System.Collections.Generic;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class SceneStartedEventArgs : BaseEventArgs
    {
        public int ConditionA { get; set; }
        public int ConditionB { get; set; }
        public int ConditionC { get; set; }

        public List<Sprite> Sprites { get; set; }
        public SceneStartedEventArgs(List<Sprite> sprites, int a, int b, int c)
        {
            Sprites = sprites;
            ConditionA = a;
            ConditionB = b;
            ConditionC = c;
        }
    }
    public class CameraSetterEventArgs : BaseEventArgs
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public CameraSetterEventArgs(int column, int row)
        {
            Column = column;
            Row = row;
        }
    }
    public class CubeControllerIsCreated : BaseEventArgs
    {
        public CubeController CubeController { get; set; }

        public CubeControllerIsCreated(CubeController cubeController)
        {
            CubeController = cubeController;
        }
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
    public class ShuffleCubesEventArgs : BaseEventArgs
    {
        public List<CubeController> CubeControllers { get; set; }
        public ShuffleCubesEventArgs(List<CubeController> cubeControllers)
        {
            CubeControllers = cubeControllers;
        }
    }
    public class WarningSenderEventArgs : BaseEventArgs
    {
        public string Message { get; set; }

        public WarningSenderEventArgs(string message)
        {
            Message = message;
        }
    }
    public class BoardIsCreatedEventArgs : BaseEventArgs
    {
    }
    public class InputThresholdEventArgs : BaseEventArgs
    {
    }
    public class InputThresholdEndEventArgs : BaseEventArgs
    {
    }
    public class ShuffleButtonClickedEventArgs : BaseEventArgs
    {
    }
}