using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
//using MoreLinq;

namespace Grafy3 {
    class Program {
        static List<Node> mojParser(string fileName) {

            string[] data2 = File.ReadAllLines(".\\" + fileName); //Pobierz plik
            Array.Resize(ref data2, data2.Length - 1); //Usuń ostatni wiersz, jest to tylko '}'
            data2 = data2.Skip(1).ToArray(); //Usuń pierwszy wiersz
            for (int i1 = 0; i1 < data2.Length; i1++) { //Usuń każdy średnik i ]
                data2[i1] = data2[i1].Remove(data2[i1].Length - 2);
            }

            List<Node> test = new List<Node>();

            foreach (string line in data2) {
                if (line.Contains("name")) {
                    string temp;
                    temp = line.Remove(0, line.IndexOf("name=") + 5);
                    temp = temp.Trim('\\','\"');
                    test.Add(new Node(temp, new List<Edge>()));
                }
                else { //Jeżeli nie ma "name to znaczy że to waga"
                    string temp = line;
                    int firstNode = int.Parse(temp.Remove(temp.IndexOf(' ')));
                    string temp2 = temp.Remove(0, temp.IndexOf('-') + 3);
                    int secondNode = int.Parse(temp2.Remove(temp2.IndexOf(' ')));
                    string temp3 = temp.Remove(0, temp.IndexOf("weight=") + 7);
                    temp3 = temp3.Trim(']', ';');
                    int weight = int.Parse(temp3);

                    Edge edge1 = new Edge(firstNode, weight);
                    Edge edge2 = new Edge(secondNode, weight);

                    test[secondNode].edges.Add(edge1);
                    test[firstNode].edges.Add(edge2);

                }
            }

            return test;
        }

        static int[] tempName(List<Node> nodes, int[] results, int nodeNow, int roadNow, int startingNode) {
            //Jeśli nie wróciliśmy na początek
            if (nodeNow != startingNode) {
                //jeśli droga którą przeszlśmy do tego miejsca jest mniejsza to ją zapisz
                if (roadNow < results[nodeNow] || results[nodeNow] == 0) {
                    results[nodeNow] = roadNow;
                    //i idź do wszystkich następnych
                    for (int i = 0; i < nodes[nodeNow].edges.Count; i++) {
                        results = tempName(nodes, results, nodes[nodeNow].edges[i].toId, roadNow + nodes[nodeNow].edges[i].weight, startingNode);
                    }
                }
                //jesli droga jest większa to koniec
            }
            return results;
        }

        static void Main(string[] args) {


            if (args.Count() == 1 && File.Exists(".\\" + args[0])) {
                List<Node> nodes = mojParser(args[0]);
                int[] sumOfRoads = new int[nodes.Count];
                int[] maxRoadToOtherCity = new int[nodes.Count];
                for (int i = 0; i < nodes.Count; i++) {
                    int[] results = new int[nodes.Count];
                    results[i] = 0;
                    for (int j = 0; j < nodes[i].edges.Count; j++) {
                        results = tempName(nodes, results, nodes[i].edges[j].toId, nodes[i].edges[j].weight, i);
                    }


                    sumOfRoads[i] = results.Sum();
                    maxRoadToOtherCity[i] = results.Max();
                }

                Console.WriteLine("Najepsze miasto do stworzenia hurtowni (najmniejsza suma odległości do wszystkich innych) to miasto " + nodes[Array.IndexOf(sumOfRoads, sumOfRoads.Min())].name);
                Console.WriteLine("Najepsze miasto do stworzenia hurtowni (najmniejsza odległość od hurtowni do najdalszego miasta) to miasto " + nodes[Array.IndexOf(maxRoadToOtherCity, maxRoadToOtherCity.Min())].name);
                Console.ReadLine();
            }
            else Console.WriteLine("Nie ma takiego pliku!");

            
        }
    }
}
