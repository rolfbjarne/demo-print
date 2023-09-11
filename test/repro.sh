#!/bin/bash -eux

set -o pipefail
IFS=$'\n\t'

cd "$(git rev-parse --show-toplevel)"
git clean -xfd
msbuild /r /bl test/test.iOS/*.csproj

