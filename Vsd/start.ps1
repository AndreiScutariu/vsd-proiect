#arg 0: slave key id
#arg 1: draw type
#arg 2: draw rotation 0(0x 1y 0z), 1(-1x 1y 0z) ...
#arg 3: draw color 1-red, 2-green, 3-blue

#Leafs
Start-Process .\Vsd.Slave\bin\Debug\Vsd.Slave.Leaf.exe 44441-1101
Start-Process .\Vsd.Slave\bin\Debug\Vsd.Slave.Leaf.exe 44442-2122
#Start-Process .\Vsd.Slave\bin\Debug\Vsd.Slave.Leaf.exe 44443-1203
#Start-Process .\Vsd.Slave\bin\Debug\Vsd.Slave.Leaf.exe 44444-2154

#Nodes
Start-Process .\Vsd.Slave.Node\bin\Debug\Vsd.Slave.Node.exe 1-44441-44442-44445
#Start-Process .\Vsd.Slave.Node\bin\Debug\Vsd.Slave.Node.exe 2-44443-44444-44446

#Start-Process .\Vsd.Slave.Node\bin\Debug\Vsd.Slave.Node.exe 1-44445-44446-44447

#Master
Start-Process .\Vsd.Master\bin\Debug\Vsd.Master.exe 44445	