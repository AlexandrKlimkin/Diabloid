using UnityEngine;


public class AIScheduler : Scheduler<AIScheduler, AIController> {

    protected override float ObjectsPerFrame {
        get {
            return 1f; // TODO: Enable Scheduler only after game start
        }
    }

    protected override void UpdateObject(AIController target) {
        target.UpdateAI();
    }
}

