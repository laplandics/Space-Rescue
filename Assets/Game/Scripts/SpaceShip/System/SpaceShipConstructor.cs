using System;
using UnityEngine;

[Serializable]
public class SpaceShipConstructor : UnitConstructor<SpaceShip>
{
    private SpaceShipConfig _config;
    private UnitObserver _observer;

    public override void Initialize(Unit owner)
    {
        base.Initialize(owner);
        _config = (SpaceShipConfig)Owner.GetConfig();
        _observer = Owner.FindSystem<UnitObserver>();
    }

    public void ConstructFrame(SpaceShipFrameType frameType, SpaceShipComponentTierType tierType)
    {
        var frameName = $"{frameType}_Frame_{tierType}";
        var frameObj = GenerateObject($"{frameType}", (false, Vector3.zero, Quaternion.identity));
        var frameScript = (SpaceShipFrame)GenerateScript($"{nameof(SpaceShip)}{frameType}Frame", frameObj);
        frameScript.tier = tierType;
        var path = $"{nameof(SpaceShip)}/{frameName}";
        GenerateSprite(path, _config.shipRenderOrder, frameObj);
        _observer.OnComponentBuilt?.Invoke(frameScript);
    }
    
    public void ConstructStructure(SpaceShipStructureType structureType, SpaceShipComponentTierType tierType)
    {
        var structureObj = GenerateObject($"{structureType}", (false, Vector3.zero, Quaternion.identity));
        var structureScript = (SpaceShipStructure)GenerateScript($"{nameof(SpaceShip)}{structureType}Structure", structureObj);
        structureScript.tier = tierType;
        _observer.OnComponentBuilt?.Invoke(structureScript);
    }

    public void ConstructWeapon(SpaceShipWeaponType weaponType, SpaceShipComponentTierType tierType)
    {
        if (weaponType == SpaceShipWeaponType.None) return;
        var weaponObj = GenerateObject($"{weaponType}", (false, Vector3.zero, Quaternion.identity));
        var weaponScript = (SpaceShipWeapon)GenerateScript($"{nameof(SpaceShip)}{weaponType}Weapon", weaponObj);
        weaponScript.tier = tierType;
        _observer.OnComponentBuilt?.Invoke(weaponScript);
    }

    public override void Deinitialize()
    {
        Owner = null;
        _config = null;
    }
}