#!/bin/bash
cd $(dirname $BASH_SOURCE) || {
    echo Error getting script directory >&2
    exit 1
}

filelist=`ls *.proto`
for file in $filelist
do
echo $file
mono protogen.exe -i:$file -o:${file%.*}.cs
done
