using System.Collections;
using System.Collections.Generic;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class CubesManager : BaseManager
    {
        private List<CubeController> _cubes = new List<CubeController>();
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
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
                    _cubes.Remove(cubeIsExplodeEventArgs.CubeController);
                    Broadcast(cubeIsExplodeEventArgs);
                    CubesUnchecker();
                    StartCoroutine(CubesIconChecker());
                    break;
            }
        }

        private IEnumerator CubesIconChecker()
        {
            yield return new WaitForSeconds(1.5f);
            Debug.Log("CheckIcon");
            for (int i = 0; i < _cubes.Count; i++)
            {
                if (!_cubes[i].isCubeChecked)
                { 
                    List<CubeController> cubeGroup = _cubes[i].IconChecker();
                    if (cubeGroup != null)
                    {
                        int count = cubeGroup.Count;
                        int colorID = cubeGroup[0].colorID;
                        //change sprite of group
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