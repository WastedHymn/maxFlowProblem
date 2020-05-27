using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
namespace testUI
{
    public partial class Form2 : Form
    {
        public List<string> path = new List<string>();
        public List<Node> nodes = new List<Node>();
        public List<int> selectedChildIndexes = new List<int>();
        public List<List<string>> pathList = new List<List<string>>();
        public Dictionary<int, List<Node>> availablePaths = new Dictionary<int, List<Node>>();
        public int pathCounter = 0;
        public Node startNode;
        public int currentNodeIndex;
        public int delta;
        public int totalMaxFlow;
        public int loopCounter = 0;
        public string numberString;
        public Form2()
        {
            InitializeComponent();
            //TextBox t1 = new TextBox();
           // t1.Name = "text1";
           // t1.Width = 300;
           // t1.Text = "T E S T";
            //this.Controls.Add(t1);
            setupDropDown();

        }

        public int findDelta()
        {
            int totalMaxFlow;
            //int nodeNumber = nodes.Count;
            int maxCapacity = 0;
            int counter = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes[i].edges.Count; j++)
                {
                    if (nodes[i].edges[j][1] > maxCapacity)
                    {
                        maxCapacity = nodes[i].edges[j][1];
                    }
                }
            }

            while (maxCapacity >= 2)
            {
                maxCapacity = maxCapacity / 2;
                counter++;
            }

