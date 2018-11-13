using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdamCmd
{
    [Verb("write-ao", HelpText = "Write to an analog output module.")]
    class WriteAOOptions : CommandOptions
    {
        [Option('c', "ch", Required = true, HelpText = "The input channel to write.")]
        public int Channel { get; set; }

        [Option('v', "value", Required = true, HelpText = "The value to write (e.g. 5.6)")]
        public double Value { get; set; }
    }
}
