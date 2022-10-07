using System.ComponentModel;
using TadPoleFramework.Core;
using TadPoleFramework.Game;
using UnityEngine;


namespace TadPoleFramework
{
    public class GameManager : BaseGameManager
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private CubesManager cubesManager;
        [SerializeField] private CameraManager cameraManager;
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {

        }

        protected override void Awake()
        {
            base.Awake();
            IMediator mediator = new BaseMediator();
            levelManager.InjectMediator(mediator);
            levelManager.InjectManager(this);
            levelManager.cubesManager = cubesManager;
            
            cubesManager.InjectMediator(mediator);
            cubesManager.InjectManager(this);
            
            cameraManager.InjectMediator(mediator);
            cameraManager.InjectManager(this);
        }

        protected override void Start()
        {
            base.Start();
            levelManager.InjectModel(_gameModel);
        }

        public void InjectModel(GameModel gameModel)
        {
            this._gameModel = gameModel;
            this._gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }

        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_gameModel.Level))
            {
                
            }
        }
    }
}