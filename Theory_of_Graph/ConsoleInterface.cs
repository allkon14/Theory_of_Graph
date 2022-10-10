using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Theory_of_Graph.Graph;

namespace Theory_of_Graph
{

    public class ConsoleInterface
    {
        private const string Directivity = "DIRECTIVITY";
        private static readonly string[] DirArgs = { "or-oriented or undir-graph" };

        private const string Weigh = "WEIGHTEDNESS";
        private static readonly string[] WeiArgs = { "wei-weighted or unwei-unweighted" };


        private const string New_graph = "NEW_GRAPH";
        private const string File_graph = "FILE_GRAPH";

        private const string AddNode = "ADD_NODE";
        private static readonly string[] AddNodeArgs = { "Node Name" };

        private const string Print = "PRINT";

        private const string Print_File = "PRINT_FILE";
        private static readonly string[] PrintFileArgs = { "File Name" };



        private const string AddRib = "ADD_RIB";
        private static readonly string[] AddWeiRibArgs = { "First Node Name", "Second Node Name", "Heigth" };
        private static readonly string[] AddUnweiRibArgs = { "First Node Name", "Second Node Name" };



        private const string DeleteNode = "DELETE_NODE";
        private static readonly string[] DeleteNodeArgs = { "Node Name" };

        private const string DeleteRib = "DELETE_RIB";
        private static readonly string[] DeleteRibArgs = { "First Node Name", "Second Node Name" };

        private const string Isolated_nodes = "ISOLATED_NODES";

        private const string One_node_way = "ONE_NODE_WAY";
        private static readonly string[] One_wayArgs = { "First Node Name", "Second Node Name" };


        private const string Hint = "HINT";
        private const string Exit = "EXIT";
        private const string e = "E";


        private const string UnknownCommand = "UNKNOWN COMMAND";
        private const string WrongArgument = "Wrong argument(s)";

        private static Graph _graph;

        private bool wrongArg = false;

