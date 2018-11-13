using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdamCmd
{
    [Verb("read-do", HelpText = "Read from a digital output module.")]
    class ReadDOOptions : CommandOptions
    {
        [Option('c', "ch", Required = true, HelpText = "The output channel to read.")]
        public int Channel { get; set; }
    }
}
