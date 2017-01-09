using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class MinerConfigData
{
    private readonly int health;
    private readonly int walkSpeed;
    private readonly int maxStamina;
    private readonly int throwForce;

    public MinerConfigData(int _health, int _walkSpeed, int _maxStamina, int _throwForce)
    {
        this.health = _health;
        this.walkSpeed = _walkSpeed;
        this.maxStamina = _maxStamina;
        this.throwForce = _throwForce;
    }

    public int getHealth()
    {
        return this.health;
    }

    public int getWalkSpeed()
    {
        return this.walkSpeed;
    }

    public int getMaxStamina()
    {
        return this.maxStamina;
    }

    public int getThrowForce()
    {
        return this.throwForce;
    }
}
