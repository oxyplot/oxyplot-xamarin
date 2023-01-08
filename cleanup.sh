#!/bin/sh

# remove 'bin' and 'obj' directories
find . -type d \( -name 'bin' -o -name 'obj' \) -prune -exec rm -rf {} \;

rm -rf Source/packages/
rm -rf Source/.vs/
