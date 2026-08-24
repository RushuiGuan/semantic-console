$install = $env:InstallDirectory;
if ($IsMacOS) {
    set-alias -n smc -v "$install/Albatross.SemanticConsole.Tool/smc";
}
else {
    set-alias -n smc -v "$install/Albatross.SemanticConsole.Tool/smc.exe";
}
