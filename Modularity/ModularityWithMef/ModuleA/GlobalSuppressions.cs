// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.

[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly",
        MessageId = "Mef")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly",
        MessageId = "Mef", Scope = "namespace", Target = "ModularityWithMef.Desktop")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly",
        MessageId = "ModuleA", Scope = "member", Target = "ModularityWithMef.Desktop.ModuleA.#Initialize()")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization",
        "CA1303:Do not pass literals as localized parameters",
        MessageId =
            "Prism.Logging.ILoggerFacade.Log(System.String,Prism.Logging.Category,Prism.Logging.Priority)", 
            Scope = "member", Target = "ModularityWithMef.Desktop.ModuleA.#Initialize()")]