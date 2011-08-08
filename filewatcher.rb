require "filesystemwatcher.rb"

dir = ARGV[0].dup
regex = ARGV[1].dup
command = ARGV[2].dup
del = ARGV[3].dup

def replace_vars(str, file)
	str.gsub(':wefile', File.basename(file, File.extname(file))).gsub(':file', File.basename(file)).gsub(':dir', File.dirname(file)).gsub(':ext', File.extname(file)).gsub(':path', file)
end

watcher = FileSystemWatcher.new
watcher.addDirectory dir, regex
watcher.sleepTime = 0.1

watcher.start {|status,file|
	case status
		when FileSystemWatcher::CREATED, FileSystemWatcher::MODIFIED
			cmd = replace_vars command, file
			puts cmd
			IO.popen cmd
		when FileSystemWatcher::DELETED
			if del != nil then
				cmd = replace_vars del, file
				puts cmd
				IO.popen cmd
			end
	end
}

watcher.join
