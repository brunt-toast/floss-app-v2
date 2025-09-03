#!/bin/bash 

fail() {
    echo "$1"
    exit 1
}

set -o errexit

# Make sure we're in the right place
# If you're running this from inside another git repo... why? 
git rev-parse --is-inside-work-tree || fail "Not inside git worktree"
cd $(git rev-parse --show-toplevel)

# Install git hooks
command -v npm || fail "npm is not installed or was not added to PATH" 
npm i -g husky@9.1.7 && husky install

# Perform some restores for better OOBE in the IDE 
command -v dotnet || fail "could not find dotnet" 
dotnet tool restore --configfile ./nuget.config
dotnet workload restore ./floss-app.slnx --configfile ./nuget.config
dotnet restore ./floss-app.slnx --configfile ./nuget.config
dotnet msbuild /t:SafeGenerateSources