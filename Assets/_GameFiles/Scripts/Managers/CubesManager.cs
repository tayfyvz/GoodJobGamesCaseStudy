using System.Collections;
using System.Collections.Generic;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class CubesManager : BaseManager
    {
        private List<Sprite> _sprites = new List<Sprite>();
        private List<CubeController> _cubes = new List<CubeController>();
        private int _a, _b, _c;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case SceneStartedEventArgs sceneStartedEventArgs:
                    _sprites = sceneStartedEventArgs.Sprites;
                    _a = sceneStartedEventArgs.ConditionA;
                    _b = sceneStartedEventArgs.ConditionB;
                    _c = sceneStartedEventArgs.ConditionC;
                    break;
                case CubeControllerIsCreated cubeControllerIsCreated:
                    cubeControllerIsCreated.CubeController.InjectManager(this);
                    _cubes.Add(cubeControllerIsCreated.CubeController);
                    break;
                case BoardIsCreatedEventArgs boardIsCreatedEventArgs:
                    StartCoroutine(CubesIconChecker());
                    break;
                case CubeIsExplodeEventArgs cubeIsExplodeEventArgs:
                    Broadcast(cubeIsExplodeEventArgs);
                    StartCoroutine(CubesIconChecker());
                    break;
            }
        }

        private IEnumerator CubesIconChecker()
        {
            yield return new WaitForSeconds(1.5f);
            bool isThereAnyGroup = false;
            for (int i = 0; i < _cubes.Count; i++)
            {
                Debug.Log("VAR");
                if (!_cubes[i].isCubeChecked)
                { 
                    List<CubeController> cubeGroup = _cubes[i].IconChecker();
                    if (cubeGroup != null)
                    {
                        int count = cubeGroup.Count;
                        int colorID = cubeGroup[0].colorID;
                        if (count > 1)
                        {
                            isThereAnyGroup = true;
                        }
                        if (count <= _a)
                        {
                            CubeGroupSpriteChanger(cubeGroup, (colorID * 4));
                        }
                        else if(count > _a && count <= _b)
                        {
                            CubeGroupSpriteChanger(cubeGroup, (colorID * 4) + 1);
                        }
                        else if (count > _b && count <= _c)
                        {
                            CubeGroupSpriteChanger(cubeGroup, (colorID * 4) + 2);
                        }
                        else if (count > _c)
                        {
                            CubeGroupSpriteChanger(cubeGroup, (colorID * 4) + 3);
                        }
                    }
                }
            }
            
            CubesUnchecker();
            
            if (!isThereAnyGroup)
            {
                ShuffleEventArgsSender();
            }
        }

        private void ShuffleEventArgsSender()
        {
            Broadcast(new ShuffleCubesEventArgs(_cubes));
        }

        private void CubeGroupSpriteChanger(List<CubeController> cubeGroup, int index)
        {
            for (int i = 0; i < cubeGroup.Count; i++)
            {
                cubeGroup[i].ChangeSprite(_sprites[index]);
            }
        }

        private void CubesUnchecker()
        {
            for (int i = 0; i < _cubes.Count; i++)
            {
                _cubes[i].isCubeChecked = false;
            }
        }
    }
}