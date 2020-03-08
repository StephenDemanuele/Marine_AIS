### Read Me

## AIS, what is it?

* A vessel identification system developed in the 1970s.
* Allows vessels to broadcast their goegraphical position and fixed data (like callsign and vessel type)
* Vessels transmit data on 2 frequencies. 161.975MHz and 162.025MHz, using GFSK modulation.
* If you're after more details, https://lmgtfy.com/?q=ais

## How to read AIS data.

* By using software defined radio to receive radio transmissions at the specified frequencies, you will first decode
	* the transmission into a string (an NMEA sentence)
	* parse the NMEA sentence into a message

Specifically, these steps:

1. Install VBCable from www.VB-CABLE.com, which is an Audio Driver working as virtual audio cable.
2. Install SDR#, listening to one of the mentioned frequencies.
3. Install aisdecoder for windows.
4. Route audio output to the newly installed VBCable Audio driver.
5. Start aisdecoderwin32.exe (installed in step3) with these arguments: aisdecoder.exe -D 2 -h 127.0.0.1 -p 12345 -a winmm -d. This application will decode the audio signal piped into VBCable audio driver, into an NMEA message. Arguments are described as follows:
	* -D 2: listens on audio device 2, -a winmm: uses windows codec
	* -h 127.0.0.1 -p 12345: writes data to local host on port 12345
	* -d: writes data to standard output
* Now launch AIS.Viewer or AIS.WebViewer from solution.

## What the projects within this solution do.

* Opens a UDP connection to listen to the NMEA messages
* Parses the messages into objects
* Maintains a state by processing the various messages 
* Provides an API to query state
* Provides a desktop and web-based user interface to view the state of the received data

## Projects in the solution.

### AIS.Parser
Contains most of the business end of AIS sentence processing. Together, the `NMEASentenceListener`, `PacketFactory` and `NMEASentenceProcessor` listen to AIS data published to a speific IP:port, then parse the AIS data, process it, update state (a `VesselCollection` instance) and publish new events. `NMEASentenceProcessor` orchestrates all this.

### AIS.WebApi
An API application, which by using an instance of `AIS.Parser.NMEASentenceProcessor`, providers access to the gathered data over HTTP Rest.
eg. GET /api/vessels/headers => returns a description of vessels tracked since started
eg. GET /api/vessels/{{userid}} => returns a detailed description of the vessel having {{userid}}

### AIS.Viewer
A WPF application, which uses an instance of `AIS.Parser.NMEASentenceProcessor` and BingMaps to show tracked vessels on a map, in realtime.

### AIS.WebViewer
A React.js app, which uses an instance of `AIS.Parser.NMEASentenceProcessor` and ReactBingMaps to show tracked vessels on a map, in realtime.

### AIS.GPSReader
You have a GPS dongle? Use this project to parse the GPS signal and pipe its input into the `AIS.Parser`, instead of manually keying in your Observation Point.

### AIS.FakeTransmission
You do not have an SDR dongle, or out of reach of AIS transmissions? Run this project together with any of the viewers of the API, and you'll process the fake AIS data.

## Dependencies.
