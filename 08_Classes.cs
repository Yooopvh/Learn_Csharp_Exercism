using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Test
{
    //class RemoteControlCar
    //{
    //    private int _batteryPercentage = 100;
    //    private int _drivenMeters = 0;


    //    public static RemoteControlCar Buy() => new RemoteControlCar();

    //    public string DistanceDisplay() => $"Driven {_drivenMeters} meters";

    //    public string BatteryDisplay()
    //    {
    //        string batteryMessage = "";
    //        if (_batteryPercentage == 0)
    //        {
    //            batteryMessage = $"Battery empty";
    //        } else
    //        {
    //            batteryMessage = $"Battery at {_batteryPercentage}%";
    //        }
    //        return batteryMessage;
    //    }

    //    public void Drive()
    //    {
    //        if (_batteryPercentage > 0) {
    //            _drivenMeters += 20;
    //            _batteryPercentage--;
    //        }



    //    }
    //}

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public class Matrix
    {
        //private string[][] _matrix;
        //public Matrix(string input)
        //{
        //    string[] inputPerRows = input.Split('\n');

        //    string[][] inputMatrix = inputPerRows.Select(x => x.Split(' ')).ToArray();
        //    _matrix = inputMatrix;

        //}

        //public int[] Row(int row)
        //{
        //    string[] rowStringArray = _matrix[row-1];
        //    int[] result = rowStringArray.Select(x => Int32.Parse(x)).ToArray();
        //    return result;
        //}

        //public int[] Column(int col)
        //{
        //    List<string> columnStringList = new List<string>();
        //    foreach (var row in _matrix)
        //    {
        //        columnStringList.Add(row[col-1]);
        //    }
        //    string[] columnStringArray = columnStringList.ToArray();
        //    int[] result = columnStringArray.Select(x => Int32.Parse(x)).ToArray();
        //    return result;
        //}


        //Versión simplificada
        private string[][] _matrix;
        public Matrix(string input) => _matrix = input.Split('\n').Select(x => x.Split(' ')).ToArray();

        public int[] Row(int row) => _matrix[row-1].Select(x => Int32.Parse(x)).ToArray();

        public int[] Column(int col) => _matrix.Select(x => Int32.Parse(x[col-1])).ToArray();
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------



    public class TreeBuildingRecord
    {
        public int ParentId { get; set; }
        public int RecordId { get; set; }
    }

    public class Tree
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public List<Tree> Children { get; set; }

        public bool IsLeaf => Children.Count == 0;
    }

    public static class TreeBuilder
    {
        public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
        {
            var ordered = new SortedList<int, TreeBuildingRecord>();

            foreach (var record in records)  ordered.Add(record.RecordId, record);

            records = ordered.Values;

            var trees = new List<Tree>();
            var previousRecordId = -1;

            foreach (var record in records)
            {
                var t = new Tree { Children = new List<Tree>(), Id = record.RecordId, ParentId = record.ParentId };
                trees.Add(t);

                if ((t.Id == 0 && t.ParentId != 0) ||
                    (t.Id != 0 && t.ParentId >= t.Id) ||
                    (t.Id != 0 && t.Id != previousRecordId + 1))
                {
                    throw new ArgumentException();
                }

                ++previousRecordId;
            }

            if (trees.Count == 0)
            {
                throw new ArgumentException();
            }

            for (int i = 1; i < trees.Count; i++)
            {
                var t = trees.First(x => x.Id == i);
                var parent = trees.First(x => x.Id == t.ParentId);
                parent.Children.Add(t);
            }

            var r = trees.First(t => t.Id == 0);
            return r;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------

    public class Node : AtributesEnum, IEquatable<Node>
    {
        public string Name { get; }

        public Node(string name)
        {
            Name = name;
        }

        public bool Equals(Node? other) => (Name == other.Name);

        public override int GetHashCode() => HashCode.Combine(Name);
    }

    public class Edge : AtributesEnum, IEquatable<Edge>
    {
        public string Node1 { get; }
        public string Node2 { get; }

        public Edge(string node1, string node2)
        {
            Node1 = node1;
            Node2 = node2;
        }

        public bool Equals(Edge? other) => (Node1 == other.Node1 && Node2 == other.Node2 );

        public override int GetHashCode() => HashCode.Combine(Node1,Node2);
    }

    public record Attr(string varName, string value);

    public class Graph : AtributesEnum//, IEquatable<Graph>
    {
        private SortedList<string,Edge> edges = new SortedList<string, Edge>();
        private SortedList<string,Node> nodes = new SortedList<string,Node>();

        public Node[] Nodes = Array.Empty<Node>();
        public Edge[] Edges = Array.Empty<Edge>();

        public void Add(Node node)
        {
            nodes.Add(node.Name, node);
            Nodes = nodes.Values.ToArray();
        }

        public void Add(Edge edge)
        {
            edges.Add(edge.Node1, edge);
            Edges = edges.Values.ToArray();
        }
    }

    public class AtributesEnum : IEnumerable<Attr>, IEquatable<AtributesEnum>
    {
        public List<Attr> Attrs = new List<Attr>();
        public IEnumerator<Attr> GetEnumerator() => Attrs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Attrs.GetEnumerator();

        public void Add(string attr1, string attr2) => Attrs.Add(new Attr(attr1, attr2));

        public bool Equals(AtributesEnum? other) => Attrs.Equals(other.Attrs);
    }


}
