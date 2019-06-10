# Find DB backup files

Command in shortcut to generate file list:
C:\Windows\System32\cmd.exe /C "cd /d C:\Users\jwei\z_fdd&&(for %F in (D:\Backup\*.bak*) do @echo %F    %~zF)>fdd.txt&&pause"