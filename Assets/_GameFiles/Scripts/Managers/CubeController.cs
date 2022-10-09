using System.Collections;
using System.Collections.Generic;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class CubeController : BaseManager
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public int colorID;
        public bool isCubeChecked;
        
        private bool _isSelected, _isHit, _isThreshold;
        
        //Angle list is used to make an expandable group search
        private List<float> _angleList = new List<float>
        {
            0,
            90,
            180,
            270
        };
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case InputThresholdEventArgs inputThresholdEventArgs:
                    _isThreshold = true;
                    break;
                case InputThresholdEndEventArgs inputThresholdEndEventArgs:
                    _isThreshold = false;
                    break;
            }
        }
        public void ChangeSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }
        public void ResetAllBool()
        {
            _isSelected = false;
            _isHit = false;
            isCubeChecked = false;
        }
        public List<CubeController> IconChecker()
        {
            List<CubeController> connectedCubes = new List<CubeController>();
            
            connectedCubes.Add(this);
            isCubeChecked = true;
            
            for (int i = 0; i < _angleList.Count; i++)
            {
                CubeController cube = CloseDistRaycast(_angleList[i], colorID);
                
                if (cube != null)
                {
                    if (!cube.isCubeChecked)
                    {
                        cube.isCubeChecked = true;
                        
                        List<CubeController> cc = cube.AroundChecker();
                        
                        for (int j = 0; j < cc.Count; j++)
                        {
                            if (!connectedCubes.Contains(cc[j]))
                            {
                                connectedCubes.Add(cc[j]);
                            }
                        }
                    }
                }
            }
            
            return connectedCubes;
        }
        private List<CubeController> AroundChecker()
        {
            List<CubeController> cubeControllers = new List<CubeController>();
            
            for (int i = 0; i < _angleList.Count; i++)
            {
                CubeController cube = CloseDistRaycast(_angleList[i], colorID);
                
                if (cube != null)
                {
                    if (!cube.isCubeChecked)
                    {
                        cube.isCubeChecked = true;
                        
                        List<CubeController> cc = cube.AroundChecker();
                        
                        for (int j = 0; j < cc.Count; j++)
                        {
                            if (!cubeControllers.Contains(cc[j]))
                            {
                                cubeControllers.Add(cc[j]);
                            }
                        }
                    }
                }
            }
            
            cubeControllers.Add(this);
            
            return cubeControllers;
        }
        private void OnMouseDown()
        {
            if (!_isSelected && !_isThreshold)
            {
                OnCubeSelected();
            }
        }
        private void OnCubeSelected()
        {
            _isSelected = true;
            
            RayThrower();
            
            if (_isHit)
            {
                gameObject.SetActive(false);
                
                Vector3 position = transform.position;
                
                BroadcastUpward(new CubeIsExplodeEventArgs(position.x, position.z, this));
            }
        }
        //The reason why the basic mechanics of the game, which can also be done by looking at the
        //positions of the cubes on the matrix, is done by throwing the ray, is to ensure that the
        //game can be developed in 3D later on.
        private void RayThrower()
        {
            for (int i = 0; i < _angleList.Count; i++)
            {
                CubeController cube = CloseDistRaycast(_angleList[i], colorID);
                
                if (cube != null)
                {
                    if (!cube._isSelected)
                    {
                        _isHit = true;
                        cube._isHit = true;
                        
                        cube.OnCubeSelected();
                    } 
                    else if(cube._isSelected && i == _angleList.Count -1)
                    {
                        break;
                    }
                }
            }
        }
        //While developing this mechanic, its extensibility was taken into consideration.
        private CubeController CloseDistRaycast(float angle, int colorId)
        {
            RaycastHit hit;
            
            Transform transform1;
            var direction = Quaternion.AngleAxis(angle, transform.up) * (transform1 = transform).forward;
            Debug.DrawRay(transform1.position, direction, Color.red, 5);
        
            if (Physics.Raycast(transform.position, direction, out hit, 1f, 1 << 6))
            {
                if (hit.transform.GetComponent<CubeController>().colorID == colorId)
                {
                    return hit.transform.GetComponent<CubeController>();
                }
            }
            
            return null;
        }
    }
}