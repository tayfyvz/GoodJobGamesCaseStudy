﻿using System.Collections;
using System.Collections.Generic;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class CubeController : BaseManager
    {
        public int colorID;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public bool isCubeChecked;
        private bool _isSelected, _isHit;
        private List<float> _angleList = new List<float>
        {
            0,
            90,
            180,
            270
        };
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            
        }
        public void ChangeSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
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
            if (!_isSelected/* && !isLocked && isPlaced*/)
            {
                OnCubeSelected();
            }
        }
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
    }
}