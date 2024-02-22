#!/usr/bin/env bash

echo "*************************** Running appcenter-pre-build ******************************"

cd UnitTest || exit 1

dotnet test