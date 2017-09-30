using System;
using System.Collections.Generic;

namespace PathfinderBuilder
{
    public static class CarryCapacityTable
    {
        private static readonly Dictionary<int, Tuple<double, double, double>> InnerTable;

        static CarryCapacityTable()
        {
            InnerTable = new Dictionary<int, Tuple<double, double, double>>
            {
                {1,  Tuple.Create(  3.0,    6.0,     10.0    )},
                {2,  Tuple.Create(  6.0,    13.0,    20.0    )},
                {3,  Tuple.Create(  10.0,   20.0,    30.0    )},
                {4,  Tuple.Create(  13.0,   26.0,    40.0    )},
                {5,  Tuple.Create(  16.0,   33.0,    50.0    )},
                {6,  Tuple.Create(  20.0,   40.0,    60.0    )},
                {7,  Tuple.Create(  23.0,   46.0,    70.0    )},
                {8,  Tuple.Create(  26.0,   53.0,    80.0    )},
                {9,  Tuple.Create(  30.0,   60.0,    90.0    )},
                {10, Tuple.Create(  33.0,   66.0,    100.0   )},
                {11, Tuple.Create(  38.0,   76.0,    115.0   )},
                {12, Tuple.Create(  43.0,   86.0,    130.0   )},
                {13, Tuple.Create(  50.0,   100.0,   150.0   )},
                {14, Tuple.Create(  58.0,   116.0,   175.0   )},
                {15, Tuple.Create(  66.0,   133.0,   200.0   )},
                {16, Tuple.Create(  76.0,   153.0,   230.0   )},
                {17, Tuple.Create(  86.0,   173.0,   260.0   )},
                {18, Tuple.Create(  100.0,  200.0,   300.0   )},
                {19, Tuple.Create(  116.0,  233.0,   350.0   )},
                {20, Tuple.Create(  133.0,  266.0,   400.0   )},
                {21, Tuple.Create(  153.0,  306.0,   460.0   )},
                {22, Tuple.Create(  173.0,  346.0,   520.0   )},
                {23, Tuple.Create(  200.0,  400.0,   600.0   )},
                {24, Tuple.Create(  233.0,  466.0,   700.0   )},
                {25, Tuple.Create(  266.0,  533.0,   800.0   )},
                {26, Tuple.Create(  306.0,  613.0,   920.0   )},
                {27, Tuple.Create(  346.0,  693.0,   1040.0  )},
                {28, Tuple.Create(  400.0,  800.0,   1200.0  )},
                {29, Tuple.Create(  466.0,  933.0,   1400.0  )},
                };

            for (int i = 30; i <= 100; i++)
            {
                var t = InnerTable[i - 10];
                InnerTable[i] = new Tuple<double, double, double>(t.Item1 * 4, t.Item2 * 4, t.Item3 * 4);
            }

        }

        public static IReadOnlyDictionary<int, Tuple<double, double, double>> Table => InnerTable;

    }
}
