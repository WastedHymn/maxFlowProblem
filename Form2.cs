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
        List<string> nameofnodes = new List<string>();
        public Dictionary<string, List<int>> bottlenecks = new Dictionary<string, List<int>>();
        public Dictionary<string, int> maxFlowEdges = new Dictionary<string, int>();
        public List<List<string>> pathList = new List<List<string>>();
        public List<int> bottleneckValues = new List<int>();
        public int currentNodeIndex;
        public int delta;
        public int totalMaxFlow;
        public string numberString;
        public Form2()
        {
            InitializeComponent();
            setupDropDown();

        }

        public int findDelta()
        {
            int totalMaxFlow;
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
                    if ((path.Contains(nodes[neighbourIndex].getNodeName()) == false) && (path.Contains(("-"+nodes[neighbourIndex].getNodeName())) == false) && nodes[neighbourIndex].edges[neighbourChildIndex][0] > 0 && (nodes[currentNodeIndex].deadNeighbours.Contains(nodes[neighbourIndex].getNodeName()) == false))
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
                    string tempString = "-" + selectedNeighbourIndex.ToString();
                    selectedChildIndexes.Add(-1);
                    return tempString;       
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
            int bottleneck = 1000000000;
            Console.WriteLine("BOTTLENECK START VALUE: " + bottleneck);
            List<string> templist = new List<string>();
            string bottlenecknodes = "";
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
                        bottlenecknodes = path[i] + "," + path[i + 1];
                        
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
                        bottlenecknodes = path[i] + "," + path[i + 1];
                    }
                }
            }
            
            if (!bottlenecks.ContainsKey(bottlenecknodes))
            {
                List<int> tempList = new List<int>();
                tempList.Add(1);
                tempList.Add(bottleneck);
                bottlenecks.Add(bottlenecknodes, tempList);
                Console.WriteLine("Bottleneck Node: " + bottlenecknodes);
                Console.WriteLine("Bottleneck Node Count" + bottlenecks[bottlenecknodes]);
            }
            else
            {
                Console.WriteLine("Bottleneck Node: " + bottlenecknodes);
                Console.WriteLine("Old Bottleneck Node Count" + bottlenecks[bottlenecknodes]);
                int tempval = bottlenecks[bottlenecknodes][0]++;
                bottlenecks[bottlenecknodes][0] = tempval;
                bottlenecks[bottlenecknodes][1] += bottleneck;
                Console.WriteLine("New Node Count: " + bottlenecks[bottlenecknodes][0] + " Bottleneck value: " + bottlenecks[bottlenecknodes][1]);
            }
            bottleneckValues.Add(bottleneck);
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

        public void minCutFunc()
        {
            List<string> stack = new List<string>();
            string startNode = nodes.Find(item => item.getNodeType() == "start").getNodeName();
            Console.WriteLine("START NODE: "+ startNode);
            int tempMaxFlow = totalMaxFlow;
            stack.Add(startNode);
            while (tempMaxFlow > 0)
            {
                string currentNodeName = stack[0];
                int index = nodes.FindIndex(item => item.getNodeName() == currentNodeName);
                for (int i = 0; i < nodes[index].edges.Count; i++)
                {
                    if (nodes[index].edges[i][0] > 0)
                    {
                        stack.Add(nodes[index].children[i].getNodeName());
                        if (nodes[index].edges[i][0] == nodes[index].edges[i][1])
                        {
                            tempMaxFlow -= nodes[index].edges[i][0];
                            string tempstring = nodes[index].getNodeName() + nodes[index].children[i].getNodeName();
                            nameofnodes.Add(tempstring);
                            if (stack.Contains(nodes[index].children[i].getNodeName()))
                            {
                                stack.Remove(nodes[index].children[i].getNodeName());
                            }
                        }
                    }
                }
                stack.RemoveAt(0);
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
                    //Console.WriteLine("strrr: " + strrr);
                    if (path.Contains(nodes[currentNodeIndex].getNodeName()) == false && path.Contains(strrr)==false )
                    { 
                        ///Adding current node to the path.
                        path.Add(nodes[currentNodeIndex].getNodeName());
                    }
                    string ind = findMaxCapacityEdge();
                    Console.WriteLine("node != finish && İND: " + ind);
                   
                    
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
                            currentNodeIndex = nodes.FindIndex(item => item.getNodeName() == path[path.Count - 1].Replace("-",""));
                            
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
                                currentNodeIndex = numb;
                                Console.WriteLine("Selected reverse direction. currentNodeIndex: " + currentNodeIndex);
                            }
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
                    Console.Write("Final Path: ");
                    List<string> tempPathList = new List<string>(path);
                    pathList.Add(tempPathList);
                    //Console.WriteLine("PATHLIST COUNT: " + pathList.Count);
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
            showAugmentingPaths();
            minCutFunc();
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

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
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
                        node.setNodeName((i-1).ToString());
                        nodeDropDown.Items.Add(i-1);
                        addChildNodeDropDown.Items.Add(i-1);
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
                       
           numberString = nodeNumberInput.Text;
            orderedPathInfo.Text = "Max. akışı bulunuz.";
            childrenInfos.Text = "Düğümleri oluşturunuz";
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
                        string tempStringg = nodes[i].getNodeName() + nodes[i].children[j].getNodeName();
                        if (nameofnodes.Contains(tempStringg))
                        {
                            newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                        }
                        else
                        {
                            newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        newEdge.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    }
                  
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
                if (nodes[nodeDropDown.SelectedIndex].edges.ContainsKey(childNode))
                {
                    MessageBox.Show("Seçili olan çocuk düğüm zaten ana düğüme eklenmiş!","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    nodes[nodeDropDown.SelectedIndex].edges.Add(childNode, new List<int>() { 0, 0, 0 });

                    for (int i = 0; i < nodes[nodeDropDown.SelectedIndex].children.Count; i++)
                    {
                        Console.WriteLine("ÇOCUKLAR: " + nodes[nodeDropDown.SelectedIndex].children[i].getNodeName());
                    }
                    Console.WriteLine("ÇOCUK SAYISI: " + nodes[nodeDropDown.SelectedIndex].children.Count);
                    string mes = nodes[nodeDropDown.SelectedIndex].getNodeName() + " düğümüne " + nodes[addChildNodeDropDown.SelectedIndex + 1].getNodeName() + " düğümü çocuk düğüm olarak eklendi.";
                    MessageBox.Show(mes, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    printChildren();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
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
             
                Console.WriteLine();
                if (nodes[mainNode].edges.ContainsKey(childNode))
                {
                    nodes[mainNode].edges[childNode][1] = Int32.Parse(setEdgeCapacityTextBox.Text);
                    Console.WriteLine("Edge capacity: " + nodes[mainNode].edges[childNode][1]);
                    MessageBox.Show("Seçilen çocuk indexi: " + childNode, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    guiChildrenInfos();
                }
                else {
                    MessageBox.Show("Seçtiğiniz ana düğümde belirtilen çocuk düğüm bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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

        public void showAugmentingPaths()
        {
            orderedPathInfo.Text = "";
            for (int i=0; i<pathList.Count;i++)
            {
                
                string convertedPath = string.Join(" => ", pathList[i]);
                Console.WriteLine("CONVERTED PATH: " + convertedPath);
                orderedPathInfo.Text += convertedPath + " Bottleneck:" + bottleneckValues[i] +"\n";
            }
            orderedPathInfo.Text += "Max. Akış: " + totalMaxFlow + "\n";
        }

        public void guiChildrenInfos()
        {
            int num = 0;
            childrenInfos.Text = "\n";
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes[i].children.Count; j++)
                {
                    childrenInfos.Text += nodes[i].getNodeName() + "--->" + nodes[i].children[j].getNodeName() + " Kapasite: "+ nodes[i].edges[j][1]+"||";
                    num++;
                    if (num % 3 == 0)
                    {
                        childrenInfos.Text += "\n";
                    }
                }
               
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            drawGraph();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
