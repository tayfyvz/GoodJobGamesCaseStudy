using System.Collections.Generic;
using TadPoleFramework.Core;

namespace TadPoleFramework
{
    public class CubesManager : BaseManager
    {
        private List<CubeController> _cubes = new List<CubeController>();
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case CubeControllerIsCreated cubeControllerIsCreated:
                    cubeControllerIsCreated.CubeController.InjectManager(this);
                    _cubes.Add(cubeControllerIsCreated.CubeController);
                    break;
                case BoardIsCreatedEventArgs boardIsCreatedEventArgs:
                    CubesIconChecker();
                    break;
                case CubeIsExplodeEventArgs cubeIsExplodeEventArgs:
                    _cubes.Remove(cubeIsExplodeEventArgs.CubeController);
                    Broadcast(cubeIsExplodeEventArgs);
                    break;
            }
        }

        private void CubesIconChecker()
        {
            for (int i = 0; i < _cubes.Count; i++)
            {
                _cubes[i].IconChecker();
                
            }
        }

        private void CreateNewCube()
        {
            
        }
    }
}