        public ConsoleInterface() { }
        public void Start()
        {
            StringBuilder sbb = new StringBuilder();
            sbb.Append(New_graph).Append('\n');
            sbb.Append(File_graph).Append('\n');
            sbb.Append(Hint).Append('\n');
            sbb.Append(Exit).Append('\n');
            Console.WriteLine(sbb);
            for (; ; )
            {
                try
                {
                    Console.Write(">>> ");
                    List<String> arguments = new List<String>(Console.ReadLine().Split(char.Parse(" ")));
                    string command = arguments[0].ToUpper();
                    arguments.RemoveAt(0);
                    switch (command)
                    {
                        case File_graph:
                            {
                                Console.WriteLine("Выберите файл (название и расширение): ");
                                Console.WriteLine("Ориентированный взвешенный (OriWeiGraph.txt): ");
                                Console.WriteLine("Ориентированный невзвешенный (OriNoWeiGraph.txt): ");
                                Console.WriteLine("Неориентированный взвешенный (NoOriWei.txt): ");
                                Console.WriteLine("Неориетированный невзвешенный (NoOriNoWei.txt): ");
                                Console.WriteLine("Другой файл: ");
                                Console.Write(">>> ");
                                string filePath = Console.ReadLine();
                                _graph = new Graph(@filePath);

                                Console.WriteLine();
                                _graph.Print();
                                Console.WriteLine();
                                Console.WriteLine(GetHint());
                                break;
                            }

                        case New_graph:

                            {
                                _graph = new Graph();
                                StringBuilder sb = new StringBuilder();
                                Console.WriteLine(sb.Append(Directivity).Append(": ").Append(String.Join(", ", DirArgs)).Append('\n'));
                                Console.Write(">>> ");
                                arguments = new List<String>(Console.ReadLine().Split(char.Parse(" ")));
                                goto case Directivity;
                            }

                        case Directivity:
                            if (arguments.Count != DirArgs.Length)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            {
                                if (wrongArg)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    Console.WriteLine(sb.Append(Directivity).Append(": ").Append(String.Join(", ", DirArgs)));
                                    Console.Write(">>> ");
                                    arguments = new List<String>(Console.ReadLine().Split(char.Parse(" ")));
                                }
                                string dir = arguments[0].ToUpper();
                                if (dir == "OR")
                                {
                                    _graph.Oriented = true;
                                    StringBuilder sb = new StringBuilder();
                                    Console.WriteLine(sb.Append(Weigh).Append(": ").Append(String.Join(", ", WeiArgs)));
                                    Console.Write(">>> ");
                                    arguments = new List<String>(Console.ReadLine().Split(char.Parse(" ")));

                                    wrongArg = false;
                                    goto case Weigh;
                                }
                                else
                                    if (dir == "UNDIR")
                                {
                                    _graph.Oriented = false;
                                    StringBuilder sb = new StringBuilder();
                                    Console.WriteLine(sb.Append(Weigh).Append(": ").Append(String.Join(", ", WeiArgs)));
                                    Console.Write(">>> ");
                                    arguments = new List<String>(Console.ReadLine().Split(char.Parse(" ")));

                                    wrongArg = false;
                                    goto case Weigh;
                                }
                                else
                                {
                                    Console.WriteLine("Неверный ввод");
                                    wrongArg = true;
                                    goto case Directivity;
                                }
                            }
                            break;

                        case Weigh:
                            if (arguments.Count != WeiArgs.Length)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else

                            {
                                if (wrongArg)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    Console.WriteLine(sb.Append(Weigh).Append(": ").Append(String.Join(", ", WeiArgs)).Append('\n'));
                                    Console.Write(">>> ");
                                    arguments = new List<String>(Console.ReadLine().Split(char.Parse(" ")));

                                }
                                string wei = arguments[0].ToUpper();
                                if (wei == "WEI")
                                {
                                    _graph.Weighed = true;
                                    if (_graph.Oriented)
                                    {
                                        Console.WriteLine("\nСоздан взвешенный ориентированный граф\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nСоздан взвешенный неориентированный граф\n");
                                    }
                                    wrongArg = false;
                                    Console.WriteLine(GetHint());
                                }
                                else
                                    if (wei == "UNWEI")
                                {
                                    _graph.Weighed = false;
                                    if (_graph.Oriented)
                                    {
                                        Console.WriteLine("\nСоздан невзвешенный ориентированный граф\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nСоздан невзвешенный неориентированный граф\n");
                                    }
                                    wrongArg = false;
                                    Console.WriteLine(GetHint());
                                }
                                else
                                {
                                    Console.WriteLine("Неверный ввод");
                                    wrongArg = true;
                                    goto case Weigh;
                                }
                            }
                            break;
                        case AddNode:
                            if (arguments.Count != AddNodeArgs.Length)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            {
                                _graph.AddNode(arguments[0]);
                                _graph.Print();

                            }
                            break;
                        case AddRib:
                            if (arguments.Count != AddWeiRibArgs.Length && _graph.Weighed)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            if (arguments.Count != AddUnweiRibArgs.Length && !_graph.Weighed)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            {
                                if (!_graph.Oriented && _graph.Weighed)
                                {
                                    _graph.AddWeiUnorRib(arguments[0], arguments[1], int.Parse(arguments[2]));
                                    _graph.Print();
                                }
                                if (!_graph.Oriented && !_graph.Weighed)
                                {
                                    _graph.AddUnweiUnorRib(arguments[0], arguments[1]);
                                    _graph.Print();
                                }
                                if (_graph.Oriented && !_graph.Weighed)
                                {
                                    _graph.AddUnweiOrRib(arguments[0], arguments[1]);
                                    _graph.Print();
                                }
                                if (_graph.Oriented && _graph.Weighed)
                                {
                                    _graph.AddWeiOrRib(arguments[0], arguments[1], int.Parse(arguments[2]));
                                    _graph.Print();
                                }
                            }
                            break;

                        case DeleteNode:
                            if (arguments.Count != DeleteNodeArgs.Length)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            {
                                if (_graph.DeleteNode(arguments[0]))
                                {
                                    Console.WriteLine("Вершина удалена\n");
                                    _graph.Print();
                                }
                                else
                                {
                                    Console.WriteLine("Вершина не удалена, проверьте данные\n");
                                }
                            }
                            break;
                        case DeleteRib:
                            if (arguments.Count != DeleteRibArgs.Length)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            {
                                if (_graph.DeleteRib(arguments[0], arguments[1]))
                                {
                                    Console.WriteLine("Ребро удалено\n");
                                    _graph.Print();
                                }
                                else
                                {
                                    Console.WriteLine("Ребро не удалено, проверьте данные\n");
                                }
                            }
                            break;

                        case Print:
                            _graph.Print();
                            break;

                        case Print_File:
                            if (arguments.Count != PrintFileArgs.Length)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            {
                                _graph.PrintFile(arguments[0]);
                            }
                            break;
                        case Isolated_nodes:
                            
                            _graph.Isolated_nodes();
                            break;

                        case One_node_way:
                            if (arguments.Count != One_wayArgs.Length)
                            {
                                Console.WriteLine(WrongArgument);
                            }
                            else
                            {
                                _graph.One_node_way(arguments[0], arguments[1]);
                            }
                            break;
                        case Hint:
                            Console.WriteLine(GetHint());
                            break;
                        case e:
                            return;
                        case Exit:
                            return;

                        default:
                            Console.WriteLine(UnknownCommand);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        private static String GetHint()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(New_graph).Append('\n');
            sb.Append(File_graph).Append('\n');
            sb.Append(AddNode).Append(": ").Append(String.Join(", ", AddNodeArgs)).Append('\n');
            sb.Append(Print).Append('\n');
            sb.Append(Print_File).Append(": ").Append(String.Join(", ", PrintFileArgs)).Append('\n');

            if (_graph.Weighed)
            {
                sb.Append(AddRib).Append(": ").Append(String.Join(", ", AddWeiRibArgs)).Append('\n');
            }
            else
            {
                sb.Append(AddRib).Append(": ").Append(String.Join(", ", AddUnweiRibArgs)).Append('\n');
            }
            sb.Append(DeleteNode).Append(": ").Append(String.Join(", ", DeleteNodeArgs)).Append('\n');
            sb.Append(DeleteRib).Append(": ").Append(String.Join(", ", DeleteRibArgs)).Append('\n').Append('\n');

            sb.Append(Isolated_nodes).Append('\n');
            sb.Append(One_node_way).Append(": ").Append(String.Join(", ", One_wayArgs)).Append('\n').Append('\n');

            sb.Append(Hint).Append('\n');
            sb.Append(Exit).Append('\n');

            return sb.ToString();
        }
    }
}