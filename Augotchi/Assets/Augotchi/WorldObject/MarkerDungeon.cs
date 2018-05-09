using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDungeon : Marker {

    private Dungeon dungeon;

    override
    protected void Start()
    {
        base.Start();

        dungeon = Dungeon.GenerateDungeon();
    }

    public override void picked()
    {
        bool rangeHit;
        int layerMask = 1 << LayerMask.NameToLayer("RangeCircle");
        rangeHit = Physics.Raycast(transform.position + new Vector3(0, 5, 0), Vector3.down, 100, layerMask);

        if (rangeHit)
        {
            Instantiate(P_MarkerPoof, transform.position, Quaternion.identity);

            executeEffect();
        }
    }

    protected override void executeEffect()
    {
        gc.showDungeon(dungeon);
    }
}
