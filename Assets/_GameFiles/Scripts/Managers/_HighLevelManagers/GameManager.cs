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
        private GameModel gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                
            }
        }

        protected override void Awake()
        {
            base.Awake();
            IMediator mediator = new BaseMediator();
            levelManager.InjectMediator(mediator);
            levelManager.InjectManager(this);
            
            cubesManager.InjectMediator(mediator);
            cubesManager.InjectManager(this);
        }

        protected override void Start()
        {
            base.Start();
            levelManager.InjectModel(gameModel);
        }

        public void InjectModel(GameModel gameModel)
        {
            this.gameModel = gameModel;
            this.gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }

        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(gameModel.InstantScore))
            {
                
            }
        }
    }
}