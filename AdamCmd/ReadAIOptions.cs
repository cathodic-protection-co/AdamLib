using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdamCmd
{
    [Verb("read-ai", HelpText = "Read from an analog input module.")]
    class ReadAIOptions : CommandOptions
    {
        [Option('c', "ch", Required = true, HelpText = "The input channel to read.")]
        public int Channel { get; set; }
    }
}
