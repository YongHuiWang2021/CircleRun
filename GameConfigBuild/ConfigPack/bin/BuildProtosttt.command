
#!/bin/bash
cd $(dirname $BASH_SOURCE) || {
    echo Error getting script directory >&2
    exit 1
}

mono SqlitePack.exe "SqliteProtoc.command"
