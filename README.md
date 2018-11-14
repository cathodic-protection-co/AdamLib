# AdamLib
Advantech ADAM 4000 library and command line tools

This is a basic, portable, library for communicating with ADAM 4000 series modules with associated command line tools, written for .NET Core.

## Contents
1. [Installation](#installation)
1. [Command Line Usage](#command-line-usage)
1. [Interactive Session Usage](#interactive-session-usage)
1. [Library Usage](#library-usage)
1. [Dependencies](#dependencies)
1. [Road Map](#road-map)

## Installation
Run the appropriate installer for your platform (only Windows x86 and Window x64 installers are currently available, but Linux packages will be available in the future).

On Windows, the install directory should be automatically added to the users PATH environment variable. A restart may be required.

## Command Line Usage
The `AdamCmd` executable provides a scriptable interface for the library. Usage is as follows:

```
adamcmd <verb> <options>
```

with the following supported verbs:

`type` - Query the type (i.e. model number and function) of a device. ([link](#type)) 

`read-ai` - Read from an analog input device (ADAM-4017, etc.) ([link](#read-ai))

`read-ao` - Read back from an alalog output device (ADAM-4024, etc.) ([link](#read-ao))

`write-ao` - Write to an analog output device. ([link](#write-ao))

`read-do` - Read back from a digital output device (ADAM-4068, etc.) ([link](#read-do))

`write-do` - Write to a digital output device. ([link](#read-do))

`interactive` - Start an interactive session on the specified port. ([link](#interactive))

`detect-ports` - List all ports connected to ADAM networks. ([link](#detect-ports))

### type
*Query device type*

Usage:

```
adamcmd type --port <port_name> [options]
```

`--port <port_name>` - The port to connect to. On Windows this will be in the form `COMX` where `X` is the assigned port number. On Linux this will be the serial port device file, e.g. `/dev/ttySX`.

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

`--unitadr <unit_adr>` - The ADAM device address (default: `1`).


### read-ai
*Read Analog Input*

Usage:

```
adamcmd read-ai --port <port_name> --ch <channel> [options]
```

`--port <port_name>` - The port to connect to. On Windows this will be in the form `COMX` where `X` is the assigned port number. On Linux this will be the serial port device file, e.g. `/dev/ttySX`.

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

`--unitadr <unit_adr>` - The ADAM device address (default: `1`).

`--ch <channel>` - The channel number to read.

### read-ao
*Read Back Analog Output*

Usage:

```
adamcmd read-ao --port <port_name> --ch <channel> [options]
```

`--port <port_name>` - The port to connect to. On Windows this will be in the form `COMX` where `X` is the assigned port number. On Linux this will be the serial port device file, e.g. `/dev/ttySX`.

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

`--unitadr <unit_adr>` - The ADAM device address (default: `1`).

`--ch <channel>` - The channel number to read.

### write-ao
*Write Analog Output*

Usage:

```
adamcmd write-ao --port <port_name> --ch <channel> --value <value> [options]
```

`--port <port_name>` - The port to connect to. On Windows this will be in the form `COMX` where `X` is the assigned port number. On Linux this will be the serial port device file, e.g. `/dev/ttySX`.

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

`--unitadr <unit_adr>` - The ADAM device address (default: `1`).

`--ch <channel>` - The channel number to write to.

`--value <value>` - The 16-bit unsigned value to write to the register.

### read-do
*Read Back Digital Output*

Usage:

```
adamcmd read-do --port <port_name> --ch <channel> [options]
```

`--port <port_name>` - The port to connect to. On Windows this will be in the form `COMX` where `X` is the assigned port number. On Linux this will be the serial port device file, e.g. `/dev/ttySX`.

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

`--unitadr <unit_adr>` - The ADAM device address (default: `1`).

`--ch <channel>` - The channel number to read.

### write-do
*Write Digital Output*

Usage:

```
adamcmd write16 --port <port_name> --ch <channel> --value <value> [options]
```

`--port <port_name>` - The port to connect to. On Windows this will be in the form `COMX` where `X` is the assigned port number. On Linux this will be the serial port device file, e.g. `/dev/ttySX`.

`--values <value>` - The 16-bit unsigned values to write to the registers, seperated by spaces.

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

`--unitadr <unit_adr>` - The ADAM device address (default: `1`).

`--ch <channel>` - The channel number to write.

'--value <value> - The value to write (`0` or `1`).

### interactive
*Start an interactive session*

Usage:

```
adamcmd interactive --port <port_name> [options]
```

`--port <port_name>` - The port to connect to. On Windows this will be in the form `COMX` where `X` is the assigned port number. On Linux this will be the serial port device file, e.g. `/dev/ttySX`.

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

See [Interactive Session Usage](#interactive-session-usage) for how to use this mode.


### detect-ports
*Attempts to list all serial ports which are connected to a ADAM network*

Usage:

```
adamcmd detect-ports [options]
```

`--baud <baud_rate>` - The baud rate for the port (default: `9600`).

`--format <format>` - The data format (default: `8N1`). Only `8` data bits are currently supported. Parity modes supported are `N` (none), `E` (even), `O` (odd), `M` (mark) and `S` (space). `1` or `2` stop bits are supported.

`--unitadr <unit_adr>` - The ADAM device address (default: `1`).

This command attempts to open each serial port in turn and issues a `Read Model` command to a device. If a valid message is received then the name of the port is shown. 

## Interactive Session Usage
The interactive session supports the following commands:

`type <unit_adr>` - Query device type.
`read-ai <unit_adr> <ch>` - Read analog input.
`read-ao <unit_adr> <ch>` - Read back analog output.
`write-ao <unit_adr> <ch> <value>` - Write analog output.
`read-do <unit_adr> <ch>` - Read back digital output.
`write-do <unit_adr> <ch> <value>` - Write digital output.
`portinfo` - Display connection information.
`help` - Display command list.
`close` - Close the port and exit.

All parameters for each command are required (i.e. there are no assumed defaults in interactive mode).

## Library Usage
*[TODO]*

## Dependencies
* .NET Core 2.1.6 / .NET Standard 2.0
* [SerialPortStream](https://www.nuget.org/packages/SerialPortStream/) - *Thanks [jcurl](https://github.com/jcurl)*
* [CommandLineParser](https://www.nuget.org/packages/CommandLineParser/) - *Thanks [eric](https://github.com/ericnewton76)*

*Note: All installers/packages include all required dependencies.*

## Road Map
* Async support for libary.
* Add support for other commonly used modules.
* Add support for running script files.
* Add `wait` command to interactive and script modes.
* Add more output formatting options.
* Add support for other standard function codes.
* Real documentation.
* Powershell cmdlets (Windows only).
* Create Linux paackages (`.deb` and `.rpm` to start)
