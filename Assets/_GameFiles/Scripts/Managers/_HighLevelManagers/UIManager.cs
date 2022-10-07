using System.ComponentModel;
using TadPoleFramework;
using TadPoleFramework.Core;
using TadPoleFramework.UI;
using UnityEngine;


public class UIManager : BaseUIManager
{
    [SerializeField] private GameViewPresenter gameViewPresenter;
    
    private GameModel _gameModel;
    protected override void Awake()
    {
        base.Awake();
        gameViewPresenter.InjectManager(this);
    }
    public override void Receive(BaseEventArgs baseEventArgs)
    {
    }
    public void InjectModel(GameModel gameModel)
    {
        this._gameModel = gameModel;
        this._gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
    }

    private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_gameModel.Score))
        {
            
        }
    }
}