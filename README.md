# Prism Samples WPF
Samples that demonstrate how to use various Prism features with WPF.  If you are just getting started with Prism, it is recommended that you start from the first sample, and work your way down the list sequentially (in order). Each sample builds on the previous sample's concept.

| Topic | Description |
-----------|-------------|
| [Bootstrapper and the Shell][1] | Create a basic bootstrapper and shell |
| [Regions][2] | Create a region |
| [Custom Region Adapter][3] | Create a custom region adapter for the StackPanel |
| [View Discovery][4] | Automatically inject views with View Discovery |
| [View Injection][5] | Manually add and remove views using View Injection |
| [View Activation/Deactivation][6] | Manually activate and deactivate views |
| [Modules with App.config][7] | Load modules using an App.config file |
| [Modules with Code][8] | Load modules using code |
| [Modules with Directory][9] | Load modules from a directory |
| [Modules loaded manually][10] | Load modules manually using the IModuleManager |
| [ViewModelLocator][11] | using the ViewModelLocator |
| [ViewModelLocator - Change Convention][12] | Change the ViewModelLocator naming conventions |
| [ViewModelLocator - Custom Registrations][13] | Manually register ViewModels for specific views |
| [DelegateCommand][14] | Use DelegateCommand and `DelegateCommand<T>` |
| [CompositeCommands][15] | Learn how to use CompositeCommands to invoke multiple commands as a single command |
| [IActiveAware Commands][16] | Make your commands IActiveAware to invoke only the active command |
| [Event Aggregator][17] | Using the IEventAggregator |
| [Event Aggregator - Filter Events][18] | Filtering events when subscribing to events |
| [RegionContext][19] | Pass data to nested regions using the RegionContext |
| [Region Navigation][20] | See how to implement basic region navigation |
| [Navigation Callback][21] | Get notifications when navigation has completed |
| [Navigation Participation][22] | Learn about View and ViewModel navigation participation with INavigationAware |
| [Navigate to existing Views][23] | Control view instances during navigation |
| [Passing Parameters][24] | Pass parameters from View/ViewModel to another View/ViewModel |
| [Confirm/cancel Navigation][25] | Use the IConfirmNavigationReqest interface to confirm or cancel navigation |
| [Controlling View lifetime][26] | Automatically remove views from memory with IRegionMemberLifetime |
| [Navigation Journal][27] | Learn how to use the Navigation Journal |
| [Interactivity - NotificationRequest][28] | Learn how to show popups using an InteractionRequest |
| [Interactivity - ConfirmationRequest][29] | Learn how to prompt a confirmation dialog using a ConfirmationRequest |
| [Interactivity - Custom Content][30] | Learn how to use your own content for a dialog shown with InteractionRequest |
| [Interactivity - Custom Request][31] | Create your own custom request to use with an InteractionRequest |
| [Interactivity - InvokeCommandAction][32] | Invoke commands in response to any event |


[1]: 01-BootstrapperShell/
[2]: 02-Regions/
[3]: 03-CustomRegions/
[4]: 04-ViewDiscovery/
[5]: 05-ViewInjection/
[6]: 06-ViewActivationDeactivation/
[7]: 07-Modules%20-%20AppConfig/
[8]: 07-Modules%20-%20Code/
[9]: 07-Modules%20-%20Directory/
[10]: 07-Modules%20-%20LoadManual/
[11]: 08-ViewModelLocator/
[12]: 09-ChangeConvention/
[13]: 10-CustomRegistrations/
[14]: 11-UsingDelegateCommands/
[15]: 12-UsingCompositeCommands/
[16]: 13-IActiveAwareCommands/
[17]: 14-UsingEventAggregator/
[18]: 15-FilteringEvents/
[19]: 16-RegionContext/
[20]: 17-BasicRegionNavigation/
[21]: 18-NavigationCallback/
[22]: 19-NavigationParticipation/
[23]: 20-NavigateToExistingViews/
[24]: 21-PassingParameters/
[25]: 22-ConfirmCancelNavigation/
[26]: 23-RegionMemberLifetime/
[27]: 24-NavigationJournal/
[28]: 25-NotificationRequest/
[29]: 26-ConfirmationRequest/
[30]: 27-CustomContent/
[31]: 28-CustomRequest/
[32]: 29-InvokeCommandAction/
