using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletMethods 
{






    public static Vector3 randomSpread(int spreadFactor)
    {
        float randX = Random.Range(-spreadFactor, spreadFactor);
        float randY = Random.Range(-spreadFactor, spreadFactor);
        float randZ = Random.Range(-spreadFactor, spreadFactor);
        return new Vector3(randX, randY, randZ);
    }
}