            double result = Math.Pow(2, Convert.ToDouble(counter));
            Console.WriteLine("Delta's first value: " + result);
            return Convert.ToInt32(result);
        }

        public string findMaxCapacityEdge()
        {
            int maxCapacity = 0;
            int index = -1;
            int selectedNeighbourIndex;
            //Search all child edges
            for (int i = 0; i < nodes[currentNodeIndex].edges.Count; i++)
            {
                if (!nodes[currentNodeIndex].deadEnds.Contains(i))
                {
                    int remainingFlowCapacity = nodes[currentNodeIndex].edges[i][1] - nodes[currentNodeIndex].edges[i][0];
                    Console.WriteLine(nodes[currentNodeIndex].getNodeName() + " düğümünün " + i + ". kenarına ait kalan akış " + remainingFlowCapacity);
                    if (remainingFlowCapacity > maxCapacity && remainingFlowCapacity >= delta)
                    {
                        string negative = "-" + nodes[currentNodeIndex].children[i].getNodeName();
                        if (path.Contains(nodes[currentNodeIndex].children[i].getNodeName())==false && path.Contains(negative) == false)
                        {
                            maxCapacity = remainingFlowCapacity;
                            index = i;
                        }
                    }
                }
            }
            if (index != -1)
            {
                selectedChildIndexes.Add(index);
                int smt = nodes.FindIndex(item => item.getNodeName() == nodes[currentNodeIndex].children[index].getNodeName());
                Console.WriteLine("Seçilen kenar: " + index);
                //smt.ToString();
                return smt.ToString();
            }
            else
            {
                int maxFlow = 0;
                selectedNeighbourIndex = -1;
                //Search neighbours
                for (int i=0; i<nodes[currentNodeIndex].neighbours.Count;i++)
                {
                    int neighbourIndex = nodes.FindIndex(item => item.getNodeName() == nodes[currentNodeIndex].neighbours[i]);
                    int neighbourChildIndex = nodes[neighbourIndex].children.FindIndex(item => item.getNodeName() == nodes[currentNodeIndex].getNodeName());
                    //check if neighbour node is not in the path and flow is greater than zero.
                    if ((path.Contains(nodes[neighbourIndex].getNodeName()) == false) && nodes[neighbourIndex].edges[neighbourChildIndex][0] > 0 && (nodes[currentNodeIndex].deadNeighbours.Contains(nodes[neighbourIndex].getNodeName()) == false))
                    {
                        if (nodes[neighbourIndex].edges[neighbourChildIndex][0] > maxFlow)
                        {
                            maxFlow = nodes[neighbourIndex].edges[neighbourChildIndex][0];
                            selectedNeighbourIndex = neighbourIndex;
                        }
                    }
                }
                if (selectedNeighbourIndex != -1)
                {
                    selectedChildIndexes.Add(-1);
                    return "-" + selectedNeighbourIndex.ToString();
                }
                else
                {
                    selectedChildIndexes.Add(-2);
                    return "null";
                }
                //return index;
            }
        }

        public int findBottleneck()
        {
            //currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1].getNodeName());
            //int startBottleneckIndex = selectedChildIndexes[0];
            int bottleneck = 1000000000;
            Console.WriteLine("BOTTLENECK START VALUE: " + bottleneck);
            //Console.Write("selectedChildIndexes: ");
            Console.WriteLine();
            for (int i = 0; i < path.Count - 1; i++)
            {
                Console.WriteLine("bottleneck for loop i value: " + i + " nodename: " + path[i]);
                int index;
                Console.WriteLine("andthebeatgoeson: " + path[i]);
                int k = selectedChildIndexes[i];
                Console.WriteLine("bottleneck k: " + k);
                if (k == -1)
                {
                    string nodename = path[i + 1].Replace("-", "");
                    index = nodes.FindIndex(item => item.getNodeName() == nodename);
                    Console.WriteLine("bottleneck index: " + index);
                    int childIndex = nodes[index].children.FindIndex(item => item.getNodeName() == path[i]);
                    int num2 = nodes[index].edges[childIndex][0];
                    if (num2 < bottleneck)
                    {
                        bottleneck = num2;
                    }
                }
                else
                {

                    string nodename = path[i].Replace("-", "");
                    index = nodes.FindIndex(item => item.getNodeName() == nodename);
                    Console.WriteLine("bottleneck index: " + index);
                    int num = nodes[index].edges[k][1] - nodes[index].edges[k][0];
                    Console.WriteLine("NODENAME: " + nodes[index].getNodeName());
                    Console.WriteLine("NUM: " + num);
                    if (num < bottleneck)
                    {
                        bottleneck = num;
                    }
                }
            }
            return bottleneck;
        }

        public void augmentTheFlow(int bottleneck)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Console.WriteLine("augmenting flow i: " + i);
                int index;
                int index2 = selectedChildIndexes[i];
                if (index2 == -1)
                {
                    index = nodes.FindIndex(item => item.getNodeName() == path[i+1].Replace("-",""));
                    Console.WriteLine("Next Node: " + nodes[index].getNodeName());
                    index2 = nodes[index].children.FindIndex(item => item.getNodeName() == path[i]);
                    Console.WriteLine("Current Node: " + nodes[index2].getNodeName());
                    nodes[index].edges[index2][0] -= bottleneck;
                }
                else
                {
                   index = nodes.FindIndex(item => item.getNodeName() == path[i].Replace("-",""));
                   nodes[index].edges[index2][0] += bottleneck;
                }
            }
        }

        public void resetDeadEnds()
        {
            for (int i=0; i<nodes.Count; i++)
            {
                nodes[i].deadEnds.Clear();
                nodes[i].deadNeighbours.Clear();
            }
        }

        public void findMaxFlow()
        {
            Console.WriteLine("############################################");
            totalMaxFlow = 0;
            delta = findDelta();
            currentNodeIndex = 0;
            while (delta > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Delta: " + delta);
               
                if (nodes[currentNodeIndex].getNodeType() != "finish")
                {
                    string strrr = "-" + nodes[currentNodeIndex].getNodeName();
                    Console.WriteLine("strrr: " + strrr);
                    if (path.Contains(nodes[currentNodeIndex].getNodeName()) == false && path.Contains(strrr)==false )
                    { 
                        ///Adding current node to the path.
                        path.Add(nodes[currentNodeIndex].getNodeName());
                    }
                    string ind = findMaxCapacityEdge();
                    Console.WriteLine("node != finish && İND: " + ind);
                    //path.FindIndex(item => item.getNodeName() == nodes[currentNodeIndex].getNodeName()) == -1
                    //path.FindIndex(item => item.Contains(nodes[currentNodeIndex].getNodeName())) == -1
                    
                    Console.Write("Path: ");
                    for (int i = 0; i < path.Count; i++)
                    {
                        Console.Write(path[i] + " ");
                    }
                    Console.WriteLine();
                    ///////////////////////////////////////////7
                    if (ind == "null")
                    {
                        //Check if currentIndexNode is a reverse direction node or a normal node
                        Console.WriteLine("ind == null");
                        int reverseNodeIndex = -4;
                        int deadChild = -4;
                        if (path[path.Count - 1].Contains("-"))
                        {
                             reverseNodeIndex = currentNodeIndex;
                        }
                        selectedChildIndexes.RemoveAt(path.Count - 1);
                        path.RemoveAt(path.Count - 1);
                        
                        if (path.Count > 0)
                        {
                            deadChild = selectedChildIndexes[path.Count - 1];
                            selectedChildIndexes.RemoveAt(selectedChildIndexes.Count - 1);
                            currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1]);
                            //selectedChildIndexes.RemoveAt(path.Count-1);
                            if (deadChild == -1)
                            {
                                nodes[currentNodeIndex].deadNeighbours.Add(nodes[reverseNodeIndex].getNodeName());
                            }
                            else
                            {
                                nodes[currentNodeIndex].deadEnds.Add(deadChild);
                            }
                        }
                        else
                        {
                            delta = delta / 2;
                            selectedChildIndexes.Clear();
                            path.Clear();
                            resetDeadEnds();
                            currentNodeIndex = 0;
                        }                      
                    }
                    else
                    {
                        int newInd = Int32.Parse(ind);
                        //reverse direction
                        if (newInd < 0)
                        {
                            int numb = -1 * newInd;
                            string reverseDirection = "-"+nodes[numb].getNodeName();
                            Console.WriteLine("Reverse Direction Node: " + reverseDirection);
                            if (path.Contains(reverseDirection)==false)
                            {
                                path.Add(reverseDirection);
                            }
                            currentNodeIndex = numb;
                            Console.WriteLine("Selected reverse direction. currentNodeIndex: " + currentNodeIndex);
                        }
                        else
                        {
                            Console.WriteLine("PATH COUNT 2: " + path.Count);
                            Console.WriteLine("Şimdiki düğüm: " + nodes[currentNodeIndex].getNodeName());
                            int num = Int32.Parse(ind);
                            Console.WriteLine("Gidilecek Düğüm: " + nodes[num].getNodeName());
                            currentNodeIndex = num;
                        }
                    }
                    /////////////////////////////////////////
                }
                else
                {
                    path.Add(nodes[currentNodeIndex].getNodeName());
                    Console.Write("Path Final: ");
                    pathList.Add(path);
                    for (int i = 0; i < path.Count; i++)
                    {
                        Console.Write(path[i] + " ");
                    }
                    Console.WriteLine();
                    int bottleneck = findBottleneck();
                    Console.WriteLine("BOTTLENECK: " + bottleneck);
                    augmentTheFlow(bottleneck);
                    totalMaxFlow += bottleneck;
                    Console.WriteLine("TOTALMAXFLOW: " + totalMaxFlow);
                    path.Clear();
                    selectedChildIndexes.Clear();
                    currentNodeIndex = 0;
                }
            }
            Console.WriteLine("Max Flow: " + totalMaxFlow);
        }
      

        public void printObjectAttributes()
        {
            for (int i=0; i<nodes.Count; i++)
            {
                Console.WriteLine("Dugum adi: " + nodes[i].getNodeName());
                Console.WriteLine("Dugum turu: " + nodes[i].getNodeType());
                Console.WriteLine("Dugum akis degeri: " + nodes[i].getNodeCurrentFlow());
                Console.WriteLine("Dugum kapasite degeri: " + nodes[i].getNodeCapacity());
                Console.WriteLine("==============================");
            }
            Console.WriteLine("Toplam Dugum Sayisi: " + Node.nodeNumber.ToString());
        }

        public void changeNodeName()
        {
            string nodeN = newNodeNameTextBox.Text;

           // bool check = nodeN.Any(x => char.IsLetter(x));
            //if (check)
            //{
                if (nodeDropDown.Items.Count != 0)
                {
                    nodeDropDown.Items[nodeDropDown.SelectedIndex] = nodeN;
                    if (nodeDropDown.SelectedIndex != 0)
                    {
                        addChildNodeDropDown.Items[nodeDropDown.SelectedIndex-1] = nodeN;
                    }
                    nodes[nodeDropDown.SelectedIndex].setNodeName(nodeN);
                    string mes = "Seçili düğümün adı " + nodes[nodeDropDown.SelectedIndex].getNodeName() + " olarak değiştirildi!";
                    Console.WriteLine(mes);
                    Console.WriteLine("");
                    Console.WriteLine("------------------------------");
                    MessageBox.Show(mes, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    printObjectAttributes();
                }
                else
                {
                    MessageBox.Show("Önce düğüm sayısını belirleyin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            //}
            //else
            //{
            //    MessageBox.Show("Lütfen bir harf kullanarak isim belirleyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ADDING NAME TO THE SELECTED NODE
            /*
            if (nodeDropDown.Items.Count != 0)
            {
                MessageBox.Show("Lütfen kapasite belirtin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Lütfen düğüm sayısını belirtin daha sonra düğümün kapasitesini değiştirin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            //int index  = nodeDropDown.SelectedIndex;
            //Console.WriteLine("deneme:    " + nodeN);

            changeNodeName();

        }

        private void addDropDownItems(int num)
        {
            Node.nodeNumber = 0;
            if (nodeDropDown.Items.Count != 0)
            {
                nodeDropDown.Items.Clear();
                addChildNodeDropDown.Items.Clear();
            }
                for (int i = 0; i < num; i++)
                {
                    Node node = new Node();
                    if (i == 0)
                    {
                        node.setNodeType("start");
                        node.setNodeName("start");
                        nodeDropDown.Items.Add("start");
                    }
                    else if (i == num - 1)
                    {
                        node.setNodeType("finish");
                        node.setNodeName("finish");
                        nodeDropDown.Items.Add("finish");
                        addChildNodeDropDown.Items.Add("finish");
                    }
                    else
                    {
                        node.setNodeType("normal");
                        node.setNodeName("Düğüm " + i);
                        nodeDropDown.Items.Add("Düğüm " + i);
                        addChildNodeDropDown.Items.Add("Düğüm " + i);
                    }
                    node.setNodeCapacity(0);
                    node.setCurrentFlow(0);
                    nodes.Add(node);
                }
            nodeDropDown.SelectedIndex = 0;
            addChildNodeDropDown.SelectedIndex = 0;
            //nodeDropDown.FindString();   
            printObjectAttributes();
        }

        public void setupDropDown()
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Regex regex = new Regex(@"[\! \# \$ \% \& \' \( \) \* \+ \, \- \. \: \; \< \= \> \? \@ \[ \] \^  \` \{ \| \} \~ /ö/ü/Ö/ÜA-Za-z]");
            //Match match = regex.Match(numberString);
            //string cap = nodeCapacityTextBox.Text;
            //!match.Success && numberString.Length > 0              
           numberString = nodeNumberInput.Text;
            int numberOfNodes = 0;
            bool check = numberString.Any(x => char.IsLetter(x));
           
            if (check)
            {
                Console.WriteLine("WRONG NODE NUMBER INPUT!");
                MessageBox.Show("Lütfen sayı belirtiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (numberString.Length == 0)
                {
                    MessageBox.Show("Lütfen sayı belirtiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Console.WriteLine("True input");
                    Console.WriteLine(numberString);
                    numberOfNodes = Int32.Parse(numberString);
                    addDropDownItems(numberOfNodes);
                    MessageBox.Show("Düğümler oluşturuldu! Lütfen düğümlerle ilgili bilgileri giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void drawGraph()
        {
            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
         
            for (int i = 0; i < nodes.Count; i++)
            {
                Console.WriteLine("i:" + i);
                Console.WriteLine("children count: " + nodes[i].children.Count);
                for (int j = 0; j < nodes[i].children.Count; j++)
                {
                    Console.WriteLine("j:" + j);
                    Console.WriteLine("Parent node: " + nodes[i].getNodeName());
                    Console.WriteLine("node name: "+ nodes[i].children[j].getNodeName());
                    int flow = nodes[i].edges[j][0];
                    var newEdge = graph.AddEdge(nodes[i].getNodeName(), nodes[i].children[j].getNodeName());
                    Microsoft.Msagl.Drawing.Node childNode = graph.FindNode(nodes[i].getNodeName());
                    string cap = nodes[i].edges[j][0].ToString() + "/" + nodes[i].edges[j][1].ToString();
                    newEdge.LabelText = cap;
                    childNode.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                    childNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                    if (flow > 0)
                    {
                        newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Orange;
                    }
                    else
                    {
                        newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    }
                    //graph.AddEdge(nodes[i].getNodeName(), nodes[i].children[j].getNodeName()).Attr.Color = Microsoft.Msagl.Drawing.Color.DarkGreen;
                    
                    //newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                    //string cap = nodes[i].edges[j][0].ToString() + "/" + nodes[i].edges[j][1].ToString();
                    //newEdge.LabelText = cap;
                }
             
            }

            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.CurrentLayoutMethod = LayoutMethod.MDS;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
        }


        private void nodePanel_Paint(object sender, PaintEventArgs e)
        {
            
        }      

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void nodeNumberInput_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button3_Click_1(object sender, EventArgs e)
        {
            /*
            //Change node capacity
            string cap = nodeCapacityTextBox.Text;
            bool check = cap.Any(x => char.IsLetter(x));
            if (check)
            {
                MessageBox.Show("Lütfen sayı giriniz!", "Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("Lütfen sayı giriniz!");
            }
            else
            {
                if (nodeDropDown.SelectedIndex == 0)
                {
                    MessageBox.Show("Başlangıç düğümünün kapasitesini değiştiremezsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if((cap.Length != 0) && (nodeDropDown.Items.Count !=0))
                {
                    int capacity = Int32.Parse(cap);
                    nodes[nodeDropDown.SelectedIndex].setNodeCapacity(capacity);
                    string mess = nodes[nodeDropDown.SelectedIndex].getNodeName() + " düğümünün kapasitesi " + nodes[nodeDropDown.SelectedIndex].getNodeCapacity() + " olarak değiştirildi!";
                    Console.WriteLine(mess);
                    Console.WriteLine("/////////////////////////");
                    MessageBox.Show(mess, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    printObjectAttributes();
                }
                else
                {
                    if (nodeDropDown.Items.Count != 0)
                    {
                        MessageBox.Show("Lütfen kapasite belirtin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Lütfen düğüm sayısını belirtin daha sonra düğümün kapasitesini değiştirin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }*/
        }

        public void printChildren()
        {
            Console.WriteLine("* * * * * * * * * * * * * * * ");
            Console.WriteLine("Children of " + nodes[nodeDropDown.SelectedIndex].getNodeName());
            for (int i=0; i< nodes[nodeDropDown.SelectedIndex].children.Count; i++)
            {
                Console.WriteLine("Child " + i + ":" + nodes[nodeDropDown.SelectedIndex].children[i].getNodeName());
                Console.WriteLine("Child " + i + " capacity:" + nodes[nodeDropDown.SelectedIndex].children[i].getNodeCapacity());
            }
        }

        private void addChildNodeButton_Click(object sender, EventArgs e)
        {
            if (nodeDropDown.SelectedIndex-1 == (addChildNodeDropDown.SelectedIndex))
            {
                MessageBox.Show("Bir düğüm kendisinin çocuk düğümü olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                nodes[nodeDropDown.SelectedIndex].addChildNode(nodes[addChildNodeDropDown.SelectedIndex + 1]);
                nodes[addChildNodeDropDown.SelectedIndex + 1].neighbours.Add(nodes[nodeDropDown.SelectedIndex].getNodeName());
                Console.WriteLine("Eklenilen çocuk düğüme ulaşan düğümler: ");
                for (int i=0; i< nodes[addChildNodeDropDown.SelectedIndex + 1].neighbours.Count;i++)
                {
                    Console.Write(nodes[addChildNodeDropDown.SelectedIndex + 1].neighbours[i] + " ");
                }
                Console.WriteLine();
                string dlg = addChildNodeDropDown.Items[addChildNodeDropDown.SelectedIndex].ToString();
                Console.WriteLine("dlg: " + dlg);
                int childNode = nodes[nodeDropDown.SelectedIndex].children.FindIndex(item => item.getNodeName() == dlg);
                Console.WriteLine("IVJ: " + childNode);
                nodes[nodeDropDown.SelectedIndex].edges.Add(childNode, new List<int>() { 0, 0, 0 });
                for (int i=0; i<nodes[nodeDropDown.SelectedIndex].children.Count; i++)
                {
                    Console.WriteLine("ÇOCUKLAR: " + nodes[nodeDropDown.SelectedIndex].children[i].getNodeName());
                }
                Console.WriteLine("ÇOCUK SAYISI: " + nodes[nodeDropDown.SelectedIndex].children.Count);
                string mes = nodes[nodeDropDown.SelectedIndex].getNodeName() + " düğümüne " + nodes[addChildNodeDropDown.SelectedIndex + 1].getNodeName() + " düğümü çocuk düğüm olarak eklendi.";
                MessageBox.Show(mes, "Uyarı",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                printChildren();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //printChildren();
            //findPaths();
            findMaxFlow();
            drawGraph();
        }

        //Set capacity of the edge
        private void button5_Click(object sender, EventArgs e)
        {
            string cap = setEdgeCapacityTextBox.Text;
            bool check = cap.Any(x => char.IsLetter(x));
            if (check || cap.Length == 0) 
            {
                MessageBox.Show("Lütfen sayı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("Lütfen sayı giriniz!");
            }
            else
            {
                int mainNode = nodeDropDown.SelectedIndex;
                string str = addChildNodeDropDown.Items[addChildNodeDropDown.SelectedIndex].ToString();
                Console.WriteLine("str: " + str);
                int childNode = nodes[mainNode].children.FindIndex(item => item.getNodeName() == str);
                Console.WriteLine("child node: "+ childNode);
                printAllKeys();
                //List<int> keyList = new List<int>(nodes[mainNode].edges.Keys);
                //var t = nodes[mainNode].edges.Keys;
                //for (int i=0; i<keyList.Count;i++)
                //{
                //  Console.Write(keyList[i] + " ");
                //   Console.WriteLine();
                //   nodes[mainNode].edges[1][0] = 5;
                //    Console.WriteLine(nodes[mainNode].edges[1][0]);
                //}
                Console.WriteLine();
                nodes[mainNode].edges[childNode][1] = Int32.Parse(setEdgeCapacityTextBox.Text);
                Console.WriteLine("Edge capacity: " + nodes[mainNode].edges[childNode][1]);
                MessageBox.Show("Seçilen çocuk indexi: " + childNode, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }       
        }

        public void printAllKeys()
        {
            for (int i=0; i<nodes.Count ; i++)
            {
                List<int> k = new List<int>(nodes[i].edges.Keys);
                for (int j = 0; j < k.Count; j++)
                {
                    Console.Write(k[j] + " ");

                }
                Console.WriteLine();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            drawGraph();
        }
    }
}

/*
public void findPaths()
{
    // nodes[currentNodeIndex] == current node
    pathCounter = 0;
    currentNodeIndex = 0;
    path.Add(nodes[currentNodeIndex].getNodeName());
    while (path.Count > 0)
    {
        if (nodes[currentNodeIndex].getNodeType() != "finish")
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            for (int b = 0; b < path.Count; b++)
            {
                Console.Write(path[b].ToUpper() + " ");
            }
            Console.WriteLine("\n--------------------------------------------------------------------------------------------");

            Console.WriteLine("Current nodename:  " + nodes[currentNodeIndex].getNodeName() + " children number: " + nodes[currentNodeIndex].children.Count);
            Console.WriteLine("CURRENT NODE INDEX:  " + currentNodeIndex);
            for (int i = 0; i < nodes[currentNodeIndex].children.Count; i++)
            {
                //nodes[currentNodeIndex].visitedNodes.FindIndex(item => item.getNodeName == nodes[currentNodeIndex].children)
                Console.WriteLine("+++++ " + nodes[currentNodeIndex].getNodeName() + " " + nodes[currentNodeIndex].children[i].getNodeName() + " " + nodes[currentNodeIndex].visitedNodes.Contains(nodes[currentNodeIndex].children[i]));
                if (!nodes[currentNodeIndex].visitedNodes.Contains(nodes[currentNodeIndex].children[i]))
                {
                    nodes[currentNodeIndex].visitedNodes.Add(nodes[currentNodeIndex].children[i]);
                    path.Add(nodes[currentNodeIndex].children[i].getNodeName());
                    string str = nodes[currentNodeIndex].children[i].getNodeName();
                    Console.WriteLine();
                    currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == str);
                    Console.WriteLine("INDEX: " + currentNodeIndex);
                    //nodes[currentNodeIndex].visitedNodes.FindIndex(item => item.getNodeName == nodes[currentNodeIndex].children)
                    break;
                }
                else if (nodes[currentNodeIndex].visitedNodes.Count == nodes[currentNodeIndex].children.Count)
                {
                    nodes[currentNodeIndex].visitedNodes.Clear();
                    path.Remove(nodes[currentNodeIndex].getNodeName());
                    if ((path.Count - 1) >= 0)
                    {
                        currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1]);
                        //currentNodeIndex = path.Count - 1;

                    }
                    else
                    {
                        //Finish searching for paths
                        break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("CURRR " + nodes[currentNodeIndex].getNodeName());
            //path.Add(nodes[currentNodeIndex]);
            //List<Node> list = new List<Node>(path);
            //availablePaths.Add(pathCounter, list);
            pathCounter++;
            int x = path.Count - 1;
            Console.WriteLine("x: " + x);
            Console.WriteLine("PATHLENGTH: " + path.Count);
            Console.WriteLine("Path: ");
            for (int i = 0; i < path.Count; i++)
            {
                Console.Write(i + " " + path[i] + " ");
            }
            path.RemoveAt(x);
            Console.WriteLine();
            currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1]);
            Console.WriteLine("indeddddd: " + currentNodeIndex);
            Console.WriteLine("brrrrrrrr: " + nodes[currentNodeIndex].getNodeName());
            //currentNodeIndex = path.Count - 1;
        }
    }
    //Console.WriteLine("Current node: " + currentNode.getNodeName() + ", capacity:" + currentNode.getNodeCapacity() + ", current flow:" + currentNode.getNodeCurrentFlow());
    //path.Add();

    //PRINT FOUND PATHS
    for (int i = 0; i < availablePaths.Count; i++)
    {
        //Console.WriteLine("PATH "+ i +":");
        for (int j = 0; j < availablePaths[i].Count; j++)
        {
            Console.Write(availablePaths[i][j].getNodeName() + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine("YOLLAR BULUNDU!");
}-------------------------------------------------------*/

//else if (loopCounter > path.Count - 1)
//{
// delta = delta / 2;
// selectedChildIndexes.Clear();
// path.Clear();
// currentNodeIndex = 0;
// loopCounter = 0;
//}

//if (path[path.Count-1].Contains("-"))
//{
//this node is a reverse direction node 
//  int reverseNodeIndex = currentNodeIndex;
//  path.RemoveAt(path.Count - 1 );
// if (path.Count > 0 )
// {
//   currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1]);
//    nodes[currentNodeIndex].deadNeighbours.Add(nodes[reverseNodeIndex].getNodeName());
// }
// else
// {
//     
//   }
//}
//else
//{
// int deadChild = selectedChildIndexes[path.Count - 1];
// path.RemoveAt(path.Count - 1);
// if(path.Count > 0)
// {
//      currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1]);

//   }
//}
/*
    if (ind == -1)
                    {
                        Console.WriteLine("İND == -1");
                        Console.WriteLine("PATH COUNT: " + path.Count);
                        //int lastPathIndex = path.Count - 1;
                        //path.RemoveAt(path.Count - 1);
                        Console.Write("Path 2 : ");
                        for (int i = 0; i<path.Count; i++)
                        {
                            Console.Write(path[i] + " ");
                        }
                        if (selectedChildIndexes.Count == 0)
                        {
                            Console.WriteLine("DELTA/2");
                            delta = delta / 2;
                            selectedChildIndexes.Clear();
                            path.Clear();
                            resetDeadEnds();
currentNodeIndex = 0;
                            //loopCounter = 0;
                        }
                        else
                        {
                            string lastNodeName = path[path.Count - 1];
//int deadEndIndex = selectedChildIndexes[lastNodeIndex];
Console.WriteLine("selectedChildIndexes Count: " + selectedChildIndexes.Count);
                            selectedChildIndexes.RemoveAt(selectedChildIndexes.Count - 1);
                            path.RemoveAt(path.Count - 1);
                            string nodeName = path[path.Count - 1];
//currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1].getNodeName());
currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == nodeName);
                            int dead = nodes[currentNodeIndex].children.FindIndex(item => item.getNodeName() == lastNodeName);
nodes[currentNodeIndex].deadEnds.Add(dead);
                            
                        }
                    }
                    else
                    {
                        //loopCounter++;
                        //Console.WriteLine("loopCounter: " + loopCounter);
                        Console.WriteLine("PATH COUNT 2: " + path.Count);
                        Console.WriteLine("Şimdiki düğüm: " + nodes[currentNodeIndex].getNodeName());
                        Console.WriteLine("Gidilecek Düğüm: " + nodes[ind].getNodeName());
                        currentNodeIndex = ind;
                    }
*/
