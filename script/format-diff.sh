#!/bin/sh

cd $(git rev-parse --show-toplevel)

# format untracked files
git ls-files --others --exclude-standard | xargs dotnet csharpier format

# format tracked files that still exist
git status --porcelain | awk 'match($1, "M"){print $2}' | dotnet csharpier format
