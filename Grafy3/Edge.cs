using System;
using System.Collections.Generic;
using System.Text;

namespace Grafy3 {
    class Edge {
        public int toId;
        public int weight;

        public Edge(int toId, int weight) {
            this.toId = toId;
            this.weight = weight;
        }
    }
}
