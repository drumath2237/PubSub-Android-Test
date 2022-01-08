@echo off
nuget install Azure.Messaging.WebPubSub -version 1.0.0 -o .\_Externals
moveFiles.bat .\_Externals .\Assets\PubSubAndroidTest\Plugins