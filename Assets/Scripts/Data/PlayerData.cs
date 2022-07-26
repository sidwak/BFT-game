using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public bool Machine1Buy = false;
    public bool Machine2Buy = false;
    public bool Machine3Buy = false;
    public bool Machine4Buy = false;
    public bool Machine5Buy = false;
    public bool Machine6Buy = false;
    public bool Machine7Buy = false;
    public bool Car1Buy = false;
    public bool Car2Buy = false;
    public bool Car3Buy = false;
    public bool Car4Buy = false;
    //public bool Car5Buy = false;
    public bool Car6Buy = false;
    public bool Area2Buy = false;
    public bool Area3Buy = false;
    public bool Area4Buy = false;
    //public bool Area5Buy = false;
    public bool Truck1Buy = false;
    public bool Truck2Buy = false;
    public bool Rack2Buy = false;
    public bool Rack3Buy = false;
    public bool Rack4Buy = false;
    public bool VendingMachingBuy = false;
    public bool ShopOpen = false;

    public bool Machine1Upgrade = false;
    public bool Machine2Upgrade = false;
    public bool Machine3Upgrade = false;
    public bool Machine4Upgrade = false;
    public bool Machine5Upgrade = false;
    public bool Machine6Upgrade = false;
    public bool Machine7Upgrade = false;

    public bool MoverAi_1Buy = false;
    public bool MoverAi_2Buy = false;
    public bool MoverAi_3Buy = false;
    public bool MoverAi_4Buy = false;
    public bool MoverAiUpgradeSpeed = false;

    public bool PlayerUpgradeSpeed = false;
    public bool PlayerUpgradeCapacity = false;

    public bool isFakeSend = false;

    public float sliderVal = 0.025f;

    //PLAYER SETTINGS
    public int totalCoins = 0;
    public int curLevel = 0;
    public bool vibrationToggle = true;
    public bool soundToggle = true;

    public PlayerData(bool machine1Buy, bool machine2Buy, bool machine3Buy, bool machine4Buy, bool machine5Buy,
        bool machine6Buy, bool machine7Buy, bool car1Buy, bool car2Buy, bool car3Buy, bool car4Buy, bool car6Buy,
        bool area2Buy, bool area3Buy, bool area4Buy, bool truck1Buy, bool truck2Buy, bool rack2Buy, bool rack3Buy,
        bool rack4Buy, bool vendingMachingBuy, bool shopOpen, bool machine1Upgrade, bool machine2Upgrade, bool machine3Upgrade,
        bool machine4Upgrade, bool machine5Upgrade, bool machine6Upgrade, bool machine7Upgrade, bool moverAi_1Buy,
        bool moverAi_2Buy, bool moverAi_3Buy, bool moverAi_4Buy, bool moverAiUpgradeSpeed, bool playerUpgradeSpeed,
        bool playerUpgradeCapacity, bool isFakeSend, float sliderVal, int totalCoins, int curLevel, bool vibrationToggle,
        bool soundToggle)
    {
        Machine1Buy = machine1Buy;
        Machine2Buy = machine2Buy;
        Machine3Buy = machine3Buy;
        Machine4Buy = machine4Buy;
        Machine5Buy = machine5Buy;
        Machine6Buy = machine6Buy;
        Machine7Buy = machine7Buy;
        Car1Buy = car1Buy;
        Car2Buy = car2Buy;
        Car3Buy = car3Buy;
        Car4Buy = car4Buy;
        Car6Buy = car6Buy;
        Area2Buy = area2Buy;
        Area3Buy = area3Buy;
        Area4Buy = area4Buy;
        Truck1Buy = truck1Buy;
        Truck2Buy = truck2Buy;
        Rack2Buy = rack2Buy;
        Rack3Buy = rack3Buy;
        Rack4Buy = rack4Buy;
        VendingMachingBuy = vendingMachingBuy;
        ShopOpen = shopOpen;
        Machine1Upgrade = machine1Upgrade;
        Machine2Upgrade = machine2Upgrade;
        Machine3Upgrade = machine3Upgrade;
        Machine4Upgrade = machine4Upgrade;
        Machine5Upgrade = machine5Upgrade;
        Machine6Upgrade = machine6Upgrade;
        Machine7Upgrade = machine7Upgrade;
        MoverAi_1Buy = moverAi_1Buy;
        MoverAi_2Buy = moverAi_2Buy;
        MoverAi_3Buy = moverAi_3Buy;
        MoverAi_4Buy = moverAi_4Buy;
        MoverAiUpgradeSpeed = moverAiUpgradeSpeed;
        PlayerUpgradeSpeed = playerUpgradeSpeed;
        PlayerUpgradeCapacity = playerUpgradeCapacity;
        this.isFakeSend = isFakeSend;
        this.sliderVal = sliderVal;
        this.totalCoins = totalCoins;
        this.curLevel = curLevel;
        this.vibrationToggle = vibrationToggle;
        this.soundToggle = soundToggle;
    }

    public override string ToString()
    {
        return $"Car2Buy: {Car2Buy}";
    }


}
