using System;
using System.Collections.Generic;
using System.Text;

namespace testUI
{
    public class Node
    {
        public static int nodeNumber = 0;
        public List<Node> visitedNodes = new List<Node>();
        private string nodeName;
        private string nodeType;
        public List<Node> children = new List<Node>();
        private float nodeCapacity;
        private float nodeCurrentFlow;

        public Node()
        {
            nodeName = "defaultName";
            nodeType = "defaultType";
            nodeCapacity = 0f;
            nodeCurrentFlow = 0f;
            nodeNumber += 1;
        }

        public string getNodeType()
        {
            return nodeType;
        }

        public string getNodeName()
        {
            return nodeName;
        }

        public float getNodeCapacity()
        {
            return nodeCapacity;
        }

        public float getNodeCurrentFlow()
        {
            return nodeCurrentFlow;
        }

        public void setCurrentFlow(float flow)
        {
            this.nodeCurrentFlow = flow;
        }

        public void setNodeName(string name)
        {
            this.nodeName = name;
        }

        public void setNodeCapacity(float capacity)
        {
            this.nodeCapacity = capacity;
        }

        public void setNodeType(string type)
        {
            this.nodeType = type;
        }

        public void addChildNode(Node child)
        {
            children.Add(child);
            Console.WriteLine(this.nodeName + " dugumune " + child.getNodeName() + " dugumum eklendi!");
        }

        public void addVisitedNode(Node visitedChild)
        {
            visitedNodes.Add(visitedChild);
            Console.WriteLine(visitedChild.getNodeName() + " dugumu, " + this.nodeName + "dugumunun ziyaret edilmis dugumler listesine eklendi.");
        }
    }
}
