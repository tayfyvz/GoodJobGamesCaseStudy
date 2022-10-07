using System.Collections;
using System.Collections.Generic;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class CubeController : BaseManager
    {
        public int colorID;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public bool isCubeChecked, isGroup;
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
        public void IconChecker()
        {
            
        }
        private void OnMouseDown()
        {
            if (!_isSelected/* && !isLocked && isPlaced*/)
            {
                StartCoroutine(OnCubeSelected()); 
                //BroadcastUpward(new BallIsClickedEventArgs());
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
                        StartCoroutine(cube.OnCubeSelected());
                    } 
                    else if(cube._isSelected && i == _angleList.Count -1)
                    {
                        break;
                    }
                }
            }
        }
        private CubeController CloseDistRaycast(float angle, int colorid)
        {
            RaycastHit hit;
            Transform transform1;
            var direction = Quaternion.AngleAxis(angle, transform.up) * (transform1 = transform).forward;
            Debug.DrawRay(transform1.position, direction, Color.red, 5);
        
            if (Physics.Raycast(transform.position, direction, out hit, 1f, 1 << 6))
            {
                if (hit.transform.GetComponent<CubeController>().colorID == colorid)
                {
                    return hit.transform.GetComponent<CubeController>();
                }
            }
            return null;
        }
        IEnumerator OnCubeSelected()
        {
            _isSelected = true;
            RayThrower();
            if (_isHit)
            {
                gameObject.SetActive(false);
                BroadcastUpward(new CubeIsExplodeEventArgs(transform.position.x, transform.position.z, this));
            }
            yield return new WaitForSeconds(0);
        }
    }
}