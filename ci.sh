#!/bin/sh

dotnet restore
dotnet clean -c Release
# netcoreapp2.0 only as test csproj also targets others
dotnet build Trie.sln \
	-c Release \
	-f netcoreapp2.0
dotnet test tests/rm.TrieTest/rm.TrieTest.csproj \
	-c Release --no-build --no-restore \
	-f netcoreapp2.0
