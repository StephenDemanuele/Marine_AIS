### Read Me

## AIS, what is it?

* A vessel identification system developed in the 1970s.
* Allows vessels to broadcast their goegraphical position and fixe data (like callsign and vessel type)
* Vessels transmit data on 2 frequencies. 161.975MHz and 162.025MHz, using GFSK modulation.
* If you're after more details, ltmgtfy....

## How to read AIS data.

* By using software defined radio to receive radio transmissions at the specified frequencies, you will first decode
	* the transmission into a string (an NMEA sentence)
	* parse the NMEA sentence into a message

Specifically, these steps:

* Install VBCable from www.VB-CABLE.com, which is an Audio Driver working as virtual audio cable.
* Install SDR#, listening to one of the mentioned frequencies.
* Install aisdecoder for windows.
* Route audio output to the newly installed VBCable Audio driver.
* Start aisdecoderwin32.exe with these arguments: aisdecoder.exe -D 2 -h 127.0.0.1 -p 12345 -a winmm -d
	* This application will decode the audio signal piped into VBCable audio driver, into an NMEA message.
	* -D 2: listens on audio device 2, -a winmm: uses windows codec
	* -h 127.0.0.1 -p 12345: writes data to local host on port 12345
	* -d: writes data to standard output
* Launch solution now

## What this solution can do.

* Opens a UDP connection to listen to the NMEA messages
* Parses the messages into objects
* Maintains a state by processing the various messages 
* Provides an API to query state
* Provides a desktop and web-based user interface to view the state of the received data

## Projects in the solution.

## Dependencies.