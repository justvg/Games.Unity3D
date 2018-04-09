using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager {
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int coins { get; private set; }
    public int mana { get; private set; }
    public int maxMana { get; private set; }
    public int men { get; private set; }

    public void StartUp() {
        Debug.Log("Player manager starting...");

        UpdateData(3, 0, 0);
        maxHealth = maxMana = 3;
        men = 0;
        
        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value) {
        health += value;
        if (health > maxHealth) {
            health = maxHealth;
        }
        if (health < 0) {
            health = 0;
        }

        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    public void UpdateData(int health, int mana, int coins) {
        this.health = health;
        this.mana = mana;
        this.coins = coins;
    }

    public void ChangeMana(int value) {
        mana += value;

        if (mana < 0) {
            mana = 0;
        }
        if (mana > maxMana) {
            mana = maxMana;
        }

        Messenger.Broadcast(GameEvent.MANA_UPDATED);
    }

    public void ChangeCoins(int value) {
        if (coins < 0) {
            coins = 0;
        }
        coins += value;

        Messenger.Broadcast(GameEvent.COINS_UPDATED);
    }

    public void ChangeMen() {
        if (men < 0) {
            men = 0;
        }
        men += 1;

        Messenger.Broadcast(GameEvent.MEN_UPDATED);
    }

    public void Respawn() {
        mana = 0;
        coins = 0;
        men = 0;
        maxHealth = maxMana = health = 3;
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
        Messenger.Broadcast(GameEvent.MANA_UPDATED);
        Messenger.Broadcast(GameEvent.COINS_UPDATED);
    }
}
