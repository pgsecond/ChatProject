using Shared.Models;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusListController : MonoBehaviour
{
    public GameObject PlayerStatusGameObjectPrefab;

    private Dictionary<Player, bool> _playerStatusList = new Dictionary<Player, bool>();

    public static PlayerStatusListController Instance;

    void Awake() => Instance = this;

    void Update()
    {
        lock (_playerStatusList)
        {
            var playerStatusGameObjectArray = transform.gameObject.GetComponentsInChildren<PlayerStatusGameObject>();

            foreach (var playerStatusGameObject in playerStatusGameObjectArray)
            {
                DestroyImmediate(playerStatusGameObject.gameObject);
            }

            foreach (var playerStatus in _playerStatusList)
            {
                _SpawnMessage(playerStatus.Key, playerStatus.Value);
            }
        }
    }

    public void UpdatePlayerStatus(Dictionary<Player, bool> playerStatusList) => _playerStatusList = playerStatusList;

    private void _SpawnMessage(Player player, bool status)
    {
        GameObject messageGameObject = Instantiate(PlayerStatusGameObjectPrefab, transform);
        messageGameObject.GetComponent<PlayerStatusGameObject>().SetMessageInfo(player, status);
    }
}
