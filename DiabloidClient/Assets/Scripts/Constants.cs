using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {
    public static class Layers {
        public static class Names {
            public const string Ground = "Ground";
            public const string Actor = "Actor";
        }

        public static class Masks {
            static Masks() {
                Ground = LayerMask.GetMask(Names.Ground);
                Actor = LayerMask.GetMask(Names.Actor);
            }

            public static int Ground { get; private set; }
            public static int Actor { get; private set; }
        }
    }
}
