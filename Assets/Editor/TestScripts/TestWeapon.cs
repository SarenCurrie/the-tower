using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;


/// <summary>
/// 
/// Tests the Weapon class.
///
/// 
/// </summary>
[TestFixture]
public class TestWeapon
{

    [Test]
    public void TestCreateRandomWeapon()
    {
        GameObject o = Item.GenerateWeapon(new Vector3(0, 0, 0), true);
        Weapon weapon = o.GetComponent<Weapon>();
        Assert.IsNotNull(weapon);

    }


    [Test]
    public void TestWeaponHasStatsInitialised()
    {
        GameObject o = Item.GenerateWeapon(new Vector3(0, 0, 0), true);
        Weapon weapon = o.GetComponent<Weapon>();
        Assert.IsNotNull(weapon.spread);
        Assert.IsNotNull(weapon.spreadRange);
        Assert.IsNotNull(weapon.fireForce);
        Assert.IsNotNull(weapon.fireFrequency);
        Assert.IsNotNull(weapon.damageMod);
        Assert.IsNotNull(weapon.weaponMinor);
        Assert.IsNotNull(weapon.weaponMajor);
        Assert.IsNotNull(weapon.strengthModifier);
        Assert.IsNotNull(weapon.dexterityModifier);
        Assert.IsNotNull(weapon.intelligenceModifier);
    }


    [Test]
    public void TestWeaponHasSpritesInitialised()
    {
        GameObject o = Item.GenerateWeapon(new Vector3(0, 0, 0), true);
        Weapon weapon = o.GetComponent<Weapon>();
        Assert.IsNotNull(weapon.selectedSprite);
        Assert.IsNotNull(weapon.unSelectedSprite);
        Assert.IsNotNull(weapon.possibleProjectileSprites);
        Assert.IsNotNull(weapon.looks);
        Assert.IsNotNull(weapon.selectedSideLooks);
        Assert.IsNotNull(weapon.unselectedSideLooks);
    }

    [Test]
    public void TestWeaponSpreadWithinRange()
    {
        GameObject o = Item.GenerateWeapon(new Vector3(0, 0, 0), true);
        Weapon weapon = o.GetComponent<Weapon>();
        float spread = weapon.spreadRange;
        Assert.IsTrue(spread <= 60);
        Assert.IsTrue(spread >= 0);
    }





}
