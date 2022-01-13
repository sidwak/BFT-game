using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public bool Machine2Buy = false;
    public bool Machine3Buy = false;
    public bool Machine4Buy = false;
    public bool Machine5Buy = false;
    public bool Machine6Buy = false;
    public bool Car2Buy = false;
    public bool Car3Buy = false;
    public bool Car4Buy = false;
    public bool Car5Buy = false;
    public bool Area2Buy = false;
    public bool Area3Buy = false;
    public bool Truck1Buy = false;
    public bool Truck2Buy = false;

    public bool Machine1Upgrade = false;
    public bool Machine2Upgrade = false;
    public bool Machine3Upgrade = false;
    public bool Machine4Upgrade = false;
    public bool Machine5Upgrade = false;
    public bool Machine6Upgrade = false;

    public bool MoverAi_1Buy = false;
    public bool MoverAi_2Buy = false;

    public bool PlayerUpgradeSpeed = false;
    public bool PlayerUpgradeCapacity = false;

    public bool isFakeSend = false;

    public int totalCoins = 0;

    public PlayerData(bool machine2Buy, bool machine3Buy, bool machine4Buy, bool machine5Buy, bool machine6Buy,
        bool car2Buy, bool car3Buy, bool car4Buy, bool car5Buy, bool area2Buy, bool area3Buy, bool truck1Buy,
        bool truck2Buy, bool machine1Upgrade, bool machine2Upgrade, bool machine3Upgrade, bool machine4Upgrade,
        bool machine5Upgrade, bool machine6Upgrade, bool moverAi_1Buy, bool moverAi_2Buy, bool playerUpgradeSpeed,
        bool playerUpgradeCapacity, bool isFakeSend, int totalCoins)
    {
        Machine2Buy = machine2Buy;
        Machine3Buy = machine3Buy;
        Machine4Buy = machine4Buy;
        Machine5Buy = machine5Buy;
        Machine6Buy = machine6Buy;
        Car2Buy = car2Buy;
        Car3Buy = car3Buy;
        Car4Buy = car4Buy;
        Car5Buy = car5Buy;
        Area2Buy = area2Buy;
        Area3Buy = area3Buy;
        Truck1Buy = truck1Buy;
        Truck2Buy = truck2Buy;
        Machine1Upgrade = machine1Upgrade;
        Machine2Upgrade = machine2Upgrade;
        Machine3Upgrade = machine3Upgrade;
        Machine4Upgrade = machine4Upgrade;
        Machine5Upgrade = machine5Upgrade;
        Machine6Upgrade = machine6Upgrade;
        MoverAi_1Buy = moverAi_1Buy;
        MoverAi_2Buy = moverAi_2Buy;
        PlayerUpgradeSpeed = playerUpgradeSpeed;
        PlayerUpgradeCapacity = playerUpgradeCapacity;
        this.isFakeSend = isFakeSend;
        this.totalCoins = totalCoins;
    }

    public override string ToString()
    {
        return $"Car2Buy: {Car2Buy}";
    }
}
