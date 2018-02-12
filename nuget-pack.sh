#!/bin/sh

#usage: nuget-pack <version>

version="$1"
if [[ -z "$version" ]]; then
	echo 1>&2 "fatal: version required"
	exec print-file-comments "$0"
	exit 1
fi

dotnet pack -c Release rm.Trie/rm.Trie.csproj \
	//p:PackageVersion="$version"
