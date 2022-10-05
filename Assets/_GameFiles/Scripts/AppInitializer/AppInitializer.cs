using TadPoleFramework;
using TadPoleFramework.Core;
using UnityEngine;

public class AppInitializer : MonoBehaviour
{

    [SerializeField] private AppManager appManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    
    private void Awake()
    {
        GameModel gameModel = new GameModel();
        
        IMediator mediator = new BaseMediator();
        uiManager.InjectMediator(mediator);
        appManager.InjectMediator(mediator);
        gameManager.InjectMediator(mediator);

        gameManager.InjectModel(gameModel);
        uiManager.InjectModel(gameModel);
    }
}