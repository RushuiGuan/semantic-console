<#
.SYNOPSIS
	The "A full run" demo from project-mgmt/design-language.html, printed with the smc CLI.

.DESCRIPTION
	Every line of the transcript is one smc verb - nothing here writes to the console directly, so the
	script is also a check that the CLI can express the whole language: five blocks, three prompts, a
	warning, a recoverable error and the run's verdict.

	The three prompts are real, so the run stops for an answer. The transcript matches the demo when the
	operator picks MSSQLSERVER, presses Enter at the connection string, then types mw_runtime followed by
	mw_svc.

.PARAMETER Smc
	The smc executable. Defaults to the smc alias set by alias.ps1.

.EXAMPLE
	. ./alias.ps1
	./project-mgmt/demo-full-run.ps1
#>
[CmdletBinding()]
param(
	[string]$Smc = 'smc'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# --- the run names itself, and reports where it will put things -----------------------------------
# --first on the action suppresses the blank line above it: nothing precedes the run.
& $Smc write-action "Anchor 4.2.1 installer." --first
& $Smc write-info "target C:\Program Files\Anchor"
& $Smc write-info "console 120 columns, colour"

# --- a block of checks, one of them off the plain path --------------------------------------------
& $Smc write-action "Checking prerequisites."
& $Smc write-info ".NET 8.0.11 runtime found"
& $Smc write-info "IIS 10.0 found"
& $Smc write-info "The URL Rewrite module is missing. It will be downloaded and installed." --status Warning

# --- a prompt: the context explains the value, the answer is picked from the menu ------------------
$instance = & $Smc read-select `
	--context "The instance that will host the mw database. Press Enter for MSSQLSERVER." `
	--question "instance:" `
	--choices sqlserver postgres `
	--default sqlserver;

# --- a prompt with a default: Enter alone answers it ----------------------------------------------
$adminConnection = & $Smc read-text `
	--context "Connection string for schema migrations. It needs schema change and database creation privileges." `
	--question "admin connection string:" `
	--default "Server=.;Database=mw"
& $Smc write-feedback "connected as sa"
Write-Verbose "admin connection string: $adminConnection"

# --- a prompt the operator can get wrong: the refusal costs one more prompt ------------------------
# The context is written once. A re-ask repeats the question alone, inside the group it already opened.
$knownLogins = @('mw_svc')
$context = "Login the server runs as. It needs read and write on mw only."
while ($true) {
	$arguments = @('read-text', '--question', 'runtime login:')
	if ($context) {
		$arguments += @('--context', $context)
	}
	$runtimeLogin = & $Smc @arguments
	if ($knownLogins -contains $runtimeLogin) {
		break
	}
	& $Smc write-feedback "That login does not exist." --status Error
	$context = $null
}
& $Smc write-feedback "created $runtimeLogin and granted read and write on mw"

# --- the work itself ------------------------------------------------------------------------------
& $Smc write-action "Applying pending schema migrations."
& $Smc write-info "applied 34 migrations in 12.4s"

& $Smc write-action "Publishing the application."
& $Smc write-info "copied to C:\Program Files\Anchor"
& $Smc write-info "registered the AnchorHost service"
& $Smc write-info "This machine already hosts 4.1.0. The old site stops when the new one starts." --status Warning

# --- checks that prove the deployment works, which is what green is for ----------------------------
& $Smc write-action "Verifying the deployment."
& $Smc write-info "health endpoint answered in 41 ms" --status Success
& $Smc write-info "schema version matches 4.2.1" --status Success

# --- the verdict, and where to find it ------------------------------------------------------------
& $Smc write-action "Installation complete." --status Success
& $Smc write-content "https://localhost:6776"
