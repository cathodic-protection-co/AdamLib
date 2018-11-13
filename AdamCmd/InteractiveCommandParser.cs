using System;
using System.Collections.Generic;
using System.Text;
using AdamLib;

namespace AdamCmd
{
    class InteractiveCommandParser
    {
        private AdvantechClient _client { get; set; }

        public InteractiveCommandParser(AdvantechClient client)
        {
            _client = client;
        }

        private void ProcessType(string[] parts)
        {
            if (parts.Length != 2)
            {
                ShowHelp("Invalid number of parameters for 'type' command.");
                return;
            }
            if (!byte.TryParse(parts[1], out byte unitAdr))
            {
                ShowHelp("Invalid parameter [unit_address]. Must be a byte.");
                return;
            }
            string type = _client.ReadModuleType(unitAdr);
            Console.Out.WriteLine(type);
        }

        private void ProcessReadAI(string[] parts)
        {
            if (parts.Length != 3)
            {
                ShowHelp("Invalid number of parameters for 'read-ai' command.");
                return;
            }
            if (!byte.TryParse(parts[1], out byte unitAdr))
            {
                ShowHelp("Invalid parameter [unit_address]. Must be a byte.");
                return;
            }
            if (!int.TryParse(parts[2], out int channel) || (channel < 0 || channel > 7))
            {
                ShowHelp("Invalid parameter [channel]. Must be integer between 0 and 7.");
                return;
            }
            double value = _client.ReadAI(unitAdr, channel);
            Console.Out.WriteLine(value);
        }

        private void ProcessReadAO(string[] parts)
        {
            if (parts.Length != 3)
            {
                ShowHelp("Invalid number of parameters for 'read-ao' command.");
                return;
            }
            if (!byte.TryParse(parts[1], out byte unitAdr))
            {
                ShowHelp("Invalid parameter [unit_address]. Must be a byte.");
                return;
            }
            if (!int.TryParse(parts[2], out int channel) || (channel < 0 || channel > 3))
            {
                ShowHelp("Invalid parameter [channel]. Must be integer between 0 and 3.");
                return;
            }
            double value = _client.ReadAO(unitAdr, channel);
            Console.Out.WriteLine(value);
        }

        private void ProcessReadDO(string[] parts)
        {
            if (parts.Length != 3)
            {
                ShowHelp("Invalid number of parameters for 'read-do' command.");
                return;
            }
            if (!byte.TryParse(parts[1], out byte unitAdr))
            {
                ShowHelp("Invalid parameter [unit_address]. Must be a byte.");
                return;
            }
            if (!int.TryParse(parts[2], out int channel) || (channel < 0 || channel > 8))
            {
                ShowHelp("Invalid parameter [channel]. Must be integer between 0 and 7.");
                return;
            }
            bool value = _client.ReadDO(unitAdr, channel);
            Console.Out.WriteLine(value ? "1" : "0");
        }

        private void ProcessWriteAO(string[] parts)
        {
            if (parts.Length != 4)
            {
                ShowHelp("Invalid number of parameters for 'write-ao' command.");
                return;
            }
            if (!byte.TryParse(parts[1], out byte unitAdr))
            {
                ShowHelp("Invalid parameter [unit_address]. Must be a byte.");
                return;
            }
            if (!int.TryParse(parts[2], out int channel) || (channel < 0 || channel > 3))
            {
                ShowHelp("Invalid parameter [channel]. Must be integer between 0 and 3.");
                return;
            }
            if (!double.TryParse(parts[3], out double value))
            {
                ShowHelp("Invalid parameter [value]. Must be integer between 0 and 3.");
                return;
            }
            _client.WriteAO(unitAdr, channel, value);
        }

        private void ProcessWriteDO(string[] parts)
        {
            if (parts.Length != 4)
            {
                ShowHelp("Invalid number of parameters for 'write-do' command.");
                return;
            }
            if (!byte.TryParse(parts[1], out byte unitAdr))
            {
                ShowHelp("Invalid parameter [unit_address]. Must be a byte.");
                return;
            }
            if (!int.TryParse(parts[2], out int channel) || (channel < 0 || channel > 7))
            {
                ShowHelp("Invalid parameter [channel]. Must be integer between 0 and 7.");
                return;
            }
            if (!int.TryParse(parts[3], out int value) || (value != 0 && value != 1))
            {
                ShowHelp("Invalid parameter [value]. Must be 0 or 1.");
                return;
            }
            _client.WriteDO(unitAdr, channel, value != 0);
        }

        private void ProcessPortInfo()
        {
            Console.Out.WriteLine($"PortName:   {_client.PortName}");
            Console.Out.WriteLine($"BaudRate:   {_client.BaudRate}");
            Console.Out.WriteLine($"DataBits:   {_client.DataBits}");
            Console.Out.WriteLine($"Parity:     {_client.Parity}");
            Console.Out.WriteLine($"StopBits:   {_client.StopBits}");
        }

        private void ShowHelp(string error = null)
        {
            if (error != null)
            {
                Console.Error.WriteLine(error);
                Console.Error.WriteLine();
            }
            Console.Error.WriteLine("Valid commands are:");
            Console.Error.WriteLine("  type     [unit_address]");
            Console.Error.WriteLine("  read-ai  [unit_address] [channel]");
            Console.Error.WriteLine("  read-ao  [unit_address] [channel]");
            Console.Error.WriteLine("  read-do  [unit_address] [channel]");
            Console.Error.WriteLine("  write-ao [unit_address] [channel] [value]");
            Console.Error.WriteLine("  write-do [unit_address] [channel] [value]");
            Console.Error.WriteLine("  portinfo");
            Console.Error.WriteLine("  help");
            Console.Error.WriteLine("  close");
        }

        public bool ProcessCommand(string cmd)
        {
            string[] parts = cmd.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "type":
                        ProcessType(parts);
                        return false;
                    case "read-ai":
                        ProcessReadAI(parts);
                        return false;
                    case "read-ao":
                        ProcessReadAO(parts);
                        return false;
                    case "read-do":
                        ProcessReadDO(parts);
                        return false;
                    case "write-ao":
                        ProcessWriteAO(parts);
                        return false;
                    case "write-do":
                        ProcessWriteDO(parts);
                        return false;
                    case "portinfo":
                        ProcessPortInfo();
                        return false;
                    case "quit":
                    case "close":
                        return true;
                    case "help":
                    default:
                        ShowHelp();
                        return false;
                }
            }
            return false;
        }
    }
}
