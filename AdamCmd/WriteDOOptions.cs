using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdamCmd
{
    [Verb("write-do", HelpText = "Write to digital output module.")]
    class WriteDOOptions : CommandOptions
    {
        [Option('c', "ch", Required = true, HelpText = "The output channel to write.")]
        public int Channel { get; set; }

        [Option('v', "value", HelpText = "Either 1 for on or 0 for off.")]
        public int Value { get; set; }

        public bool BoolValue => Value != 0;
    }
}
