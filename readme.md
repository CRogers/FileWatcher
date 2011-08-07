FileWatcher
===

Usage: `ruby filewatcher.rb [directory] [regex] [cmd] [delcmd]`

* `[directory]` - the current directory to watch
* `[regex]` - filter pattern for files to watch in said directory
* `[cmd]` - command to run when a file is created/modified
* `[delcmd]` - command to run when a file is deleted

The following selectors can be used in commands:

* `:file` - the name of the file
* `:wefile` - name of the file _without extension_
* `:ext` - the file's extension
* `:dir` - the directory the file is in
* `:path` - the full expanded path of file

---

Place this at the bottom of your `~/.bashrc` file (Ubuntu, `~/.bash_profile` for others) to use `filewatcher` as a command:

	function filewatcher() {
		FW_PATH=/media/Storage/Dropbox/Programming/ruby/FileWatcher;
		export RUBYLIB=$FW_PATH/:$RUBYLIB;
		ruby $FW_PATH/filewatcher.rb "$@";
	}
