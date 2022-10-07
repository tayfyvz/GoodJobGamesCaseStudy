using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TadPoleFramework;
using TadPoleFramework.Core;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private List<Sprite> sprites = new List<Sprite>();

        [HideInInspector] public CubesManager cubesManager;
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case CubeIsExplodeEventArgs cubeIsExplodeEventArgs:
                    Vector3 newCubePos = new Vector3(cubeIsExplodeEventArgs.Column, 0, rowLength + cubeIsExplodeEventArgs.Row);
                    CreateNewCube(newCubePos, cubeIsExplodeEventArgs.CubeController);
                    break;
                case ShuffleCubesEventArgs shuffleCubesEventArgs:
                    ShuffleCubes(shuffleCubesEventArgs.CubeControllers);
                    break;
            }
        }

        protected override void Start()
        {
            Broadcast(new SceneStartedEventArgs(sprites, conditionA, conditionB, conditionC));
            Broadcast(new CameraSetterEventArgs(columnLength, rowLength));
            /*StartCoroutine(CreateBoard());*/
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
        private void CreateNewCube(Vector3 pos, CubeController cubeController)
        {
            cubeController.transform.position = pos;
            Randomizer(cubeController);
            cubeController.ResetBools();
            cubeController.gameObject.SetActive(true);
        }

        private void Randomizer(CubeController cc)
        {
            int randomSpriteNum = UnityEngine.Random.Range(0, totalNumOfColors);
            cc.ChangeSprite(sprites[randomSpriteNum * 4]);
            cc.colorID = randomSpriteNum;
        }
        private void ShuffleCubes(List<CubeController> cubeControllers)
        {
            cubeControllers.Shuffle();
            
            List<Vector3> newPositions = cubeControllers.Select(t => t.transform.position).ToList();

            for (int i = 0; i < cubeControllers.Count; i++)
            {
                cubeControllers[i].transform.position = newPositions[i];
                Randomizer(cubeControllers[i]);
                cubeControllers[i].ResetBools();
            }
            
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