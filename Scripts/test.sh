#! /bin/sh

/Applications/Unity/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-nographics \
	-projectPath $(pwd) \
	-executeMethod UnityTest.Batch.RunUnitTests
