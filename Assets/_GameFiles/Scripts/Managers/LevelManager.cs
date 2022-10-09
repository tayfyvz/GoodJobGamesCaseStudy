using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DG.Tweening;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class LevelManager : BaseManager
    {
        [Header("Board Size")]
        [Range(2, 10)] [SerializeField] private int rowLength; 
        [Range(2, 10)] [SerializeField] private int columnLength;

        [Header("Conditions")]
        [SerializeField] private int conditionA;
        [SerializeField] private int conditionB;
        [SerializeField] private int conditionC;

        [Header("Color Settings")]
        [Range(1, 6)] [SerializeField] private int totalNumOfColors;
        [SerializeField] private CubeController cubeControllerPrefab;

        [HideInInspector] public CubesManager cubesManager;
        
        private List<Sprite> sprites = new List<Sprite>();
        
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case CubeIsExplodeEventArgs cubeIsExplodeEventArgs:
                    Vector3 newCubePos = new Vector3(cubeIsExplodeEventArgs.Column, 0, rowLength + cubeIsExplodeEventArgs.Row);
                    PoolingNewCube(newCubePos, cubeIsExplodeEventArgs.CubeController);
                    break;
                case ShuffleCubesEventArgs shuffleCubesEventArgs:
                    BroadcastUpward(shuffleCubesEventArgs);
                    ShuffleCubes(shuffleCubesEventArgs.CubeControllers);
                    break;
            }
        }
        protected override void Start()
        {
            if (totalNumOfColors >= rowLength * columnLength)
            {
                BroadcastUpward(new WarningSenderEventArgs("Total number of colors is greater then or equal to board size"));
            }
            else
            {
                BroadcastUpward(new WarningSenderEventArgs("You can change board size and other conditions from Level Manager"));

            }
            
            sprites = new List<Sprite>(Resources.LoadAll<Sprite>("Icons"));
            
            Broadcast(new SceneStartedEventArgs(sprites, conditionA, conditionB, conditionC));
            Broadcast(new CameraSetterEventArgs(columnLength, rowLength));
            
            CreateBoard();
            
            Broadcast(new BoardIsCreatedEventArgs());
        }
        private void CreateBoard()
        {
            for (int i = 0; i < columnLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    Vector3 tempPos = new Vector3(i, 0, j);
                    CreateNewCube(tempPos);
                }
            }
        }
        private void CreateNewCube(Vector3 pos)
        {
            CubeController cc = Instantiate(cubeControllerPrefab, pos, Quaternion.identity, cubesManager.transform);
            
            Randomizer(cc);
            
            Broadcast(new CubeControllerIsCreated(cc));
        }
        private void PoolingNewCube(Vector3 pos, CubeController cubeController)
        {
            cubeController.transform.position = pos;
            Randomizer(cubeController);
            cubeController.ResetAllBool();
            cubeController.gameObject.SetActive(true);
        }
        private void ShuffleCubes(List<CubeController> cubeControllers)
        {
            List<CubeController> tempList = cubeControllers.ToList();

            tempList.Shuffle();
            
            List<Vector3> newPositions = tempList.Select(t => t.transform.position).ToList();

            for (int i = 0; i < cubeControllers.Count; i++)
            {
                cubeControllers[i].transform.DOMove(newPositions[i], 0.2f).SetEase(Ease.Linear);
                cubeControllers[i].ResetAllBool();
            }
        }
        private void Randomizer(CubeController cc)
        {
            int randomSpriteNum = UnityEngine.Random.Range(0, totalNumOfColors);
            
            cc.ChangeSprite(sprites[randomSpriteNum * 4]);
            cc.colorID = randomSpriteNum;
        }
        public void InjectModel(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }
        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_gameModel.Level))
            {
                
            }
        }
    }
}