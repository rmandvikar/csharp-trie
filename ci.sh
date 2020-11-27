#!/bin/sh

dotnet restore
dotnet clean -c Release
# netcoreapp3.0 only as test csproj also targets others
dotnet build Trie.sln \
	-c Release \
	-f net5.0
dotnet test tests/rm.TrieTest/rm.TrieTest.csproj \
	-c Release --no-build --no-restore \
	-f net5.0 \
	-v normal \
	--filter "TestCategory!=very.slow"
