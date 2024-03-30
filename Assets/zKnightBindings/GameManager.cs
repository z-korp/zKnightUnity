using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bottlenoselabs.C2CS.Runtime;
using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Threading.Tasks;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] WorldManager worldManager;

    [SerializeField] WorldManagerData dojoConfig;
    [SerializeField] GameManagerData gameManagerData; 

    private BurnerManager burnerManager;
    private Dictionary<FieldElement, string> spawnedBurners = new();
    public PlaySystem play;

    public static string StringToHexString(string input)
    {
        string hexOutput = "";
        foreach (char c in input)
        {
            hexOutput += String.Format("{0:X2}", (int)c);
        }
        return hexOutput;
    }
    
    void Start()
    {
        var provider = new JsonRpcClient(dojoConfig.rpcUrl);
        var signer = new SigningKey(gameManagerData.masterPrivateKey);
        var account = new Account(provider, signer, new FieldElement(gameManagerData.masterAddress));

        Debug.Log(account.Address);

        burnerManager = new BurnerManager(provider, account);

        worldManager.synchronizationMaster.OnEntitySpawned.AddListener(InitEntity);
        foreach (var entity in worldManager.Entities())
        {
            InitEntity(entity);
        }
    }

    void Update()
    {
        
    }

    private void InitEntity(GameObject entity)
    {
        Tile tileComponent = entity.GetComponent<Tile>();
        if (tileComponent != null)
        {
            // This entity is of type Tile, perform actions with its index
            //Debug.Log($"Tile entity spawned with index: {tileComponent.index}");
        }

        Game gameComponent = entity.GetComponent<Game>();
        if (gameComponent != null)
        {
            // This entity is of type Tile, perform actions with its index
            Debug.Log($"Game entity spawned with id: {gameComponent.game_id}");
        }
    }

    public async void TriggerCreatePlayAsync()
    {
        var burner = await burnerManager.DeployBurner();
        spawnedBurners[burner.Address] = null;
        var player = StringToHexString("player");
        var name = StringToHexString("name");
        var txHash = await play.Create(burner, dojoConfig.worldAddress, player, 10, name);
        // Do something with txHash, like logging it
        Debug.Log($"Transaction Hash: {txHash.Hex()}");
    }
}