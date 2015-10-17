using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

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
    public void TestWeaponMajorMinorDifferent()
    {
        GameObject o = Item.GenerateWeapon(new Vector3(0, 0, 0), true);
        Weapon weapon = o.GetComponent<Weapon>();
        Assert.IsTrue(weapon.weaponMajor != weapon.weaponMinor);
        
    }
   
    [Test]
    public void TestWeaponSpreadWithinRange()
    {
        GameObject o = Item.GenerateWeapon(new Vector3(0, 0, 0), true);
        Weapon weapon = o.GetComponent<Weapon>();
        Assert.AreNotSame(weapon.weaponMajor, weapon.weaponMinor);

    }
}
