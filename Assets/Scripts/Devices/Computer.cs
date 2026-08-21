using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Device, IInteract {
    protected override void enableDevice() {
        base.enableDevice();
    }
    protected override void disableDevice() {
        base.disableDevice();
    }
    public void interacted(Player player) {
        
    }
}
