using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Theory_of_Graph
{
    public class Graph
    {
        Dictionary<string, Dictionary<string, int>> _graph;
        /// <summary>Скрытая булевская переменная, показывающая, является ли граф ориентированным</summary>
        /// <remarks>По умолчанию true</remarks>
        private bool _oriented = true;

        /// <summary>Скрытая булевская переменная, показывающая, является ли граф взвешенным</summary>
        /// <remarks>По умолчанию true</remarks>
        private bool _weighed = true;

        /// <summary>Свойство ориентированности</summary>
        public bool Oriented
        {
            get
            {
                return _oriented;
            }
            set
            {
                _oriented = value;
            }
        }
        /// <summary>Свойство взвешенности</summary>
        public bool Weighed
        {
            get
            {
                return _weighed;
            }
            set
            {
                _weighed = value;
            }
        }
        /// <summary>Свойство, позволяющее получить весь граф</summary>
        public Dictionary<string, Dictionary<string, int>> DictGraph
        {
            get
            {
                return _graph;
            }

        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Graph()
        {
            _graph = new Dictionary<string, Dictionary<string, int>>();
        }
        /// <summary>
        /// Конструктор копирования графа
        /// </summary>
        /// <param name="graph">копируемый граф</param> 
        public Graph(Graph graph)
        {
            _oriented = graph.Oriented;
            _weighed = graph.Weighed;
            _graph = new Dictionary<string, Dictionary<string, int>>();
            foreach (var item in graph.DictGraph)
            {
                string nameNode = "";
                nameNode += item.Key;
                Dictionary<string, int> vertex = new Dictionary<string, int>();
                foreach (var items in item.Value)
                {
                    vertex.Add(items.Key, items.Value);
                }
                _graph.Add(nameNode, vertex);
            }
        }
        /// <summary>
        /// Конструктор чтения из файла
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        public Graph(string filePath)
        {
            _graph = new Dictionary<string, Dictionary<string, int>>();
            using (StreamReader file = new StreamReader(@filePath))
            {
                string[] orAndWei = file.ReadLine().Split();

                if (orAndWei[0] == "1")
                    _oriented = true;
                else
                    _oriented = false;

                if (orAndWei[1] == "1")
                    _weighed = true;
                else
                    _weighed = false;

                string tempStr;
                string[] nodeStr;
                string[] nodesStr;
                int nodesStr_Len;

                while ((tempStr = file.ReadLine()) != null)
                {
                    if (tempStr == "")
                        break;
                    //nodesStr = tempStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    nodesStr = tempStr.Split();
                    if (nodesStr[nodesStr.Length - 1] == "")
                        nodesStr_Len = nodesStr.Length - 1;
                    else
                        nodesStr_Len = nodesStr.Length;

                    Dictionary<string, int> tempNextNodes = new Dictionary<string, int>();
                    for (int i = 1; i < nodesStr_Len; i++)
                    {
                        nodeStr = nodesStr[i].Split(':');
                        tempNextNodes.Add(nodeStr[0], int.Parse(nodeStr[1]));
                    }
                    _graph.Add(nodesStr[0], tempNextNodes);
                }
            }
        }

        /// <summary>Метод, добавляющий вершину</summary>
        /// <param name="firstNode">Название вершины</param>
        /// <remarks>Если вершина уже есть в графе, выводится предупреждение на консоль</remarks>
        public void AddNode(string firstNode)
        {
            if (_graph.ContainsKey(firstNode))
            {
                Console.WriteLine("Такая вершина уже содержится в графе");
            }
            else { _graph.Add(firstNode, new Dictionary<string, int>()); }

        }
        /// <summary>Метод, добавляющий дугу в ориентированный взвешенный граф</summary>
        /// <param name="firstNode">Название первой вершины (откуда)</param>
        /// <param name="secondNode">Название второй вершины (куда)</param>
        /// <param name="height">Вес дуги</param>
        public void AddWeiOrRib(string firstNode, string secondNode, int height)
        {
            if (_graph[firstNode].ContainsKey(secondNode))
            {
                Console.WriteLine("Такое ребро уже содержится в графе");
            }
            else
            {
                _graph[firstNode].Add(secondNode, height);
            }
        }


        /// <summary>Метод, добавляющий дугу в ориентированный невзвешенный граф</summary>
        /// <param name="firstNode">Название первой вершины (откуда)</param>
        /// <param name="secondNode">Название второй вершины (куда)</param>
        public void AddUnweiOrRib(string firstNode, string secondNode)
        {
            if (_graph[firstNode].ContainsKey(secondNode))
            {
                Console.WriteLine("Такое ребро уже содержится в графе");
            }
            else
            {
                _graph[firstNode].Add(secondNode, 0);
            }
        }

        /// <summary>Метод, добавляющий ребро в неориентированный взвешенный граф</summary>
        /// <param name="firstNode">Название первой вершины (откуда)</param>
        /// <param name="secondNode">Название второй вершины (куда)</param>
        /// <param name="height">Вес дуги</param>
        public void AddWeiUnorRib(string firstNode, string secondNode, int height)
        {
            if (_graph[firstNode].ContainsKey(secondNode))
            {
                Console.WriteLine("Такое ребро уже содержится в графе");
            }
            else
            {
                _graph[firstNode].Add(secondNode, height);
                if (firstNode != secondNode)
                    _graph[secondNode].Add(firstNode, height);
            }
        }


        /// <summary>Метод, добавляющий ребро в неориентированный невзвешенный граф</summary>
        /// <param name="firstNode">Название первой вершины (откуда)</param>
        /// <param name="secondNode">Название второй вершины (куда)</param>
        public void AddUnweiUnorRib(string firstNode, string secondNode)
        {
            if (_graph[firstNode].ContainsKey(secondNode))
            {
                Console.WriteLine("Такое ребро уже содержится в графе");
            }
            else
            {
                _graph[firstNode].Add(secondNode, 0);
                if (firstNode != secondNode)
                    _graph[secondNode].Add(firstNode, 0);
            }
        }

        /// <summary>Метод, удаляющий вершину</summary>
        /// <param name="nameNode">Название удаляемой вершины</param>
        public bool DeleteNode(string nameNode)
        {
            bool deleted = _graph.Remove(nameNode);
            foreach (var keyValue in _graph)
            {
                _graph[keyValue.Key].Remove(nameNode);
            }
            if (deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>Метод, удаляющий ребро/дугу в графе</summary>
        /// <param name="firstNode">Название первой вершины (откуда)</param>
        /// <param name="secondNode">Название второй вершины (куда)</param>
        public bool DeleteRib(string firstNode, string secondNode)
        {
            bool deletedEdge = false;
            if (String.IsNullOrEmpty(firstNode) || String.IsNullOrEmpty(secondNode))
            {
                return false;
            }
            if (_oriented)
            {
                if (_graph[firstNode].ContainsKey(secondNode))
                {
                    deletedEdge = true;
                    _graph[firstNode].Remove(secondNode);
                }
            }
            else
            {
                if (_graph[firstNode].ContainsKey(secondNode) && _graph[secondNode].ContainsKey(firstNode) && (firstNode != secondNode))
                {
                    deletedEdge = true;
                    _graph[firstNode].Remove(secondNode);
                    _graph[secondNode].Remove(firstNode);
                }
                else
                    if (_graph[firstNode].ContainsKey(secondNode) && _graph[secondNode].ContainsKey(firstNode) && (firstNode == secondNode))
                {
                    deletedEdge = true;
                    _graph[firstNode].Remove(secondNode);
                }

            }
            return deletedEdge;
        }

        /// <summary>Метод, который выводит граф на консоль</summary>
        public void Print()
        {
            foreach (KeyValuePair<string, Dictionary<string, int>> keyValue in _graph)
            {
                Console.Write("{0}: ", keyValue.Key);
                foreach (var keyValue2 in keyValue.Value)
                {
                    if (Weighed)
                    {
                        Console.Write(keyValue2.Key + ":" + keyValue2.Value + " ");
                    }
                    else
                    {
                        Console.Write(keyValue2.Key + " ");
                    }
                }
                Console.WriteLine();
            }

            /* foreach (KeyValuePair<string, Dictionary<string, int>> pair in _graph)
            {
                Console.Write("{0}: ", pair.Key);
                foreach (var item in pair.Value)
                {
                    Console.Write("{0} ", item);

                }
                Console.WriteLine();
            }*/
        }

        /// <summary>Метод, который печатает граф в файл</summary>
        public void PrintFile(string fileName)
        {
            using (StreamWriter fileout = new StreamWriter(@fileName))
            {
                StringBuilder s = new StringBuilder();
                if (_oriented) s.Append("1").Append(" ");
                else s.Append("0").Append(" ");
                if (_weighed) s.Append("1");
                else s.Append("0");
                s.Append('\n');
                foreach (KeyValuePair<string, Dictionary<string, int>> keyValue in _graph)
                {
                    s.Append(keyValue.Key).Append(" ");
                    foreach (var keyValue2 in keyValue.Value)
                    {
                        s.Append(keyValue2.Key + ":" + keyValue2.Value + " ");
                    }
                    s.Append('\n');
                }
                fileout.WriteLine(s);
                DirectoryInfo dir = new DirectoryInfo(@fileName);
                Console.WriteLine("Файл {0} добавлен по директории: {1}", @fileName, dir.FullName);
            }

        }

        ///<summary>Метод, выводящий все изолированные вершины</summary>
        ///
        public void Isolated_nodes()
        {
            string[] nodes = GetNodes();
            
            bool flag = false;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (GetCountInDegreeNode(nodes[i]) == 0 && GetCountOutDegree(nodes[i]) == 0)
                {
                    Console.Write(nodes[i] + " ");
                    flag = true;
                }
            }
            if (flag == false)
            {
                Console.Write("Таких вершин нет");
            }
            Console.WriteLine();

            //foreach (KeyValuePair<string, Dictionary<string, int>> pair in _graph)
            //{
            //    if (pair.Value.Count == 0)
            //        Console.Write("{0} ", pair.Key);
            //}
            //Console.WriteLine();
        }
        public void One_node_way(string firstNode, string secondNode)
        {

            foreach (var item in _graph[firstNode])
            {
                if (_graph[item.Key].ContainsKey(secondNode))
                {
                    Console.WriteLine(item.Key);
                    //break;
                }
            }
        }
        //все вершины графа
        public string[] GetNodes()
        {
            string[] node = new string[_graph.Count];
            int i = 0;
            foreach (var item in _graph)
            {
                node[i] = item.Key;
                i++;
            }
            return node;
        }

        //подсчет степени выхода
        public int GetCountOutDegree(string node)
        {
            return _graph[node].Count;
        }

        //подсчет степени входа
        public int GetCountInDegreeNode(string node)
        {
            int count = 0;
            foreach (var keyValue in _graph)
            {
                foreach (var keyValue2 in keyValue.Value)
                {
                    if (keyValue2.Key == node)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

    }
}
