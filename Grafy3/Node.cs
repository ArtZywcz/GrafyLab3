using System;
using System.Collections.Generic;
using System.Text;

namespace Grafy3 {
    class Node {
        public string name;
        public List<Edge> edges;


        public Node(string name, List<Edge> edges) {
            this.name = name;
            this.edges = edges;
        }
    }
}
