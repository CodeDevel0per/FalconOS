﻿using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using Cosmos;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;
using System.Drawing;

namespace FalconOS
{
    public class Kernel : Sys.Kernel
    {
        public static Shell shell = new Shell(data.osname, data.ver);

        public static string cUser;
        public static bool asRoot = false;
        public static string lastcmd = " ";
        public static bool gui = false;
        public static Canvas canvas;

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Welcome to ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("FalconOS");
            Console.ForegroundColor = ConsoleColor.White;
            log.sPrint("!");
            initsys init = new initsys();
        }

        protected override void Run()
        {
            if (!gui)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(cUser + "@falcon:" + data.currentDir + " # ");
                var input = "";
                var key = new ConsoleKeyInfo();
                while (!(key.Key == ConsoleKey.Enter))
                {
                    Console.SetCursorPosition((cUser + "@falcon:" + data.currentDir + " # " + input).Length, Console.CursorTop);
                    if (Console.CursorLeft >= Console.WindowWidth - 1)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(cUser + "@falcon:" + data.currentDir + " # " + lastcmd);
                        input = lastcmd;
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        if (input.Length > 0)
                        {
                            input = input.Remove(input.Length - 1, 1);
                        }
                        Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input + " ");
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                    }
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        input += " ";
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if (Console.CursorLeft > (cUser + "@falcon:" + data.currentDir + " # ").Length)
                        {
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        if (Console.CursorLeft < (cUser + "@falcon:" + data.currentDir + " # " + input).Length)
                        {
                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        }
                    }
                    else if (key.Key == ConsoleKey.Tab)
                    {
                        if (input.StartsWith("cle"))
                        {
                            input = "clear";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("falc"))
                        {
                            input = "falcp";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("ex"))
                        {
                            input = "exit";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("sysc"))
                        {
                            input = "sysctl ";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("sysctl sh"))
                        {
                            input = "sysctl shutdown";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("sysctl re"))
                        {
                            input = "syctl reboot";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("fet"))
                        {
                            input = "fetch ";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("fetch di"))
                        {
                            input = "fetch disks";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("lo"))
                        {
                            input = "logout";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("sle"))
                        {
                            input = "sleep";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("ec"))
                        {
                            input = "echo \"";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("cle"))
                        {
                            input = "clear";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("fas"))
                        {
                            input = "fash ";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("scr"))
                        {
                            input = "screenfetch";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("feo"))
                        {
                            input = "feofetch";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else if (input.StartsWith("fals"))
                        {
                            input = "falsay ";
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input);
                        } else
                        {
                            Console.Beep();
                        }
;                   }
                    else if (key.Modifiers == ConsoleModifiers.Control)
                    {
                        if (key.Key == ConsoleKey.C)
                        {
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # " + input + "^C\n");
                            input = "";
                            Console.Write(cUser + "@falcon:" + data.currentDir + " # ");
                        }
                    }
                    else
                    {
                        input += key.KeyChar;
                    }
                }
                Console.WriteLine();
                if (input.Length > 0)
                {
                    input = input.Remove(input.Length - 1, 1);
                }
                if (input.Contains("&&"))
                {
                    var inp = input.Split("&&");
                    foreach (var cmd in inp)
                    {
                        shell.exec(cmd, asRoot);
                    }
                }
                else
                {
                    shell.exec(input, asRoot);
                }
                lastcmd = input;
            } else
            {
                canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(640,480, ColorDepth.ColorDepth32));

                canvas.Clear(Color.Blue);

                canvas.DrawFilledEllipse(Color.White, (int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 3, 3);
                canvas.Display();
            }
        }
    }
}
