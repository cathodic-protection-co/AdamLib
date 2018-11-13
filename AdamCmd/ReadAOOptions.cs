using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdamCmd
{
    [Verb("read-ao", HelpText = "Read from an analog output module.")]
    class ReadAOOptions : CommandOptions
    {
        [Option('c', "ch", Required = true, HelpText = "The input channel to read.")]
        public int Channel { get; set; }
    }
}
