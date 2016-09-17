﻿using System.Management.Automation;
using OfficeDevPnP.Core.Utilities;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Enums;

namespace SharePointPnP.PowerShell.Commands.Base
{
    [Cmdlet("Get", "SPOStoredCredential")]
    [CmdletHelp("Returns a stored credential from the Windows Credential Manager", 
        Category = CmdletHelpCategory.Base)]
    [CmdletExample(Code = "PS:> Get-SPOnlineStoredCredential -Name O365", 
        Remarks = "Returns the credential associated with the specified identifier",
        SortOrder = 1)]
    [CmdletExample(Code = "PS:> Get-SPOnlineStoredCredential -Name testEnvironment -Type OnPrem", 
        Remarks = "Returns the credential associated with the specified identifier and use the onpremises credential manager",
        SortOrder = 2)]
    public class GetStoredCredential : PSCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The credential to retrieve.")]
        public string Name;

        [Parameter(Mandatory = false, HelpMessage = "The object type of the credential to return from the Credential Manager. Possible valus are 'O365', 'OnPrem' or 'PSCredential'")]
        public CredentialType Type = CredentialType.O365;

        protected override void ProcessRecord()
        {
            switch (Type)
            {
                case CredentialType.O365:
                    {
                        WriteObject(CredentialManager.GetSharePointOnlineCredential(Name));
                        break;
                    }
                case CredentialType.OnPrem:
                    {
                        WriteObject(CredentialManager.GetCredential(Name));
                        break;
                    }
                case CredentialType.PSCredential:
                    {
                        WriteObject(Utilities.CredentialManager.GetCredential(Name));
                        break;
                    }
            }
        }
    }
}
