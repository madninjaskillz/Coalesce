# Coalesce
Combine Multiple tracking sensors, process and output to any system.

#The Goal:
There are many projects currently to support VR inputs (such as Opentrack, PSMoveService) via many different means. There are many different VR systems (such as OSVR, SteamVR, Occulus) that benefit from VR inputs.

Coalesce is designed to support any type of input (either by existing systems or via direct support), do basic processing and output to any existing system via plugin, direct game support via plugin or via a new coalesce protocol 
(also implemented via plugin).

The idea is that you have a single point to configure and control your devices and be able to support any system in a simpler manner, allowing more complicated scenarios easily.

#Example:
Currently, if you want to use SteamVR with RiftCat/Vridge as your headset, KinectV2 for head position and Sony move controllers for hand controllers with current systems, you're looking at using OpenTrack and OpenTrackKinect, PSMoveService and the RiftCat server.

Once Coalesce is fully supported it would allow you to add a Sony Move plugin, a Kinect Plugin and an OpenTrack Receiver (for the phones gyro readings) and save that as a configuration - you would then be able to add a number of output plugins including SteamVR - there's no reason you wouldn't be able to output multiple outputs at the same time meaning no reconfigure when you switch.

#What Coalesce is not:
* Coalesce isn't anything more than sensor combining. 
* It's not designed to stream VR content to a phone, its not a replacement for OSVR (more of a bridge/companion for OSVR)

#Developing Plugins:
More info coming soon...
(for now, if you are using c#, easy life, other languages, not sure yet)
