using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdamLib;
using CommandLine;
using RJCP.IO.Ports;

namespace AdamCmd
{
    class Program
    {
        static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<TypeOptions, ReadAIOptions, ReadDOOptions, WriteDOOptions, InteractiveOptions, DetectPortsOptions>(args)
                .MapResult(
                    (TypeOptions opts) => RunType(opts),
                    (ReadAIOptions opts) => RunReadAI(opts),
                    (ReadDOOptions opts) => RunReadDO(opts),
                    (WriteDOOptions opts) => RunWriteDO(opts),
                    (ReadAOOptions opts) => RunReadAO(opts),
                    (WriteAOOptions opts) => RunWriteAO(opts),
                    (InteractiveOptions opts) => RunInteractive(opts),
                    (DetectPortsOptions opts) => RunDetectPorts(opts),
                    errs => 1
                );
        }

        static bool OpenPort(AdvantechClient client)
        {
            try
            {
                client.Open();
            }
            catch (Exception exp) when (
                exp is UnauthorizedAccessException ||
                exp is InvalidOperationException ||
                exp is IOException)
            {
                Console.Error.WriteLine("Unable to open port.");
                return false;
            }
            return true;
        }

        static int HandleException(Exception exp)
        {
            if (exp is TimeoutException)
            {
                Console.Error.WriteLine("Timeout.");
                return 2;
            }
            else if (exp is FormatException)
            {
                Console.Error.WriteLine("Frame Error.");
                return 3;
            }
            else
            {
                throw exp;
            }
        }

        static string TypeDescriptionLookup(string type)
        {
            switch (type)
            {
                case "4017":
                    return "8ch Analog Input Module";
                case "4017P":
                    return "8ch Analog Input Module w/Modbus";
                case "4024":
                    return "4ch Analog Output Module";
                case "4021":
                    return "1ch Analog Output Module";
                case "4060":
                    return "4ch Relay Module";
                case "4068":
                    return "8ch Relay Module";
                default:
                    return null;
            }
        }

        static int RunType(TypeOptions opts)
        {
            using (var client = new AdvantechClient())
            {
                client.PortName = opts.PortName;
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                if (!OpenPort(client))
                    return 1;

                string type;
                try
                {
                    type = client.ReadModuleType(opts.UnitAddress);
                }
                catch (Exception exp)
                {
                    return HandleException(exp);
                }
                string desc = TypeDescriptionLookup(type);
                
                Console.Out.Write(type);
                if (!string.IsNullOrEmpty(desc))
                    Console.Out.WriteLine($" ({desc})");

                return 0;
            }
        }

        static int RunReadAI(ReadAIOptions opts)
        {
            using (var client = new AdvantechClient())
            {
                client.PortName = opts.PortName;
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                if (!OpenPort(client))
                    return 1;

                double value;
                try
                {
                    value = client.ReadAI(opts.UnitAddress, opts.Channel);
                }
                catch (Exception exp)
                {
                    return HandleException(exp);
                }
                
                Console.Out.WriteLine(value);
                return 0;
            }
        }

        static int RunReadDO(ReadDOOptions opts)
        {
            using (var client = new AdvantechClient())
            {
                client.PortName = opts.PortName;
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                if (!OpenPort(client))
                    return 1;

                bool value;
                try
                {
                    value = client.ReadDO(opts.UnitAddress, opts.Channel);
                }
                catch (Exception exp)
                {
                    return HandleException(exp);
                }

                Console.Out.WriteLine(value ? "1" : "0");
                return 0;
            }
        }

        static int RunWriteDO(WriteDOOptions opts)
        {
            using (var client = new AdvantechClient())
            {
                client.PortName = opts.PortName;
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                if (!OpenPort(client))
                    return 1;
                
                try
                {
                    client.WriteDO(opts.UnitAddress, opts.Channel, opts.BoolValue);
                }
                catch (Exception exp)
                {
                    return HandleException(exp);
                }

                return 0;
            }
        }

        static int RunReadAO(ReadAOOptions opts)
        {
            using (var client = new AdvantechClient())
            {
                client.PortName = opts.PortName;
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                if (!OpenPort(client))
                    return 1;

                double value;
                try
                {
                    value = client.ReadAO(opts.UnitAddress, opts.Channel);
                }
                catch (Exception exp)
                {
                    return HandleException(exp);
                }

                Console.Out.WriteLine(value);
                return 0;
            }
        }

        static int RunWriteAO(WriteAOOptions opts)
        {
            using (var client = new AdvantechClient())
            {
                client.PortName = opts.PortName;
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                if (!OpenPort(client))
                    return 1;
                
                try
                {
                    client.WriteAO(opts.UnitAddress, opts.Channel, opts.Value);
                }
                catch (Exception exp)
                {
                    return HandleException(exp);
                }

                return 0;
            }
        }

        static int RunInteractive(InteractiveOptions opts)
        {
            using (var client = new AdvantechClient())
            {
                client.PortName = opts.PortName;
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                if (!OpenPort(client))
                    return 1;

                var parser = new InteractiveCommandParser(client);
                bool close = false;
                do
                {
                    Console.Error.Write("] ");
                    var line = Console.In.ReadLine();
                    try
                    {
                        close = parser.ProcessCommand(line);
                    }
                    catch (Exception exp)
                    {
                        HandleException(exp);
                    }
                    Console.Out.WriteLine();
                }
                while (!close);
            }
            return 0;
        }

        static int RunDetectPorts(DetectPortsOptions opts)
        {
            var validPorts = new List<PortDescription>();
            using (var client = new AdvantechClient())
            {
                client.BaudRate = opts.BaudRate;
                client.DataBits = opts.DataBits;
                client.Parity = opts.Parity;

                foreach (var portDesc in SerialPortStream.GetPortDescriptions())
                {
                    client.PortName = portDesc.Port;
                    if (opts.Verbose)
                    {
                        Console.Error.Write($"Trying {portDesc.Port}");
                        if (!string.IsNullOrEmpty(portDesc.Description))
                            Console.Out.Write($" ({portDesc.Description})");
                        Console.Out.Write("...");
                    }
                    try
                    {
                        client.Open();
                    }
                    catch (Exception exp) when (exp is UnauthorizedAccessException || exp is InvalidOperationException)
                    {
                        if (opts.Verbose)
                            Console.Error.WriteLine("already in use.");
                        continue;
                    }
                    catch (IOException)
                    {
                        if (opts.Verbose)
                            Console.Error.WriteLine("cannot open.");
                        continue;
                    }

                    try
                    {
                        client.ReadModuleType(opts.UnitAddress);
                    }
                    catch (TimeoutException)
                    {
                        if (opts.Verbose)
                            Console.Error.WriteLine("timeout.");
                        client.Close();
                        continue;
                    }
                    catch (FormatException)
                    {
                        if (opts.Verbose)
                            Console.Error.WriteLine("frame error.");
                        client.Close();
                        continue;
                    }
                    if (opts.Verbose)
                        Console.Error.WriteLine("valid.");
                    validPorts.Add(portDesc);
                    client.Close();
                }
            }
            if (opts.Verbose)
                Console.Error.WriteLine();
            foreach (var portDesc in validPorts)
            {
                Console.Out.Write($"{portDesc.Port}");
                if (!string.IsNullOrEmpty(portDesc.Description))
                    Console.Out.Write($" ({portDesc.Description})");
                Console.Out.WriteLine();
            }

            return 0;
        }
    }
}
